using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Bassi.API.Configuration.Model;
using Microsoft.AspNetCore.Builder;
using Bassi.API.Configuration.Logger;

namespace Bassi.API.Configuration.Filters
{
    public class ExceptionLoggerMiddleware
    {
        readonly RequestDelegate _next;
        readonly IServiceLoggerRepository _serviceLoggerRepository;
        readonly IAppSettings _appSettings;

        public ExceptionLoggerMiddleware(RequestDelegate next, IServiceLoggerRepository serviceLoggerRepository, IAppSettings appSettings)
        {
            _next = next;
            _serviceLoggerRepository = serviceLoggerRepository;
            _appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {

            //Guid logId = _requestLogger.Log(DateTime.Now, httpContext.Request.Path, "GEMSAPI", httpContext.Request.Method, "TEST", ex.Message);

            if (httpContext.Items.ContainsKey("serviceLogger"))
            {
                RegisterLogger(httpContext.TraceIdentifier, (ServiceApiRequestLogger)httpContext.Items["serviceLogger"], ex);
            }
            else
            {
                _serviceLoggerRepository.RegisterLogger(Guid.Parse(httpContext.TraceIdentifier), DateTime.UtcNow,
                    Guid.Parse(_appSettings.Getkey("ApplicationId")),
                    $"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value}{httpContext.Request.Path.Value}",
                    $"{{\"header\": {JsonSerializer.Serialize(httpContext.Request.Headers.ToList())}}}",
                    httpContext.Request.Method,
                    null, null, ex.Message
                    );
            }
            //TODO : MEJORAR ESTO
            if (!httpContext.Response.Headers.ContainsKey("logid"))
            {
                httpContext.Response.Headers.Add("logid", httpContext.TraceIdentifier);
            }
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";

            var exceptionBody = new ServiceApiResponseException()
            {
                InternalStatusCode = 666,
                Message = "Unexpected error ocurred",
                Data = "Check header"
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(exceptionBody));
        }

        private void RegisterLogger(string contextHttpId, ServiceApiRequestLogger serviceApi, Exception ex)
        {
            _serviceLoggerRepository.RegisterLogger(Guid.Parse(contextHttpId),
                serviceApi.RequestITime,
                Guid.Parse(_appSettings.Getkey("ApplicationId")),
                serviceApi.Url,
                GetAdditionalInformations(serviceApi),
                serviceApi.Method,
                serviceApi.Request,
                serviceApi.Response, ex.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName">Nombre del controller</param>
        /// <param name="actionName">Nombre de la accion del controller</param>
        /// <returns></returns>
        private string GetAdditionalInformations(ServiceApiRequestLogger serviceApiRequestLogger) =>
            $"{{\"header\": {serviceApiRequestLogger.RequestHeader}, \"controllerName\":\"{serviceApiRequestLogger.Controller}\", \"actionName\":\"{serviceApiRequestLogger.Action}\"}}";

    }

    public static class ExceptionLoggerMiddlewareExtention
    {
        public static IApplicationBuilder UseExceptionLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLoggerMiddleware>();
        }
    }
}
