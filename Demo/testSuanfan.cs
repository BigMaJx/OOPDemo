using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class testSuanfan
    {
        public static void Show() 
        {
            int[] arr = { 8, 5, 2, 6, -1, 9, 3, 1, 4, 0, 7 };             
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    var now = arr[i];
                    var next = arr[j];
                    if (next > now)
                    {
                        arr[i] = next;
                        arr[j] = now;
                    } 
                }
            }
            foreach (var item in arr)
            { 
                Console.WriteLine(item);
            }
            Console.Read();           
        }


        //递归
        public static void TestA()
        {
            Console.WriteLine(Foo(6));
        }
        public static int Foo(int i)
        {
            if (i <= 0)
                return 0;
            else if (i > 0 && i <= 2)
                return 1;
            else return Foo(i - 1) + Foo(i - 2);
        }
    }
}
