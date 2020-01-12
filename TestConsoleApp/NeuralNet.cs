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
        public float Neurone(float[] input, float[] weight, float bias, int functionNumber)
        {
            //Console.WriteLine(input.Length+"//"+weight.Length);

            float result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += input[i] * weight[i];
            }
            result += bias;


            switch (functionNumber)
            {
                //skokowa unipolarna
                case 0:
                    if (result >= 0) return 1;
                    else return 0;
                //skokowa bipolarna
                case 1:
                    if (result > 0) return 1;
                    else return -1;
                //sigmoidalna
                case 2:
                    double cuni = 1 / (1 + Math.Pow(Math.E, -result));
                    return (float)cuni;
                //tangens hiperboliczny
                case 3:
                    double cbi = (Math.Pow(Math.E, result) - Math.Pow(Math.E, -result))/(Math.Pow(Math.E, result) + Math.Pow(Math.E, -result));
                    return (float)cbi;
            }
            return 0;
        }

        public float Delta(float[] input, float[] weight, float bias, int derivativeNumber, float secondFactor)
        {
            float result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += input[i] * weight[i];
            }
            result += bias;

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

            return derivative * secondFactor;
        }

        public float DeltaWeight(float eta, float delta, float currentX)
        {
            return eta * delta * currentX ;
        }

    }
}
