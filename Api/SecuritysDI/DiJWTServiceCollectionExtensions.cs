using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.SecuritysDI
{
    /// <summary>
    /// jwt 用户权限
    /// </summary>
    public static class JwtRole
    {
        /// <summary>
        /// 用户登录的
        /// </summary>
        public const string UserInfo = "UserInfo";

        /// <summary>
        /// 设备登录的
        /// </summary>
        public const string DeviceInfo = "DeviceInfo";
    }


    /// <summary>
    /// 微软 System.IdentityModel.Tokens.Jwt 
    /// </summary>
    public class JwtService
    {

        private TokenValidationParameters _tokenValidationParameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenValidationParameters"></param>
        public JwtService(TokenValidationParameters tokenValidationParameters)
        {
            _tokenValidationParameters = tokenValidationParameters;
        }


        /// <summary>
        /// 生产JWTToken
        /// </summary>
        /// <param name="claim"></param>
        /// <param name="role"></param>
        /// <param name="expires">过期时间</param>
        /// <returns></returns>
        public string GenerateJwtToken(Claim claim, string role, DateTime expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                      claim,
                      new Claim(ClaimTypes.Role,role)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(_tokenValidationParameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        /// <summary>
        ///  验证token是否合理，返回对应的值
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ValidatedJwtToken(string authenticationToken, out ClaimsPrincipal user)
        {
            bool result = false;
            user = null;
            try
            {

                if (string.IsNullOrEmpty(authenticationToken))
                {
                    return false;
                }
                //校验并解析token
                var handler = new JwtSecurityTokenHandler();
                var claimsPrincipal = handler.ValidateToken(authenticationToken, _tokenValidationParameters,
                    out SecurityToken validatedToken);//validatedToken:解密后的对象

                //user = ((JwtSecurityToken)validatedToken).Payload.SerializeToJson(); //获取payload中的数据 
                result = claimsPrincipal.Identity.IsAuthenticated;
                user = claimsPrincipal;

            }
            catch (SecurityTokenExpiredException stee)
            {
                //表示过期 
            }
            catch (SecurityTokenException ste)
            {
                //表示token错误

            }
            catch (Exception e)
            {

            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public bool ValidatedJwtToken(string authenticationToken, out object payload)
        {
            bool result = false;
            payload = null;
            try
            {

                if (string.IsNullOrEmpty(authenticationToken))
                {
                    return false;
                }
                //校验并解析token
                var handler = new JwtSecurityTokenHandler();
                var claimsPrincipal = handler.ValidateToken(authenticationToken, _tokenValidationParameters,
                    out SecurityToken validatedToken);//validatedToken:解密后的对象

                //user = ((JwtSecurityToken)validatedToken).Payload.SerializeToJson(); //获取payload中的数据 
                result = claimsPrincipal.Identity.IsAuthenticated;
                var jwtVT = ((JwtSecurityToken)validatedToken);
                payload = jwtVT.Payload.GetValueOrDefault(ClaimTypes.Authentication) ?? jwtVT.Payload.GetValueOrDefault(ClaimTypes.UserData);
            }
            catch (SecurityTokenExpiredException stee)
            {
                //表示过期 
                Console.WriteLine("Token过期");
            }
            catch (SecurityTokenException ste)
            {
                //表示token错误
                Console.WriteLine("Token错误");

            }
            catch (Exception e)
            {

            }
            return result;
        }
    }



    public static class DiJWTServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwtService(this IServiceCollection services,string serkey)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,//是否验证Issuer
                ValidIssuer = "ApiServer",
                ValidateAudience = false,//是否验证Audience
                ValidateLifetime = true,//是否验证失效时间
                ClockSkew = TimeSpan.FromSeconds(60),
                ValidateIssuerSigningKey = true,//是否验证SecurityKey
                                                //ValidAudience = Const.Domain,//Audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(serkey))//拿到SecurityKey
            };
            //将此配置 注入服务，因为还有其他地方需要使用
            services.AddSingleton(tokenValidationParameters);

            //注册 自定义服务
            services.AddSingleton<JwtService>();
            return services;
        }
    }
}
