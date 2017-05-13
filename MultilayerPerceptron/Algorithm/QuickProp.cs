using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultilayerPerceptron.Views;
using MultilayerPerceptron.Network;

namespace MultilayerPerceptron.Algorithm
{
    public class QuickProp
    {
        private AlgorithmConfig _config = null;
        private Random _random = null;
        private double alfaMax = 1.75;
        private double lambda = 0.0001;
        public event EventHandler<MessageContainer> RaiseMessageEvent;

        public QuickProp(AlgorithmConfig config)
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
            double[][][] previousWeights = null;
            double[][] previousOffsets = null;
            double[][][] previousCorrectionWeightSum = null;
            double[][] previousCorrectionOffSum = null;
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
                    double[][][] deltaWeights = new double[network.Layers.Length][][];
                    double[][] deltaOffsets = new double[network.Layers.Length][];
                    double[][][] correctionWeightsArray = new double[network.Layers.Length][][];
                    double[][] correctionOffsetArray = new double[network.Layers.Length][];

                    // Начинаем процесс для одного пакета данных. 
                    for (int inBatchIndex = currentIndex; inBatchIndex < currentIndex + _config.BatchSize && inBatchIndex < data.Count; inBatchIndex++)
                    {
                        // рассчитываем выход сети, прямой прогон
                        double[] realOutput = network.ComputeOutput(data[trainingID[inBatchIndex]]);
                        // обратный прогон. Распространение ошибки
                        int lastLayerLength = network.Layers.Length - 1; // номер последнего слоя
                        int lastLayerNeurons = network.Layers[lastLayerLength].Neurons.Length; // кол-во нейронов в последнем слое
                        correctionOffsetArray[lastLayerLength] = new double[lastLayerNeurons];
                        correctionWeightsArray[lastLayerLength] = new double[lastLayerNeurons][];
                        nablaWeights[lastLayerLength] = new double[lastLayerNeurons][]; // инициализируем по кол-ву нейронов в последнем слое
                        nablaOffsets[lastLayerLength] = new double[lastLayerNeurons];
                        deltaWeights[lastLayerLength] = new double[lastLayerNeurons][];
                        deltaOffsets[lastLayerLength] = new double[lastLayerNeurons];
                        for (int j = 0; j < lastLayerNeurons; j++) // Для всех нейронов последнего слоя
                        {
                            network.Layers[lastLayerLength].Neurons[j].Derivative =
                                _config.ErrorFunction.CalculateDerivaitve(data[inBatchIndex].result, realOutput, j)
                            * network.Layers[lastLayerLength].Neurons[j].ActivationFunction.ComputeFirstDerivative
                            (network.Layers[lastLayerLength].Neurons[j].LastInput);

                            double correctionOffSum = network.Layers[lastLayerLength].Neurons[j].Derivative + lambda * network.Layers[lastLayerLength].Neurons[j].Offset;
                            correctionOffsetArray[lastLayerLength][j] = correctionOffSum;
                            double betak = 0.0;
                            double alfaOffset = 0.0;
                            //рассчитаем показатель альфа для смещения
                            if (previousCorrectionOffSum == null)
                                betak = 0;
                            else
                                betak = correctionOffSum / (previousCorrectionOffSum[lastLayerLength][j] - correctionOffSum);
                            if (betak > alfaMax)
                            {
                                if (previousOffsets == null)
                                    alfaOffset = betak;
                                else
                                {
                                    if (correctionOffSum * previousOffsets[lastLayerLength][j] * betak < 0)
                                        alfaOffset = alfaMax;
                                    else
                                        alfaOffset = betak;
                                }
                            }
                            else
                                alfaOffset = betak;


                            if (previousOffsets == null)
                            {
                                //коррекция смещения
                                nablaOffsets[lastLayerLength][j] -= (_config.LearningRate) * correctionOffSum;
                            }
                            else
                            {//коррекция смещения
                                if (previousOffsets[lastLayerLength][j] == 0 || previousOffsets[lastLayerLength][j] * correctionOffSum > 0)
                                {
                                    nablaOffsets[lastLayerLength][j] = (-_config.LearningRate * correctionOffSum) + alfaOffset * previousOffsets[lastLayerLength][j];
                                }
                                else
                                {
                                    nablaOffsets[lastLayerLength][j] = -0 + alfaOffset * previousOffsets[lastLayerLength][j];
                                }
                            }

                            //коррекция остальных весов  
                            nablaWeights[lastLayerLength][j] = new double[network.Layers[lastLayerLength].Neurons[j].Weights.Length];
                            deltaWeights[lastLayerLength][j] = new double[network.Layers[lastLayerLength].Neurons[j].Weights.Length];
                            correctionWeightsArray[lastLayerLength][j] = new double[network.Layers[lastLayerLength].Neurons[j].Weights.Length];
                            for (int i = 0; i < network.Layers[lastLayerLength].Neurons[j].Weights.Length; i++)
                            {
                                double correctionWeightSum = (network.Layers[lastLayerLength].Neurons[j].Derivative + lambda * network.Layers[lastLayerLength].Neurons[j].Weights[i]);
                                correctionWeightsArray[lastLayerLength][j][i] = correctionWeightSum;
                                double betakw = 0.0;
                                double alfaWeight = 0.0;
                                //рассчитаем показатель альфа для смещения
                                if (previousCorrectionWeightSum == null)
                                    betakw = 0;
                                else
                                    betakw = correctionWeightSum / (previousCorrectionWeightSum[lastLayerLength][j][i] - correctionWeightSum);
                                if (betakw > alfaMax)
                                {
                                    if (previousWeights == null)
                                        alfaWeight = betakw;
                                    else
                                    {
                                        if (correctionWeightSum * previousWeights[lastLayerLength][j][i] * betakw < 0)
                                            alfaWeight = alfaMax;
                                        else
                                            alfaWeight = betakw;
                                    }

                                }
                                else
                                    alfaWeight = betakw;
                                if (previousWeights == null)
                                {
                                    deltaWeights[lastLayerLength][j][i] = -_config.LearningRate * correctionWeightSum;
                                    nablaWeights[lastLayerLength][j][i] =
                                    deltaWeights[lastLayerLength][j][i] * (network.Layers.Length > 1 ?
                                                                network.Layers[lastLayerLength - 1].Neurons[i].LastState :
                                                                data[inBatchIndex].input[i]);
                                }
                                else
                                {
                                    if (previousWeights[lastLayerLength][j][i] == 0 || previousWeights[lastLayerLength][j][i] * correctionWeightSum > 0)
                                    {
                                        deltaWeights[lastLayerLength][j][i] = (-_config.LearningRate * correctionWeightSum) + alfaWeight * previousWeights[lastLayerLength][j][i];
                                        nablaWeights[lastLayerLength][j][i] =
                                    deltaWeights[lastLayerLength][j][i] * (network.Layers.Length > 1 ?
                                                                network.Layers[lastLayerLength - 1].Neurons[i].LastState :
                                                                data[inBatchIndex].input[i]);
                                    }
                                    else
                                    {
                                        deltaWeights[lastLayerLength][j][i] = 0 + alfaWeight * previousWeights[lastLayerLength][j][i];
                                        nablaWeights[lastLayerLength][j][i] =
                                         deltaWeights[lastLayerLength][j][i] * (network.Layers.Length > 1 ?
                                                                network.Layers[lastLayerLength - 1].Neurons[i].LastState :
                                                                data[inBatchIndex].input[i]);
                                    }
                                }

                            }
                        }
                        //скрытые слои
                        for (int hiddenLayerIndex = lastLayerLength - 1; hiddenLayerIndex >= 0; hiddenLayerIndex--)
                        {
                            nablaWeights[hiddenLayerIndex] = new double[network.Layers[hiddenLayerIndex].Neurons.Length][];
                            nablaOffsets[hiddenLayerIndex] = new double[network.Layers[hiddenLayerIndex].Neurons.Length];
                            deltaOffsets[hiddenLayerIndex] = new double[network.Layers[hiddenLayerIndex].Neurons.Length];
                            correctionOffsetArray[hiddenLayerIndex] = new double[network.Layers[hiddenLayerIndex].Neurons.Length];
                            correctionWeightsArray[hiddenLayerIndex] = new double[network.Layers[hiddenLayerIndex].Neurons.Length][];
                            deltaWeights[hiddenLayerIndex] = new double[network.Layers[hiddenLayerIndex].Neurons.Length][];
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

                                //// коррекция смещения
                                double correctionOffSum = network.Layers[hiddenLayerIndex].Neurons[j].Derivative + lambda * network.Layers[hiddenLayerIndex].Neurons[j].Offset;
                                correctionOffsetArray[hiddenLayerIndex][j] = correctionOffSum;
                                double betak = 0.0;
                                double alfaOffset = 0.0;
                                //рассчитаем показатель альфа для смещения
                                if (previousCorrectionOffSum == null)
                                    betak = 0;
                                else
                                    betak = correctionOffSum / (previousCorrectionOffSum[hiddenLayerIndex][j] - correctionOffSum);
                                if (betak > alfaMax)
                                {
                                    if (previousOffsets == null)
                                        alfaOffset = betak;
                                    else
                                    {
                                        if (correctionOffSum * previousOffsets[hiddenLayerIndex][j] * betak < 0)
                                            alfaOffset = alfaMax;
                                        else
                                            alfaOffset = betak;
                                    }

                                }
                                else
                                    alfaOffset = betak;


                                if (previousOffsets == null)
                                {
                                    //коррекция смещения
                                    nablaOffsets[hiddenLayerIndex][j] -= (_config.LearningRate) * correctionOffSum;
                                }
                                else
                                {//коррекция смещения
                                    if (previousOffsets[hiddenLayerIndex][j] == 0 || previousOffsets[hiddenLayerIndex][j] * correctionOffSum > 0)
                                    {
                                        nablaOffsets[hiddenLayerIndex][j] = (-_config.LearningRate * correctionOffSum) + alfaOffset * previousOffsets[hiddenLayerIndex][j];
                                    }
                                    else
                                    {
                                        nablaOffsets[hiddenLayerIndex][j] = -0 + alfaOffset * previousOffsets[hiddenLayerIndex][j];
                                    }
                                }

                                // коррекция весов
                                nablaWeights[hiddenLayerIndex][j] = new double[network.Layers[hiddenLayerIndex].Neurons[j].Weights.Length];
                                deltaWeights[hiddenLayerIndex][j] = new double[network.Layers[hiddenLayerIndex].Neurons[j].Weights.Length];
                                correctionWeightsArray[hiddenLayerIndex][j] = new double[network.Layers[hiddenLayerIndex].Neurons[j].Weights.Length];
                                for (int i = 0; i < network.Layers[hiddenLayerIndex].Neurons[j].Weights.Length; i++)
                                {
                                    double correctionWeightSum = (network.Layers[hiddenLayerIndex].Neurons[j].Derivative + lambda * network.Layers[hiddenLayerIndex].Neurons[j].Weights[i]);
                                    correctionWeightsArray[hiddenLayerIndex][j][i] = correctionWeightSum;
                                    double betakw = 0.0;
                                    double alfaWeight = 0.0;
                                    //рассчитаем показатель альфа для смещения
                                    if (previousCorrectionWeightSum == null)
                                        betakw = 0;
                                    else
                                        betakw = correctionWeightSum / (previousCorrectionWeightSum[hiddenLayerIndex][j][i] - correctionWeightSum);
                                    if (betakw > alfaMax)
                                    {
                                        if (previousWeights == null)
                                            alfaWeight = betakw;
                                        else
                                        {
                                            if (correctionWeightSum * previousWeights[hiddenLayerIndex][j][i] * betakw < 0)
                                                alfaWeight = alfaMax;
                                            else
                                                alfaWeight = betakw;
                                        }

                                    }
                                    else
                                        alfaWeight = betakw;
                                    if (previousWeights == null)
                                    {
                                        deltaWeights[hiddenLayerIndex][j][i] = -_config.LearningRate * correctionWeightSum;
                                        nablaWeights[hiddenLayerIndex][j][i] =
                                        deltaWeights[hiddenLayerIndex][j][i] * (hiddenLayerIndex > 0 ?
                                                                    network.Layers[hiddenLayerIndex - 1].Neurons[i].LastState :
                                                                    data[inBatchIndex].input[i]);
                                    }
                                    else
                                    {
                                        if (previousWeights[hiddenLayerIndex][j][i] == 0 || previousWeights[hiddenLayerIndex][j][i] * correctionWeightSum > 0)
                                        {
                                            deltaWeights[hiddenLayerIndex][j][i] = (-_config.LearningRate * correctionWeightSum) + alfaWeight * previousWeights[hiddenLayerIndex][j][i];
                                            nablaWeights[hiddenLayerIndex][j][i] =
                                        deltaWeights[hiddenLayerIndex][j][i] * (hiddenLayerIndex > 0 ?
                                                                    network.Layers[hiddenLayerIndex - 1].Neurons[i].LastState :
                                                                    data[inBatchIndex].input[i]);
                                        }
                                        else
                                        {
                                            deltaWeights[hiddenLayerIndex][j][i] = 0 + alfaWeight * previousWeights[hiddenLayerIndex][j][i];
                                            nablaWeights[hiddenLayerIndex][j][i] =
                                             deltaWeights[hiddenLayerIndex][j][i] * (hiddenLayerIndex > 0 ?
                                                                    network.Layers[hiddenLayerIndex - 1].Neurons[i].LastState :
                                                                    data[inBatchIndex].input[i]);
                                        }
                                    }
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

                    previousWeights = deltaWeights;
                    previousOffsets = nablaOffsets;
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