using System.Collections.Generic;
using System;
using System.IO;

namespace TestConsoleApp
{
    //Trzeba pozmieniać nazwy (zrobione?)
    public class NeuralNet
    {
        Random rand = new Random();



        //fundamental 
        private int[] layers;//layers
        private float[][] neurons;//neurons
        private float[][] biases;//biasses
        private float[][][] weights;//weights
        private int[] activations;//layers

        //genetic
        public float fitness = 0;//fitness

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


        private void InitNeurons()//create empty storage array for the neurons in the network.
        {
            List<float[]> neuronsList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                neuronsList.Add(new float[layers[i]]);
            }
            neurons = neuronsList.ToArray();
        }

        private void InitBiases()//initializes random array for the biases being held within the network.
        {


            List<float[]> biasList = new List<float[]>();
            for (int i = 1; i < layers.Length; i++)
            {
                float[] bias = new float[layers[i]];
                for (int j = 0; j < layers[i]; j++)
                {
                    //bias[j] = UnityEngine.Random.Range(-0.5f, 0.5f);
                    bias[j] = (float)rand.NextDouble() - 0.5f;
                }
                biasList.Add(bias);
            }
            biases = biasList.ToArray();
        }

        private void InitWeights()//initializes random array for the weights being held in the network.
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

        public float[] FeedForward(float[] inputs)//feed forward, inputs >==> outputs.
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
        //Backpropagation implemtation down until mutation.
        public float activate(float value, int layer)//all activation functions
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
        public float activateDer(float value, int layer)//all activation function derivatives
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

        public float sigmoid(float x)//activation functions and their corrosponding derivatives
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

        public void BackPropagate(float[] inputs, float[] expected)//backpropogation;
        {
            float[] output = FeedForward(inputs);//runs feed forward to ensure neurons are populated correctly

            cost = 0;
            for (int i = 0; i < output.Length; i++) cost += (float)Math.Pow(output[i] - expected[i], 2);//calculated cost of network
            cost = cost / 2;//this value is not used in calculions, rather used to identify the performance of the network

            float[][] gamma;


            List<float[]> gammaList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                gammaList.Add(new float[layers[i]]);
            }
            gamma = gammaList.ToArray();//gamma initialization

            int layer = layers.Length - 2;
            for (int i = 0; i < output.Length; i++) gamma[layers.Length - 1][i] = (output[i] - expected[i]) * activateDer(output[i], layer);//Gamma calculation
            for (int i = 0; i < layers[layers.Length - 1]; i++)//calculates the w' and b' for the last layer in the network
            {
                biases[layers.Length - 2][i] -= gamma[layers.Length - 1][i] * learningRate;
                for (int j = 0; j < layers[layers.Length - 2]; j++)
                {

                    weights[layers.Length - 2][i][j] -= gamma[layers.Length - 1][i] * neurons[layers.Length - 2][j] * learningRate;//*learning 
                }
            }

