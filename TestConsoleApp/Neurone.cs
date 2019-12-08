using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    public class Neurone
    {
        public Neurone()
        {
        }

        public double NeuroneFunction(List<float> input, List<float> weight, float bias, string function)
        {
            float result = 0;
            for (int i = 0; i <= input.Count; i++)
            {
                result += input[i] * weight[i];
            }
            result += bias;

            switch (function)
            {
                case "Dyskretna unipolarna":
                    if (result >= 0) return 1;
                    else return 0;
                    break;
                case "Dyskretna bipolarna":
                    if (result > 0) return 1;
                    else return - 1;
                    break;
                case "Ciągła unipolarna":
                    return 1 / (1 + Math.Pow(Math.E,result));
                    break;
                case "Ciągła bipolarna":
                    return (2 / (1 + Math.Pow(Math.E, result))-1);
                    break;
            }
            return 0;
        }
    }
}