using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    public class Neurone
    {
        public Neurone()
        {
        }

        public int NeuroneFunction(List<float> input, List<float> weight, float bias, string function)
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
                    if (result >= 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                    break;
                case "Dyskretna bipolarna":
                    if (result > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                    //    case "Ciągła unipolarna":

                    //        break;
                    //    case "Ciągła bipolarna":

                    //        break;
                    //    case "Sigmoidalna funkcja unipolarna":

                    //        break;
                    //    case "Sigmoidalna funkcja bipolarna":

                    //        break;

                    //case "f1":
                    //    //f1 return();
                    //    break;
                    //case "f2":
                    //    //f2 return();
                    //    break;
                    //case "f3":
                    //    //f3 return();
                    //    break;
                    //return 0;
            }
            return 0;
        }
    }
}