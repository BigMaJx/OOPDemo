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

            //PolymorphismTest.Show();

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

            }
        }
    }
}
