using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilayerPerceptron.Network
{
    [Serializable()]
    public class NeuralNetwork
    {
        private Layer[] layers;

        public NeuralNetwork(int amountOfLayers, int[] amountOfNeurons, int vectorLength, int amountOfClasses, double beta)
        {
            layers = new Layer[amountOfLayers];
            layers[0] = new Layer(amountOfNeurons[0], vectorLength, beta);   // первый слой 
            for (int i = 1; i < amountOfLayers - 1; i++)
                layers[i] = new Layer(amountOfNeurons[i], amountOfNeurons[i - 1], beta); // промежуточные скрытые слои
            layers[amountOfLayers - 1] = new Layer(amountOfClasses, amountOfNeurons[amountOfNeurons.Length - 1], beta); // Выходной слой
        }
        public Layer[] Layers
        { get { return layers; } set { layers = value; } }
        
        // Расчёт выхода сети по одному входному вектору
        public double[] ComputeOutput(Vector inputVector)
        {
            double[] response = layers[0].Compute(inputVector);
            for (int i = 1; i < layers.Length; i++)
                response = layers[i].Compute(new Vector(response, inputVector.result));
            return response;
        }
    }
}
