using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultilayerPerceptron.Network;
using MultilayerPerceptron.Views;

namespace MultilayerPerceptron.Algorithm
{
    public class BackPropagation
    {
        private AlgorithmConfig _config = null;
        private Random _random = null;
        public event EventHandler<MessageContainer> RaiseMessageEvent;

        public BackPropagation(AlgorithmConfig config)
        {
            _config = config;
            _random = new Random();
        }
        
        public void Train(NeuralNetwork network, List<Vector> data)
        {
            if (_config.BatchSize < 1 || _config.BatchSize > data.Count)
                _config.BatchSize = data.Count;
            double currentError = Single.MaxValue;
            double lastError = 0;
            int cycleNumber = 0;

            do
            {
                lastError = currentError;
                int[] trainingID = new int[data.Count]; // каждой выборке присвоим номер
                for (int i = 0; i < data.Count; i++)
                {
                    trainingID[i] = i;
                }

                int currentIndex = 0;
                do
                {   // Выполняем для всех записей в массиве данных
                    // здесь будут храниться аккумулятивные ошибки весов и смещения для пачки входных данных.
                    // Если онлайн обучение то в них окажется градиент по одному примеру тестовых данных
                    double[][][] nablaWeights = new double[network.Layers.Length][][];
                    double[][] nablaOffsets = new double[network.Layers.Length][];

                    // Начинаем процесс для одного пакета данных. 
                    for (int inBatchIndex = currentIndex; inBatchIndex < currentIndex + _config.BatchSize && inBatchIndex < data.Count; inBatchIndex++)
                    {
                        // рассчитываем выход сети, прямой прогон
                        double[] realOutput = network.ComputeOutput(data[trainingID[inBatchIndex]]);
                        // обратный прогон. Рапсространение ошибки
                        int lastLayerLength = network.Layers.Length - 1; // номер последнего слоя
                        int lastLayerNeurons = network.Layers[lastLayerLength].Neurons.Length; // кол-во нейронов в последнем слое
                        nablaWeights[lastLayerLength] = new double[lastLayerNeurons][]; // инициализируем по кол-ву нейронов в последнем слое
                        nablaOffsets[lastLayerLength] = new double[lastLayerNeurons];
                        for (int j = 0; j < lastLayerNeurons; j++) // Для всех нейронов последнего слоя
                        {
                            network.Layers[lastLayerLength].Neurons[j].Derivative =
                                _config.ErrorFunction.CalculateDerivaitve(data[inBatchIndex].result, realOutput, j)
                            * network.Layers[lastLayerLength].Neurons[j].ActivationFunction.ComputeFirstDerivative
                            (network.Layers[lastLayerLength].Neurons[j].LastInput);

                            //коррекция смещения
                            nablaOffsets[lastLayerLength][j] -= _config.LearningRate *
                                                                        network.Layers[lastLayerLength].Neurons[j].Derivative;
                            //коррекция остальных весов  
                            nablaWeights[lastLayerLength][j] = new double[network.Layers[lastLayerLength].Neurons[j].Weights.Length];
                            for (int i = 0; i < network.Layers[lastLayerLength].Neurons[j].Weights.Length; i++)
                            {
                                nablaWeights[lastLayerLength][j][i] -=
                                    _config.LearningRate * (network.Layers[lastLayerLength].Neurons[j].Derivative *
                                                          (network.Layers.Length > 1 ?
                                                                network.Layers[lastLayerLength - 1].Neurons[i].LastState :
                                                                data[inBatchIndex].input[i])
                                                            );
                            }
                        }
                        //скрытые слои
                        for (int hiddenLayerIndex = lastLayerLength - 1; hiddenLayerIndex >= 0; hiddenLayerIndex--)
                        {
                            nablaWeights[hiddenLayerIndex] = new double[network.Layers[hiddenLayerIndex].Neurons.Length][];
                            nablaOffsets[hiddenLayerIndex] = new double[network.Layers[hiddenLayerIndex].Neurons.Length];
                            // для всех нейронов текущего скрытого слоя
                            for (int j = 0; j < network.Layers[hiddenLayerIndex].Neurons.Length; j++)
                            {
                                network.Layers[hiddenLayerIndex].Neurons[j].Derivative = 0;
                                // суммирование предыдущего слоя (веса и градиенты)

                                for (int k = 0; k < network.Layers[hiddenLayerIndex + 1].Neurons.Length; k++)
                                {
                                    network.Layers[hiddenLayerIndex].Neurons[j].Derivative +=
                                        network.Layers[hiddenLayerIndex + 1].Neurons[k].Weights[j] *
                                        network.Layers[hiddenLayerIndex + 1].Neurons[k].Derivative;

                                }

                                // умножаем на производную функции активации текущего слоя. получили дельту скрытого слоя
                                network.Layers[hiddenLayerIndex].Neurons[j].Derivative *=
                                    network.Layers[hiddenLayerIndex].Neurons[j].ActivationFunction.
                                        ComputeFirstDerivative(
                                            network.Layers[hiddenLayerIndex].Neurons[j].LastInput
                                        );

                                // коррекция смещения
                                nablaOffsets[hiddenLayerIndex][j] -= _config.LearningRate *
                                                                   network.Layers[hiddenLayerIndex].Neurons[j].Derivative;
                                // коррекция весов
                                nablaWeights[hiddenLayerIndex][j] = new double[network.Layers[hiddenLayerIndex].Neurons[j].Weights.Length];
                                for (int i = 0; i < network.Layers[hiddenLayerIndex].Neurons[j].Weights.Length; i++)
                                {
                                    nablaWeights[hiddenLayerIndex][j][i] -= _config.LearningRate * (
                                        network.Layers[hiddenLayerIndex].Neurons[j].Derivative *
                                        (hiddenLayerIndex > 0 ? network.Layers[hiddenLayerIndex - 1].Neurons[i].LastState : data[inBatchIndex].input[i])
                                        );
                                }
                            }
                        }
                    }
                    //обновить веса и смещение
                    for (int layerIndex = 0; layerIndex < network.Layers.Length; layerIndex++)
                    {
                        for (int neuronIndex = 0; neuronIndex < network.Layers[layerIndex].Neurons.Length; neuronIndex++)
                        {
                            network.Layers[layerIndex].Neurons[neuronIndex].Offset += nablaOffsets[layerIndex][neuronIndex];
                            for (int weightIndex = 0; weightIndex < network.Layers[layerIndex].Neurons[neuronIndex].Weights.Length; weightIndex++)
                            {
                                network.Layers[layerIndex].Neurons[neuronIndex].Weights[weightIndex] +=
                                    nablaWeights[layerIndex][neuronIndex][weightIndex];
                            }
                        }
                    }

                    currentIndex += _config.BatchSize;
                } while (currentIndex < data.Count);
                // пересчитаем ошибку на всех данных
                currentError = 0;

                for (int i = 0; i < data.Count; i++)
                {
                    double[] realOutput = network.ComputeOutput(data[i]);

                    currentError += _config.ErrorFunction.Calculate(data[i].result, realOutput);
                }
                currentError *= 1d / data.Count;
                cycleNumber++;

            } while (cycleNumber < _config.MaxCycles &&
                    currentError > _config.MinError);


            OnRaiseMessageEvent(new MessageContainer("Learning process ended. \nCycles of learning " + cycleNumber + "\n" + "Error: " + currentError + ";\n"));
        }
        /// <summary>
        /// Слушатель появления сообщения
        /// </summary>
        /// <param name="e">Контейнер для хранения сообщения</param>
        protected virtual void OnRaiseMessageEvent(MessageContainer e)
        {
            EventHandler<MessageContainer> handler = RaiseMessageEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
