using System;
using TestConsoleApp;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<List<int>> netStructure = new List<List<int>>();
            //List<List<float>> xses = new List<List<float>>();
            //List<List<float>> weight = new List<List<float>>();

            int[] layers = new int[4] { 6, 3, 2, 1 };

            int xsesCount = 3;
            int bias = 1;

            //PIERWSZY: WARSTWA; DRUGI: NEURON
            int[][] netStructure = new int[layers.Length][];

            //PIERWSZY: WARSTWA; DRUGI: NEURON; TRZECI: WAGA
            float[][][] weight = new float[layers.Length][][];

            //PIERWSZY: WARSTWA; DRUGI: NEURON Z POPRZEDNIEJ WARSTWY
            float[][] xses = new float[layers.Length][];

            for (int i = 0; i < layers.Length; i++)
            {
                //int currentLayerNeuroneCount;
                //Console.WriteLine("Podaj liczbę neuronów w warstwie " + (i + 1));
                //currentLayerNeuroneCount = Convert.ToInt32(Console.Read());
                netStructure[i] = new int[layers[i]];
                weight[i] = new float[layers[i]][];

                if (i == 0)
                {
                    xses[i] = new float[xsesCount];
                }
                else
                {
                    xses[i] = new float[netStructure[i - 1].Length];
                }
                //layers[i] or layers[j]? V V V
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

            foreach (var item in netStructure)
            {
                Console.WriteLine(item);
            }

            string[] functionNames = new string[] {
                "Dyskretna unipolarna",
                "Dyskretna bipolarna",
                "Ciągła unipolarna",
                "Ciągła bipolarna"
            };

            Neurone neurone = new Neurone();


            //Console.WriteLine(xses.Length);
            //Console.WriteLine(weight[0][0].Length);

            for (int i = 0; i < netStructure.Length; i++)
            {
                for (int j = 0; j < netStructure[i].Length; j++)
                {
                    //Console.WriteLine(xses[i].Length + " / " + weight[i][j].Length);
                    xses[i+1][j] = neurone.NeuroneFunction(xses[i], weight[i][j], bias, functionNames[netStructure[i][j]]);
                }
            }


            //for (int i = 0; i < xses[xses.Length].Length; i++)
            //{
            //    Console.WriteLine(xses[xses.Length][i]);
            //}



            Console.ReadKey();
            //for (int n = 0; n < netStructure[netStructure.Count].Count; n++)
            //{
            //    Console.WriteLine(xses[netStructure.Count][n]);
            //}

        }
    }
}