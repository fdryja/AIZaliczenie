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
        public float Neurone(float[] input, float[] weight, float bias, string functionNumber)
        {
            Console.WriteLine(input.Length+"//"+weight.Length);

            float result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += input[i] * weight[i];
            }
            result += bias;

            switch (functionNumber)
            {
                //skokowa unipolarna
                case "Dyskretna unipolarna":
                    if (result >= 0) return 1;
                    else return 0;
                //skokowa bipolarna
                case "Dyskretna bipolarna":
                    if (result > 0) return 1;
                    else return -1;
                //sigmoidalna
                case "Ciągła unipolarna":
                    double cuni = 1 / (1 + Math.Pow(Math.E, -result));
                    return (float)cuni;
                //tangens hiperboliczny
                case "Ciągła bipolarna":
                    double cbi = (Math.Pow(Math.E, result) - Math.Pow(Math.E, -result)) / 
                        (Math.Pow(Math.E, result) + Math.Pow(Math.E, -result));
                    return (float)cbi;
            }

        }

        public float DeltaWeight(float[] input, float[] weight, float eta, int derivativeNumber)
        {

            float result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += input[i] * weight[i];
            }
            result += bias;

            //DO CAŁKOWITEJ EDYCJI
            float weightedSum = 0;
            for (int i = 0; i <= input.Length; i++)
            {
                weightedSum += /* delta z poprzedniej warstwy */ * weight[i];
            }


            float derivative = 0;
            switch (derivativeNumber)
            {
                //od funkcji skokowej nie da się zrobić pochodnej

                //sigmoidalna pochodna działa
                case 3:
                    double cuni = 1 / (1 + Math.Pow(Math.E, result));
                    derivative =  (float)cuni * (1 - (float)cuni);
                    break;
                //tangens hiperboliczny działa
                case 4:
                    derivative =(float)(1 -Math.Pow((Math.Pow(Math.E, result) - Math.Pow(Math.E, - result)) / 
                        (Math.Pow(Math.E, result) + Math.Pow(Math.E, -result)), 2));
                    break;
            }
            return eta * derivative * weightedSum /* *x */ ; //to jest deltaW
        }
    }
}
