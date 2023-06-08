using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lock
{
    public class DieTransfer
    {
        private readonly object _dieLock = new object();
        public void Roll(AccumulatedValue value)
        {
            lock (_dieLock)
            {
                Random rnd = new Random();
                int TotalRollValue = 0;
                for (int i = 0; i < 3; i++)
                {
                    TotalRollValue += rnd.Next(1, 7);
                }
                Console.WriteLine($"Rolled a value of {TotalRollValue} with 3 dices");
                value.Value += TotalRollValue;
            }
        }
        public void Transfer(AccumulatedValue value1, AccumulatedValue value2)
        {
            if (Monitor.TryEnter(value1, 100))
            {
                Thread.Sleep(100);
                try
                {
                    if (Monitor.TryEnter(value2, 100))
                    {
                        try
                        {
                            value2.Value += value1.Value;
                            value1.Value = 0;

                        }
                        finally
                        {
                            Monitor.Exit(value2);
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(value1);
                }
            }
        }
        public void TransferDeadlock(AccumulatedValue value1, AccumulatedValue value2)
        {
            lock (value1)
            {
                Thread.Sleep(100);
                if  (value1.Value > 0)
                {
                    lock (value2)
                    {
                        value2.Value += value1.Value;
                        value1.Value = 0;
                    }
                }
            }
        }
    }
}
