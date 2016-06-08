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
    /// Klasa reprezentujaca wykresy przebiegu wzorowego z porównywanym.
    /// Dziedziczaca po SignalGraph.
    /// </summary>
    class MSEGraph : SignalGraph
    {
        #region Private Fileds

        /// <summary>
        /// Pole reprezentujące sygnał zworowy
        /// </summary>
        private Signal model;

        /// <summary>
        /// Wartość MSE
        /// </summary>
        private double MSE;

        #endregion //end Private Fileds

        #region Events

        /// <summary>
        /// Zdarzenie rysujace sygnal na wykresie
        /// </summary>
        protected override void DrawPlotEvent(object sender, PaintEventArgs e)
        {
            CalculateMSE();
            graphics = e.Graphics;
            Pen pen_sygnal = new Pen(Color.Blue);
            Pen pen_model = new Pen(Color.Red);
            LengthOfBuffer = this.Signal.LengthOfBuffer;
            MaxFromBuffor = this.Signal.Course.Max();
            int sy1, sy2, my1, my2; // poczatek i koniec lini w ukladzie wspolzednych y

            AutoscaleObserver();
            for (int iterator = 1; iterator < (horizontialZoom / 100) * this.LengthOfBuffer; iterator++)
            {

                // Signal
                sy1 = Convert.ToInt32(signal.Course[shift + iterator - 1] * yPercent * (-1)) + RealHeightOfChart / 2;
                sy2 = Convert.ToInt32(signal.Course[shift + iterator] * yPercent * (-1)) + RealHeightOfChart / 2;

                // Model
                my1 = Convert.ToInt32(model.Course[shift + iterator - 1] * yPercent * (-1)) + RealHeightOfChart / 2;
                my2 = Convert.ToInt32(model.Course[shift + iterator] * yPercent * (-1)) + RealHeightOfChart / 2;


                if (horizontialZoom < 100)
                {
                    /// Rysowanie lini sygnału
                    graphics.DrawLine(pen_sygnal, (int)((iterator - 1) * (100 / horizontialZoom)) + widthOfVerticalAxis + leftPadding, sy1, (int)(iterator * (100 / horizontialZoom)) + widthOfVerticalAxis + leftPadding, sy2);

                    /// Rysowanie lini modelu
                    graphics.DrawLine(pen_model, (int)((iterator - 1) * (100 / horizontialZoom)) + widthOfVerticalAxis + leftPadding, my1, (int)(iterator * (100 / horizontialZoom)) + widthOfVerticalAxis + leftPadding, my2);
                }

                else
                {
                    /// Rysowanie lini sygnału
                    graphics.DrawLine(pen_sygnal, iterator - 1 + widthOfVerticalAxis + leftPadding, sy1, iterator + widthOfVerticalAxis + leftPadding, sy2);

                    /// Rysowanie lini modelu
                    graphics.DrawLine(pen_model, iterator - 1 + widthOfVerticalAxis + leftPadding, my1, iterator + widthOfVerticalAxis + leftPadding, my2);
                }
                if ((iterator - shift) > (100.0 / horizontialZoom) * Signal.LengthOfBuffer)
                    break;
            }

            /// Podpisywanie MSE
            TextRenderer.DrawText(graphics, "MSE = " + MSE.ToString(), this.Font, new Point(10, Height - 20), Color.Green);
        }

        /// <summary>
        /// Zdarzenie rysujace pusty wykres z podpisanymi osiami
        /// </summary>
        protected override void PrepareChartEvent(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            Pen pen = new Pen(Color.LightGreen);

            //pozioma linia punktu odniesienia
            graphics.DrawLine(pen, leftPadding, RealHeightOfChart / 2, Width, RealHeightOfChart / 2);

            //pozioma oś
            graphics.DrawLine(pen, leftPadding, RealHeightOfChart + 2, Width, RealHeightOfChart + 2);
            graphics.DrawLine(pen, leftPadding, RealHeightOfChart + 1, Width, RealHeightOfChart + 1);
            graphics.DrawLine(pen, leftPadding, RealHeightOfChart, Width, RealHeightOfChart);

            //pionowa oś
            graphics.DrawLine(pen, leftPadding, 0, leftPadding, RealHeightOfChart);
            graphics.DrawLine(pen, leftPadding + 1, 0, leftPadding + 1, RealHeightOfChart);
            graphics.DrawLine(pen, leftPadding + 2, 0, leftPadding + 2, RealHeightOfChart);


            //podpis osi poziomej
            TextRenderer.DrawText(graphics, xLabel, this.Font, new Point(RealWidthOfChart, RealHeightOfChart + 2), Color.Green);

            //podpis osi pionowej
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            e.Graphics.DrawString(yLabel, this.Font, new SolidBrush(Color.Green), new Point(0, ((RealHeightOfChart) / 2) - 30), stringFormat);

            //Nazwa wykresu
            TextRenderer.DrawText(graphics, description, this.Font, new Point((Width / 2) + leftPadding, 0), Color.Green);

            //punkt odniesienia
            stringFormat = new StringFormat();
            e.Graphics.DrawString("0", this.Font, new SolidBrush(Color.Black), new Point(leftPadding - 10, (RealHeightOfChart / 2 - 6)), stringFormat);

            e.Graphics.DrawString("Max = " + Signal.Course.Max().ToString("E4"), this.Font, new SolidBrush(Color.Black), new Point(leftPadding, RealHeightOfChart + 2), stringFormat);
        }

        #endregion //end Events

        #region Constructor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MSEGraph(int x, int y, int Height, int Width, string description)
            : base(x, y, Height, Width, "Time", "Amplitude", description, "MODEL_COURSE") { }

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
        private void CalculateMSE()
        {
            double mse = 0;
            for (int i = 0; i < signal.Course.Length; i++)
            {
                mse += (signal.Course[i] - model.Course[i])*(signal.Course[i] - model.Course[i]);
            }
            MSE = mse;
        }

        protected override double getData(int index)
        {
            return signal.Course[index];
        }
       

        #endregion // end Private Methods

        #region Public Mehtods

        /// <summary>
        /// Ustawia wartość dla signal
        /// </summary>
        /// <param name="sourceSignal"></param>
        public override void setSignal(Signal sourceSignal)
        {
            this.signal = sourceSignal;
        }

        #endregion //end Public Methods

    }
}
