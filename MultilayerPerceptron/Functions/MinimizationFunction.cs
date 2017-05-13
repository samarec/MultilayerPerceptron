using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilayerPerceptron.Functions
{
    [Serializable()]
    public class MinimizationFunction
    {
        // Расчет целевой функции Е = 0.5*SUM ((d-y)^2)
        public double Calculate(double[] etalon, double[] real)
        {
            double d = 0;
            for (int i = 0; i < real.Length; i++)
            {
                d += (etalon[i] - real[i]) * (etalon[i] - real[i]);
            }
            return 0.5 * d;
        }

        // Расчет СКО
        public double CalculateRMS(double[] etalon, double[] real)
        {
            double d = 0;
            for (int i = 0; i < real.Length; i++)
            {
                d += (etalon[i] - real[i]) * (etalon[i] - real[i]);
            }
            return d;
        }

        // Расчет первой производной
        public double CalculateDerivaitve(double[] etalon, double[] real, int index)
        {
            double deriv = real[index] - etalon[index];
            return deriv;
        }
    }
}