            for (int i = layers.Length - 2; i > 0; i--)//runs on all hidden layers
            {
                layer = i - 1;
                for (int j = 0; j < layers[i]; j++)//outputs
                {
                    gamma[i][j] = 0;
                    for (int k = 0; k < gamma[i + 1].Length; k++)
                    {
                        gamma[i][j] = gamma[i + 1][k] * weights[i][k][j];
                    }
                    gamma[i][j] *= activateDer(neurons[i][j], layer);//calculate gamma
                }
                for (int j = 0; j < layers[i]; j++)//itterate over outputs of layer
                {
                    biases[i - 1][j] -= gamma[i][j] * learningRate;//modify biases of network
                    for (int k = 0; k < layers[i - 1]; k++)//itterate over inputs to layer
                    {
                        weights[i - 1][j][k] -= gamma[i][j] * neurons[i - 1][k] * learningRate;//modify weights of network
                    }
                }
            }
        }

        //Genetic implementations down onwards until save.

        //public void Mutate(int high, float val)//used as a simple mutation function for any genetic implementations.
        //{
        //    for (int i = 0; i < biases.Length; i++)
        //    {
        //        for (int j = 0; j < biases[i].Length; j++)
        //        {
        //            biases[i][j] = (rand.NextDouble()*(0 - high) + 0 <= 2) ? biases[i][j] += rand.NextDouble() * (-val - val) -val : biases[i][j];
        //        }
        //    }

        //    for (int i = 0; i < weights.Length; i++)
        //    {
        //        for (int j = 0; j < weights[i].Length; j++)
        //        {
        //            for (int k = 0; k < weights[i][j].Length; k++)
        //            {
        //                weights[i][j][k] = (UnityEngine.Random.Range(0f, high) <= 2) ? weights[i][j][k] += UnityEngine.Random.Range(-val, val) : weights[i][j][k];
        //            }
        //        }
        //    }
        //}

        public int CompareTo(NeuralNet other) //Comparing For Genetic implementations. Used for sorting based on the fitness of the network
        {
            if (other == null) return 1;

            if (fitness > other.fitness)
                return 1;
            else if (fitness < other.fitness)
                return -1;
            else
                return 0;
        }

        public NeuralNet copy(NeuralNet nn) //For creatinga deep copy, to ensure arrays are serialzed.
        {
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    nn.biases[i][j] = biases[i][j];
                }
            }
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        nn.weights[i][j][k] = weights[i][j][k];
                    }
                }
            }
            return nn;
        }

        //save and load functions
        public void Load(string path)//this loads the biases and weights from within a file into the neural network.
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
        public void Save(string path)//this is used for saving the biases and weights within the network to a file.
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


        public int[] ReadNetStructure()
        {

            return null;
        }



        public static void ReadToLearn()
        {
            int linesCount = 0;

            String netStructureLine, xsesLine, expectedString;
            expectedString = "";
            netStructureLine = "";
            try
            {
                using (StreamReader sr = new StreamReader(@"Data\data.txt"))
                {
                    //policzenie linii w tekście

                    linesCount = File.ReadAllLines(@"Data\data.txt").Length;

                    int ileWarstw = 0;
                    String useless = "";

                    for (int i = 0; i < linesCount; i++)
                    {
                        useless = sr.ReadLine();
                        if (useless.Equals("/") == false) { ileWarstw++; } else { break; }

                    }
                    sr.DiscardBufferedData();

                    StreamReader sr1 = new StreamReader(@"Data\data.txt");


                    int[][] netStructure = new int[ileWarstw][];

                    //XSES
                    float [][] learnXses = new float[linesCount - ileWarstw - 1][];

                    float[]expected = new float[linesCount - 1];



                    List<float> listLearnXses = new List<float>();
                    List<int> listNetStructure = new List<int>();

                    for (int i = 0; i < ileWarstw; i++)
                    {
                        netStructureLine = sr1.ReadLine();

                        netStructureLine.Trim();

                        netStructureLine += ' ';
                        for (int j = 0; j < netStructureLine.Length; j++)
                        {
                            if (netStructureLine[j] != ' ')
                            {
                                expectedString += netStructureLine[j];
                            }
                            else if (netStructureLine[j] == ' ')
                            {
                                if (expectedString == "/") { break; }
                                listNetStructure.Add(int.Parse(expectedString));
                                expectedString = "";
                            }
                        }
                        netStructure[i] = new int[listNetStructure.Count];


                        for (int l = 0; l < netStructure[i].Length; l++)
                        {
                            netStructure[i][l] = listNetStructure[l];
                        }
                        listNetStructure.Clear();
                    }
                    Console.WriteLine(linesCount - ileWarstw);

                    for (int i = 0; i < linesCount - ileWarstw - 1; i++)
                    {
                        xsesLine = sr1.ReadLine();
                        Console.WriteLine(xsesLine);
                        xsesLine.Trim();
                        xsesLine += ' ';
                        for (int j = 0; j < xsesLine.Length; j++)
                        {
                            if (xsesLine[j] != 'e' && xsesLine[j] != ' ')
                            {
                                expectedString += xsesLine[j];
                            }
                            else if (xsesLine[j] == 'e')
                            {
                                expected[i] = float.Parse(expectedString, System.Globalization.CultureInfo.InvariantCulture);
                                expectedString = "";
                            }
                            else if (xsesLine[j] == ' ')
                            {
                                if (expectedString != "/")
                                {
                                    listLearnXses.Add(float.Parse(expectedString, System.Globalization.CultureInfo.InvariantCulture));
                                }
                                expectedString = "";
                            }
                        }
                        learnXses[i] = new float[listLearnXses.Count];
                        Console.WriteLine(listLearnXses.Count);
                        for (int l = 0; l < listLearnXses.Count; l++)
                        {
                            learnXses[i][l] = listLearnXses[l];
                        }
                        listLearnXses.Clear();
                    }




                    for (int i = 0; i < learnXses.Length; i++)
                    {
                        for (int j = 0; j < learnXses[i].Length; j++)
                        {
                            Console.Write(learnXses[i][j] + ", ");
                        }
                        Console.WriteLine("");
                    }
                    Console.WriteLine("");
                    for (int i = 0; i < netStructure.Length; i++)
                    {
                        for (int j = 0; j < netStructure[i].Length; j++)
                        {
                            Console.Write(netStructure[i][j] + ", ");
                        }
                        Console.WriteLine("");
                    }


                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Nie można odczytać");
                Console.WriteLine(e.Message);
            }


        }
    }
}
