using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultFilter : IResultFilter
    {
        private ILogger<ResultFilter> loggerHelper { get; set; }
        private StringBuilder sb;
        /// <summary>
        /// 
        /// </summary>
        public ResultFilter(ILogger<ResultFilter> loggerHelper, StringBuilder stringBuilder)
        {
            this.loggerHelper = loggerHelper;
            this.sb = stringBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (sb.Length > 0)
            {                
                var ab = context.Result as ObjectResult;
                if (ab != null && ab.Value != null)
                {
                    var data = ab.Value.ToJson();
                    sb.AppendLine("Response Body:" + ab.Value);
                }
                else
                {
                    var ob = context.Result as JsonResult;
                    if (ob != null && ob.Value != null)
                    {                         
                        sb.AppendLine("Response Body:" + ob.Value.ToJson());
                    }
                }
                sb.AppendLine($"Request EndTime:{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:fff")}");
                loggerHelper.LogInformation(sb.ToString());                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
    }
}
