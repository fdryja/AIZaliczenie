using System;
using TestConsoleApp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestConsoleApp
{
    class Program
    {
        //public static int[] layers = new int[4] { 6, 3, 2, 1 };
        //public static int[] layers;

        //public static int xsesCount = 3;
        public static float[][] learnXses;
        public static float bias = 1;
        public static float eta = 0.2f;

        //PIERWSZY: WARSTWA; DRUGI: NEURON
        public static int[][] netStructure;

        //PIERWSZY: WARSTWA; DRUGI: NEURON; TRZECI: WAGA
        public static float[][][] weight;

        //PIERWSZY: WARSTWA; DRUGI: NEURON Z POPRZEDNIEJ WARSTWY
        public static float[][] xses;



        //float expected = 0;
        public static float[] expected;

        static void Main(string[] args)
        {

            List<float> deltaToSumCurrent = new List<float>();
            List<float> deltaToSumNext = new List<float>();
            float weightedDeltaSum;


            

            string[] functionNames = new string[] {
                "Dyskretna unipolarna",
                "Dyskretna bipolarna",
                "Ciągła unipolarna",
                "Ciągła bipolarna"
            };

            NeuralNet net = new NeuralNet();
            //Beginning:
            Console.WriteLine("1 - wykonuj\n2 - ucz");
            int wybor;
            wybor = Convert.ToInt32(Console.ReadLine());
            switch (wybor)
            {
                case 1:
                    //pobranie X od użytkownika, xses[0]

                    ReadToWork();
                    //GenerateArrays();
                    Fill();

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

                    int linesCount = 0;
                    String netStructureLine, xsesLine, expectedString;
                    expectedString = "";
                    netStructureLine = "";
                    try
                    {
                        using (StreamReader sr = new StreamReader("text.txt"))
                        {
                            //policzenie linii w tekście

                            linesCount = File.ReadAllLines("text.txt").Count();

                            int ileWarstw = 0;
                            String useless = "";

                            for (int i = 0; i < linesCount; i++)
                            {
                                useless = sr.ReadLine();
                                if (useless.Equals("/") == false) { ileWarstw++; } else { break; }

                            }
                            sr.DiscardBufferedData();

                            StreamReader sr1 = new StreamReader("text.txt");


                            int[][] netStructure = new int[ileWarstw][];

                            //XSES
                            float[][] learnXses = new float[linesCount - ileWarstw - 1][];

                            float [] expected = new float[linesCount - 1];



                            List<float> listLearnXses = new List<float>();
                            List<int> listNetStructure = new List<int>();

                            for (int i = 0; i < ileWarstw; i++)
                            {
                                netStructureLine = sr1.ReadLine();

                                netStructureLine.Trim();

                                netStructureLine += ' ';
                                for (int j = 0; j < netStructureLine.Length; j++)
                                {
                                    if (netStructureLine[j] != ' ')
                                    {
                                        expectedString += netStructureLine[j];
                                    }
                                    else if (netStructureLine[j] == ' ')
                                    {
                                        if (expectedString == "/") { break; }
                                        listNetStructure.Add(int.Parse(expectedString));
                                        expectedString = "";
                                    }
                                }
                                netStructure[i] = new int[listNetStructure.Count];


                                for (int l = 0; l < netStructure[i].Length; l++)
                                {
                                    netStructure[i][l] = listNetStructure[l];
                                }
                                listNetStructure.Clear();
                            }
                            Console.WriteLine(linesCount - ileWarstw);

                            for (int i = 0; i < linesCount - ileWarstw - 1; i++)
                            {
                                xsesLine = sr1.ReadLine();
                                Console.WriteLine(xsesLine);
                                xsesLine.Trim();
                                xsesLine += ' ';
                                for (int j = 0; j < xsesLine.Length; j++)
                                {
                                    if (xsesLine[j] != 'e' && xsesLine[j] != ' ')
                                    {
                                        expectedString += xsesLine[j];
                                    }
                                    else if (xsesLine[j] == 'e')
                                    {
                                        expected[i] = float.Parse(expectedString, System.Globalization.CultureInfo.InvariantCulture);
                                        expectedString = "";
                                    }
                                    else if (xsesLine[j] == ' ')
                                    {
                                        if (expectedString != "/")
                                        {
                                            listLearnXses.Add(float.Parse(expectedString, System.Globalization.CultureInfo.InvariantCulture));
                                        }
                                        expectedString = "";
                                    }
                                }
                                learnXses[i] = new float[listLearnXses.Count];
                                Console.WriteLine(listLearnXses.Count);
                                for (int l = 0; l < listLearnXses.Count; l++)
                                {
                                    learnXses[i][l] = listLearnXses[l];
                                }
                                listLearnXses.Clear();
                            }




                            for (int i = 0; i < learnXses.Length; i++)
                            {
                                for (int j = 0; j < learnXses[i].Length; j++)
                                {
                                    Console.Write(learnXses[i][j] + ", ");
                                }
                                Console.WriteLine("");
                            }
                            Console.WriteLine("");
                            for (int i = 0; i < netStructure.Length; i++)
                            {
                                for (int j = 0; j < netStructure[i].Length; j++)
                                {
                                    Console.Write(netStructure[i][j] + ", ");
                                }
                                Console.WriteLine("");
                            }


                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Nie można odczytać");
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine(netStructure[0].Length);

                    //Deklaracja tablic niestandardowych:
                    for (int i = 0; i < netStructure.Length; i++)
                    {
                        //netStructure[i] = new int[layers[i]];
                        weight[i] = new float[netStructure[i].Length][];

                        for (int j = 0; j < netStructure[i].Length; j++)
                        {
                            if (i == 0)
                            {
                                weight[i][j] = new float[learnXses[0].Length];
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
                            xses[i] = new float[learnXses[0].Length];
                        }
                        else
                        {
                            xses[i] = new float[netStructure[i - 1].Length];
                        }
                    }
                    Random rnd = new Random();

                    Console.WriteLine("Weights:");
                    for (int i = 0; i < netStructure.Length; i++)
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

                    for (int q = 0; q < learnXses.Length; q++)
                    {

                        xses[0] = learnXses[q];

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
                        weightedDeltaSum = expected[q] -xses[xses.Length - 1][0]; //oczekiwana - wynik
                        for (int i = weight.Length - 1; i == 0; i--)
                        {

                            for (int j = 0; j < weight[i].Length; j++)
                            {
                                deltaToSumNext.Add(net.Delta(xses[i + 1], weight[i + 1][j], bias, netStructure[i][j], weightedDeltaSum));

                                if (i != weight.Length - 1)
                                {
                                    for (int l = 0; l <= deltaToSumCurrent.Count; l++)
                                    {
                                        weightedDeltaSum += deltaToSumCurrent[l] * weight[i + 1][l][j];
                                    }
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
                    //goto Beginning;
                    break;
                    
            }

            Console.ReadKey();
        }

        public static void GenerateArrays(int xsesCount)
        {
            //Deklaracja tablic niestandardowych:
            for (int i = 0; i < netStructure.Length; i++)
            {
                //netStructure[i] = new int[layers[i]];
                weight[i] = new float[netStructure[i].Length][];

                for (int j = 0; j < netStructure[i].Length; j++)
                {
                    if (i == 0)
                    {
                        weight[i][j] = new float[learnXses[0].Length];
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
        }

        public static void Fill()
        {
            //Wypełnienie tablic niestandardowych:
            Random rnd = new Random();
           
            Console.WriteLine("Weights:");
            for (int i = 0; i < netStructure.Length; i++)
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
            
        }

        public static void ReadToLearn()
        {
            int linesCount = 0;
            String netStructureLine, xsesLine, expectedString;
            expectedString = "";
            netStructureLine = "";
            try
            {
                using (StreamReader sr = new StreamReader("text.txt"))
                {
                    //policzenie linii w tekście

                    linesCount = File.ReadAllLines("text.txt").Count();

                    int ileWarstw = 0;
                    String useless = "";

                    for (int i = 0; i < linesCount; i++)
                    {
                        useless = sr.ReadLine();
                        if (useless.Equals("/") == false) { ileWarstw++; } else { break; }

                    }
                    sr.DiscardBufferedData();
                  
                    StreamReader sr1 = new StreamReader("text.txt");
                    

                    netStructure = new int[ileWarstw][];

                    //XSES
                    learnXses = new float[linesCount - ileWarstw - 1][];

                    expected = new float[linesCount - 1];



                    List<float> listLearnXses = new List<float>();
                    List<int> listNetStructure = new List<int>();

                    for (int i = 0; i < ileWarstw; i++)
                    {
                        netStructureLine = sr1.ReadLine();
                       
                        netStructureLine.Trim();

                        netStructureLine += ' ';
                        for (int j = 0; j < netStructureLine.Length; j++)
                        {
                            if (netStructureLine[j] != ' ')
                            {
                                expectedString += netStructureLine[j];
                            }
                            else if (netStructureLine[j] == ' ')
                            {
                                if (expectedString == "/") { break; }
                                listNetStructure.Add(int.Parse(expectedString));
                                expectedString = "";
                            }
                        }
                        netStructure[i] = new int[listNetStructure.Count];
                      

                        for (int l = 0; l < netStructure[i].Length; l++)
                        {
                            netStructure[i][l] = listNetStructure[l];
                        }
                        listNetStructure.Clear();
                    }
                    Console.WriteLine(linesCount - ileWarstw);

                    for (int i = 0; i < linesCount - ileWarstw - 1; i++)
                    {
                        xsesLine = sr1.ReadLine();
                        Console.WriteLine(xsesLine);
                        xsesLine.Trim();
                        xsesLine += ' ';
                        for (int j = 0; j < xsesLine.Length; j++)
                        {
                            if (xsesLine[j] != 'e' && xsesLine[j] != ' ')
                            {
                                expectedString += xsesLine[j];
                            }
                            else if (xsesLine[j] == 'e')
                            {
                                expected[i] = float.Parse(expectedString, System.Globalization.CultureInfo.InvariantCulture);
                                expectedString = "";
                            }
                            else if (xsesLine[j] == ' ')
                            {
                                if (expectedString != "/")
                                {
                                    listLearnXses.Add(float.Parse(expectedString, System.Globalization.CultureInfo.InvariantCulture));
                                }
                                expectedString = "";
                            }
                        }
                        learnXses[i] = new float[listLearnXses.Count];
                        Console.WriteLine(listLearnXses.Count);
                        for (int l = 0; l < listLearnXses.Count; l++)
                        {
                            learnXses[i][l] = listLearnXses[l];
                        }
                        listLearnXses.Clear();
                    }




                    for (int i = 0; i < learnXses.Length; i++)
                    {
                        for (int j = 0; j < learnXses[i].Length; j++)
                        {
                            Console.Write(learnXses[i][j] + ", ");
                        }
                        Console.WriteLine("");
                    }
                    Console.WriteLine("");
                    for (int i = 0; i < netStructure.Length; i++)
                    {
                        for (int j = 0; j < netStructure[i].Length; j++)
                        {
                            Console.Write(netStructure[i][j] + ", ");
                        }
                        Console.WriteLine("");
                    }

                   
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Nie można odczytać");
                Console.WriteLine(e.Message);
            }


        }

        public static void ReadToWork() 
            {
            
            }
        
    }
}