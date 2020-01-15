using System;
using AiZaliczenie;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AiZaliczenie
{
    class Program : System.Windows.Forms.Form
    {
        public Program() 
        {
           
            InitializeComponent();
            //start();
        }

        static int wyborMetody;
        static float[] expected, tab;
        static int[] layers;
        private Button button1;
        private Button button2;
        private TextBox textBoxLayers;
        private Label label2;
        private Label label3;
        private TextBox textBoxActivation;
        private Label label4;
        private TextBox textBoxInputs;
        private Label label5;
        private TextBox textBoxExpected;
        private Label label6;
        private Label label7;
        private TextBox wynik;
        private Button button3;
        private Button button4;
        private Button button5;
        static string newLine = Environment.NewLine;
        static string[] layerActivations;
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new Program());
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Program));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxLayers = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxActivation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxInputs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxExpected = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.wynik = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(1153, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(281, 97);
            this.button1.TabIndex = 0;
            this.button1.Text = "Uruchom";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(1153, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(281, 97);
            this.button2.TabIndex = 1;
            this.button2.Text = "Wczytaj z pliku";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxLayers
            // 
            this.textBoxLayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxLayers.Location = new System.Drawing.Point(499, 42);
            this.textBoxLayers.Name = "textBoxLayers";
            this.textBoxLayers.Size = new System.Drawing.Size(556, 32);
            this.textBoxLayers.TabIndex = 4;
            this.textBoxLayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(733, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Warstwy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(696, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "Funkcje aktywacji";
            // 
            // textBoxActivation
            // 
            this.textBoxActivation.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxActivation.Location = new System.Drawing.Point(499, 104);
            this.textBoxActivation.Name = "textBoxActivation";
            this.textBoxActivation.Size = new System.Drawing.Size(556, 32);
            this.textBoxActivation.TabIndex = 6;
            this.textBoxActivation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(733, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "Wejścia";
            // 
            // textBoxInputs
            // 
            this.textBoxInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxInputs.Location = new System.Drawing.Point(499, 166);
            this.textBoxInputs.Name = "textBoxInputs";
            this.textBoxInputs.Size = new System.Drawing.Size(556, 32);
            this.textBoxInputs.TabIndex = 8;
            this.textBoxInputs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(683, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Wartości oczekiwane";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBoxExpected
            // 
            this.textBoxExpected.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxExpected.Location = new System.Drawing.Point(499, 228);
            this.textBoxExpected.Name = "textBoxExpected";
            this.textBoxExpected.Size = new System.Drawing.Size(556, 32);
            this.textBoxExpected.TabIndex = 10;
            this.textBoxExpected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxExpected.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(84, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "Jak wprowadzać dane:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(28, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(368, 540);
            this.label7.TabIndex = 13;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // wynik
            // 
            this.wynik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.wynik.Location = new System.Drawing.Point(432, 312);
            this.wynik.Multiline = true;
            this.wynik.Name = "wynik";
            this.wynik.ReadOnly = true;
            this.wynik.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.wynik.Size = new System.Drawing.Size(715, 261);
            this.wynik.TabIndex = 14;
            this.wynik.Text = "\r\n\r\n\r\n\r\n\r\n\r\nWynik\r\n";
            this.wynik.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.wynik.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Location = new System.Drawing.Point(1153, 295);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(281, 97);
            this.button3.TabIndex = 15;
            this.button3.Text = "Zapisz";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button4.Location = new System.Drawing.Point(1153, 421);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(281, 97);
            this.button4.TabIndex = 16;
            this.button4.Text = "Wprowadź zapisane";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button5.Location = new System.Drawing.Point(432, 592);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(715, 47);
            this.button5.TabIndex = 17;
            this.button5.Text = "Wyczyść";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Program
            // 
            this.ClientSize = new System.Drawing.Size(1469, 672);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.wynik);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxExpected);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxInputs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxActivation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLayers);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Program";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Program do badania sieci neuronowych jednokierunkowych i wielowarstwowych ze sprz" +
    "ężeniem zwrotnym";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        public void Wczytaj()
        {
            wynik.Text = string.Empty;
            NeuralNet.Read();
            textBoxLayers.Text = NeuralNet.layersString;
            textBoxActivation.Text = NeuralNet.activateString;
            textBoxInputs.Text = NeuralNet.inputsString;
            if (NeuralNet.expectedString != " ") textBoxExpected.Text = NeuralNet.expectedString;
            if (NeuralNet.error == true) goto Error;
            layers = NeuralNet.layersRead;
            layerActivations = NeuralNet.activationFunctions;
            tab = NeuralNet.inputs;
            wynik.Text += "WARSTWY:" + newLine;
            Console.WriteLine("WARSTWY");
            for (int i = 0; i < layers.Length; i++)
            {
                wynik.Text += layers[i] + ", ";
                Console.Write(layers[i] + ", ");
            }
            wynik.Text += newLine + "FUNKCJE AKTYWACJI:" + newLine;
            Console.WriteLine("\nFUNKCJE AKTYWACJI");
            for (int i = 0; i < layerActivations.Length; i++)
            {
                wynik.Text += layerActivations[i] + ", ";
                Console.Write(layerActivations[i] + ", ");
            }
            wynik.Text += newLine + "WEJŚCIA:" + newLine;
            Console.WriteLine("\nINPUTY");
            for (int i = 0; i < tab.Length; i++)
            {
                wynik.Text += tab[i] + ", ";
                Console.Write(tab[i] + ", ");
            }
            if (NeuralNet.expectedBool.Equals(true))
            {
                wynik.Text += newLine + "WARTOŚCI OCZEKIWANE:" + newLine;
                Console.WriteLine("\nEXPECTED");
                expected = NeuralNet.expected;
                for (int i = 0; i < expected.Length; i++)
                {
                    wynik.Text += expected[i] + ", ";
                    Console.Write(expected[i] + ", ");
                }
                Console.WriteLine("");
                wynik.Text += newLine;
                //Sprzężenie zwrotne
                NeuralNet neuralNet = new NeuralNet(layers, layerActivations);

                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i] + newLine;
                    Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                }

                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i] + newLine;
                    Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                }

                for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                {
                    neuralNet.BackPropagate(tab, expected);
                    wynik.Text += "Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i] + newLine;
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
        Error:;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Wczytaj(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            wynik.Text = newLine + newLine + newLine + newLine + newLine + newLine + "Wynik";
            textBoxLayers.Text = "";
            textBoxInputs.Text = "";
            textBoxActivation.Text = "";
            textBoxExpected.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxLayers.Text == "" || textBoxActivation.Text == "" || textBoxInputs.Text == "")
            {
                wynik.Text = "Jedno z pól tekstowych zostało puste.";
            }
            else
            {
                Uruchom();
            }
        }

        static bool success;

        public void Uruchom()
        {
            success = false;
            bool expectedBool = false;
            string layersString = textBoxLayers.Text.Trim();
            layersString = System.Text.RegularExpressions.Regex.Replace(layersString, @"\s+", " ");
            string activateString = textBoxActivation.Text.Trim();
            activateString = System.Text.RegularExpressions.Regex.Replace(activateString, @"\s+", " ");
            string inputsString = textBoxInputs.Text.Trim();
            inputsString = System.Text.RegularExpressions.Regex.Replace(inputsString, @"\s+", " ");
            string expectedString = textBoxExpected.Text;
            expectedString = System.Text.RegularExpressions.Regex.Replace(expectedString, @"\s+", " ");
            if (expectedString != null) expectedString.Trim();
            layersString += ' ';
            activateString += ' ';
            inputsString += ' ';
            expectedString += ' ';

            if (expectedString[0] == ' ')
            {
                List<int> layersList = new List<int>();
                List<string> activateList = new List<string>();
                List<float> inputsList = new List<float>();

                string layersValue = "";
                string activateValue = "";
                string inputsValue = "";

                for (int i = 0; i < layersString.Length; i++)
                {

                    if (layersString[i] != ' '&& Char.IsDigit(layersString[i]))
                    {
                        layersValue += layersString[i];
                    }
                    else
                    {
                        if(Regex.IsMatch(layersValue, @"^\d+$"))
                        {
                            int layersElement = Int32.Parse(layersValue);
                            layersList.Add(layersElement);
                            layersValue = "";
                        }
                        else
                        {
                            wynik.Text = "Błędny format warstw";
                            goto Error;
                        }
                        

                    }
                }

                for (int i = 0; i < activateString.Length; i++)
                {
                    if (activateString[i] != ' ' && Char.IsDigit(activateString[i]))
                    {
                        activateValue += activateString[i];
                    }
                    else
                    {
                        if (activateValue == "1")
                        {
                            activateValue = "sigmoid";
                        }
                        else if(activateValue == "2")
                        {
                            activateValue = "tanh";

                        }
                        else if (activateValue == "3")
                        {
                            activateValue = "relu";

                        }
                        else if (activateValue == "4")
                        {
                            activateValue = "leakyrelu";
                        }
                        else
                        {
                            wynik.Text = "Podano błędną funkcję aktywacji";
                            goto Error;
                        }

                        activateList.Add(activateValue);
                        activateValue = "";
                    }
                }

                for (int i = 0; i < inputsString.Length; i++)
                {
                    if (inputsString[i] != ' ' || Char.IsDigit(inputsString[i]) || inputsString[i]==',')
                    {
                        inputsValue += inputsString[i];
                    }
                    else
                    {
                        float inputsElement = float.Parse(inputsValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                        inputsList.Add(inputsElement);
                        inputsValue = "";

                    }
                }

                if ((layersList.Count + 1) != activateList.Count)
                {
                    wynik.Text = "Liczba warstw i funkcji aktywacji jest różna" + newLine + "Liczba warstw: " + (layersList.Count + 1) + newLine + "Liczba funkcji aktywacji: " + activateList.Count;

                    Console.WriteLine("Liczba warstw i funkcji aktywacji jest różna" + newLine + "Liczba warstw: " + (layersList.Count + 1) + "\nLiczba funkcji aktywacji: " + activateList.Count);
                    goto Exit;
                }

                layers = new int[layersList.Count + 1];
                layerActivations = new string[activateList.Count];
                tab = new float[inputsList.Count];

                for (int i = 1; i < layersList.Count + 1; i++)
                {
                    layers[i] = layersList[i - 1];
                }
                for (int i = 0; i < activateList.Count; i++)
                {
                    layerActivations[i] = activateList[i];
                }
                for (int i = 0; i < inputsList.Count; i++)
                {
                    tab[i] = inputsList[i];
                }
                layers[0] = tab.Length;
            }
            else if (expectedString[0] != ' ')
            {
                expectedBool = true;
                List<int> layersList = new List<int>();
                List<string> activateList = new List<string>();
                List<float> inputsList = new List<float>();
                List<float> expectedList = new List<float>();

                string layersValue = "";
                string activateValue = "";
                string inputsValue = "";
                string expectedValue = "";

                for (int i = 0; i < layersString.Length; i++)
                {

                    if (layersString[i] != ' ')
                    {
                        layersValue += layersString[i];
                    }
                    else
                    {
                        if (layersValue.Equals(""))
                        {
                            break;
                        }
                        else
                        {
                            if(Regex.IsMatch(layersValue, @"^\d+$"))
                            {
                                int layersElement = Int32.Parse(layersValue);
                                layersList.Add(layersElement);
                            }
                            else
                            {
                                wynik.Text = "Błędny format warstw";
                                goto Error;
                            }
                            
                        }

                        layersValue = "";

                    }
                }

                for (int i = 0; i < activateString.Length; i++)
                {
                    if (activateString[i] != ' ')
                    {
                        activateValue += activateString[i];
                    }
                    else
                    {
                        if (activateValue == "1")
                        {
                            activateValue = "sigmoid";
                        }
                        else if (activateValue == "2")
                        {
                            activateValue = "tanh";

                        }
                        else if (activateValue == "3")
                        {
                            activateValue = "relu";

                        }
                        else if (activateValue == "4")
                        {
                            activateValue = "leakyrelu";
                        }
                        else
                        {
                            wynik.Text = "Podano błędną funkcję aktywacji";
                            goto Error;
                        }

                        activateList.Add(activateValue);
                        activateValue = "";
                    }
                }

                for (int i = 0; i < inputsString.Length; i++)
                {
                    if (inputsString[i] != ' ')
                    {
                        inputsValue += inputsString[i];
                    }
                    else
                    {
                        float f;
                        if (float.TryParse(inputsValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,out f))
                        {
                            float inputsElement = float.Parse(inputsValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                            inputsList.Add(inputsElement);
                            inputsValue = "";
                        }
                        else
                        {
                            wynik.Text = "Błędny format wejść";
                            goto Error;
                        }

                        

                    }
                }

                for (int i = 0; i < expectedString.Length; i++)
                {
                    if (expectedString[i] != ' ')
                    {
                        expectedValue += expectedString[i];
                    }
                    else
                    {
                        if (expectedValue.Equals(""))
                        {
                            break;
                        }
                        else
                        {
                            float f;
                            if (float.TryParse(expectedValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out f))
                            {
                                float expectedElement = float.Parse(expectedValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                expectedList.Add(expectedElement);
                            }
                            else
                            {
                                wynik.Text = "Błędny format wartości oczekiwanych";
                                goto Error;
                            }
                        }

                        expectedValue = "";
                    }
                }

                if ((layersList.Count + 2) != activateList.Count)
                {
                    wynik.Text = "Liczba warstw i funkcji aktywacji jest różna" + newLine + "Liczba warstw: " + (layersList.Count + 2) + newLine + "Liczba funkcji aktywacji: " + activateList.Count;
                    Console.WriteLine("Liczba warstw i funkcji aktywacji jest różna\nLiczba warstw: " + (layersList.Count + 2) + "\nLiczba funkcji aktywacji: " + activateList.Count);
                    goto Exit;
                }

                layers = new int[layersList.Count + 2];
                layerActivations = new string[activateList.Count];
                tab = new float[inputsList.Count];
                expected = new float[expectedList.Count];

                for (int i = 1; i < layersList.Count + 1; i++)
                {
                    layers[i] = layersList[i - 1];
                }
                for (int i = 0; i < activateList.Count; i++)
                {
                    layerActivations[i] = activateList[i];
                }
                for (int i = 0; i < inputsList.Count; i++)
                {
                    tab[i] = inputsList[i];
                }
                for (int i = 0; i < expectedList.Count; i++)
                {
                    expected[i] = expectedList[i];
                }
                layers[0] = tab.Length;
                layers[layers.Length - 1] = expected.Length;
            }
            else
            {
                wynik.Text = "Błąd w pliku." + newLine + "Powinny być 3 lub 4 linijki a znajduje się " + NeuralNet.linesCount + " linijek";
                //Console.WriteLine("Błąd w pliku.\nPowinny być 3 lub 4 linijki a znajduje się " + linesCount + " linijek");
            }

            wynik.Text = string.Empty;

            wynik.Text += "WARSTWY:" + newLine;
            Console.WriteLine("WARSTWY");
            for (int i = 0; i < layers.Length; i++)
            {
                wynik.Text += layers[i] + ", ";
                Console.Write(layers[i] + ", ");
            }
            wynik.Text += newLine + "FUNKCJE AKTYWACJI:" + newLine;
            Console.WriteLine("\nFUNKCJE AKTYWACJI");
            for (int i = 0; i < layerActivations.Length; i++)
            {
                wynik.Text += layerActivations[i] + ", ";
                Console.Write(layerActivations[i] + ", ");
            }
            wynik.Text += newLine + "WEJŚCIA:" + newLine;
            Console.WriteLine("\nINPUTY");
            for (int i = 0; i < tab.Length; i++)
            {
                wynik.Text += tab[i] + ", ";
                Console.Write(tab[i] + ", ");
            }
            if (expectedBool.Equals(true))
            {
                wynik.Text += newLine + "WARTOŚCI OCZEKIWANE:" + newLine;
                Console.WriteLine("\nEXPECTED");

                for (int i = 0; i < expected.Length; i++)
                {
                    wynik.Text += expected[i] + ", ";
                    Console.Write(expected[i] + ", ");
                }
                Console.WriteLine("");
                wynik.Text += newLine;
                //Sprzężenie zwrotne
                NeuralNet neuralNet = new NeuralNet(layers, layerActivations);

                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i] + newLine;
                    Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                }

                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i] + newLine;
                    Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                }

                for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                {
                    neuralNet.BackPropagate(tab, expected);
                    wynik.Text += "Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i] + newLine;
                    Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
                }
            }
            else
            {
                //Jednokierunkowa
                NeuralNet neuralNet = new NeuralNet(layers, layerActivations);
                wynik.Text += newLine;

                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i] + newLine;
                    Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                }
                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i] + newLine;
                    Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                }
                for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                {
                    wynik.Text += "Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i] + newLine;
                    Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
                }
                
            }
            success = true;
        Error:;
        Exit:;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBoxLayers.Text == "" || textBoxActivation.Text == "" || textBoxInputs.Text == "")
            {
                wynik.Text = "Jedno z pól tekstowych zostało puste lub jest źle uzupełnione.";
            }
            else
            {
                Uruchom();
                Console.WriteLine(success);
                if (success.Equals(true))
                {
                    string expectedText = textBoxExpected.Text;
                    expectedText.Trim();
                    if (expectedText.Length >= 1)
                    {
                        NeuralNet.WriteBack(layers, layerActivations, tab, expected);
                        wynik.Text = newLine + newLine + newLine + newLine + newLine + newLine + "Zapisano";
                    }
                    else
                    {
                        NeuralNet.Write(layers, layerActivations, tab);
                        wynik.Text = newLine + newLine + newLine + newLine + newLine + newLine + "Zapisano";
                    }
                }
                else
                {
                    wynik.Text = "Jedno z pól tekstowych zostało źle uzupełnione.";
                }
            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wynik.Text = string.Empty;
            NeuralNet.ReadSaved();
            if (NeuralNet.error == true) goto Error;
            layers = NeuralNet.layersRead;
            textBoxLayers.Text = NeuralNet.layersString;
            layerActivations = NeuralNet.activationFunctions;
            textBoxActivation.Text = NeuralNet.activateString;
            textBoxInputs.Text = NeuralNet.inputsString;
            if(NeuralNet.expectedString!=" ") textBoxExpected.Text = NeuralNet.expectedString;




            tab = NeuralNet.inputs;
            wynik.Text += "WARSTWY:" + newLine;
            Console.WriteLine("WARSTWY");
            for (int i = 0; i < layers.Length; i++)
            {
                wynik.Text += layers[i] + ", ";
                Console.Write(layers[i] + ", ");
            }
            wynik.Text += newLine + "FUNKCJE AKTYWACJI:" + newLine;
            Console.WriteLine("\nFUNKCJE AKTYWACJI");
            for (int i = 0; i < layerActivations.Length; i++)
            {
                wynik.Text += layerActivations[i] + ", ";
                Console.Write(layerActivations[i] + ", ");
            }
            wynik.Text += newLine + "WEJŚCIA:" + newLine;
            Console.WriteLine("\nINPUTY");
            for (int i = 0; i < tab.Length; i++)
            {
                wynik.Text += tab[i] + ", ";
                Console.Write(tab[i] + ", ");
            }
            if (NeuralNet.expectedBool.Equals(true))
            {
                wynik.Text += newLine + "WARTOŚCI OCZEKIWANE:" + newLine;
                Console.WriteLine("\nEXPECTED");
                expected = NeuralNet.expected;
                for (int i = 0; i < expected.Length; i++)
                {
                    wynik.Text += expected[i] + ", ";
                    Console.Write(expected[i] + ", ");
                }
                Console.WriteLine("");
                wynik.Text += newLine;
                //Sprzężenie zwrotne
                NeuralNet neuralNet = new NeuralNet(layers, layerActivations);

                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i] + newLine;
                    Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                }

                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i] + newLine;
                    Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                }

                for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                {
                    neuralNet.BackPropagate(tab, expected);
                    wynik.Text += "Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i] + newLine;
                    Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
                }
            }
            else
            {
                //Jednokierunkowa
                NeuralNet neuralNet = new NeuralNet(layers, layerActivations);
                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i] + newLine;
                    Console.WriteLine("Liczba neuronów, wastwa " + (i + 1) + "||" + layers[i]);
                }
                for (int i = 0; i < layers.Length; i++)
                {
                    wynik.Text += "Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i] + newLine;
                    Console.WriteLine("Nazwa funckcji, wastwa " + (i + 1) + "||" + layerActivations[i]);
                }
                for (int i = 0; i < neuralNet.FeedForward(tab).Length; i++)
                {
                    wynik.Text += "Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i] + newLine;
                    Console.WriteLine("Element zwróconej tablicy numer " + (i + 1) + " " + neuralNet.FeedForward(tab)[i]);
                }
            }
        Error:;
        }

        public static void start()//stara metoda, która działa(częściowo) z wersją konsolową. Praktycznie bezużyteczna w przypadku GUI
        {
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
                    BrakZapisu:;
                    neuralNet.Save(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\weightsAndBiases.txt");
                    
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
                    if (wyborZapis == 1)
                    {
                        NeuralNet.WriteBack(layers, layerActivations, tab, expected);
                        Console.WriteLine("Zapisano");
                    }
                    else if (wyborZapis == 2)
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
            }
            else if (wyborMetody == 2)
            {
                NeuralNet.Read();
                if (NeuralNet.error == true) goto Error;
                layers = NeuralNet.layersRead;
                layerActivations = NeuralNet.activationFunctions;
                tab = NeuralNet.inputs;
                Console.WriteLine("WARSTWY");
                for (int i = 0; i < layers.Length; i++)
                {
                    Console.Write(layers[i] + ", ");
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
            Error:;
        }
    }
}