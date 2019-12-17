﻿using System;
using TestConsoleApp;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // ListaList/TablicaTablic NetStucture =

            List<float> weight = new List<float>();

            Random rnd = new Random();
            for (int r = 0; r < netStructure.Count; r++)
            {
                weight.Add(rnd.Next(1, 11));
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
                    xses[i + 1].Add(neurone.NeuroneFunction(xses[i], weight, bias, functionNames[netStructure[i][j]]));
                }
            }

            //for (i)
            //{
            //    for (j)
            //    {
            //        //Wynik[i][j] = Neurone(input, weight, bias, functionNames[ NetStructure[i][j] ])
            //    }
            //}

        }
    }
}

//test