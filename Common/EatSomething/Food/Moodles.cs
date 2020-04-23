using System;
using System.Collections.Generic;
using System.Text;

namespace Common.EatSomething.Food
{
    /// <summary>
    /// 面条
    /// </summary>
    public class Moodles : FoodAbstract
    {
        public Moodles() : base("面条")
        {
        }

        public override string Cook()
        {
            return "捞面条做好了，客人请用餐";
        }

        public override string PrepareTheTableware()
        {
            return "筷子有了";
        }
    }
}
