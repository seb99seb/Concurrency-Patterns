using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_Pool
{
    public class CallCenter
    {
        public int Employees { get; set; }
        public int EmployeeCounter { get; set; }
        public Stopwatch stopWatch = new Stopwatch();
        public TimeSpan CallTime { get; set; }
        public TimeSpan CallAltTime { get; set; }
        public CallCenter(int employees)
        {
            Employees = employees;
            if (Employees < 24)
            {
                Employees = 24;
            }
            EmployeeCounter = Employees;
            ThreadPool.SetMaxThreads(Employees, 100);
            ThreadPool.SetMinThreads(Employees, 100);
        }
        public void Call()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            {
                //Call ID == Thread ID
                Console.WriteLine($"Call ID: {Thread.CurrentThread.ManagedThreadId} - Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}");
                Thread.Sleep(500);
                Console.WriteLine($"Call {Thread.CurrentThread.ManagedThreadId} Done");
                ThreadPool.GetAvailableThreads(out int x, out int y);
                if (x == Employees - 1)
                {
                    stopWatch.Stop();
                    CallTime = stopWatch.Elapsed;
                }
            }));
        }
        public void CallAlt(int callsTotal)
        {
            for (int i = 0; i < callsTotal; i++)
            {
                while (true)
                {
                    if (EmployeeCounter > 0)
                    {
                        EmployeeCounter--;
                        new Thread(() =>
                        {
                            Console.WriteLine($"Call ID: {Thread.CurrentThread.ManagedThreadId - Employees} - Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}");
                            Thread.Sleep(500);
                            Console.WriteLine($"Call {Thread.CurrentThread.ManagedThreadId - Employees} Done");

                            EmployeeCounter++;

                            if (EmployeeCounter == Employees-1)
                            {
                                stopWatch.Stop();
                                CallAltTime = stopWatch.Elapsed;
                            }
                        }).Start();
                        break;
                    }
                }
            }
        }
        public override string ToString()
        {
            return $"Elapsed time for Call(): {CallTime}\n" +
                $"Elapsed time for CallAlt(): {CallAltTime}";
        }
    }
}
