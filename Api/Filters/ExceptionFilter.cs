using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api.Filters
{
    /// <summary>
    /// 记录错误日子
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private ILogger<ExceptionFilter> loggerHelper { get; set; }
        private StringBuilder sb;
        /// <summary>
        /// 
        /// </summary>
        public ExceptionFilter(ILogger<ExceptionFilter> loggerHelper, StringBuilder stringBuilder)
        {
            this.loggerHelper = loggerHelper;
            this.sb = stringBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            //系统级别报错    
            sb.AppendLine($"Error:Message:{context.Exception.Message}{Environment.NewLine}StackTrace:{context.Exception.StackTrace}");
            var dto = new
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Message = "内部错误"
            };
            loggerHelper.LogError(sb.ToString());
            sb.Clear();
            context.Result = new JsonResult(dto); 

        }
    }
}
