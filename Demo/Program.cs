using Common.Alipay;
using Common.EatSomething;
using Common.EatSomething.Enums;
using Common.EatSomething.Food;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            BasicUsage();

            FactoryBasicUsage();
            Console.ReadLine();
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

        public void TestAliPay() 
        {
            try
            {
                new AliPay().GetQrCodeUrl("", 0, "", "", "");
            }
            catch (Exception e)
            {

            }
        }
    }
}
