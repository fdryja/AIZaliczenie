using System;
using TestConsoleApp;
using System.Collections.Generic;
using System.IO;



namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();


            Console.WriteLine("Podaj liczbę warstw");
            int layersCount = Convert.ToInt32(Console.ReadLine());

            int[] layers = new int[layersCount] ;
            string[] layerActivations = new string[layersCount];
            float[] expected = new float[] { 1000 };

            for (int i = 1; i < layers.Length; i++)
            {

                if (i == layers.Length-1)
                {
                    layers[i] = expected.Length;
                    break;
                }

                int neurones;
                Console.WriteLine("Podaj liczbę neuronów dla warstwy " + (i + 1));
                neurones = Convert.ToInt32(Console.ReadLine());
                layers[i] = neurones;
            }

            //string[] layerActivations = new string[] { "sigmoid", "tanh", "sigmoid" };


            
            for (int i = 0; i < layersCount; i++)
            {
                OnceAgain:
                Console.WriteLine("Podaj funckję aktywacji dla warstwy " + (i + 1)+ "\n1 - sigmoidalna\n2 - tangens hiperboliczny\n3 - ReLU\n4 - Leaky ReLU");
                int wybor = Convert.ToInt32(Console.ReadLine());
                if(wybor == 1)
                {
                    layerActivations[i] = "sigmoid";
                    Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na sigmoidalną!");

                } else if(wybor == 2)
                {
                    layerActivations[i] = "tanh";
                    Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na tangens hiperboliczny!");
                }
                else if(wybor == 3)
                {
                    layerActivations[i] = "relu";
                    Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na ReLU!");

                } else if (wybor == 4)
                {
                    layerActivations[i] = "leakyrelu";
                    Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na Leaky ReLU!");
                }
                else
                {
                    Console.WriteLine("nie ma takiego numeru");
                    goto OnceAgain;
                }

            }




            


            for (int i = 0; i < layers.Length; i++)
            {
                Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) +"||"+ layers[i]);
            }

            for (int i = 0; i < layers.Length; i++)
            {
                Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) +"||"+ layerActivations[i]);
            }


            //float[] tab = new float[4];

            float[]tab = new float[] { 80, 186, 3 };
            layers[0] = tab.Length;

            //for (int i = 0; i < tab.Length; i++)
            //{
            //    tab[i] =(float) rand.NextDouble() * rand.Next(1, 80);
            //}
            
            NeuralNet neuralNet = new NeuralNet(layers, layerActivations);
            neuralNet.Load(Path.Combine(Environment.CurrentDirectory, @"Data\text.txt"));
            for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
            {
                Console.WriteLine("Element zwróconej tablicy numer "+(i+1)+" "+neuralNet.FeedForward(tab)[i]);
            }


            for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
            {
                neuralNet.BackPropagate(tab, expected);
                Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
            }


            neuralNet.Save(Path.Combine(Environment.CurrentDirectory,@"Data\text.txt"));

            Console.ReadKey();
        }
    }
}