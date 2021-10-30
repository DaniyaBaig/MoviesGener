using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mechanic.API.Helpers
{
    public class ResponseMetaData<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCodes StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Errors { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="obj"></param>
        /// <param name="errorMessage"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static ResponseMetaData<T> CreateResponse(HttpStatusCode statusCode, T obj, string errorMessage = "", ModelStateDictionary modelState = null)
        {
            return new ResponseMetaData<T>()
            {
                Version = "v1",
                StatusCode = (HttpStatusCodes)statusCode,
                ErrorMessage = errorMessage,
                Result = obj,
                Timestamp = DateTime.UtcNow,
                Errors = null// modelState?.Values?.SelectMany(x => x.Errors)?.Select(x => x.ErrorMessage).ToList() ?? new List<string>()
            };
        }
    }


    public enum HttpStatusCodes
    {
        /// <summary>
        /// 
        /// </summary>
        Continue = 100,
        /// <summary>
        /// 
        /// </summary>
        SwitchingProtocols = 101,
        /// <summary>
        /// 
        /// </summary>
        Ok = 200,
        /// <summary>
        /// 
        /// </summary>
        Created = 201,
        /// <summary>
        /// 
        /// </summary>
        Accepted = 202,
        /// <summary>
        /// 
        /// </summary>
        NonAuthoritativeInformation = 203,
        /// <summary>
        /// 
        /// </summary>
        NoContent = 204,

        /// <summary>
        /// 
        /// </summary>
        ResetContent = 205,

        /// <summary>
        /// 
        /// </summary>
        PartialContent = 206,

        /// <summary>
        /// 
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// 
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// 
        /// </summary>
        PaymentRequired = 402,

        /// <summary>
        /// 
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// 
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 
        /// </summary>
        MethodNotAllowed = 405,

        /// <summary>
        /// 
        /// </summary>
        NotAcceptable = 406,

        /// <summary>
        /// 
        /// </summary>
        ProxyAuthenticationRequired = 407,

        /// <summary>
        /// 
        /// </summary>
        RequestTimeout = 408,

        /// <summary>
        /// 
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// 
        /// </summary>
        Gone = 410,

        /// <summary>
        /// 
        /// </summary>
        LengthRequired = 411,

        /// <summary>
        /// 
        /// </summary>
        PreconditionFailed = 412,

        /// <summary>
        /// 
        /// </summary>
        RequestEntityTooLarge = 413,

        /// <summary>
        /// 
        /// </summary>
        RequestUriTooLong = 414,

        /// <summary>
        /// 
        /// </summary>
        UnsupportedMediaType = 415,

        /// <summary>
        /// 
        /// </summary>
        RequestedRangeNotSatisfiable = 416,

        /// <summary>
        /// 
        /// </summary>
        ExpectationFailed = 417,

        /// <summary>
        /// 
        /// </summary>
        UpgradeRequired = 426,

        /// <summary>
        /// 
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// 
        /// </summary>
        NotImplemented = 501,

        /// <summary>
        /// 
        /// </summary>
        BadGateway = 502,

        /// <summary>
        /// 
        /// </summary>
        ServiceUnavailable = 503,

        /// <summary>
        /// 
        /// </summary>
        GatewayTimeout = 504,

        /// <summary>
        /// 
        /// </summary>
        HttpVersionNotSupported = 505
    }
}
