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
            List<List<float>> weight = new List<List<float>>();

            List<int> temp = new List<int>();
            List<float> temp2 = new List<float>();
            List<float> temp3 = new List<float>();

            Random rnd = new Random();

            float bias = 1;

            for (int i = 0; i < netStructure.Count; i++)
            {
                for (int j = 0; i < netStructure[i].Count; j++)
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


           // for (int n = 0; n < netStructure[netStructure.Count].Count; n++) 
          //  {
          //      Console.WriteLine(xses[netStructure.Count][n]);
          //  }

        }
    }
}