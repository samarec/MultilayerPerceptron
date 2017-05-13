using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultilayerPerceptron.Functions;


namespace MultilayerPerceptron.Network
{
    [Serializable()]
    public class Neuron
    {
        private static int neuronNumber = 0;
        public int index;
        public int VectorLength { get; private set; }   // Длина вектора входных данных
        private Function activationFunction;
        private double[] weights;                       // Веса входного вектора     
        private double offset;                          // Смещение нейрона 
        private double lastState;                       // Последнее состояние нейрона 
        private double lastInput;                       // Последняя сумма произведений значений входного вектора на веса
        private double derivative;
        private double sko;
        private Random random;

        public Neuron(int vectorLength, double beta)
        {
            index = ++neuronNumber;
            random = new Random(index);
            VectorLength = vectorLength;
            weights = new double[vectorLength];
            lastState = 0;
            RandomiseWeights();
            activationFunction = new Sigmoid(beta);

        }

        public double[] Weights
        { get { return weights; } }
        public double this[int i]
        {
            get
            {
                return weights[i];
            }
            set
            {
                weights[i] = value;
            }
        }

        public double Offset
        { get { return offset; } set { offset = value; } }

        public double LastState
        { get { return lastState; } set { lastState = value; } }

        public double LastInput
        { get { return lastInput; } set { lastInput = value; } }

        public Function ActivationFunction
        { get { return activationFunction; } set { activationFunction = value; } }

        public double Derivative
        { get { return derivative; } set { derivative = value; } }

        public double Sko
        { get { return sko; } set { sko = value; } }

        // Начальные веса
        private void RandomiseWeights()
        {
            double maxValue = Math.Sqrt(VectorLength) / 2;
            double minValue = -maxValue;
            for (int i = 0; i < VectorLength; i++)
                weights[i] = random.NextDouble() * (maxValue - minValue) + (minValue);
            offset = random.NextDouble() * (maxValue - minValue) + (minValue);
        }
        
        // Расчёт ответа нейрона
        public double Calculate(Vector inputVector)
        {
            double sum = 0;

            for (int i = 0; i < VectorLength; i++)
            {
                sum += weights[i] * inputVector.input[i];  // суммирование компонентов входного вектора и веса ребра

            }
            sum += offset;                   // смещение
            lastInput = sum;

            lastState = activationFunction.Value(sum);         // рассчитываем реакцию нейрона по функции активации
            return lastState;
        }
    }
}
