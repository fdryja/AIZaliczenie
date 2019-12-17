using System;
using TestConsoleApp;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> netStructure = new List<List<int>>();
            List<List<float>> xses = new List<List<float>>();
            //List<List<float>> weights = new List<List<float>>();

            List<List<float>> weight = new List<List<float>>();

            

            Random rnd = new Random();
            for (int r = 0; r < netStructure.Count; r++)
            {
                weight[r].Add(rnd.Next(1, 11));
            }
            float bias = 0;

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
                    xses[i + 1].Add(neurone.NeuroneFunction(xses[i], weight[i], bias, functionNames[netStructure[i][j]]));
                }
            }

            Console.WriteLine(xses[netStructure.Count - 1]);

        }
    }
}