using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    public class InterviewQuestionsTest
    {
        public static void Show()
        {
            One();
            Two();
            Three();
            Four(); 
            Console.WriteLine($"第五题答案：会执行，return后执行");

            Console.WriteLine($"第六题答案：常用的大概这些 ViewResult,JsonResult,FileResult,EmptyResult");
            
            Console.WriteLine($"第七题答案：var a=DateTime.Now.ToString(\"yyyy - MM - dd HH: mm:ss\")");


            Console.WriteLine($"第八题答案：");
            Console.WriteLine("方法1：Select * from a order by Id limit 0,50");
            Console.WriteLine("方法2：Select Top 50 * from a order by Id ");
            Console.WriteLine("方法3：Select * from (select ROW_NUMBER() over(order by id asc) as 'rowNumber', *from a) as temp where rowNumber BETWEEN(1) AND(50)");

            Console.WriteLine($"第九题答案：git、SVN、tfs");


            Console.WriteLine("第十题答案：");
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => {
                    Console.WriteLine(i);
                });
            }

            Thread.Sleep(1000);

            Console.WriteLine("第十一题答案：D");

            Console.WriteLine("第十二题答案：");
            {
                int x = 5;
                int y = x++;
                Console.WriteLine(x);
                Console.WriteLine(y);
                y = ++x;
                Console.WriteLine(y);
            }


            Console.WriteLine("第十三题答案：D");

           
            //{
            //    int x = 5;
            //    Console.WriteLine($"{x++}:{++x}");
            //    int y = x++ + ++x;
            //    int z = ++x + x++;
            //    Console.WriteLine(y + "==" + z);
            //}
        }

        public static void One()
        {
            int a = 3;
            int b = 6;
            var c = 3m;
            var c1 = 3.0;
            var d = a / b;
            var e = c / b;
            var f = c1 / b;            
            Console.WriteLine($"第一题答案： d:{d},e:{e},f:{f}");

        }

        public static void Two()
        {
            var a = 3m;
            var b = 3f;
            var c = 3d;

            Console.WriteLine("第二题答案：a:decmail b:float c:double");
        }

        public static void Three()
        {
            var a = new abc();
            Console.WriteLine($"第三题答案：a:{a.a} b:{a.b} c:{a.c} d:{a.d} e:{a.e}");
        }

        public static void Four()
        {
            var a = new abc();
            Console.WriteLine($"第四题答案：A");
        }
        
        public static int Five(abc a) 
        {
            a.b = 3;            
            try
            {               
                return a.b;
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                a.b = 4; 
                Console.WriteLine("finally结束了");
            }
        }

    }

    public class abc
    {
        public bool a { get; set; }

        public int b { get; set; }

        public float c { get; set; }

        public double d { get; set; }

        public decimal e { get; set; }

    }
}

