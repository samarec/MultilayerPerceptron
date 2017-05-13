using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MultilayerPerceptron.Network;
using MultilayerPerceptron.Views;
using MultilayerPerceptron.Functions;
using MultilayerPerceptron.Algorithm;
using System.Globalization;

namespace MultilayerPerceptron
{
    public partial class MainForm : Form
    {
        private int errors = 0;
        private int[] neuronsCount = null;
        private double beta;
        private NeuralNetwork myNet = null;
        AlgorithmConfig configuration = null;

        public MainForm()
        {
            InitializeComponent();
            algorithmBox.SelectedIndex = 0;
        }

        private void learnButton_Click(object sender, EventArgs e)
        {
            outputBox.AppendText("Learning process is started!\n");
            this.Refresh();

            Vector[] learnVectors = GetInputVectors("learn-data.txt");
            if (learnVectors != null)
            {
                int classesCount = defineExpValue(learnVectors);
                double[] minX = (double[])learnVectors[0].input.Clone();
                double[] maxX = (double[])learnVectors[0].input.Clone();
                defMinMax(ref minX, ref maxX, learnVectors);
                NormalizeVectors(ref learnVectors, minX, maxX);
                int cyclesCount = (int)learningCycles.Value;
                double rateCount = (double)learningSpeed.Value;
                double minError = (double)minimalError.Value;
                MinimizationFunction minFunc = new MinimizationFunction();
                configuration = new AlgorithmConfig
                {
                    BatchSize = 1,
                    ErrorFunction = minFunc,
                    LearningRate = rateCount,
                    MaxCycles = cyclesCount,
                    MinError = minError
                };
                testWithParametr(configuration, learnVectors, classesCount);
            }
        }

        private void testWithParametr(AlgorithmConfig configuration, Vector[] learnVectors, int classes)
        {
            neuronsCount = null;
            int layersCount = (int)hiddenLayers.Value + 1;
            switch (layersCount)
            {
                case 2:
                    {
                        neuronsCount = new int[1];
                        neuronsCount[0] = (int)firstLayerNeurons.Value;
                    } break;
                case 3:
                    {
                        neuronsCount = new int[2];
                        neuronsCount[0] = (int)firstLayerNeurons.Value;
                        neuronsCount[1] = (int)secondtLayerNeurons.Value;
                    } break;
                case 4:
                    {
                        neuronsCount = new int[3];
                        neuronsCount[0] = (int)firstLayerNeurons.Value;
                        neuronsCount[1] = (int)secondtLayerNeurons.Value;
                        neuronsCount[2] = (int)thirdtLayerNeurons.Value;
                    } break;
            }

            beta = 0.7;
            myNet = new NeuralNetwork(layersCount, neuronsCount, learnVectors[0].input.Length, classes, beta);
            List<Vector> data = new List<Vector>(learnVectors.Length);
            data.AddRange(learnVectors);
            switch (algorithmBox.SelectedIndex)
            {
                case 0:
                    {
                        BackPropagation method = new BackPropagation(configuration);
                        method.RaiseMessageEvent += AddMessage;
                        method.Train(myNet, data);
                        break;
                    }
                case 1:
                    {
                        QuickProp method = new QuickProp(configuration);
                        method.RaiseMessageEvent += AddMessage;
                        method.Train(myNet, data);
                        break;
                    }
            }
            
        }

