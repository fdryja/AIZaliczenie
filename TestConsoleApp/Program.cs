using System;
using TestConsoleApp;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] layers = new int[3] { 2, 3, 1 };
            int xsesCount = 4;

            float bias = 1;
            float eta = 0.2f;

            //PIERWSZY: WARSTWA; DRUGI: NEURON
            int[][] netStructure = new int[layers.Length][];

            //PIERWSZY: WARSTWA; DRUGI: NEURON; TRZECI: WAGA
            float[][][] weight = new float[layers.Length][][];

            //PIERWSZY: WARSTWA; DRUGI: NEURON Z POPRZEDNIEJ WARSTWY
            float[][] xses = new float[layers.Length + 1][];

            

            //Deklaracja tablic niestandardowych:
            for (int i = 0; i < layers.Length; i++)
            {
                netStructure[i] = new int[layers[i]];
                weight[i] = new float[layers[i]][];

                for (int j = 0; j < layers[i]; j++)
                {
                    if (i == 0)
                    {
                        weight[i][j] = new float[xsesCount];
                    }
                    else
                    {
                        weight[i][j] = new float[netStructure[i - 1].Length];
                    }
                }
            }
            for (int i = 0; i < xses.Length; i++)
            {
                if (i == 0)
                {
                    xses[i] = new float[xsesCount];
                }
                else
                {
                    xses[i] = new float[netStructure[i - 1].Length];
                }
            }


            //ZESTAWY UCZĄCE:
            //xses[0][0] = ;
            //xses[0][1] = ;
            //xses[0][2] = ;
            //xses[0][3] = ;
            //xses[0][4] = ;
            //xses[0][5] = ;


            float[][] learnXses = new float[3][];
            learnXses[0] = new float[] {16711680.0f, 255.0f, 52224.0f, 16776960.0f};
            learnXses[1] = new float[] { 65535.0f, 16737792.0f, 6684876.0f, 16777113.0f};
            learnXses[2] = new float[] { 16777215.0f, 204.0f, 65280.0f, 16777215.0f};

            float[] expected = new float[3] { 8418112.0f, 10066329.0f, 8437939.0f };

            float[] testXses = new float[4] {16711680.0f, 16776960.0f, 52224.0f, 255.0f};

            //Wypełnienie tablic niestandardowych:
            Random rnd = new Random();

            Console.WriteLine("Struktura sieci. (funkcje)");
            for (int i = 0; i < layers.Length; i++)
            {
                for (int j = 0; j < netStructure[i].Length; j++)
                {
                    netStructure[i][j] = 3;
                }
                Console.WriteLine();
            }

            //Console.WriteLine("Struktura sieci. (funkcje)");
            //for (int i = 0; i < layers.Length; i++)
            //{
            //    for (int j = 0; j < netStructure[i].Length; j++)
            //    {
            //        netStructure[i][j] = rnd.Next(1, 4);
            //        Console.Write(netStructure[i][j]+", ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("Weights:");
            for (int i = 0; i < layers.Length; i++)
            {
                for (int j = 0; j < weight[i].Length; j++)
                {
                    Console.WriteLine("Wagi neuronu " + j + " z warstwy " + i);
                    for (int k = 0; k < weight[i][j].Length; k++)
                    {
                        weight[i][j][k] = rnd.Next(1, 11);
                        Console.Write(weight[i][j][k] + ", ");
                    }
                    Console.WriteLine();
                }
            }
            //Console.WriteLine("Input (X):");
            //for (int i = 0; i < xses[0].Length; i++)
            //{
            //    xses[0][i] = rnd.Next(1,34);
            //    Console.Write(xses[0][i]+", ");
            //}
            //Console.WriteLine();

            string[] functionNames = new string[] {
                "Dyskretna unipolarna",
                "Dyskretna bipolarna",
                "Ciągła unipolarna",
                "Ciągła bipolarna"
            };

            NeuralNet net = new NeuralNet();
           

            //Pętla wykonująca uczenie się sieci neuronowej
            List<float> deltaToSumCurrent = new List<float>();
            List<float> deltaToSumNext = new List<float>();
            float weightedDeltaSum;

            for (int q = 0; q < learnXses.Length; q++)
            {
                Console.WriteLine(learnXses[q]);
                xses[0] = learnXses[q];
                Console.WriteLine(xses[0][0]);
                //Pętla wykonująca pracę sieci neuronowej:
                Console.WriteLine(xses.Length);
                for (int i = 0; i < netStructure.Length; i++)
                {
                    for (int j = 0; j < netStructure[i].Length; j++)
                    {
                        //Console.WriteLine(xses[i].Length + " / " + weight[i][j].Length);
                        Console.WriteLine(xses[i][0] +", "+ weight[i][j][0] + ", " + bias + ", " + functionNames[netStructure[i][j]]);
                        xses[i + 1][j] = net.Neurone(xses[i], weight[i][j], bias, netStructure[i][j]);
                        Console.WriteLine(xses[i + 1][j]);
                    }
                }

                weightedDeltaSum = expected[q] - xses[xses.Length - 1][0];
                Console.WriteLine("Oczekiwana dla zestawu: " + expected[q] + "Wynik sieci: " + xses[xses.Length - 1][0] + "Różnica: " + weightedDeltaSum);
                Console.WriteLine();
                for (int i = weight.Length; i == 0; i--)
                {

                    for (int j = 0; j < weight[i].Length; j++)
                    {
                        deltaToSumNext.Add(net.Delta(xses[i + 1], weight[i + 1][j], bias, netStructure[i][j], weightedDeltaSum));

                        for (int l = 0; l <= deltaToSumCurrent.Count; l++)
                        {
                            weightedDeltaSum += deltaToSumCurrent[l] * weight[i + 1][l][j];
                        }

                        Console.WriteLine("Nowe wagi neuronu " + j + " warstwy " + i);

                        for (int k = 0; k < weight[i][j].Length; k++)
                        {
                            weight[i][j][k] += net.DeltaWeight(eta, deltaToSumCurrent[deltaToSumCurrent.Count - 1], xses[i][j]);

                            Console.Write(weight[i][j][k] + ", ");

                        }
                    }

                    deltaToSumCurrent = deltaToSumNext;
                    deltaToSumNext.Clear();
                }

               
            }

            xses[0] = testXses;

            //Pętla wwykonująca pracę sieci neuronowej:
            Console.WriteLine(xses.Length);
            for (int i = 0; i < netStructure.Length; i++)
            {
                for (int j = 0; j < netStructure[i].Length; j++)
                {
                    //Console.WriteLine(xses[i].Length + " / " + weight[i][j].Length);
                    xses[i + 1][j] = net.Neurone(xses[i], weight[i][j], bias, netStructure[i][j]);
                }
            }

            //Wypisanie wyniku:
            Console.WriteLine();
            Console.WriteLine("Wynik testu:");
            Console.WriteLine(xses[xses.Length - 1][0]);
            Console.ReadKey();

        }
    }
}