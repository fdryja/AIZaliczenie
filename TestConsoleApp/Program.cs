using System;
using AiZaliczenie;
using System.Collections.Generic;
using System.IO;

namespace AiZaliczenie
{
    class Program
    {
        static int wyborMetody;
        static float[] expected, tab;
        static int[] layers;
        static string[] layerActivations;
        static void Main(string[] args)
        {
            //NeuralNet.Write();
            

            Console.WriteLine("Wybierz metodę wprowadzania danych:\n1 - ręcznie\n2 - odczyt z pliku");
            wyborMetody = Convert.ToInt16(Console.ReadLine());

            if (wyborMetody == 1)
            {

                int wyborTypu;
                Console.WriteLine("Wybierz typ działania programu:\n1 - jednokierunkowe\n2 - ze sprzężeniem zwrotnym");
                wyborTypu = Convert.ToInt16(Console.ReadLine());
                if (wyborTypu == 1)
                {
                    //jednokierunkowe
                    Console.WriteLine("Podaj liczbę wejść");
                    int inputsCount = Convert.ToInt32(Console.ReadLine());
                    tab = new float[inputsCount];

                    for (int i = 0; i < tab.Length; i++)
                    {
                        Console.WriteLine("Podaj wartość dla wejścia numer " + (i + 1));
                        float input = (float)Convert.ToDouble(Console.ReadLine());
                        tab[i] = input;
                    }

                    Console.WriteLine("Podaj liczbę warstw");
                    int layersCount = Convert.ToInt32(Console.ReadLine());

                    layers = new int[layersCount];
                    layerActivations = new string[layersCount];
                    layers[0] = tab.Length;

                    for (int i = 1; i < layers.Length; i++)
                    {
                        int neurones;
                        if (i == layers.Length - 1)
                        {

                            Console.WriteLine("Podaj liczbę neuronów dla warstwy wyjściowej");
                            neurones = Convert.ToInt32(Console.ReadLine());
                            layers[i] = neurones;
                            break;
                        }


                        Console.WriteLine("Podaj liczbę neuronów dla warstwy " + (i + 1) + " z " + (layersCount - 1));
                        neurones = Convert.ToInt32(Console.ReadLine());
                        layers[i] = neurones;
                    }

                    for (int i = 0; i < layersCount; i++)
                    {
                    OnceAgain:
                        Console.WriteLine("Podaj funckję aktywacji dla warstwy " + (i + 1) + "\n1 - sigmoidalna\n2 - tangens hiperboliczny\n3 - ReLU\n4 - Leaky ReLU");
                        int wybor = Convert.ToInt32(Console.ReadLine());
                        if (wybor == 1)
                        {
                            layerActivations[i] = "sigmoid";
                            Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na sigmoidalną!");

                        }
                        else if (wybor == 2)
                        {
                            layerActivations[i] = "tanh";
                            Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na tangens hiperboliczny!");
                        }
                        else if (wybor == 3)
                        {
                            layerActivations[i] = "relu";
                            Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na ReLU!");

                        }
                        else if (wybor == 4)
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
                        Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                    }

                    for (int i = 0; i < layers.Length; i++)
                    {
                        Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                    }

                    NeuralNet neuralNet = new NeuralNet(layers, layerActivations);
                    for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                    {
                        Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
                    }
                    Console.WriteLine("Czy chcesz zapisać strukturę sieci?\n1 - Tak\n2 - Nie");
                    int wyborZapis = Convert.ToInt16(Console.ReadLine());
                    if (wyborZapis == 1)
                    {
                        NeuralNet.Write(layers, layerActivations, tab);
                        Console.WriteLine("Zapisano");
                    }
                    else if (wyborZapis == 2)
                    {
                        goto BrakZapisu;
                    }
                    neuralNet.Save(Path.Combine(Environment.CurrentDirectory, @"Data\weightsAndBiases.txt"));
                    BrakZapisu:;
                }
                else if (wyborTypu == 2)
                {
                    //ze sprzężeniem zwrotnym
                    Console.WriteLine("Podaj liczbę wejść");
                    int inputsCount = Convert.ToInt32(Console.ReadLine());
                    tab = new float[inputsCount];

                    for (int i = 0; i < tab.Length; i++)
                    {
                        Console.WriteLine("Podaj wartość dla wejścia numer " + (i + 1));
                        float input = (float)Convert.ToDouble(Console.ReadLine());
                        tab[i] = input;
                    }


                    Console.WriteLine("Podaj liczbę wyjść");
                    int expectedCount = Convert.ToInt32(Console.ReadLine());
                    expected = new float[expectedCount];

                    for (int i = 0; i < expected.Length; i++)
                    {
                        Console.WriteLine("Podaj wartość dla wyjścia numer " + (i + 1));
                        float input = (float)Convert.ToDouble(Console.ReadLine());
                        expected[i] = input;
                    }

                    Console.WriteLine("Podaj liczbę warstw");
                    int layersCount = Convert.ToInt32(Console.ReadLine());

                    layers = new int[layersCount];
                    layerActivations = new string[layersCount];
                    layers[0] = tab.Length;

                    for (int i = 1; i < layers.Length; i++)
                    {

                        if (i == layers.Length - 1)
                        {
                            layers[i] = expected.Length;
                            break;
                        }

                        int neurones;
                        Console.WriteLine("Podaj liczbę neuronów dla warstwy " + (i + 1) + " z " + (layersCount - 1));
                        neurones = Convert.ToInt32(Console.ReadLine());
                        layers[i] = neurones;
                    }

                    for (int i = 0; i < layersCount; i++)
                    {
                    OnceAgain:
                        Console.WriteLine("Podaj funckję aktywacji dla warstwy " + (i + 1) + "\n1 - sigmoidalna\n2 - tangens hiperboliczny\n3 - ReLU\n4 - Leaky ReLU");
                        int wybor = Convert.ToInt32(Console.ReadLine());
                        if (wybor == 1)
                        {
                            layerActivations[i] = "sigmoid";
                            Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na sigmoidalną!");

                        }
                        else if (wybor == 2)
                        {
                            layerActivations[i] = "tanh";
                            Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na tangens hiperboliczny!");
                        }
                        else if (wybor == 3)
                        {
                            layerActivations[i] = "relu";
                            Console.WriteLine("Funkcja aktywacji dla warstwy " + (i + 1) + " ustawiona na ReLU!");

                        }
                        else if (wybor == 4)
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
                        Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                    }

                    for (int i = 0; i < layers.Length; i++)
                    {
                        Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                    }


                    NeuralNet neuralNet = new NeuralNet(layers, layerActivations);
                    //Tutaj coś się wywala, ale nie wiem dlaczego ani gdzie używać tego loada.
                    //neuralNet.Load(Path.Combine(Environment.CurrentDirectory, @"Data\weightsAndBiases.txt"));

                    for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                    {
                        neuralNet.BackPropagate(tab, expected);
                        Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
                    }
                    Console.WriteLine("Czy chcesz zapisać strukturę sieci?\n1 - Tak\n2 - Nie");
                    int wyborZapis = Convert.ToInt16(Console.ReadLine());
                    if(wyborZapis == 1)
                    {
                        NeuralNet.WriteBack(layers, layerActivations, tab, expected);
                        Console.WriteLine("Zapisano");
                    }else if (wyborZapis == 2)
                    {
                        goto BrakZapisu;
                    }
                    else
                    {
                        Console.WriteLine("Nie ma takiego numeru");
                    }
                    BrakZapisu:
                    neuralNet.Save(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\weightsAndBiases.txt");
                }
                else
                {
                    Console.WriteLine("Nie ma takiego numeru");
                }
                }else if(wyborMetody == 2)
                {
                NeuralNet.Read();
                if (NeuralNet.error == true) goto Error;
                layers = NeuralNet.layersRead;
                layerActivations = NeuralNet.activationFunctions;
                tab = NeuralNet.inputs;



                Console.WriteLine("WARSTWY");
                for (int i = 0; i < layers.Length; i++)
                {
                    Console.Write(layers[i]+", ");
                }
                Console.WriteLine("\nFUNKCJE AKTYWACJI");
                for (int i = 0; i < layerActivations.Length; i++)
                {
                    Console.Write(layerActivations[i] + ", ");
                }
                Console.WriteLine("\nINPUTY");
                for (int i = 0; i < tab.Length; i++)
                {
                    Console.Write(tab[i] + ", ");
                }
                if (NeuralNet.expectedBool.Equals(true))
                {
                    Console.WriteLine("\nEXPECTED");
                    expected = NeuralNet.expected;
                    for (int i = 0; i < expected.Length; i++)
                    {
                        Console.Write(expected[i] + ", ");
                    }
                    Console.WriteLine("");
                    //Sprzężenie zwrotne
                    NeuralNet neuralNet = new NeuralNet(layers, layerActivations);

                    for (int i = 0; i < layers.Length; i++)
                    {
                        Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                    }

                    for (int i = 0; i < layers.Length; i++)
                    {
                        Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                    }

                    for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                    {
                        neuralNet.BackPropagate(tab, expected);
                        Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
                    }

                }
                else
                {
                    //Jednokierunkowa
                    NeuralNet neuralNet = new NeuralNet(layers, layerActivations);
                    for (int i = 0; i < layers.Length; i++)
                    {
                        Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                    }

                    for (int i = 0; i < layers.Length; i++)
                    {
                        Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                    }

                    for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                    {
                        Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
                    }

                    }
                }
            else
            {
                Console.WriteLine("Nie ma takiego numeru");
            }
            Error:
            Console.ReadKey();
        }
    }
}