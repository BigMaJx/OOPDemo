using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class YangHuiSanjian
    {
        /// <summary>
        /// 利用公式计算
        /// </summary>
        /// <param name="n">第几行</param>
        /// <param name="m">第几个数字</param>
        public static void Show(int n, int m) 
        {
            n = n - 1; m = m - 1;
            if (m > n)
            {
                Console.WriteLine("来个寂寞！");
                return;
            }
            var result = Cnm1(n, m);
            Console.Write(result+" ");
        }

        /// <summary>
        /// 使用下个书是上个数之和计算
        /// </summary>
        /// <param name="n">第几行</param>
        /// <param name="m">第几个数字</param>
        public static void Show2(int n, int m)
        { 
            if (m > n)
            {
                Console.WriteLine("来个寂寞！");
                return;
            }
            var result = Cnm2(n, m);
            Console.Write(result + " ");
        }

        //还没走几行，就挂了
        /// <summary>
        /// 排列组合中的 C(n,m)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private static decimal Cnm(int n,int m) 
        {
            decimal left = 1;
            for (int i = 1; i <= n; i++)
            {
                left = left * i;
            }

            decimal right1 = 1;
            for (int i = 1; i <= m; i++)
            {
                right1 = right1 * i;
            }

            decimal right2 = 1;
            for (int i = 1; i <= n-m; i++)
            {
                right2 = right2 * i;
            }
            return left / right1/ right2;
        }

        //能支持到95行
        /// <summary>
        /// 排列组合中的 C(n,m)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static object Cnm1(int n, int m)
        {
            List<decimal> lefts = new List<decimal>();
            List<decimal> rights = new List<decimal>();
            for (int i = 1; i <= n; i++)
            {
                var lValue = i;
                var maxMvalue = m >= (n - m) ? m : (n - m);
                var mValue = i > maxMvalue ? 1 : i;
                int mCount = 1;
                if (i <= m)
                {
                    if (i <= (n - m))
                    {
                        mCount = 2;
                    }
                }
                var pmVale = mCount == 2 ? mValue * mValue : mValue;
                if (lValue != pmVale)
                {
                    lefts.Add(lValue);
                    rights.Add(mValue);
                    if (mCount > 1)
                    {
                        rights.Add(mValue);
                    }
                }
            } 
            decimal l = 1;
            foreach (var item in lefts)
            {
                int rIndex = rights.IndexOf(item);
                if (rIndex > 0)
                {
                    rights.RemoveAt(rIndex);
                }
                else
                {
                    l = l * item;
                    while (rights.Count > 1 && l % rights[0] == 0)
                    {
                        l = l / rights[0];
                        rights.RemoveAt(0);
                    }
                } 
            } 
            decimal r = 1;
            foreach (var item in rights)
            {
                r = r * item;
            }
            var result = l / r;
            return result;
        }


        private static ConcurrentDictionary<int, List<decimal>> AllYHSJ = new ConcurrentDictionary<int, List<decimal>>();

        //能支持到100行
        /// <summary>
        /// 使用下个是上两个数之和 计算
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static object Cnm2(int n, int m) 
        {
            var pre =  AllYHSJ.LastOrDefault().Value==null ?new List<decimal>():AllYHSJ.LastOrDefault().Value;
            
            if (AllYHSJ.Count >= n)
            {
                var list = AllYHSJ.GetValueOrDefault(n);
                return list[(m-1)];
            }
            var result = 0m;
            for (int i = AllYHSJ.Count + 1; i <= n; i++)
            {
                var now = new List<decimal>();
                for (int j = 1; j <= pre.Count + 1; j++)
                {
                    var value = 1m;
                    if (j > 1 && j != pre.Count + 1)
                    {
                        value = pre[j - 1 - 1] + pre[j - 1];
                    }                    
                    now.Add(value);
                    if (i == n && j == m)
                    {
                        result= value;
                    }
                }
                AllYHSJ.TryAdd(i, now);
                pre = now;
            }
            return result;
        }
    }
}
