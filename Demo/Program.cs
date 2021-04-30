using Common.Alipay;
using Common.EatSomething;
using Common.EatSomething.Enums;
using Common.EatSomething.Food;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Demo
{
    public static class  MyTest
    {
        public static int tally = 0;

}
    public class Teas {
        
        public void a(int b) 
        {
            for (int i = 0; i < 50; i++)
            {
                MyTest.tally += 1;
                Console.WriteLine($"{b}:"+ MyTest.tally);
            }
        }
    }

   
    class Program
    {
   
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //testSuanfan.Show();

            testSuanfan.TestA();
            //ClassValue.Test();

            //PolymorphismTest.Show();

            //var abaa = new Teas();

            //Task.Run(() =>
            //{
            //    abaa.a(111);
            //});


            //Task.Run(() =>
            //{
            //    abaa.a(222);
            //});



            //InterviewQuestionsTest.Show();

            //for (int j = 1; j <= 20; j++)
            //{
            //    Console.WriteLine($" ");
            //    for (int i = 1; i <= j; i++)
            //    {
            //        YangHuiSanjian.Show(j, i);
            //    }
            //}
            //Console.WriteLine($" ");
            //for (int j = 1; j <= 20; j++)
            //{
            //    Console.WriteLine($" ");
            //    for (int i = 1; i <= j; i++)
            //    {
            //        YangHuiSanjian.Show2(j, i);
            //    }
            //}


            // TestApollo.Show();

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
