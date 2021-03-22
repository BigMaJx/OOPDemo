using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //参考 xCode Git:https://gitee.com/NewLifeX/NewLife.Cube/blob/master/NewLife.Cube/Common/ReadOnlyEntityController.cs
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExportCsv()
        {
            var rs = Response;
            var headers = rs.Headers;
            headers[HeaderNames.ContentEncoding] = "UTF8";
            headers[HeaderNames.ContentType] = "application/vnd.ms-excel";
            Response.Headers.Add("Content-Disposition", $"Attachment;filename=测试导出_{DateTime.Now:yyyyMMddHHmmss}.csv");
            // 允许同步IO，便于CsvFile刷数据Flush
            var ft = HttpContext.Features.Get<Microsoft.AspNetCore.Http.Features.IHttpBodyControlFeature>();
            if (ft != null) ft.AllowSynchronousIO = true;

            var data = GetData();
            OnExportExcel(data, rs.Body);
            return new EmptyResult(); 
        }

        private IEnumerable<TL> GetData()
        {
            //最多1000页，再多不要了   
            for (int i = 1; i < 1000; i++)
            {
                var list = GetList(i);
                if (HttpContext.RequestAborted.IsCancellationRequested) yield break;
                if (list == null || !list.Any()) break; 
                foreach (var item in list)
                {
                    yield return item;
                }
            }
        }

        private  List<TL> GetList(int page) 
        {
            List<TL> result = new List<TL>();
            int pageSize = 5000;
            int nowI = page > 1 ? ((page - 1) * pageSize + 1) : 1;
            for (int i = 0; i < pageSize; i++)
            {
                result.Add(new TL()
                {
                    Id = nowI,
                    Name = "牛逼" + nowI
                });
                nowI++;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"> DisplayName 名称为列名 </param>
        /// <param name="output"></param>
        private void OnExportExcel<T>(IEnumerable<T> list, Stream output)
        {
            var csv = new StreamWriter(output, Encoding.UTF8, 1024, true);
            var headers = GetPropertyByType<T>();
            // 列头
            var title = string.Join(",", headers.Values.ToList());
            csv.WriteLine(title);

            var itemType = Activator.CreateInstance<T>().GetType();
            // 内容
            foreach (var entity in list)
            {
                var lineSb = new StringBuilder();
                foreach (var item in headers)
                {
                    var entityValue = itemType.GetProperty(item.Key);//获取对应列名
                    if (entityValue != null)
                    {
                        var value = entityValue.GetValue(entity);
                        value = value == null ? string.Empty : value;
                        lineSb.Append(value + ",");
                    }

                }
                var result = lineSb.ToString();
                if (result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                }
                csv.WriteLine(result);
            } 
        }

        private Dictionary<string, string> GetPropertyByType<In>()
        {
            var dict = new Dictionary<string, string>();
            var type = typeof(In);
            try
            {
                foreach (var item in type.GetProperties())
                {
                    var displayName = item.GetCustomAttribute<DisplayNameAttribute>();
                    if (displayName != null)
                    {
                        dict.Add(item.Name, displayName.DisplayName);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return dict;
        }



        public class TL 
        {
            [DisplayName("序号")]
            public int Id { get; set; }

            [DisplayName("名称")]
            public string Name { get; set; }
        }
    }
}
