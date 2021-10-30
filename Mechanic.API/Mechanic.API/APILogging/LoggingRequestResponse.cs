using Mechanic.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Mechanic.API.APILogging
{
    public class LoggingRequestResponse
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LoggingRequestResponse(RequestDelegate requestDelegate, IHttpContextAccessor httpContextAccessor)
        {
            _requestDelegate = requestDelegate;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context, IRequestResponseLoggingService requestResponseLoggingService)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _requestDelegate(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);

                // The action is a POST.
                if (context.Request.Method == HttpMethod.Post.Method)
                {
                    //TODO: Save log to chosen datastore
                    await requestResponseLoggingService.ResponseRequestLoggingPostMethod(request, response,
                        remoteIpAddress);
                }
                else if (context.Request.Method == HttpMethod.Put.Method)
                {
                    await requestResponseLoggingService.ResponseRequestLoggingPutMethod(request, response,
                        remoteIpAddress);
                }
                else
                {
                    //TODO: Save log to chosen datastore
                    await requestResponseLoggingService.ResponseRequestLoggingGetMethod(request, response,
                        remoteIpAddress);
                }

                //Copy the contents of the new memory stream (which contains the response) to the original
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body.Position = 0;

            return $"{request.Scheme}{request.Host}{request.Path}{request.QueryString}{bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }
    }
}
