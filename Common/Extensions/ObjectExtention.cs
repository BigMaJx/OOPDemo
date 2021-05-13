using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static  class ObjectExtention
    {
        public static string ToJson<T>(this T value)
        {
             return  JsonSerializer.Serialize(value);
        }
    }
}
