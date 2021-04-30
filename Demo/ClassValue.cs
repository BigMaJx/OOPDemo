using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class TL
    {
        [DisplayName("序号")]
        public int Id { get; set; }

        [DisplayName("名称")]
        public string Name { get; set; }
    }

 
    public class ClassValue
    {

        public static void Test() 
        {
            var tasks = new List<Task>();
            var tasks2 = new List<Task>();
            var tasks3 = new List<Task>();
            var tasks4 = new List<Task>();
            var list = GetList(1);
            foreach (var item in list)
            {
                tasks.Add(Task.Run(() =>
                {
                    Console.WriteLine(item.Id);
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("哈下一个");

            var myTl = new TL();
            for (int i = 0; i < list.Count; i++)
            {
                myTl = list[i];
                tasks2.Add(Task.Run(() =>
                {
                    Console.WriteLine(myTl.Id);
                }));
            }
            Task.WaitAll(tasks2.ToArray());
            Console.WriteLine("哈哈下一个");
            for (int i = 0; i < list.Count-1; i++)
            { 
                tasks3.Add(Task.Run(() =>
                {
                    Console.WriteLine(list[i].Id);
                }));
            }
            Task.WaitAll(tasks3.ToArray());
            Console.WriteLine("哈哈哈下一个");

            for (int i = 0; i < list.Count; i++)
            {
                int id = list[i].Id;
                tasks4.Add(Task.Run(() =>
                {
                    Console.WriteLine(id);
                }));
            }
            Task.WaitAll(tasks4.ToArray());
            Console.WriteLine("哈哈哈哈结束");
        }

        private static List<TL> GetList(int page)
        {
            List<TL> result = new List<TL>();
            int pageSize = 20;
            int nowI = page > 1 ? ((page - 1) * pageSize + 1) : 1;
            for (int i = 0; i < pageSize; i++)
            {
                result.Add(new TL()
                {
                    Id = nowI,
                    Name = "牛逼" + nowI
                });
                nowI++;
            }
            return result;
        }
    }
}
