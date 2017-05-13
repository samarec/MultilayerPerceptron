using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilayerPerceptron.Network
{
    [Serializable()]
    public class Layer
    {
        Neuron[] neurons;
        double[] lastOutput;
        int inputVectorLength;
        public Neuron[] Neurons
        { get { return neurons; } set { neurons = value; } }
        public double[] LastOutput
        { get { return lastOutput; } }

        public Layer(int amountOfNeurons, int inputVectorLength, double beta)
        {
            neurons = new Neuron[amountOfNeurons];
            this.inputVectorLength = inputVectorLength;
            for (int i = 0; i < amountOfNeurons; i++)
                neurons[i] = new Neuron(inputVectorLength, beta);
        }
        
        // Расчёт выхода слоя
        public double[] Compute(Vector inputVector)
        {
            int length = neurons.Length;
            lastOutput = new double[length];
            for (int i = 0; i < length; i++)
                lastOutput[i] = neurons[i].Calculate(inputVector);
            return lastOutput;
        }
    }
}
