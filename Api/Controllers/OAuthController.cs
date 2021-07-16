using Api.SecuritysDI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// 鉴权中心
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private JwtService _jwtService;

        /// <summary>
        ///
        /// </summary>
        public OAuthController(JwtService jwtService)
        {
            this._jwtService = jwtService;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public string GetToken()
        {
            var content = "我是登录成功后 保存的内容哦：" + DateTime.Now.Ticks.ToString();
            var token = _jwtService.GenerateJwtToken(new Claim(ClaimTypes.Authentication, content), nameof(JwtRole.UserInfo), DateTime.Now.AddDays(2));
            return token;
        }

        [HttpGet]
        public string Get()
        {
            var b = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Authentication);
            return "牛逼Get===jwt自带的数据：" + b.Value;
        }
    }
}
