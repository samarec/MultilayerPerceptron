using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilayerPerceptron.Functions
{
    // Сигмоидальная функция активации
    [Serializable()]
    public class Sigmoid : Function
    {
        public DoubleValue Beta { get; set; }

        public Sigmoid(double beta)
        {
            Beta = beta;
        }

        public override double Value(double x)
        {
            double value = 1 / (1 + Math.Exp(-Beta * x));
            if (Double.IsNaN(value))
            {
                value = Double.NaN;
            }
            return value;
        }

        public override double ComputeFirstDerivative(double x)
        {
            return Beta * this.Value(x) * (1 - this.Value(x));
        }

    }
}
