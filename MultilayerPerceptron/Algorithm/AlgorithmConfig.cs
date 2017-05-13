using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultilayerPerceptron.Functions;

namespace MultilayerPerceptron.Algorithm
{
    [Serializable()]
    public class AlgorithmConfig
    {
        public double LearningRate { get; set; }
        // Размер пакета
        public int BatchSize { get; set; }
        // Макисмальное количество циклов 
        public int MaxCycles { get; set; }
        // минимальный предел акуммулятивной ошибки для всех обучающих выборок, при достижении которого алгоритм останавливается
        public double MinError { get; set; }

        public MinimizationFunction ErrorFunction { get; set; }
    }
}
