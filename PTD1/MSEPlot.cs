using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PTD1
{

    /// <summary>
    /// Klasa reprezentujaca wykresy zmian MSE w zależności od ilości harmonicznych w aproksymowanym sygnale.
    /// Dziedzicząca po SignalGraph.
    /// </summary>
    class MSEPlot : SignalGraph
    {
        #region Enumeration

        public enum Approx {Square, Triangle, Sawer};

        public enum type { Normal, Log };

        #endregion //end Enumeration

        #region Private Fileds

        /// <summary>
        /// Pole reprezentujące sygnał zworowy
        /// </summary>
        private Signal model;

        /// <summary>
        /// Wartość MSE
        /// </summary>
        private double MSE;

        /// <summary>
        /// Wartości MSE
        /// </summary>
        private double[] MSEValues;

        private int limitOfM;

        /// <summary>
        /// Typ notacji osi X
        /// </summary>
        private MSEPlot.type typeOfX;

        #endregion //end Private Fileds

        #region Events

        /// <summary>
        /// Zdarzenie rysujace sygnal na wykresie
        /// </summary>
        protected override void DrawPlotEvent(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);

            int y1, y2; // poczatek i koniec lini w ukladzie wspolzednych y

            AutoscaleObserver();

            for (int iterator = 1; iterator < limitOfM; iterator++)
            {
                if (typeOfX == MSEPlot.type.Log)
                {
                    y1 = Convert.ToInt32((10 * Math.Log10(MSEValues[shift + iterator - 1]) * yPercent * (-1)) + RealHeightOfChart);
                    y2 = Convert.ToInt32((10 * Math.Log10(MSEValues[shift + iterator]) * yPercent * (-1)) + RealHeightOfChart);
                }
                else
                {
                    y1 = Convert.ToInt32((MSEValues[shift + iterator - 1] * yPercent * (-1)) + RealHeightOfChart);
                    y2 = Convert.ToInt32((MSEValues[shift + iterator] * yPercent * (-1)) + RealHeightOfChart);
                }

                /// Rysowanie lini sygnału
                if (horizontialZoom < 100)
                {
                    graphics.DrawLine(pen, (int)((iterator - 1) * (100/horizontialZoom)) + widthOfVerticalAxis + leftPadding, y1, (int)(iterator * (100/horizontialZoom)) + widthOfVerticalAxis + leftPadding, y2);
                }

                else
                    graphics.DrawLine(pen, iterator - 1 + widthOfVerticalAxis + leftPadding, y1, iterator + widthOfVerticalAxis + leftPadding, y2);
                if ((iterator - shift) > (100.0 / horizontialZoom) * Signal.LengthOfBuffer)
                    break;
            }
            /// Podpisywanie MSE
            TextRenderer.DrawText(graphics, "Max MSE: " + ((int)MSEValues.Max()).ToString(), this.Font, new Point((Width / 2) + leftPadding, 15), Color.Green);
        }

        /// <summary>
        /// Zdarzenie rysujace pusty wykres z podpisanymi osiami
        /// </summary>
        protected override void PrepareChartEvent(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            Pen pen = new Pen(Color.LightGreen);

            //pozioma linia
            graphics.DrawLine(pen, leftPadding, RealHeightOfChart + 2, Width, RealHeightOfChart + 2);
            graphics.DrawLine(pen, leftPadding, RealHeightOfChart + 1, Width, RealHeightOfChart + 1);
            graphics.DrawLine(pen, leftPadding, RealHeightOfChart, Width, RealHeightOfChart);

            //pionowa linia
            graphics.DrawLine(pen, leftPadding, 0, leftPadding, RealHeightOfChart);
            graphics.DrawLine(pen, leftPadding + 1, 0, leftPadding + 1, RealHeightOfChart);
            graphics.DrawLine(pen, leftPadding + 2, 0, leftPadding + 2, RealHeightOfChart);

            //podpis osi poziomej
            TextRenderer.DrawText(graphics, xLabel, this.Font, new Point(RealWidthOfChart - 30, RealHeightOfChart + 2), Color.Green);

            //podpis osi pionowej
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            e.Graphics.DrawString(yLabel, this.Font, new SolidBrush(Color.Green), new Point(0, ((RealHeightOfChart) / 2) - 30), stringFormat);

            //Nazwa wykresu
            TextRenderer.DrawText(graphics, description, this.Font, new Point((Width / 2) + leftPadding, 0), Color.Green);
        }

        #endregion //end Events

        #region Constructor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MSEPlot(int x, int y, int Height, int Width, string description)
            : base(x, y, Height, Width, "MSE", "Number of harmonics", description, "MSE") { }

        public MSEPlot(int x, int y, int Height, int Width, string description, MSEPlot.type type)
            : base(x, y, Height, Width, "MSE", "Number of harmonics", description, "MSE") 
        {
            this.typeOfX = type;
        }

        #endregion //end Constructor

        #region MSEGraph Members

        public Signal Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
            }
        }

        #endregion //end MSEGraph Members

        #region Private Methods

        /// <summary>
        /// Oblicza MSE
        /// </summary>
        private double CalculateMSE()
        {
            double mse = 0;
            for (int i = 0; i < signal.Course.Length; i++)
            {
                mse += Math.Pow((signal.Course[i] - model.Course[i]),2);
            }
            MSE = mse;
            return mse;
        }
        protected override double getData(int index)
        {
            return signal.Course[index];
        }
        #endregion // end Private Methods

        #region Public Methods

        /// <summary>
        /// Generuje przebiegi i wypełnia tablice wartościami MSE
        /// </summary>
        /// <param name="typeOfApprox">Typ aproksymacji</param>
        /// <param name="LimitOfM">Maksymalna ilość harmonicznych</param>
        /// <param name="amplitude">Amplituda sygnałów</param>
        /// <param name="frequency">Częstotliwość sygnałów</param>
        /// <param name="samplingFrequency">Częstotliwość próbkowania sygnałów</param>
        /// <param name="lengthOfSignal">Długość sygnału</param>
        public void GenerateValues(Approx typeOfApprox, int LimitOfM, double amplitude, double frequency, double samplingFrequency, double lengthOfSignal)
        {
            this.limitOfM = LimitOfM;
            this.LengthOfBuffer = limitOfM;
            MSEValues = new double[LimitOfM];
            switch (typeOfApprox)
            {
                case Approx.Square:
                    model = new AnalogSignal(amplitude, frequency, samplingFrequency, lengthOfSignal).GenerateModelSquare();
                    for (int m = 0; m < LimitOfM; m++)
                    {
                        signal = new AnalogSignal(amplitude, frequency, samplingFrequency, lengthOfSignal).GenerateSquareSignal(m);
                        MSEValues[m] = CalculateMSE();
                    }
                    break;

                case Approx.Triangle:

                    model = new AnalogSignal(amplitude, frequency, samplingFrequency, lengthOfSignal).GenerateModelTriangle(amplitude * 3.2);
                    for (int m = 0; m < LimitOfM; m++)
                    {
                        signal = new AnalogSignal(amplitude, frequency, samplingFrequency, lengthOfSignal).GenerateTriangleSignal(m);
                        MSEValues[m] = CalculateMSE();
                    }
                    break;

                case Approx.Sawer:

                    model = new AnalogSignal(amplitude, frequency, samplingFrequency, lengthOfSignal).GenerateModelSawer(amplitude);
                    for (int m = 0; m < LimitOfM; m++)
                    {
                        signal = new AnalogSignal(amplitude, frequency, samplingFrequency, lengthOfSignal).GenerateSawSignal(m);
                        MSEValues[m] = CalculateMSE();
                    }
                    break;

                default:
                    model = new AnalogSignal(amplitude, frequency, samplingFrequency, lengthOfSignal).GenerateModelSquare();
                    for (int m = 0; m < LimitOfM; m++)
                    {
                        signal = new AnalogSignal(amplitude, frequency, samplingFrequency, lengthOfSignal).GenerateSquareSignal(m);
                        MSEValues[m] = CalculateMSE();
                    }
                    break;
            }
            MaxFromBuffor = MSEValues.Max();
        }

        /// <summary>
        /// Ustawia wartość dla signal
        /// </summary>
        /// <param name="sourceSignal"></param>
        public override void setSignal(Signal sourceSignal)
        {
            this.signal = sourceSignal;
        }

        #endregion //end Public Methods

        #region Propertis


        #endregion //end Propertis
    }
}
