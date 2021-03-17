using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
   public class TestApollo
    {
        public static void Show() 
        {
            var apollokey = MyConfigs.GetConfig("AuthExpiration");
            Console.WriteLine("展示Apollo：Key:AuthExpiration  Value:" + apollokey);
        }
    }
}
