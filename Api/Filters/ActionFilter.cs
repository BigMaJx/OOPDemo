using Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters; 
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text; 

namespace Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionFilter : IActionFilter
    {
        

        private ILogger<ActionFilter> loggerHelper { get; set; }
        private StringBuilder sb;
        /// <summary>
        /// 
        /// </summary>
        public ActionFilter(ILogger<ActionFilter> loggerHelper, StringBuilder stringBuilder)
        { 
            this.loggerHelper = loggerHelper;
            this.sb = stringBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string[] skipUrls = {  "/" };
            if (!skipUrls.Contains(context.HttpContext.Request.Path.Value))
            {
                sb.AppendLine($"Request TraceIdentifier:{context.HttpContext.TraceIdentifier}");
                sb.AppendLine($"Request StartTime:{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:fff")}");
                sb.AppendLine($"Request Url:{ context.HttpContext.Request.Path}");                
                sb.AppendLine($"Request Body:{string.Join(';', context.ActionArguments?.ToJson())}");
            }

            //if (context.Controller.GetType().GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any())
            //{
            //    return;
            //}
            //else
            //{
            //    var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //    if (controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any())
            //    {
            //        return;
            //    }
            //    if (context.HttpContext.Request.Method != "POST")
            //    {
            //        return;
            //    }
            //}
             
        }


        /// <summary>
        /// 
        /// </summary>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
