using System;
using System.Collections.Generic;
using System.Text;

namespace Common.EatSomething.Food
{
    /// <summary>
    /// 抽象食物
    /// </summary>
    public abstract class FoodAbstract
    {
        private string Name { get; set; }
        public FoodAbstract(string name) 
        {
            Name = name;
        }
        /// <summary>
        /// 准备餐具
        /// </summary>
        /// <returns></returns>
        public abstract string PrepareTheTableware();

        /// <summary>
        /// 烹调
        /// </summary>
        /// <returns></returns>
        public abstract string Cook();

        /// <summary>
        /// 刷碗
        /// </summary>
        /// <returns></returns>
        private string BrushBowl() 
        {
            return "吃完了，刷碗";
        }
        /// <summary>
        /// 开吃
        /// </summary>
        /// <returns></returns>
        public string BeginEat()
        {            
            //这个地方 就很好的体现了多态
            //不同食物要用不同的餐具
            //不同的食物要用不同烹饪方法
            //但是他们吃完后都得刷碗。为了简单体验，我们默认刷碗是一样的工程

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Name);
            //获得餐具
            sb.AppendLine(PrepareTheTableware());
            //开始烹饪
            sb.AppendLine(Cook());
            //吃完刷碗
            sb.AppendLine(BrushBowl());
            return sb.ToString();
        }
    }
}
