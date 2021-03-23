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

            PolymorphismTest.Show();




            for (int j = 1; j <= 20; j++)
            {
                Console.WriteLine($" ");
                for (int i = 1; i <= j; i++)
                {
                    YangHuiSanjian.Show(j, i);
                }
            }
            Console.WriteLine($" ");
            for (int j = 1; j <= 20; j++)
            {
                Console.WriteLine($" ");
                for (int i = 1; i <= j; i++)
                {
                    YangHuiSanjian.Show2(j, i);
                }
            }


            TestApollo.Show();

            Console.ReadLine();
        }

        

      

        public void TestAliPay() 
        {
            try
            {
                new AliPay().GetQrCodeUrl("", 0, "", "", "");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
