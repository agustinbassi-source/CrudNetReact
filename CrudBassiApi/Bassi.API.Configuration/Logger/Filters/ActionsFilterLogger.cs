using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Bassi.API.Configuration.Model;
using System.Text.Json;
using Bassi.API.Configuration.Logger;

namespace Bassi.API.Configuration.Filters
{
    public class ActionsFilterLogger : IActionFilter
    {
        readonly IAppSettings _configuration;
        readonly IServiceLoggerRepository _serviceLoggerRepository; 

        public ActionsFilterLogger(IAppSettings configuration, IServiceLoggerRepository serviceLoggerRepository)
        {
            _configuration = configuration;
            _serviceLoggerRepository = serviceLoggerRepository;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            AddResponse(context);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            addRequest(context);
        } 

        private void addRequest(ActionExecutingContext context)
        {
            Type jao = context.ActionDescriptor.GetType();

            context.HttpContext.Items.Add("serviceLogger", new ServiceApiRequestLogger()
            {
                RequestHeader = JsonSerializer.Serialize(context.HttpContext.Request.Headers.ToList()),
                RequestITime = DateTime.UtcNow,
                Controller = jao.GetProperty("ControllerName").GetValue(context.ActionDescriptor).ToString(),
                Action = jao.GetProperty("ActionName").GetValue(context.ActionDescriptor).ToString(),
                Method = context.HttpContext.Request.Method,
                Url = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host.Value}{context.HttpContext.Request.Path.Value}",
                Request = JsonSerializer.Serialize(context.ActionArguments.Values)
            });
        }

        private void AddResponse(ActionExecutedContext context)
        {
            if (EnbledRequestLoger(_configuration.Getkey("EnabledRequestLogger")) && context.Exception == null)
            {
                //request = System.Text.Json.JsonSerializer.Serialize(context.HttpContext);
                if (context.HttpContext.Items.ContainsKey("serviceLogger"))
                {
                    ((ServiceApiRequestLogger)context.HttpContext.Items["serviceLogger"]).Response = JsonSerializer.Serialize(((OkObjectResult)context.Result).Value);
                    RegisterLogger(context.HttpContext.TraceIdentifier, (ServiceApiRequestLogger)context.HttpContext.Items["serviceLogger"]);
                    if (!context.HttpContext.Response.Headers.ContainsKey("logid"))
                    {
                        context.HttpContext.Response.Headers.Add("logid", context.HttpContext.TraceIdentifier);
                    }
                }
            }
        }

        private void RegisterLogger(string contextHttpId, ServiceApiRequestLogger serviceApi)
        {
            _serviceLoggerRepository.RegisterLogger(Guid.Parse(contextHttpId),
                serviceApi.RequestITime,
                Guid.Parse(_configuration.Getkey("ApplicationId")),
                serviceApi.Url,
                GetAdditionalInformations(serviceApi),
                serviceApi.Method,
                serviceApi.Request,
                serviceApi.Response, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName">Nombre del controller</param>
        /// <param name="actionName">Nombre de la accion del controller</param>
        /// <returns></returns>
        private string GetAdditionalInformations(ServiceApiRequestLogger serviceApiRequestLogger) => 
            $"{{\"header\": {serviceApiRequestLogger.RequestHeader}, \"controllerName\":\"{serviceApiRequestLogger.Controller}\", \"actionName\":\"{serviceApiRequestLogger.Action}\"}}";

        private bool EnbledRequestLoger(string enableLogger) => enableLogger != null && enableLogger == "true";
    }
}
