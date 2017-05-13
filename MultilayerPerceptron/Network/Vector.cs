using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilayerPerceptron.Network
{
    public class Vector
    {
        public double[] input { get; private set; } 

        public double[] result { get; set; }

        public Vector(double[] input, double[] result)
        {
            this.input = input;
            this.result = result;
        }
    }
}
