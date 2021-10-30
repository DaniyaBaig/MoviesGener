using AutoMapper;
using Mechanic.API.ModuleInjections;
using Mechanic.Common.Utilities;
using Mechanic.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Mechanic.API.APILogging;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using Mechanic.API.Helpers;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Mechanic.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddCors();
            ConnectionStrings connectionStrings = new ConnectionStrings();
            Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            //Create singleton from instance
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            services.AddSingleton(connectionStrings);
            services.AddAutoMapper(typeof(Startup));
            new ServicesInjection(services).MapServices();
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<RijndaelCredentials>(Configuration.GetSection("RijndaelCredentials"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICryptography, Cryptography>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "API",
                    Description = "API with ASP.NET",
                    Contact = new OpenApiContact()
                    {
                        //Email = "munazamalik100@gmail.com",
                        Name = "API Administration"
                    }
                });
                c.OperationFilter<SwaggerHeaderFilter>();
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IGeneric, Generic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader()
                    .AllowCredentials()
            );
            var localizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("es-pr"),
                    new CultureInfo("fr-ca"),
                    new CultureInfo("en-us"),
                },
                DefaultRequestCulture = new RequestCulture("en-US")
            };
            // Adding our UrlRequestCultureProvider as first object in the list
            localizationOptions.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider
            {
                Options = localizationOptions
            });
            //app.UseMiddleware<LoggingRequestResponse>();
            app.UseRequestLocalization(localizationOptions);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "API";
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "V1");
                c.RoutePrefix = "docs";
            });
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();
                    ErrorLogging.ErrorLogging.AddErrorByException(exceptionHandlerPathFeature?.Error, context);
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(
                        ResponseMetaData<object>.CreateResponse(HttpStatusCode.InternalServerError, null,
                            exceptionHandlerPathFeature?.Error.Message)));
                });
                app.UseHsts();
            });
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
