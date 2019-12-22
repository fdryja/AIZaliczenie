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

                    ReadToLearn();
                    GenerateArrays(learnXses[0].Length);
                    Fill();
                    
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
                        weight[i][j] = new float[xsesCount];
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
            //Console.WriteLine("Struktura sieci. (funkcje)");
            //for (int i = 0; i < layers.Length; i++)
            //{
            //    for (int j = 0; j < netStructure[i].Length; j++)
            //    {
            //        netStructure[i][j] = rnd.Next(1, 4);
            //        Console.Write(netStructure[i][j] + ", ");
            //    }
            //    Console.WriteLine();
            //}
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
            //Console.WriteLine("Input (X):");
            //for (int i = 0; i < xses[0].Length; i++)
            //{
                //xses[0][i] = rnd.Next(1, 34);
                //Console.Write(xses[0][i] + ", ");
            //}
            //Console.WriteLine();
        }

        public static void ReadToLearn()
        {
            int linesCount = 0;
            //float expected = 0;
            String netStructureLine, xsesLine, expectedString;
            expectedString = "";
            netStructureLine = "";
            try
            {
                using (StreamReader sr = new StreamReader("text.txt"))
                {
                    //policzenie linii w tekście

                    linesCount = File.ReadAllLines("text.txt").Count();

                    //netStructureLine = sr.ReadLine();
                    //netStructureLine.Trim();

                    int layersCount = 0;

                    int ileWarstw = 0;
                    String useless = "";

                    for (int i = 0; i < linesCount; i++)
                    {
                        useless = sr.ReadLine();
                        if (useless.Equals("/") == false) { ileWarstw++; } else { break; }

                    }
                    sr.DiscardBufferedData();
                    //sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                    StreamReader sr1 = new StreamReader("text.txt");
                    //Console.WriteLine(ileWarstw);

                    //Console.ReadKey();

                    ////DODANIE WARSTW UŻYTWKOWNIKA
                    //char ch1;
                    //for (int i = 0; i < firstLine.Length; i++)
                    //{
                    //    ch1 = firstLine[i];
                    //    if (ch1 == ' ')
                    //    {
                    //        layersCount++;
                    //    }
                    //}
                    //layersCount += 1;
                    //if (firstLine.Length == 0)
                    //{
                    //    layersCount = 0;
                    //}
                    //String layerCh = "";
                    //List<int> warstwy = new List<int>();
                    ////Console.WriteLine(firstLine.Length);
                    //firstLine += ' ';
                    //for (int i = 0; i < firstLine.Length; i++)
                    //{

                    //    if (firstLine[i] != ' ')
                    //    {
                    //        layerCh += firstLine[i];
                    //        //Console.WriteLine(layerCh);

                    //    } else if (firstLine[i]==' ')
                    //    {
                    //        //Console.WriteLine(layerCh);
                    //        int tabElem = int.Parse(layerCh);
                    //        //Console.WriteLine(tabElem);
                    //        warstwy.Add(tabElem);

                    //        layerCh = "";
                    //    }
                    //}



                    //for (int i = 0; i < secondLine.Length; i++)
                    //{
                    //    char ch = secondLine[i];
                    //    if (ch == ' ')
                    //    {
                    //        xsesCount++;
                    //    }
                    //}
                    //xsesCount += 1;
                    //if (secondLine.Length == 0)
                    //{
                    //    xsesCount = 0;
                    //}


                    //expected = float.Parse(thirdLine, System.Globalization.CultureInfo.InvariantCulture);

                    //Console.WriteLine(layersCount);
                    //Console.WriteLine(firstLine);
                    //Console.WriteLine(secondLine);
                    //Console.WriteLine(thirdLine);



                    //WARSTWY
                    //int[] layers = new int[layersCount];

                    netStructure = new int[ileWarstw][];

                    //XSES
                    learnXses = new float[linesCount - ileWarstw - 1][];

                    expected = new float[linesCount - 1];



                    List<float> listLearnXses = new List<float>();
                    List<int> listNetStructure = new List<int>();

                    for (int i = 0; i < ileWarstw; i++)
                    {
                        netStructureLine = sr1.ReadLine();
                        //Console.WriteLine(netStructureLine);
                        //if (netStructureLine == null)
                        //{
                        //    netStructureLine = "";
                        //}
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
                                //Console.WriteLine(expectedString);
                                if (expectedString == "/") { break; }
                                listNetStructure.Add(int.Parse(expectedString));
                                expectedString = "";
                            }
                        }
                        netStructure[i] = new int[listNetStructure.Count];
                        //Console.WriteLine(listNetStructure.Count);
                        //Console.WriteLine("i: " +i);

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



                    //if (i > ileWarstw)
                    //{

                    //    int index = 0;
                    //    while (index < linesCount - i)
                    //    { 
                    //        

                    //        index++;
                    //    }
                    //}


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

                    for (int i = 0; i < expected.Length; i++)
                    {
                        //Console.WriteLine(expected[i]);
                    }

                    //for (int i = 0; i < warstwy.Count; i++)
                    //{
                    //    layers[i] = warstwy[i];
                    //}

                    //for (int i = 0; i < layers.Length; i++)
                    //{
                    //    //Console.WriteLine(layers[i]);
                    //}

                    //Console.WriteLine(warstwy.Count);

                    //for (int i = 0; i < warstwy.Count; i++)
                    //{
                    //    Console.WriteLine(warstwy[i]);
                    //}
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