using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lock
{
    public class AccumulatedValue
    {
        public int Value { get; set; }
        public AccumulatedValue(int value)
        { 
            Value = value;
        }
        public override string ToString()
        {
            return $"Has rolled a total sum of {Value}";
        }
    }
}
