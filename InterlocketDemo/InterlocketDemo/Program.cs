using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterlocketDemo
{
    class Counter
    {
        public static int count;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(delegate ()
                {
                    for (int j = 0; j < 1000000; j++)
                    {
                        Interlocked.Increment(ref Counter.count);
                    }
                });
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine($"Counter = {Counter.count}");
            Console.ReadKey();
        }
    }
}
