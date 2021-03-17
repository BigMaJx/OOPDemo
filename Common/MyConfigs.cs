using Com.Ctrip.Framework.Apollo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{ 
    public class MyConfigs
    {
        private static IConfiguration _config;
        private static IConfiguration Configs
        {
            get
            {
                if (_config == null)
                {
                    string path = Directory.GetCurrentDirectory();
                    var configBuilder = new ConfigurationBuilder()
                           .SetBasePath(path)
                           .AddJsonFile("appsettings.json", true, true);

                    var apollo = configBuilder.Build().GetSection("apollo");
                    string nameSpace = apollo["NameSpace"];
                    if (string.IsNullOrEmpty(nameSpace))
                    {
                        _config = configBuilder.AddApollo(apollo).AddDefault().Build();
                    }
                    else
                    {
                        var apolloDefault = configBuilder.AddApollo(apollo).AddDefault();
                        var nameSpaces = nameSpace.Split(',',';');
                        foreach (var nameSpaceItem in nameSpaces)
                        {
                            apolloDefault = apolloDefault.AddNamespace(nameSpaceItem);
                        }
                        _config = apolloDefault.Build();
                    }
                }
                return _config;
            }
        }
        public static string GetConfig(string key)
        {
            var value = GetConfig<string>(key);
            return value;
        }
        public static T GetConfig<T>(string key)
        {            
            T value = Configs.GetValue<T>(key);
            return value;
        } 
    }
}
