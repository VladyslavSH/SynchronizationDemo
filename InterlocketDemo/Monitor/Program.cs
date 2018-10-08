using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    class InterlockedCounter
    {
        int field1;
        int field2;
        public int Field1 { get { return field1; } }
        public int Field2 { get { return field2; } }

        public void UpdateFields()
        {
                for (int i = 0; i < 100000; i++)
                {
                    Interlocked.Increment(ref field1);
                    if (field1 % 2 == 0)
                    {
                        Interlocked.Increment(ref field2);
                    }
                }
        }
    }
    class MonitorCount
    {
        int field1;
        int field2;
        public int Field1 { get { return field1; } }
        public int Field2 { get { return field2; } }

        public void UpdateFields()
        {
            lock (this)
            {
                for (int i = 0; i < 100000; i++)
                {
                    Interlocked.Increment(ref field1);
                    if (field1 % 2 == 0)
                    {
                        Interlocked.Increment(ref field2);
                    }
                }
            }
        }
    }

    
    class Program
    {
        static void BadAsync()
        {
            Console.WriteLine("Синхронизация методами Interlocked");
            InterlockedCounter c = new InterlockedCounter();
            Thread[] thread = new Thread[5];
            for (int i = 0; i < thread.Length; i++)
            {
                thread[i] = new Thread(c.UpdateFields);
                thread[i].Start();
            }
            for (int i = 0; i < thread.Length; i++)
            {
                thread[i].Join();
            }
            Console.WriteLine($"Fields 1 = {c.Field1}, 2 = {c.Field2}");
        }
        static void GoodAsync()
        {
            Console.WriteLine("Синхронизация методами Monitor");
            MonitorCount c = new MonitorCount();
            Thread[] thread = new Thread[5];
            for (int i = 0; i < thread.Length; i++)
            {
                thread[i] = new Thread(c.UpdateFields);
                thread[i].Start();
            }
            for (int i = 0; i < thread.Length; i++)
            {
                thread[i].Join();
            }
            Console.WriteLine($"Fields 1 = {c.Field1}, 2 = {c.Field2}");
        }
        static void Main(string[] args)
        {
            BadAsync();
            GoodAsync();
            Console.ReadKey();
        }
    }
}
