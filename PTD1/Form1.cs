using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTD1
{
    public partial class Form1 : Form
    {
        SignalGraph 
            courseGraph1,
            courseGraph2,
            courseGraph3,
            courseGraph4;

        SpectrumGraph
            spectrumGraph1,
            spectrumGraph2,
            spectrumGraph3,
            spectrumGraph4;

        AnalogSignal 
            signal1,
            signal2,
            signal3;

        DigitalSignal 
            digital;

        FourierTransform ft;

        public Form1()
        {
            /* Funkcja inicjalizująca wykresy dla danego laboratorium
             
             * * * * * * * * * * * * * * * * * * * * 
             *  lab1 i lab2                  : 1    *
             *  lab3                         : 2    *
             *  lab4 (ASK)                   : 3    *
             *  lab4 (FSK i PSK)             : 4    *
             *  lab5 (demodulacja ASK)       : 5    *
             *  lab5 (demodulacja PSK)       : 6    *
             *  lab5 (demodulacja FSK)       : 7    *
             *  Generowanie przebiegów       : 8    * 
             *  MSE                          : 9    *
             *  Zmiany MSE                   : 10   *
             *  Autokorelacja                : 11   *
             *  Korelacja wzajemna           : 12   *
             *  Odfiltrowanie składowej      : 13   *
             *  Filtracja syg. delta         : 14   *
             *  Filtracja szumu              : 15   *
             *  Filtr FIR                    : 16   *
             *  Filtr SOI                    : 17   *
             *  Filtr SOI v2                 : 18   *
             *                                      *
             * * * * * * * * * * * * * * * * * * * */

            initialize(12);

            timer1.Start();
        }
        /// <summary>
        /// Funkcja inicjalizująca obiekty sygnałów do wybranego labolatorium
        /// </summary>
        private void initialize(int lab)
        {
            Controls.Remove(courseGraph1);
            Controls.Remove(courseGraph2);
            Controls.Remove(courseGraph3);
            Controls.Remove(courseGraph4);

            Controls.Remove(spectrumGraph1);
            Controls.Remove(spectrumGraph2);
            Controls.Remove(spectrumGraph3);
            Controls.Remove(spectrumGraph4);

            courseGraph1 = null;
            courseGraph2 = null;
            courseGraph3 = null;
            courseGraph4 = null;

            spectrumGraph1 = null;
            spectrumGraph2 = null;
            spectrumGraph3 = null;
            spectrumGraph4 = null;

            signal1 = null;
            signal2 = null;
            signal3 = null;

            digital = null;

            ft = new FourierTransform();

            this.Controls.Clear();

            switch (lab)
            {
                case 1:
                    initialize_Lab1_Lab2();
                    break;
                case 2:
                    initialize_Lab3();
                    break;
                case 3:
                    initialize_Lab4_ASK();
                    break;
                case 4:
                    initialize_Lab4_FSK_PSK();
                    break;
                case 5:
                    initialize_Lab5_ASK();
                    break;
                case 6:
                    initialize_Lab5_PSK();
                    break;
                case 7:
                    initialize_Lab5_FSK();
                    break;
                case 8:
                    initialize_SquareTriangleSaw();
                    break;
                case 9:
                    initialize_MSE();
                    break;
                case 10:
                    initialize_MSEPlot();
                    break;
                case 11:
                    initialize_Correlation_1();
                    break;
                case 12:
                    initialize_Correlation_2();
                    break;
                case 13:
                    initialize_IDFT();
                    break;
                case 14:
                    initialize_Sinc();
                    break;
                case 15:
                    initialize_FilteringOfNoise();
                    break;
                case 16:
                    initialize_generateFilter_1();
                    break;
                case 17:
                    initialize_generateFilter_2();
                    break;
                case 18:
                    initialize_generateFilter_3();
                    break;
                case 19:
                    initialize_CDMA();
                    break;
                default:
                    initialize_Lab1_Lab2();
                    break;
            }
            Controls.Add(courseGraph1);
            Controls.Add(courseGraph2);
            Controls.Add(courseGraph3);
            Controls.Add(courseGraph4);

            Controls.Add(spectrumGraph1);
            Controls.Add(spectrumGraph2);
            Controls.Add(spectrumGraph3);
            Controls.Add(spectrumGraph4);

            InitializeComponent();
        }
        private void initialize_CDMA()
        {
            // inicjalizacja obiektow klasy wykresow przebiegu sygnalu
            courseGraph1 = new CourseGraph(10, 24, 150, 500, "Dane");
            courseGraph2 = new CourseGraph(10, 184, 150, 500, "Kod rozpaszający");
            courseGraph3 = new CourseGraph(10, 344, 150, 500, "Kod rozpaszający");

            // inicjalizacja obiektow klasy wykresow widma
            spectrumGraph1 = new SpectrumGraph(520, 24, 150, 500, "Dane");
            spectrumGraph2 = new SpectrumGraph(520, 184, 150, 500, "Kod rozpaszający");
            spectrumGraph3 = new SpectrumGraph(520, 344, 150, 500, "Dane rozszerzone");

            // inicjalizacja obiektow klasy sygnalow
            digital = new DigitalSignal("10101011", 1000, 0.125);

            // rysowanie wykresow
            courseGraph1.Signal = digital;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            spectrumGraph1.Signal = digital;
            spectrumGraph1.DFT();
            spectrumGraph1.Autoscale();
            spectrumGraph1.DrawGraph();

            digital = new DigitalSignal("1010101110101011", 1000, 0.03125);

            //rysowanie wykresów 
            spectrumGraph2.Signal = digital;
            spectrumGraph2.DFT();
            spectrumGraph2.Autoscale();
            spectrumGraph2.DrawGraph();

            courseGraph2.Signal = digital;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            //rysowanie wykresów 
            spectrumGraph3.Signal = new DigitalSignal("1010101110101011", 1000, 0.03125) * new DigitalSignal("10101011", 1000, 0.125);
            spectrumGraph3.DFT();
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();

            courseGraph3.Signal = new DigitalSignal("1010101110101011", 1000, 0.03125) * new DigitalSignal("10101011", 1000, 0.125);
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();
        }
        private void initialize_generateFilter_3()
        {
            AnalogSignal delta = new AnalogSignal(1, 300, 1000, 0.5).GenerateDelta(50);
            signal1 = new AnalogSignal(1, 300, 1000, 0.5).IIR(delta, delta.GenerateFilter_HighBand_IIR());
            courseGraph1 = new CourseGraph(10, 24, 220, 500, "Przebieg charakterystyki");
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            spectrumGraph1 = new SpectrumGraph(520, 24, 220, 700, "Odpowiedź filtru");
            spectrumGraph1.Signal = signal1;
            spectrumGraph1.DFT();
            spectrumGraph1.Autoscale();
            spectrumGraph1.DrawGraph();


            signal2 = new AnalogSignal(1, 100, 1000, 0.5).GenerateNoise(0.5);
            courseGraph2 = new CourseGraph(10, 254, 220, 500, "Szum");
            courseGraph2.Signal = signal2;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            spectrumGraph2 = new SpectrumGraph(520, 254, 220, 500, "Widmo szumu");
            spectrumGraph2.Signal = signal2;
            spectrumGraph2.DFT();
            spectrumGraph2.Autoscale();
            spectrumGraph2.DrawGraph();


            signal3 = new AnalogSignal(1, 300, 1000, 0.5).IIR(signal2, signal2.GenerateFilter_HighBand_IIR());
            courseGraph3 = new CourseGraph(10, 484, 220, 500, "Przebieg po filtracji");
            courseGraph3.Signal = signal3;
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();

            spectrumGraph3 = new SpectrumGraph(520, 484, 220, 500, "Widmo po filtracji");
            spectrumGraph3.Signal = signal3;
            spectrumGraph3.DFT();
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();
        }
        private void initialize_generateFilter_2()
        {
            AnalogSignal delta = new AnalogSignal(1, 300, 1000, 0.5).GenerateDelta();
            signal1 = new AnalogSignal(1, 300, 1000, 0.5).IIR(delta, delta.GenerateFilter_IIR());
            courseGraph1 = new CourseGraph(10, 24, 220, 500, "Przebieg charakterystyki");
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            spectrumGraph1 = new SpectrumGraph(520, 24, 220, 700, "Odpowiedź filtru");
            spectrumGraph1.Signal = signal1;
            spectrumGraph1.DFT();
            spectrumGraph1.Autoscale();
            spectrumGraph1.DrawGraph();


            signal2 = new AnalogSignal(1, 100, 1000, 0.5).GenerateNoise(0.5);
            courseGraph2 = new CourseGraph(10, 254, 220, 500, "Szum");
            courseGraph2.Signal = signal2;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            spectrumGraph2 = new SpectrumGraph(520, 254, 220, 500, "Widmo szumu");
            spectrumGraph2.Signal = signal2;
            spectrumGraph2.DFT();
            spectrumGraph2.Autoscale();
            spectrumGraph2.DrawGraph();


            signal3 = new AnalogSignal(1, 100, 1000, 0.5).IIR(signal2, signal2.GenerateFilter_IIR());
            courseGraph3 = new CourseGraph(10, 484, 220, 500, "Przebieg po filtracji");
            courseGraph3.Signal = signal3;
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();

            spectrumGraph3 = new SpectrumGraph(520, 484, 220, 500, "Widmo po filtracji");
            spectrumGraph3.Signal = signal3;
            spectrumGraph3.DFT();
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();
        }

        private void initialize_generateFilter_1()
        {
            signal1 = new AnalogSignal(1, 300, 1000, 0.5).GenerateFilter1();
            courseGraph1 = new CourseGraph(10, 24, 220, 500, "Przebieg charakterystyki");
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            spectrumGraph1 = new SpectrumGraph(520, 24, 220, 700, "Odpowiedź filtru");
            spectrumGraph1.Signal = signal1;
            spectrumGraph1.DFT();
            spectrumGraph1.Autoscale();
            spectrumGraph1.DrawGraph();


            signal2 = new AnalogSignal(1, 100, 1000, 0.5).GenerateNoise(0.5);
            courseGraph2 = new CourseGraph(10, 254, 220, 500, "Szum");
            courseGraph2.Signal = signal2;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            spectrumGraph2 = new SpectrumGraph(520, 254, 220, 500, "Widmo szumu");
            spectrumGraph2.Signal = signal2;
            spectrumGraph2.DFT();
            spectrumGraph2.Autoscale();
            spectrumGraph2.DrawGraph();


            signal3 = new AnalogSignal(1, 100, 1000, 0.5).Weave(signal1, signal2);
            courseGraph3 = new CourseGraph(10, 484, 220, 500, "Przebieg po filtracji");
            courseGraph3.Signal = signal3;
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();

            spectrumGraph3 = new SpectrumGraph(520, 484, 220, 500, "Widmo po filtracji");
            spectrumGraph3.Signal = signal3;
            spectrumGraph3.DFT();
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();
        }

        /// <summary>
        /// Filtracja szumu
        /// </summary>
        private void initialize_FilteringOfNoise()
        {
            signal1 = new AnalogSignal(1, 300, 1000, 0.5).GenerateSincSignal(19);
            courseGraph1 = new CourseGraph(10, 24, 220, 500, "Sygnał Sinc");
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            spectrumGraph1 = new SpectrumGraph(520, 24, 220, 500, "Widmo Sinc");
            spectrumGraph1.Signal = signal1;
            spectrumGraph1.DFT();
            spectrumGraph1.Autoscale();
            spectrumGraph1.DrawGraph();

            signal2 = new AnalogSignal(1, 100, 1000, 0.5).GenerateNoise(0.5);
            courseGraph2 = new CourseGraph(10, 254, 220, 500, "Sygnał Delta");
            courseGraph2.Signal = signal2;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();


            spectrumGraph2 = new SpectrumGraph(520, 254, 220, 500, "Widmo delta");
            spectrumGraph2.Signal = signal2;
            spectrumGraph2.DFT();
            spectrumGraph2.Autoscale();
            spectrumGraph2.DrawGraph();

            signal3 = new AnalogSignal(1, 100, 1000, 0.5).Weave(signal1, signal2);
            courseGraph3 = new CourseGraph(10, 484, 220, 500, "Przebieg po filtracji");
            courseGraph3.Signal = signal3;
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();


            spectrumGraph3 = new SpectrumGraph(520, 484, 220, 500, "Widmo po filtracji");
            spectrumGraph3.Signal = signal3;
            spectrumGraph3.DFT();
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();

        }
        /// <summary>
        /// Filtracja sygnału delta
        /// </summary>
        private void initialize_Sinc()
        {
            signal1 = new AnalogSignal(1, 100, 1000, 0.5).GenerateSincSignal(5);
            courseGraph1 = new CourseGraph(10, 24, 220, 500, "Sygnał Sinc");
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();


            spectrumGraph1 = new SpectrumGraph(520, 24, 220, 500, "Widmo Sinc");
            spectrumGraph1.Signal = signal1;
            spectrumGraph1.DFT();
            spectrumGraph1.Autoscale();
            spectrumGraph1.DrawGraph();

            signal2 = new AnalogSignal(1, 100, 1000, 0.5).GenerateDelta();
            courseGraph2 = new CourseGraph(10, 254, 220, 500, "Sygnał Delta");
            courseGraph2.Signal = signal2;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();


            spectrumGraph2 = new SpectrumGraph(520, 254, 220, 500, "Widmo delta");
            spectrumGraph2.Signal = signal2;
            spectrumGraph2.DFT();
            spectrumGraph2.Autoscale();
            spectrumGraph2.DrawGraph();

            signal3 = new AnalogSignal(1, 100, 1000, 0.5).Weave(signal2, signal1);
            courseGraph3 = new CourseGraph(10, 484, 220, 500, "Przebieg odpowiedzi impulsowej");
            courseGraph3.Signal = signal3;
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();


            spectrumGraph3 = new SpectrumGraph(520, 484, 220, 500, "Widmo odpowiedzi impulsowej");
            spectrumGraph3.Signal = signal3;
            spectrumGraph3.DFT();
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();

        }

        /// <summary>
        /// Filtracja składowej sygnału
        /// </summary>
        private void initialize_IDFT()
        {
            
            signal1 =
                new AnalogSignal(1, 110, 1000, 0.5).GenerateSinusSignal()
                + new AnalogSignal(2, 285, 1000, 0.5).GenerateCosinusSignal()
                + new AnalogSignal(3, 155, 1000, 0.5).GenerateSinusSignal();

            signal2 = new AnalogSignal(3, 155, 1000, 0.5).GenerateSinusSignal();

            courseGraph1 = new CourseGraph(10, 24, 220, 500, "Sygnał 1.");
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            spectrumGraph1 = new SpectrumGraph(520, 24, 200, 500, "Widmo sygnału");
            spectrumGraph1.Signal = signal1;
            spectrumGraph1.Autoscale();
            spectrumGraph1.DFT();
            spectrumGraph1.DrawGraph();

            courseGraph2 = new CourseGraph(10, 254, 220, 500, "Sygnał po iDFT");
            courseGraph2.Signal = new AnalogSignal(1, 110, 1000, 0.5).iDFT(spectrumGraph1.complexFrequency, ft);
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            spectrumGraph2 = new SpectrumGraph(520, 254, 200, 500, "Widmo sygnału");
            spectrumGraph2.Signal = courseGraph2.Signal;
            spectrumGraph2.Autoscale();
            spectrumGraph2.DFT();
            spectrumGraph2.DrawGraph();

            spectrumGraph3 = new SpectrumGraph(520, 484, 200, 500, "Widmo sygnału");
            spectrumGraph3.Signal = signal1;
            spectrumGraph3.DFT();
            spectrumGraph3.filtering(signal2);
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();

            courseGraph3 = new CourseGraph(10, 484, 220, 500, "Sygnał po iDFT");
            courseGraph3.Signal = new AnalogSignal(1, 110, 1000, 0.5).iDFT(spectrumGraph3.complexFrequency, ft);
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();
        }

        /// <summary>
        /// Korelacja wzajemna
        /// </summary>
        private void initialize_Correlation_2()
        {
            // inicjalizacja obiektow klasy wykresow przebiegu sygnalu
            courseGraph1 = new CourseGraph(10, 24, 220, 500, "Sygnał 1.");
            courseGraph2 = new CourseGraph(10, 254, 220, 500, "Sygnał 2.");
            courseGraph3 = new CourseGraph(10, 484, 220, 500, "Korelacja");

            // inicjalizacja obiektow klasy sygnalow
            signal1 = new AnalogSignal(20, 10, 1000, 0.5).GeneratePeak(0.5, 0.01);
            signal2 = (new AnalogSignal(20, 10, 1000, 0.5).GenerateNoise(0.5)) + ((new AnalogSignal(20, 10, 1000, 0.1)) | signal1);
            

            // rysowanie wykresow sygnalu 1.
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();
            

            // rysowanie wykresow sygnalu 2.
            courseGraph2.Signal = signal2;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            courseGraph3.Signal = (new AnalogSignal(20, 100, 1000, 0.5)).Correlation(signal1, signal2);
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();
        }

        /// <summary>
        /// Autokorelacja
        /// </summary>
        private void initialize_Correlation_1()
        {
            // inicjalizacja obiektow klasy wykresow przebiegu sygnalu
            courseGraph1 = new CourseGraph(10, 24, 220, 500, "Sygnał 1.");
            courseGraph2 = new CourseGraph(10, 254, 220, 500, "Sygnał 2.");
            courseGraph3 = new CourseGraph(10, 484, 220, 500, "Korelacja");

            // inicjalizacja obiektow klasy sygnalow
            signal1 = (new AnalogSignal(20, 20, 1000, 0.5).GenerateNoise(50)) + (new AnalogSignal(20, 20, 1000, 0.5).GenerateSinusSignal());
            signal2 = signal1;// new AnalogSignal(20, 10, 1000, 0.5).GenerateSinusSignal();

            // rysowanie wykresow sygnalu 1.
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            // rysowanie wykresow sygnalu 2.
            courseGraph2.Signal = signal2;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            courseGraph3.Signal = (new AnalogSignal(20, 10, 1000, 0.5)).Correlation(signal1, signal1);
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();
        }

        /// <summary>
        /// Funkcja inicjalizujące zadanie z przedstawieniem przebiegów prostokątnego, trójkątnego 
        /// i piłokszatnego, oraz wylicznienie MSE
        /// </summary>
        private void initialize_MSEPlot()
        {
            MSEPlot mse1 = new MSEPlot(5, 24, 200, 500, "MSE p. piłokształtnego", MSEPlot.type.Normal);
            mse1.GenerateValues(MSEPlot.Approx.Sawer, 100, 20, 5, 1000, 0.5);
            mse1.DrawGraph();
            mse1.Autoscale();
            Controls.Add(mse1);

            MSEPlot mse2 = new MSEPlot(5, 234, 200, 500, "MSE p. prostokątnego", MSEPlot.type.Normal);
            mse2.Autoscale();
            mse2.GenerateValues(MSEPlot.Approx.Square, 100, 20, 5, 1000, 0.5);
            mse2.DrawGraph();
            Controls.Add(mse2);

            MSEPlot mse3 = new MSEPlot(5, 444, 200, 500, "MSE p. trójkątnego", MSEPlot.type.Normal);
            mse3.GenerateValues(MSEPlot.Approx.Triangle, 100, 20, 5, 1000, 0.5);
            mse3.Autoscale();
            mse3.DrawGraph();
            Controls.Add(mse3);
        }
        private void initialize_MSE()
        {
            MSEGraph mse1 = new MSEGraph(5, 24, 200, 500, "MSE p. piłokształtnego");
            mse1.Signal = new AnalogSignal(20, 5, 1000, 0.5).GenerateSawSignal(20);
            mse1.Model = new AnalogSignal(20, 5, 1000, 0.5).GenerateModelSawer(mse1.Signal.Amplitude);
            mse1.DrawGraph();
            Controls.Add(mse1);

            MSEGraph mse2 = new MSEGraph(5, 234, 200, 500, "MSE p. prostokątnego");
            mse2.Signal = new AnalogSignal(20, 5, 1000, 0.5).GenerateSquareSignal(20);
            mse2.Model = new AnalogSignal(20, 5, 1000, 0.5).GenerateModelSquare();
            mse2.DrawGraph();
            Controls.Add(mse2);

            MSEGraph mse3 = new MSEGraph(5, 444, 200, 500, "MSE p. trójkątnego");
            mse3.Signal = new AnalogSignal(10, 5, 1000, 0.5).GenerateTriangleSignal(100);
            mse3.Model = new AnalogSignal(10, 5, 1000, 0.5).GenerateModelTriangle(mse3.Signal.Amplitude*3.2);
            mse3.DrawGraph();
            Controls.Add(mse3);
        }

        /// <summary>
        /// Funkcja inicjalizująca zadanie z przedstawieniem przebiegów prostokątnego, piłokształtnego oraz trójkątnego
        /// </summary>
        private void initialize_SquareTriangleSaw()
        {
            signal1 = new AnalogSignal(20, 10, 1000, 0.5).GenerateSquareSignal(10);

            courseGraph1 = new CourseGraph(5, 24, 200, 500, "Przebieg prostokątny");
            courseGraph1.Signal = signal1;
            courseGraph1.Limit(100); // 100%
            courseGraph1.DrawGraph();

            signal2 = new AnalogSignal(10, 10, 1000, 0.5).GenerateTriangleSignal(10);

            courseGraph2 = new CourseGraph(5, 234, 200, 500, "Przebieg trójkątny");
            courseGraph2.Signal = signal2;
            courseGraph2.Limit(100); // 100%
            courseGraph2.DrawGraph();

            signal3 = new AnalogSignal(10, 10, 1000, 0.5).GenerateSawSignal(10);

            courseGraph3 = new CourseGraph(5, 444, 200, 500, "Przebieg piłokształtny");
            courseGraph3.Signal = signal3;
            courseGraph3.Limit(100); // 100%
            courseGraph3.DrawGraph();


            spectrumGraph1 = new SpectrumGraph(520, 24, 200, 500, "Widmo prostokątnego");
            spectrumGraph2 = new SpectrumGraph(520, 234, 200, 500, "Widmo trójkątnego");
            spectrumGraph3 = new SpectrumGraph(520, 444, 200, 500, "Widmo piłokształtnego");

            spectrumGraph1.Signal = signal1;
            spectrumGraph1.Autoscale();
            spectrumGraph1.DFT();
            spectrumGraph1.DrawGraph();

            spectrumGraph2.Signal = signal2;
            spectrumGraph2.Autoscale();
            spectrumGraph2.DFT();
            spectrumGraph2.DrawGraph();

            spectrumGraph3.Signal = signal3;
            spectrumGraph3.Autoscale();
            spectrumGraph3.DFT();
            spectrumGraph3.DrawGraph();
        }


        /// <summary>
        /// Funkcja inicjalizujaca sygnaly i ich wykresy dla lab5 dla demodulacji FSK
        /// </summary>
        private void initialize_Lab5_FSK()
        {
            Demodulation demodul = new Demodulation();
            string word = "10101011";
            courseGraph1 = new CourseGraph(10, 24, 150, 500, word);
            courseGraph2 = new CourseGraph(10, 184, 150, 500, "Kod FSK");
            courseGraph3 = new CourseGraph(10, 344, 150, 500, "Kod cyfrowy");

            digital = new DigitalSignal(word, 1000, 0.0625); //informacja

            // s. zmodulowany
            AnalogSignal FSK = new AnalogSignal(10, 0, 1000, 0.5).GenerateSinusSignal();
            FSK.FSK(digital, 2);

            // Rysowanie przebiegów:

            // Sygnał nośny
            courseGraph1.Signal = digital;
            courseGraph1.Limit(5000);
            courseGraph1.DrawGraph();

            //sygnal zmodulowany FSK
            courseGraph2.Signal = FSK;
            courseGraph2.Limit(5000);
            courseGraph2.DrawGraph();

            //Kod cyfrowy
            courseGraph3.Signal = demodul.demodulationFSK(FSK, 0.0625, 2); // demodulacja
            courseGraph3.Limit(5000);
            courseGraph3.DrawGraph();
        }
        /// <summary>
        /// Funkcja inicjalizujaca sygnaly i ich wykresy dla lab5 dla demodulacji PSK
        /// </summary>
        private void initialize_Lab5_PSK()
        {
            Demodulation demodul = new Demodulation();

            string word = "10101011";

            courseGraph1 = new CourseGraph(10, 24, 150, 500, word);
            courseGraph2 = new CourseGraph(10, 184, 150, 500, "Kod PSK");
            courseGraph3 = new CourseGraph(10, 344, 150, 500, "Kod cyfrowy");

            digital = new DigitalSignal(word, 1000, 0.0625); //informacja


            // s. zmodulowany
            AnalogSignal PSK = new AnalogSignal(10, 20, 1000, 0.5).GenerateSinusSignal();
            PSK.PSK(digital);


            // Rysowanie przebiegów:

            // Sygnał nośny
            courseGraph1.Signal = digital;
            courseGraph1.Limit(5000);
            courseGraph1.DrawGraph();

            //sygnal zmodulowany ASK
            courseGraph2.Signal = PSK;
            courseGraph2.Limit(5000);
            courseGraph2.DrawGraph();

            //Kod cyfrowy
            courseGraph3.Signal = demodul.demodulationASKandPSK(PSK, 0.0625, 200, false); //demodualcja
            courseGraph3.Limit(5000);
            courseGraph3.DrawGraph();
        }
        /// <summary>
        /// Funkcja inicjalizujaca sygnaly i ich wykresy dla lab5 dla demodulacji ASK
        /// </summary>
        private void initialize_Lab5_ASK()
        {
            Demodulation demodul = new Demodulation();
            string word = "10101011";
            courseGraph1 = new CourseGraph(10, 24, 150, 500, "Nośna");
            courseGraph2 = new CourseGraph(10, 184, 150, 500, word);
            courseGraph3 = new CourseGraph(10, 344, 150, 500, "Kod ASK");

            signal1 = new AnalogSignal(7, 32.5, 1000, 0.5); // nośna
            digital = new DigitalSignal(word, 1000, 0.0625); //informacja

            // s. nośny
            signal1.GenerateSinusSignal();

            // s. zmodulowany
            AnalogSignal ASK = new AnalogSignal(signal1);
            ASK.ASK(signal1, digital);

            // Rysowanie przebiegów:

            // Sygnał nośny
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            //sygnal zmodulowany ASK
            courseGraph2.Signal = ASK;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            //Kod cyfrowy
            courseGraph3.Signal =  demodul.demodulationASKandPSK(ASK, 0.0625, 200, true); // demodulacja
            courseGraph3.Autoscale();
            courseGraph3.DrawGraph();
        }
        /// <summary>
        /// Funkcja inicjalizujaca sygnaly i ich wykresy dla lab4 dla modulacji FSK i PSK
        /// </summary>
        private void initialize_Lab4_FSK_PSK()
        {
            // inicjalizacja obiektow klasy wykresow przebiegu sygnalu
            courseGraph1 = new CourseGraph(10, 24, 150, 500, "Sygnał Cyfrowy");
            courseGraph2 = new CourseGraph(10, 184, 150, 500, "FSK");
            courseGraph3 = new CourseGraph(10, 344, 150, 500, "PSK");

            // inicjalizacja obiektow klasy wykresow widma
            spectrumGraph2 = new SpectrumGraph(520, 184, 150, 500, "FSK");
            spectrumGraph3 = new SpectrumGraph(520, 344, 150, 500, "PSK");

            // inicjalizacja obiektow klasy sygnalow
            digital = new DigitalSignal("10101011", 1000, 0.0625);

            // rysowanie wykresow sygnalu 2.
            courseGraph1.Signal = digital;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            // deklaracja i inicjalizacja obiektu sygnału zmodulowanego FSK

            AnalogSignal FSK = new AnalogSignal(new AnalogSignal(0, 0, 1000, 0.5));
            FSK.FSK(digital, 1);

            //rysowanie wykresów ASK
            courseGraph2.Signal = FSK;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            spectrumGraph2.Signal = FSK;
            spectrumGraph2.Autoscale();
            spectrumGraph2.DrawGraph();

            // deklaracja i inicjalizacja obiektu sygnału zmodulowanego PSK

            AnalogSignal PSK = new AnalogSignal(new AnalogSignal(0, 0, 1000, 0.5));
            PSK.PSK(digital);

            //rysowanie wykresów ASK
            courseGraph3.Signal = PSK;
            courseGraph3.Limit(5000);
            courseGraph3.DrawGraph();

            spectrumGraph3.Signal = PSK;
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();
        }
        
        /// <summary>
        /// Funkcja inicjalizujaca sygnaly i ich wykresy dla lab4 dla modulacji ASK
        /// </summary>
        private void initialize_Lab4_ASK()
        {
            // inicjalizacja obiektow klasy wykresow przebiegu sygnalu
            courseGraph1 = new CourseGraph(10, 24, 150, 500, "Sygnał Cyfrowy");
            courseGraph2 = new CourseGraph(10, 184, 150, 500, "Nosna");
            courseGraph3 = new CourseGraph(10, 344, 150, 500, "ASK");

            // inicjalizacja obiektow klasy wykresow widma
            spectrumGraph2 = new SpectrumGraph(520, 184, 150, 500, "Nośna");
            spectrumGraph3 = new SpectrumGraph(520, 344, 150, 500, "ASK");

            // inicjalizacja obiektow klasy sygnalow
            signal1 = new AnalogSignal(50, 100, 1000, 1); // nośna
            digital = new DigitalSignal("10101011", 1000, 0.125);

            // Generowanie sygnalow na podstawie 
            // podanych w konstruktorze parametrow
            signal1.GenerateSinusSignal();

            // rysowanie wykresow sygnalu 2.
            courseGraph1.Signal = digital;
            courseGraph1.Limit(5000);
            courseGraph1.DrawGraph();

            // rysowanie wykresow sygnalu 1.
            courseGraph2.Signal = signal1;
            courseGraph2.DrawGraph();

            spectrumGraph2.setSignal(signal1);
            spectrumGraph2.Autoscale();
            spectrumGraph2.DrawGraph();

            

            // deklaracja i inicjalizacja obiektu sygnału zmodulowanego ASK

            AnalogSignal ASK = new AnalogSignal(signal1);
            ASK.ASK(signal1, digital);

            //rysowanie wykresów ASK
            courseGraph3.Signal = ASK;
            courseGraph3.DrawGraph();

            spectrumGraph3.setSignal(ASK);
            spectrumGraph3.Autoscale();
            spectrumGraph3.DrawGraph();


        }

        /// <summary>
        /// Funkcja inicjalizujaca sygnaly i ich wykresy dla lab3
        /// </summary>
        private void initialize_Lab3()
        {
            // inicjalizacja obiektow klasy wykresow przebiegu sygnalu
            courseGraph1 = new CourseGraph(10, 24, 150, 500, "Nośna");
            courseGraph2 = new CourseGraph(10, 184, 150, 500, "Sygnał informacyjny");
            courseGraph3 = new CourseGraph(10, 344, 150, 500, "AM");
            courseGraph4 = new CourseGraph(10, 504, 150, 500, "PM");

            // inicjalizacja obiektow klasy wykresow widma
            spectrumGraph1 = new SpectrumGraph(520, 24, 150, 500, "Nośna");
            spectrumGraph2 = new SpectrumGraph(520, 184, 150, 500, "Sygnał informacyjny");
            spectrumGraph3 = new SpectrumGraph(520, 344, 150, 500, "AM");
            spectrumGraph4 = new SpectrumGraph(520, 504, 150, 500, "PM");

            // inicjalizacja obiektow klasy sygnalow
            signal1 = new AnalogSignal(50, 100, 1000, 0.5); // nośna
            signal2 = new AnalogSignal(50, 10, 1000, 0.5); // sygnał informacyjny

            // Generowanie sygnalow na podstawie 
            // podanych w konstruktorze parametrow
            signal1.GenerateSinusSignal();
            signal2.GenerateSinusSignal();

            // rysowanie wykresow sygnalu 1.
            courseGraph1.Signal = signal1;
            courseGraph1.DrawGraph();

            spectrumGraph1.Signal = signal1;
            spectrumGraph1.Limit(2);
            spectrumGraph1.DrawGraph();

            // rysowanie wykresow sygnalu 2.
            courseGraph2.Signal = signal2;
            courseGraph2.DrawGraph();

            spectrumGraph2.Signal = signal2;
            spectrumGraph2.Limit(2);
            spectrumGraph2.DrawGraph();

            // rysowanie wykresow AM
            AnalogSignal AM = new AnalogSignal();
            AM.AmplitudeModulation(signal1, signal2, 0.01);

            courseGraph3.Signal = AM;
            courseGraph3.Limit(1500);
            courseGraph3.DrawGraph();

            spectrumGraph3.Signal = AM;
            spectrumGraph3.Limit(15);
            spectrumGraph3.DrawGraph();

            //rysowanie wykresow PW
            AnalogSignal PM = new AnalogSignal();
            PM.PhaseModulation(signal1, signal2, 0.5);

            courseGraph4.Signal = PM;
            courseGraph4.DrawGraph();

            spectrumGraph4.Signal = PM;
            spectrumGraph4.Limit(2);
            spectrumGraph4.DrawGraph();
        }

        /// <summary>
        /// Funkcja inicjalizujaca sygnaly i ich wykresy dla lab1 i lab2
        /// </summary>
        private void initialize_Lab1_Lab2()
        {
            // inicjalizacja obiektow klasy wykresow przebiegu sygnalu
            courseGraph1 = new CourseGraph(10, 24, 150, 500, "Sygnał 1.");
            courseGraph2 = new CourseGraph(10, 184, 150, 500, "Sygnał 2.");
            courseGraph3 = new CourseGraph(10, 344, 150, 500, "Suma");
            courseGraph4 = new CourseGraph(10, 504, 150, 500, "Iloczyn");

            // inicjalizacja obiektow klasy wykresow widma
            spectrumGraph1 = new SpectrumGraph(520, 24, 150, 500, "Sygnał 1.");
            spectrumGraph2 = new SpectrumGraph(520, 184, 150, 500, "Sygnał 2.");
            spectrumGraph3 = new SpectrumGraph(520, 344, 150, 500, "Suma");
            spectrumGraph4 = new SpectrumGraph(520, 504, 150, 500, "Iloczyn");

            // inicjalizacja obiektow klasy sygnalow
            signal1 = new AnalogSignal(20, 100, 1000, 0.5);
            signal2 = new AnalogSignal(50, 10, 1000, 0.5);

            // Generowanie sygnalow na podstawie 
            // podanych w konstruktorze parametrow
            signal1.GenerateSinusSignal();
            signal2.GenerateSinusSignal();

            // rysowanie wykresow sygnalu 1.
            courseGraph1.Signal = signal1;
            courseGraph1.Autoscale();
            courseGraph1.DrawGraph();

            spectrumGraph1.Signal = signal1;
            spectrumGraph1.Autoscale();
            spectrumGraph1.DFT();
            spectrumGraph1.DrawGraph();

            // rysowanie wykresow sygnalu 2.
            courseGraph2.Signal = signal2;
            courseGraph2.Autoscale();
            courseGraph2.DrawGraph();

            spectrumGraph2.Signal = signal2;
            spectrumGraph2.Autoscale();
            spectrumGraph2.DFT();
            spectrumGraph2.DrawGraph();

           // rysowanie wykresow sumy sygnalow
          Signal sumOfSignals = signal1 + signal2;

          courseGraph3.Signal = sumOfSignals;
          courseGraph3.Autoscale();
          courseGraph3.DrawGraph();

          spectrumGraph3.Signal = sumOfSignals;
          spectrumGraph3.Autoscale();
          spectrumGraph3.DFT();
          spectrumGraph3.DrawGraph();
             
             
          // rysowanie wykresow iloczynu sygnalow
          Signal productOfSignals = signal1 * signal2;

          courseGraph4.Signal = productOfSignals;
          courseGraph4.Autoscale();
          courseGraph4.DrawGraph();

          spectrumGraph4.Signal = productOfSignals;
          spectrumGraph4.Autoscale();
          spectrumGraph4.DFT();
          spectrumGraph4.DrawGraph();

        }
        private void stwórzNowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditingWindow editingWindow = new EditingWindow();
            editingWindow.ShowDialog(this);
            editingWindow.Dispose();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void lab1Ilab2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(1);
        }

        private void lab3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(2);
        }

        private void lab4ASKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(3);
        }

        private void lab4FSKIPSKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(4);
        }

        private void lab5demodulacjaASKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(5);
        }

        private void lab5demodulacjaPSKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(6);
        }

        private void lab5demodulacjaFSKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(7);
        }

        private void aproksymacjaPrzebiegówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(8);
        }

        private void mSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(9);
        }

        private void zmianyMSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(10);
        }

        private void autokorelacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(11);
        }

        private void korelacjaWzajemnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(12);
        }

        private void filtracjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(13);
        }

        private void filtracjaDeltyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(14);
        }

        private void filtracjaSzumuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialize(15);
        }

    }
}
