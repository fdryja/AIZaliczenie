﻿using System;
using TestConsoleApp;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] layers = new int[4] { 6, 3, 2, 1 };
            int xsesCount = 3;

            float bias = 1;
            float eta = 0.2f;

            //PIERWSZY: WARSTWA; DRUGI: NEURON
            int[][] netStructure = new int[layers.Length][];

            //PIERWSZY: WARSTWA; DRUGI: NEURON; TRZECI: WAGA
            float[][][] weight = new float[layers.Length][][];

            //PIERWSZY: WARSTWA; DRUGI: NEURON Z POPRZEDNIEJ WARSTWY
            float[][] xses = new float[layers.Length + 1][];

            List<float> deltaToSumCurrent = new List<float>();
            List<float> deltaToSumNext = new List<float>();
            float weightedDeltaSum;

            float expected = 0;

            //Deklaracja tablic niestandardowych:
            for (int i = 0; i < layers.Length; i++)
            {
                netStructure[i] = new int[layers[i]];
                weight[i] = new float[layers[i]][];

                for (int j = 0; j <layers[i] ; j++)
                {
                    if (i==0)
                    {
                        weight[i][j] = new float[xsesCount];
                    }
                    else
                    {
                        weight[i][j] = new float[netStructure[i-1].Length];
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
            //Wypełnienie tablic niestandardowych:
            Random rnd = new Random();
            Console.WriteLine("Struktura sieci. (funkcje)");
            for (int i = 0; i < layers.Length; i++)
            {
                for (int j = 0; j < netStructure[i].Length; j++)
                {
                    netStructure[i][j] = rnd.Next(1, 4);
                    Console.Write(netStructure[i][j]+", ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Weights:");
            for (int i = 0; i < layers.Length; i++)
            {
                for (int j = 0; j < weight[i].Length; j++)
                {
                    Console.WriteLine("Wagi neuronu "+ j + " z warstwy " + i);
                    for (int k = 0; k < weight[i][j].Length; k++)
                    {
                        weight[i][j][k] = rnd.Next(1, 11);
                        Console.Write(weight[i][j][k] + ", ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Input (X):");
            for (int i = 0; i < xses[0].Length; i++)
            {
                xses[0][i] = rnd.Next(1,34);
                Console.Write(xses[0][i]+", ");
            }
            Console.WriteLine();

            string[] functionNames = new string[] {
                "Dyskretna unipolarna",
                "Dyskretna bipolarna",
                "Ciągła unipolarna",
                "Ciągła bipolarna"
            };

            NeuralNet net = new NeuralNet();
            Beginning:
            Console.WriteLine("1 - wykonuj\n2 - ucz");
            int wybor;
            wybor = Convert.ToInt32(Console.ReadLine());
            switch (wybor)
            {
                case 1:
                    //pobranie X od użytkownika, xses[0]

                    //Pętla wykonująca pracę sieci neuronowej:
                    Console.WriteLine(xses.Length);
                    for (int i = 0; i < netStructure.Length; i++)
                    {
                        for (int j = 0; j < netStructure[i].Length; j++)
                        {
                            Console.WriteLine(xses[i].Length + " / " + weight[i][j].Length);
                            xses[i + 1][j] = net.Neurone(xses[i], weight[i][j], bias, functionNames[netStructure[i][j]]);
                        }
                    }
                    //Wypisanie wyniku:
                    Console.WriteLine(xses[xses.Length - 1][0]);
                    Console.ReadKey();
                    break;
                case 2:
                    //pobranie listy xses[0] i expected -> Zbiór uczący

                    for (int q = 0; q < ZbiorUczacy.Length; q++)
                    {

                        //Pętla wykonująca pracę sieci neuronowej:
                        Console.WriteLine(xses.Length);
                        for (int i = 0; i < netStructure.Length; i++)
                        {
                            for (int j = 0; j < netStructure[i].Length; j++)
                            {
                                Console.WriteLine(xses[i].Length + " / " + weight[i][j].Length);
                                xses[i + 1][j] = net.Neurone(xses[i], weight[i][j], bias, functionNames[netStructure[i][j]]);
                            }
                        }
                        //Wypisanie wyniku:
                        Console.WriteLine(xses[xses.Length - 1][0]);

                        //Pętla wykonująca uczenie się sieci neuronowej:
                        weightedDeltaSum = expected -xses[xses.Length - 1][0];
                        for (int i = weight.Length; i == 0; i--)
                        {

                            for (int j = 0; j < weight[i].Length; j++)
                            {
                                deltaToSumNext.Add(net.Delta(xses[i + 1], weight[i + 1][j], bias, netStructure[i][j], weightedDeltaSum));

                                for (int l = 0; l <= deltaToSumCurrent.Count; l++)
                                {
                                    weightedDeltaSum += deltaToSumCurrent[l] * weight[i + 1][l][j];
                                }

                                for (int k = 0; k < weight[i][j].Length; k++)
                                {
                                    weight[i][j][k] += net.DeltaWeight(eta, deltaToSumCurrent[deltaToSumCurrent.Count - 1], xses[i][j]);
                                }
                            }

                            deltaToSumCurrent = deltaToSumNext;
                            deltaToSumNext.Clear();
                        }

                    }
                    break;
                default:
                    goto Beginning;
                    break;
            }

            



            
        }
    }
}