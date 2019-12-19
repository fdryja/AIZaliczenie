using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    //Trzeba pozmieniać nazwy (zrobione?)
    public class NeuralNet
    {
        public NeuralNet()
        {
        }

        public float Neurone(float[] input, float[] weight, int bias, string function)
        {
            Console.WriteLine(input.Length+"//"+weight.Length);

            float result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += input[i] * weight[i];
            }
            result += bias;

            switch (function)
            {
                case "Dyskretna unipolarna":
                    if (result >= 0) return 1;
                    else return 0;
                case "Dyskretna bipolarna":
                    if (result > 0) return 1;
                    else return -1;
                case "Ciągła unipolarna":
                    double cuni = 1 / (1 + Math.Pow(Math.E, result));
                    return (float)cuni;
                case "Ciągła bipolarna":
                    double cbi = 2 / (1 + Math.Pow(Math.E, result)) - 1;
                    return (float)cbi;
            }
            return 0;
        }

        public float DeltaWeight(List<float> input, List<float> weight, int derivativeNumber)
        {
            float weightedSum = 0;
            for (int i = 0; i <= input.Count; i++)
            {
                weightedSum += input[i] * weight[i];
            }


            float derivative = 0;
            switch (derivativeNumber)
            {
                case 1:
                    //derivative = ;
                    break;
            }
            return /* eta */ derivative * weightedSum;
        }
    }
}