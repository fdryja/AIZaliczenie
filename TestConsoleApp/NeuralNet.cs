using System.Collections.Generic;
using System;
using System.IO;

namespace TestConsoleApp
{
    //Trzeba pozmieniać nazwy (zrobione?)
    public class NeuralNet
    {
        Random rand = new Random();



        //podstawowa struktura 
        private int[] layers;//warstwy
        private float[][] neurons;//neurony
        private float[][] biases;//biasy
        private float[][][] weights;//wagi
        private int[] activations;//funkcje aktywacji


        //backprop
        public float learningRate = 0.01f;//learning rate
        public float cost = 0;

        private float[][] deltaBiases;//biasses
        private float[][][] deltaWeights;//weights
        private int deltaCount;

        public NeuralNet(int[] layers, string[] layerActivations)
        {
            this.layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                this.layers[i] = layers[i];
            }
            activations = new int[layers.Length - 1];
            for (int i = 0; i < layers.Length - 1; i++)
            {
                string action = layerActivations[i];
                switch (action)
                {
                    case "sigmoid":
                        activations[i] = 0;
                        break;
                    case "tanh":
                        activations[i] = 1;
                        break;
                    case "relu":
                        activations[i] = 2;
                        break;
                    case "leakyrelu":
                        activations[i] = 3;
                        break;
                    default:
                        activations[i] = 2;
                        break;
                }
            }
            InitNeurons();
            InitBiases();
            InitWeights();
        }