        private Vector[] GetInputVectors(string path)
        {
            Vector[] inputVectors = null;
            try
            {
                string[] lines = File.ReadAllLines(path);
                inputVectors = new Vector[lines.Length];
                for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
                {
                    double[] values = lines[lineIndex].Split(',').Select(item => Double.Parse(item, NumberFormatInfo.InvariantInfo)).ToArray();
                    int valuesLength = values.Length;

                    double[] x = new double[valuesLength - 1];
                    for (int index = 0; index < x.Length; index++)
                    {
                        x[index] = values[index];
                    }

                    double[] y = new double[1];
                    y[0] = values[valuesLength - 1];
                    inputVectors[lineIndex] = new Vector(x, y);
                }
                return inputVectors;
            }
            catch (FileNotFoundException fe)
            {

                MessageBox.Show("File Not Found", "Error file reading!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return inputVectors;
            }

        }

        private int defineExpValue(Vector[] checkVectors)
        {
            List<double> classlist = checkVectors.Select(v => v.result[0]).Distinct().OrderBy(y => y).ToList();
            foreach (Vector c in checkVectors)
            {
                int i = 0;
                foreach (double val in classlist)
                {
                    if (c.result[0] == val)
                    {
                        c.result = new double[classlist.Count];
                        int index = classlist.IndexOf(val);
                        c.result[index] = 1;
                    }
                    i++;
                }

            }
            return classlist.Count;

        }

        private void defMinMax(ref double[] minX, ref double[] maxX, Vector[] checkVectors)
        {
            int length = checkVectors.Length;
            for (int vectorIndex = 1; vectorIndex < length; vectorIndex++)
            {
                for (int xIndex = 0; xIndex < minX.Length; xIndex++)
                {
                    if (minX[xIndex] > checkVectors[vectorIndex].input[xIndex])
                        minX[xIndex] = checkVectors[vectorIndex].input[xIndex];

                    if (maxX[xIndex] < checkVectors[vectorIndex].input[xIndex])
                        maxX[xIndex] = checkVectors[vectorIndex].input[xIndex];
                }
            }

        }

        private void NormalizeVectors(ref Vector[] inputVectors, double[] minX, double[] maxX)
        {
            for (int inputVectorIndex = 0; inputVectorIndex < inputVectors.Length; inputVectorIndex++)
            {
                for (int xIndex = 0; xIndex < minX.Length; xIndex++)
                {
                    inputVectors[inputVectorIndex].input[xIndex] =
                         (inputVectors[inputVectorIndex].input[xIndex] - minX[xIndex]) / (maxX[xIndex] - minX[xIndex]);
                }
            }
        }

        public void AddMessage(object sender, MessageContainer e)
        {
            outputBox.AppendText(e.Message);
        }

        private void outputBox_TextChanged(object sender, EventArgs e)
        {
            outputBox.SelectionStart = outputBox.Text.Length;
            outputBox.ScrollToCaret();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            Vector[] checkVectors = GetInputVectors("test-data.txt");
            defineExpValue(checkVectors);

            double[] minX = (double[])checkVectors[0].input.Clone();
            double[] maxX = (double[])checkVectors[0].input.Clone();
            defMinMax(ref minX, ref maxX, checkVectors);

            NormalizeVectors(ref checkVectors, minX, maxX);
            List<Vector> testData = new List<Vector>(checkVectors.Length);

            testData.AddRange(checkVectors);
            errors = 0;
            double currentError = 0.0;
            for (int i = 0; i < testData.Count; i++)
            {
                outputBox.AppendText("Vector: " + i + " - ");
                double[] result = myNet.ComputeOutput(testData[i]);
                int classNum = identifyClass(result);
                currentError += configuration.ErrorFunction.CalculateRMS(testData[i].result, result);
                if (testData[i].result[classNum - 1] != 1)
                {
                    outputBox.AppendText("Error on " + i + " vector.\n");
                    errors++;

                    for (int k = 0; k < testData[i].result.Length; k++)
                    {
                        if (testData[i].result[k] == 1)
                            outputBox.AppendText("Etalon: " + (k + 1) + " - ");
                    }
                    outputBox.AppendText("Real: " + classNum + "\n");
                }
                outputBox.AppendText("Class: " + classNum + "\n");
            }
            currentError *= 1d / (testData.Count * 3 - 1);
            outputBox.AppendText(errors + " errors\n" + "RMS: " + currentError + ";\n");
            outputBox.AppendText("Errors percent " + Math.Round(errors / (Convert.ToDouble(testData.Count) / 100), 2) + ";\n");
        }

        private int identifyClass(double[] vector)
        {
            int index = 0;
            double max = vector[0];
            for (int i = 1; i < vector.Length; i++)
            {
                if (vector[i] > max)
                {
                    index = i;
                    max = vector[i];
                }
            }
            return ++index;
        }

        private void hiddenLayers_ValueChanged(object sender, EventArgs e)
        {
            switch ((int)(hiddenLayers.Value))
            {
                case 1:
                    secondtLayerNeuronsText.Visible = false;
                    secondtLayerNeurons.Visible = false;
                    thirdtLayerNeuronsText.Visible = false;
                    thirdtLayerNeurons.Visible = false;
                    break;
                case 2:
                    secondtLayerNeuronsText.Visible = true;
                    secondtLayerNeurons.Visible = true;
                    thirdtLayerNeuronsText.Visible = false;
                    thirdtLayerNeurons.Visible = false;
                    break;
                case 3:
                    secondtLayerNeuronsText.Visible = true;
                    secondtLayerNeurons.Visible = true;
                    thirdtLayerNeuronsText.Visible = true;
                    thirdtLayerNeurons.Visible = true;
                    break;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            outputBox.Clear();
        }

        private void learningDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"learn-data.txt");
        }

        private void testingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"test-data.txt");
        }
    }
}
