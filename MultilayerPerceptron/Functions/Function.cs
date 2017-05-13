using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilayerPerceptron.Functions
{
    [Serializable()]
    public abstract class Function
    {
        public abstract double Value(double x);
        public abstract double ComputeFirstDerivative(double x);
        [Serializable()]
        public struct DoubleValue
        {
            private double value;

            public DoubleValue(double value)
            {
                this.value = value;
            }

            static public implicit operator DoubleValue(double value)
            {
                return new DoubleValue(value);
            }

            static public implicit operator double(DoubleValue doubleValue)
            {
                return doubleValue.value;
            }

            public override string ToString()
            {
                return value.ToString();
            }
        }
    }
}