        private void InitNeurons()//tworzymy puste miejsce na neurony w naszej sieci
        {
            List<float[]> neuronsList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                neuronsList.Add(new float[layers[i]]);
            }
            neurons = neuronsList.ToArray();
        }

        private void InitBiases()//stworzenie losowych biasów
        {


            List<float[]> biasList = new List<float[]>();
            for (int i = 1; i < layers.Length; i++)
            {
                float[] bias = new float[layers[i]];
                for (int j = 0; j < layers[i]; j++)
                {
                    bias[j] = (float)rand.NextDouble() - 0.5f;
                }
                biasList.Add(bias);
            }
            biases = biasList.ToArray();
        }

        private void InitWeights()//stworzenie losowych wag
        {
            List<float[][]> weightsList = new List<float[][]>();
            for (int i = 1; i < layers.Length; i++)
            {
                List<float[]> layerWeightsList = new List<float[]>();
                int neuronsInPreviousLayer = layers[i - 1];
                for (int j = 0; j < layers[i]; j++)
                {
                    float[] neuronWeights = new float[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                    {
                        neuronWeights[k] = (float)rand.NextDouble() - 0.5f;
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            weights = weightsList.ToArray();
        }

        public float[] FeedForward(float[] inputs)//jednokierunkowa
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }
            for (int i = 1; i < layers.Length; i++)
            {
                int layer = i - 1;
                for (int j = 0; j < layers[i]; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < layers[i - 1]; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    neurons[i][j] = activate(value + biases[i - 1][j], layer);
                }
            }
            return neurons[layers.Length - 1];
        }
        public float activate(float value, int layer)//funkcje aktywacji
        {
            switch (activations[layer])
            {
                case 0:
                    return sigmoid(value);
                case 1:
                    return tanh(value);
                case 2:
                    return relu(value);
                case 3:
                    return leakyrelu(value);
                default:
                    return relu(value);
            }
        }
        public float activateDer(float value, int layer)//pochodne funkcji aktywacji
        {
            switch (activations[layer])
            {
                case 0:
                    return sigmoidDer(value);
                case 1:
                    return tanhDer(value);
                case 2:
                    return reluDer(value);
                case 3:
                    return leakyreluDer(value);
                default:
                    return reluDer(value);
            }
        }

        public float sigmoid(float x)//funkcje aktywacji i ich pochodne
        {
            double cuni = 1 / (1 + Math.Pow(Math.E, -x));
            return (float)cuni;
        }
        public float tanh(float x)
        {
            double cbi = (Math.Pow(Math.E, x) - Math.Pow(Math.E, -x)) /
                        (Math.Pow(Math.E, x) + Math.Pow(Math.E, -x));
            return (float)cbi;
        }
        public float relu(float x)
        {
            return (0 >= x) ? 0 : x;
        }
        public float leakyrelu(float x)
        {
            return (0 >= x) ? 0.01f * x : x;
        }
        public float sigmoidDer(float x)
        {
            float derivative;
            double cuni = 1 / (1 + Math.Pow(Math.E, x));
            return derivative = (float)cuni * (1 - (float)cuni);
        }
        public float tanhDer(float x)
        {
            float derivative = (float)(1 - Math.Pow((Math.Pow(Math.E, x) - Math.Pow(Math.E, -x)) /
                        (Math.Pow(Math.E, x) + Math.Pow(Math.E, -x)), 2));
            return derivative;
        }
        public float reluDer(float x)
        {
            return (0 >= x) ? 0 : 1;
        }
        public float leakyreluDer(float x)
        {
            return (0 >= x) ? 0.01f : 1;
        }

        public void BackPropagate(float[] inputs, float[] expected)//propagacja wsteczna;
        {
            float[] output = FeedForward(inputs);//odpalenie jednokierunkowej sieci

            cost = 0;
            for (int i = 0; i < output.Length; i++) cost += (float)Math.Pow(output[i] - expected[i], 2);//obliczenie kosztu sieci
            cost = cost / 2;//ustalenie jak dobra jest nasza sieć

            float[][] gamma;


            List<float[]> gammaList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                gammaList.Add(new float[layers[i]]);
            }
            gamma = gammaList.ToArray();//tworzenie gamma

            int layer = layers.Length - 2;
            for (int i = 0; i < output.Length; i++) gamma[layers.Length - 1][i] = (output[i] - expected[i]) * activateDer(output[i], layer);//obliczanie gamma
            for (int i = 0; i < layers[layers.Length - 1]; i++)//obliczanie w' i b' dla ostatniej warstwy sieci
            {
                biases[layers.Length - 2][i] -= gamma[layers.Length - 1][i] * learningRate;
                for (int j = 0; j < layers[layers.Length - 2]; j++)
                {

                    weights[layers.Length - 2][i][j] -= gamma[layers.Length - 1][i] * neurons[layers.Length - 2][j] * learningRate;//uczenie 
                }
            }

            for (int i = layers.Length - 2; i > 0; i--)//działa na wszystkich warstwach ukrytych
            {
                layer = i - 1;
                for (int j = 0; j < layers[i]; j++)//wyjścia
                {
                    gamma[i][j] = 0;
                    for (int k = 0; k < gamma[i + 1].Length; k++)
                    {
                        gamma[i][j] = gamma[i + 1][k] * weights[i][k][j];
                    }
                    gamma[i][j] *= activateDer(neurons[i][j], layer);//obliczanie gamma
                }
                for (int j = 0; j < layers[i]; j++)//iteracja przez wyjścia warstwy
                {
                    biases[i - 1][j] -= gamma[i][j] * learningRate;//zmiana biasów
                    for (int k = 0; k < layers[i - 1]; k++)//iteracja przez wejścia do warstwy
                    {
                        weights[i - 1][j][k] -= gamma[i][j] * neurons[i - 1][k] * learningRate;//zmiana wag
                    }
                }
            }
        }



        //public NeuralNet copy(NeuralNet nn) //For creatinga deep copy, to ensure arrays are serialzed.
        //{
        //    for (int i = 0; i < biases.Length; i++)
        //    {
        //        for (int j = 0; j < biases[i].Length; j++)
        //        {
        //            nn.biases[i][j] = biases[i][j];
        //        }
        //    }
        //    for (int i = 0; i < weights.Length; i++)
        //    {
        //        for (int j = 0; j < weights[i].Length; j++)
        //        {
        //            for (int k = 0; k < weights[i][j].Length; k++)
        //            {
        //                nn.weights[i][j][k] = weights[i][j][k];
        //            }
        //        }
        //    }
        //    return nn;
        //}

        //zapis i odczyt wag+biasy
        public void Load(string path)//ładowanie wag i biasów z pliku tekstowego
        {
            TextReader tr = new StreamReader(path);
            int NumberOfLines = (int)new FileInfo(path).Length;
            string[] ListLines = new string[NumberOfLines];
            int index = 1;
            for (int i = 1; i < NumberOfLines; i++)
            {
                ListLines[i] = tr.ReadLine();
            }
            tr.Close();
            if (new FileInfo(path).Length > 0)
            {
                for (int i = 0; i < biases.Length; i++)
                {
                    for (int j = 0; j < biases[i].Length; j++)
                    {
                        biases[i][j] = float.Parse(ListLines[index]);
                        index++;
                    }
                }

                for (int i = 0; i < weights.Length; i++)
                {
                    for (int j = 0; j < weights[i].Length; j++)
                    {
                        for (int k = 0; k < weights[i][j].Length; k++)
                        {
                            weights[i][j][k] = float.Parse(ListLines[index]); ;
                            index++;
                        }
                    }
                }
            }
        }
        public void Save(string path)//zapisywanie wag i biasów do pliku tekstowego
        {
            File.Create(path).Close();
            StreamWriter writer = new StreamWriter(path, true);

            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    writer.WriteLine(biases[i][j]);
                }
            }

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        writer.WriteLine(weights[i][j][k]);
                    }
                }
            }
            writer.Close();
        }

        public static bool error = false;
        public static bool expectedBool = false;
        public static int[] layersRead;
        public static string[] activationFunctions;
        public static float[] inputs;
        public static float[] expected;
        public static int linesCount;

        public static void Read()//odczyt warstw, neuronów, funkcji aktywacji wejść i (jeżeli istnieją) wyjść z pliku
        {
            StreamReader sr = new StreamReader(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\data.txt");
            linesCount = File.ReadAllLines(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\data.txt").Length;
            string layersString = sr.ReadLine().Trim();
            layersString += ' ';
            string activateString = sr.ReadLine().Trim();
            activateString += ' ';
            string inputsString = sr.ReadLine().Trim();
            inputsString += ' ';
            string expectedString = sr.ReadLine();
            if (expectedString != null) expectedString.Trim();
            expectedString += ' ';

            if (expectedString[0] == ' ')
            {
                List<int> layersList = new List<int>();
                List<string> activateList = new List<string>();
                List<float> inputsList = new List<float>();

                string layersValue = "";
                string activateValue = "";
                string inputsValue = "";

                for (int i = 0; i < layersString.Length; i++)
                {

                    if (layersString[i] != ' ')
                    {
                        layersValue += layersString[i];
                    }
                    else
                    {
                        int layersElement = Int32.Parse(layersValue);
                        layersList.Add(layersElement);
                        layersValue = "";

                    }
                }

                for (int i = 0; i < activateString.Length; i++)
                {
                    if (activateString[i] != ' ')
                    {
                        activateValue += activateString[i];
                    }
                    else
                    {
                        switch (activateValue)
                        {
                            case "1":
                                activateValue = "sigmoid";
                                break;
                            case "2":
                                activateValue = "tanh";
                                break;
                            case "3":
                                activateValue = "relu";
                                break;
                            case "4":
                                activateValue = "leakyrelu";
                                break;
                        }
                        activateList.Add(activateValue);
                        activateValue = "";
                    }
                }

                for (int i = 0; i < inputsString.Length; i++)
                {
                    if (inputsString[i] != ' ')
                    {
                        inputsValue += inputsString[i];
                    }
                    else
                    {
                        float inputsElement = float.Parse(inputsValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                        inputsList.Add(inputsElement);
                        inputsValue = "";

                    }
                }

                if ((layersList.Count + 1) != activateList.Count)
                {
                    error = true;
                    Console.WriteLine("Liczba warstw i funkcji aktywacyji jest różna\nLiczba warstw: " + (layersList.Count + 1) + "\nLiczba funkcji aktywacji: " + activateList.Count);
                    goto Exit;
                }

                layersRead = new int[layersList.Count + 1];
                activationFunctions = new string[activateList.Count];
                inputs = new float[inputsList.Count];

                for (int i = 1; i < layersList.Count + 1; i++)
                {
                    layersRead[i] = layersList[i - 1];
                }
                for (int i = 0; i < activateList.Count; i++)
                {
                    activationFunctions[i] = activateList[i];
                }
                for (int i = 0; i < inputsList.Count; i++)
                {
                    inputs[i] = inputsList[i];
                }
                layersRead[0] = inputs.Length;
            }
            else if (expectedString[0] != ' ')
            {
                expectedBool = true;
                List<int> layersList = new List<int>();
                List<string> activateList = new List<string>();
                List<float> inputsList = new List<float>();
                List<float> expectedList = new List<float>();

                string layersValue = "";
                string activateValue = "";
                string inputsValue = "";
                string expectedValue = "";

                for (int i = 0; i < layersString.Length; i++)
                {

                    if (layersString[i] != ' ')
                    {
                        layersValue += layersString[i];
                    }
                    else
                    {
                        int layersElement = Int32.Parse(layersValue);
                        layersList.Add(layersElement);
                        layersValue = "";

                    }
                }

                for (int i = 0; i < activateString.Length; i++)
                {
                    if (activateString[i] != ' ')
                    {
                        activateValue += activateString[i];
                    }
                    else
                    {
                        switch (activateValue)
                        {
                            case "1":
                                activateValue = "sigmoid";
                                break;
                            case "2":
                                activateValue = "tanh";
                                break;
                            case "3":
                                activateValue = "relu";
                                break;
                            case "4":
                                activateValue = "leakyrelu";
                                break;
                        }
                        activateList.Add(activateValue);
                        activateValue = "";
                    }
                }

                for (int i = 0; i < inputsString.Length; i++)
                {
                    if (inputsString[i] != ' ')
                    {
                        inputsValue += inputsString[i];
                    }
                    else
                    {
                        float inputsElement = float.Parse(inputsValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                        inputsList.Add(inputsElement);
                        inputsValue = "";

                    }
                }

                for (int i = 0; i < expectedString.Length; i++)
                {
                    if (expectedString[i] != ' ')
                    {
                        expectedValue += expectedString[i];
                    }
                    else
                    {
                        float expectedElement = float.Parse(expectedValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                        expectedList.Add(expectedElement);
                        expectedValue = "";

                    }
                }

                if ((layersList.Count + 2) != activateList.Count)
                {
                    error = true;
                    Console.WriteLine("Liczba warstw i funkcji aktywacyji jest różna\nLiczba warstw: " + (layersList.Count + 2) + "\nLiczba funkcji aktywacji: " + activateList.Count);
                    goto Exit;
                }

                layersRead = new int[layersList.Count + 2];
                activationFunctions = new string[activateList.Count];
                inputs = new float[inputsList.Count];
                expected = new float[expectedList.Count];

                for (int i = 1; i < layersList.Count + 1; i++)
                {
                    layersRead[i] = layersList[i - 1];
                }
                for (int i = 0; i < activateList.Count; i++)
                {
                    activationFunctions[i] = activateList[i];
                }
                for (int i = 0; i < inputsList.Count; i++)
                {
                    inputs[i] = inputsList[i];
                }
                for (int i = 0; i < expectedList.Count; i++)
                {
                    expected[i] = expectedList[i];
                }
                layersRead[0] = inputs.Length;
                layersRead[layersRead.Length - 1] = expected.Length;
            }
            else
            {
                Console.WriteLine("Błąd w pliku.\nPowinny być 3 lub 4 linijki a znajduje się " + linesCount + " linijek");
            }
        Exit:;
        }

        public static void Write()//zapis warstw, neuronów, funkcji aktywacji wejść i (jeżeli istnieją) wyjść do pliku
        {

        }

    }

}