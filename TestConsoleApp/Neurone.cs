using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    public class Neurone
    {
        public Neurone()
        {
        }

        public Neurone(List<float> inputs, List<float> weights, float bias, String function)
        {
            float result = 0;
            for (int i = 0; i <= inputs.Count; i++)
            {
                result += inputs[i] * weights[i];
            }
            result += bias;


            switch (function)
            {
                case "f1":
                    //f1 return();
                    break;
                case "f2":
                    //f2 return();
                    break;
                case "f3":
                    //f3 return();
                    break;
            }
        }
    }
}