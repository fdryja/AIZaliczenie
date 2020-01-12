using System;
using TestConsoleApp;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj liczbę warstw");
            int layersCount = Convert.ToInt32(Console.ReadLine());

            int[] layers = new int[layersCount] ;

            for (int i = 0; i < layers.Length; i++)
            {
                int neurones;
                Console.WriteLine("Podaj liczbę neuronów dla warstwy " + (i + 1));
                neurones = Convert.ToInt32(Console.ReadLine());
            }

            string[] layerActivations = new string[] { "sigmoid", "tanh", "sigmoid" };













            NeuralNet neuralNet = new NeuralNet(layers, layerActivations);



            Console.WriteLine(neuralNet.FeedForward());
            

            Console.ReadKey();
        }
    }
}