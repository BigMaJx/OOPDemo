using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{ 
    public class MyConfigs
    {
        private static IConfiguration _config;
        public static IConfiguration Configs
        {
            get
            {
                if (_config == null)
                {
                    string path = Directory.GetCurrentDirectory();
                    var configBuilder = new ConfigurationBuilder()
                           .SetBasePath(path)
                           .AddJsonFile("appsettings.json", true, true);
                    _config = configBuilder.Build();

                    //阿波罗配置
                    //var apollo = configBuilder.Build().GetSection("apollo");
                    //string nameSpace = apollo["NameSpace"];
                    //if (nameSpace.IsNullOrEmpty())
                    //{
                    //    _config = configBuilder.AddApollo(apollo).AddDefault().Build();
                    //}
                    //else
                    //{
                    //    _config = configBuilder.AddApollo(apollo).AddDefault().AddNamespace(nameSpace).Build();
                    //}
                }
                return _config;
            }
        }

        public static string GetConfig(string key)
        {
            string value = Configs.GetValue(key, "");
            return value;
        }
    }
}
