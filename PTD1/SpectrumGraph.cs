using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace PTD1
{
    /// <summary>
    /// Klasa reprezentujaca wykres widma amplitudowego sygnalu.
    /// Dziedziczaca po SignalGraph.
    /// </summary>
    class SpectrumGraph : SignalGraph
    {
        #region Private Fields
        /// <summary>
        /// Reprezentacja zespolona sygnalu
        /// </summary>
        public Complex[] complexFrequency = null;

        private FourierTransform FT;

        #endregion //end Private Fields

        #region Private Methods

        /// <summary>
        /// Zwraca element z tablicy zawierającej informacje o częstotliwości
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override double getData(int index)
        {
            return complexFrequency[index].Magnitude;
        }

        #endregion //end Private Methods

        #region Public Methods
        /// <summary>
        /// Metoda obliczajaca widmo sygnalu
        /// </summary>
        public void DFT()
        {
            LengthOfBuffer = Convert.ToInt32(Signal.SamplingFrequency / 2);
            this.complexFrequency = FT.DFT(this.Signal);
            MaxFromBuffor = FT.maxFrequency;
        }

        /*public void FFT()
        {
            LengthOfBuffer = Convert.ToInt32(Signal.SamplingFrequency / 4);
            int N = Convert.ToInt32(Signal.SamplingFrequency / 2);
            this.complexFrequency = FT.FFT(Signal.Course, N);

            double max = 0;
            for (int n = 0; n < N; n++)
            {
                if (complexFrequency[n].Magnitude > max)
                    max = complexFrequency[n].Magnitude;
            }
            MaxFromBuffor = max;
        }
         * */
        

        /// <summary>
        /// Ustawia wartość dla signal
        /// </summary>
        /// <param name="sourceSignal"></param>
        public override void setSignal(Signal sourceSignal)
        {
            this.signal = sourceSignal;
            DFT();
        }

        /// <summary>
        /// Odejmowanie widm
        /// </summary>
        /// <param name="characteristicOfFilter"></param>
        /// <returns></returns>
        public SpectrumGraph filtering(AnalogSignal subtractedSignal)
        {
            Complex[] filter = FT.DFT(subtractedSignal);
            for (int i = 0; i < LengthOfBuffer; i++)
            {
                complexFrequency[i] -= filter[i];
            }
            return this;
        }

        #endregion // end Public Methods

        #region Constructor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public SpectrumGraph(int x, int y, int Height, int Width, string description) 
            : base(x, y, Height, Width,"Frequency","Amplitude", description, "SPECTRUM") 
        {
            FT = new FourierTransform();

        }

        #endregion //end Constructor

        #region Events

        /// <summary>
        /// Zdarzenie rysujace sygnal na wykresie
        /// </summary>
        protected override void DrawPlotEvent(object sender, PaintEventArgs e)
        {
            
            graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            int N = Convert.ToInt32(Signal.SamplingFrequency / 2);
            double y1, y2; // poczatek i koniec lini w ukladzie wspolzednych na osi y
            AutoscaleObserver();
            for (int iterator = 1; iterator < (horizontialZoom/100)*this.LengthOfBuffer; iterator++)
            {
                y1 = complexFrequency[N - (iterator + shift)].Magnitude * yPercent*2 + RealHeightOfChart;
                y2 = complexFrequency[N - (iterator + shift) - 1].Magnitude * yPercent*2 + RealHeightOfChart;

                if(Double.IsNaN(y1)) y1 = 0;
                if(Double.IsNaN(y2)) y2 = 0;


                //graphics.DrawLine(pen, iterator*2 - 1, Height - Convert.ToInt32(y1), iterator*2, Height - Convert.ToInt32(y2));
                if (horizontialZoom < 100)
                {
                    graphics.DrawLine(pen, (int)((iterator * 2 - 1) * (100 / horizontialZoom)) + widthOfVerticalAxis + leftPadding, RealHeightOfChart * 2 - Convert.ToInt32(y1), (int)(iterator * 2 * (100 / horizontialZoom)) + widthOfVerticalAxis + leftPadding, RealHeightOfChart * 2 - Convert.ToInt32(y2));
                }

                else
                    graphics.DrawLine(pen, (iterator * 2 - 1) + widthOfVerticalAxis + leftPadding, RealHeightOfChart*2 - Convert.ToInt32(y1), (iterator * 2) + widthOfVerticalAxis + leftPadding, RealHeightOfChart*2 - Convert.ToInt32(y2));
                if ((iterator - shift) > (100.0 / horizontialZoom) * Signal.LengthOfBuffer)
                    break;
            }
        }

        /// <summary>
        /// Zdarzenie przygotowuje pusty wykres z podpisanymi osiami
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
            TextRenderer.DrawText(graphics, xLabel, this.Font, new Point(RealWidthOfChart-30, RealHeightOfChart + 2), Color.Green);

            //podpis osi pionowej
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            e.Graphics.DrawString(yLabel, this.Font, new SolidBrush(Color.Green), new Point(0, ((RealHeightOfChart) / 2) - 30), stringFormat);

            //Nazwa wykresu
            TextRenderer.DrawText(graphics, description, this.Font, new Point((Width / 2) + leftPadding, 0), Color.Green);
        }

        #endregion //end Events

    }
}
