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
            int layersCount = 4;
            int xsesCount = 3;
            
            //PIERWSZY: WARSTWA; DRUGI: NEURON
            int[][] netStructure = new int[layersCount][];

            //PIERWSZY: WARSTWA; DRUGI: NEURON; TRZECI: WAGA
            float[][][] weight = new float[layersCount][][];

            //PIERWSZY: WARSTWA; DRUGI: NEURON Z POPRZEDNIEJ WARSTWY
            float[][] xses = new float[layersCount][];

            for (int i = 0; i < layersCount; i++)
            {
                int currentLayerNeuroneCount;
                Console.WriteLine("Podaj liczbę neuronów w warstwie " + i + 1);
                currentLayerNeuroneCount = Convert.ToInt32(Console.Read());
                netStructure[i] = new int[currentLayerNeuroneCount];
                weight[i] = new float[currentLayerNeuroneCount][];

                if (i == 0)
                {
                    xses[i] = new float[xsesCount];
                }
                else
                {
                    xses[i] = new float[netStructure[i - 1].Length];
                }

                for (int j = 0; j <currentLayerNeuroneCount ; j++)
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

            List<int> temp = new List<int>();
            List<float> temp2 = new List<float>();
            List<float> temp3 = new List<float>();

            Random rnd = new Random();

            float bias = 1;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; i < 4; j++)
                {
                    temp.Add(rnd.Next(1, 3));
                }
                netStructure.Add(temp);
                temp.Clear();
            }

            for (int i = 0; i < netStructure.Count; i++)
            {
                for (int j = 0; i < netStructure[i].Count; j++)
                {
                    temp2.Add(rnd.Next(1, 11));
                }
                weight.Add(temp2);
                temp2.Clear();
            }



            string[] functionNames = new string[] {
                "Liniowa",
                "Dyskretna unipolarna",
                "Dyskretna bipolarna",
                "Ciągła unipolarna",
                "Ciągła bipolarna"
            };

            Neurone neurone = new Neurone();

            for (int i = 0; i < netStructure.Count; i++)
            {
                for (int j = 0; i < netStructure[i].Count; j++)
                {
                    temp3.Add(neurone.NeuroneFunction(xses[i], weight[i], bias, functionNames[netStructure[i][j]]));
                }
                xses.Add(temp3);
                temp3.Clear();
            }

            
            Console.WriteLine(netStructure[0][0]);

            Console.ReadKey();
            //for (int n = 0; n < netStructure[netStructure.Count].Count; n++)
            //{
            //    Console.WriteLine(xses[netStructure.Count][n]);
            //}

        }
    }
}