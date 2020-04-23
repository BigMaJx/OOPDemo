using System;
using System.Collections.Generic;
using System.Text;

namespace Common.EatSomething.Food
{
    /// <summary>
    /// 大米
    /// </summary>
    public class Rice : FoodAbstract
    {
        public Rice() : base("蛋炒饭")
        {
        }

        public override string Cook()
        {
            return "蛋炒饭做好了，不加鸡蛋不加饭";
        }

        public override string PrepareTheTableware()
        {
            return "自己拿个勺子";
        }
    }
}
