using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lock
{
    public class Die
    {
        private int AccumulatedValue { get; set; }
        private readonly object _dieLock = new object();
        public void LocklessRoll()
        {
            Random rnd = new Random();
            int TotalRollValue = 0;
            for (int i = 0; i < 3; i++)
            {
                TotalRollValue += rnd.Next(1, 7);
            }
            Console.WriteLine($"Rolled a value of {TotalRollValue} with 3 dices");
            AccumulatedValue += TotalRollValue;
        }
        public void Roll()
        {
            lock (_dieLock )
            {
                Random rnd = new Random();
                int TotalRollValue = 0;
                for (int i = 0; i < 3; i++)
                {
                    TotalRollValue += rnd.Next(1, 7);
                }
                Console.WriteLine($"Rolled a value of {TotalRollValue} with 3 dices");
                AccumulatedValue += TotalRollValue;
            }
        }
        public void Transfer(AccumulatedValue value1, AccumulatedValue value2)
        {
            lock (value1)
            {
                lock (value2)
                {

                }
            }
        }
        public override string ToString()
        {
            return $"Has rolled a total sum of {AccumulatedValue}";
        }
    }
}
