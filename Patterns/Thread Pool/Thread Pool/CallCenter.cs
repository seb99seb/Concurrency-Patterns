using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_Pool
{
    public class CallCenter
    {
        public int Employees { get; set; }
        public CallCenter(int employees)
        {
            Employees = employees;
            if (Employees > 24)
            {
                Employees = 24;
            }
            ThreadPool.SetMaxThreads(Employees, 100);
        }
        public void Call()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            {
                Random CallLength = new Random();

                //Call ID == Thread ID
                Console.WriteLine($"Call ID: {Thread.CurrentThread.ManagedThreadId} - Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}");

                Thread.Sleep(CallLength.Next(500, 2001));

                Console.WriteLine($"Call {Thread.CurrentThread.ManagedThreadId} Done");
            }));
        }
    }
}
