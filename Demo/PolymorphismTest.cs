﻿using Common.EatSomething;
using Common.EatSomething.Enums;
using Common.EatSomething.Food;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class PolymorphismTest
    {
        public static void Show() 
        {
            //基础用法
            BasicUsage();

            /// <summary>
            /// 工厂用法
            /// </summary>
            FactoryBasicUsage();
        }
        //基础用法
        static void BasicUsage()
        {
            Console.WriteLine("基础用法");
            FoodAbstract foodAbstract = new Rice();
            Console.WriteLine(foodAbstract.BeginEat());
            foodAbstract = new Moodles();
            Console.WriteLine(foodAbstract.BeginEat());
        }

        /// <summary>
        /// 工厂用法
        /// </summary>
        static void FactoryBasicUsage()
        {
            Console.WriteLine("工厂用法");
            var food = EatFactory.FoodFactory(FoodEnum.Moodles);
            Console.WriteLine(food.BeginEat());
            food = EatFactory.FoodFactory(FoodEnum.Rice);
            Console.WriteLine(food.BeginEat());
        }
    }
}
