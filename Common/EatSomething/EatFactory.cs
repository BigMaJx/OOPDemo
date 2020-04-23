using Common.EatSomething.Enums;
using Common.EatSomething.Food;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.EatSomething
{
    public class EatFactory
    {
        public static FoodAbstract FoodFactory(FoodEnum foodEnum)
        {
            FoodAbstract result = null;
            switch (foodEnum)
            {
                case FoodEnum.Moodles:
                    result = new Moodles();
                    break;
                case FoodEnum.Rice:
                    result = new Rice();
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
