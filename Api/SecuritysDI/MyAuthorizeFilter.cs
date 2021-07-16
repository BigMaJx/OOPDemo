using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.SecuritysDI
{
    /// <summary>
    /// 只为重写 OnAuthorizationAsync  自定义返回数据消息
    /// </summary>
    public class MyAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private JwtService _jwtService { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jwtService"></param>
        public MyAuthorizeFilter(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        private static bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            var filters = context.Filters;
            for (var i = 0; i < filters.Count; i++)
            {
                if (filters[i] is IAllowAnonymousFilter)
                {
                    return true;
                }
            }

            // When doing endpoint routing, MVC does not add AllowAnonymousFilters for AllowAnonymousAttributes that
            // were discovered on controllers and actions. To maintain compat with 2.x,
            // we'll check for the presence of IAllowAnonymous in endpoint metadata.
            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (!context.IsEffectivePolicy(this))
            {
                return;
            }
            // Allow Anonymous skips all authorization
            if (HasAllowAnonymous(context))
            {
                return;
            }
            else
            {
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()
                   ?? context.HttpContext.Request.Headers["X-Token"].FirstOrDefault()
                   ?? context.HttpContext.Request.Query["Token"].FirstOrDefault()
                   ?? context.HttpContext.Request.Cookies["Token"];

                ClaimsPrincipal user = context.HttpContext.User;
                if (_jwtService.ValidatedJwtToken(token, out user))
                {
                    //验证通过，验证成功  赋值给User
                    context.HttpContext.User = user;
                }
                else
                {
                    context.Result = new JsonResult(new
                    {
                        Code = 401,
                        Message = "没有权限"
                    });
                }
            }
            //Console.WriteLine(JsonHelper.SerializeObject(context.HttpContext.Items));
        }
    }
}
