using Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipelines;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api
{

    //做过一次压测，发现两者记录日志 速度差不了太多
    //中间件，使用会更简单些
    //过滤器 能做些其他业务
    
    /// <summary>
    /// 日志中间件
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<RequestLoggingMiddleware> _nLoger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _nLoger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Request TraceIdentifier:{context.TraceIdentifier}");
            //sb.AppendLine(tokenInfoContext.GetRecordInfo());
            bool isErr = false;
            bool isInLog = true;
            var _stopwatch = Stopwatch.StartNew();
            try
            {
                if (context.Request.Path == "/")
                {
                    isInLog = false;
                }
                await WriteLogByRequestBodyAsync(sb, context);
                // 获取Response.Body内容
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    await _next(context);
                    sb.AppendLine("Response Body:" + await GetResponse(context.Response));
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                isErr = true;
                //系统级别报错    
                sb.AppendLine($"Error:Message:{ex.Message}{Environment.NewLine}StackTrace:{ex.StackTrace}");
                context.Response.ContentType = "application/json;charset=utf-8";
                await context.Response.WriteAsync(new
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = "内部错误"
                }.ToJson());
            }
            finally
            {
                _stopwatch.Stop();
                sb.AppendLine($"请求耗时:{_stopwatch.ElapsedMilliseconds}毫秒");
                if (isInLog)
                {
                    if (isErr)
                    {
                        _nLoger.LogError(sb.ToString());
                    }
                    else
                    {
                        _nLoger.LogInformation(sb.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
        //参考https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/request-response?view=aspnetcore-3.1
        /// <summary>
        /// 写日请求前日志
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task WriteLogByRequestBodyAsync(StringBuilder sb, HttpContext context)
        {
            context.Request.EnableBuffering();
            sb.AppendLine($"Request Url:{context.Request.Path}");
            var pipeReader = PipeReader.Create(context.Request.Body);
            var strList = await GetListOfStringFromPipe(pipeReader);
            sb.AppendLine($"Request Body:{string.Join(';', strList)}");
            context.Request.Body.Position = 0;
        }
         
        private async Task<List<string>> GetListOfStringFromPipe(PipeReader reader)
        {
            List<string> results = new List<string>();
            ReadResult readResult = await reader.ReadAsync();
            var buffer = readResult.Buffer;
            var readOnlySequence = buffer.Slice(0, buffer.Length);
            AddStringToList(ref results, in readOnlySequence);
            return results;
        }

        private static void AddStringToList(ref List<string> results, in ReadOnlySequence<byte> readOnlySequence)
        {
            // Separate method because Span/ReadOnlySpan cannot be used in async methods
            ReadOnlySpan<byte> span = readOnlySequence.IsSingleSegment ? readOnlySequence.First.Span : readOnlySequence.ToArray().AsSpan();
            results.Add(Encoding.UTF8.GetString(span));
        }
    }


    /// <summary>
    /// 扩展中间件
    /// </summary>
    public static class RequestLoggingMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
