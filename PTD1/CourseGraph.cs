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
    /// Klasa reprezentujaca wykres przebiegu sygnalu.
    /// Dziedziczaca po SignalGraph.
    /// </summary>
    class CourseGraph : SignalGraph
    {
        #region Private Fileds

        #endregion //end private Fileds
        #region Events

        /// <summary>
        /// Zdarzenie rysujace sygnal na wykresie
        /// </summary>
        protected override void DrawPlotEvent(object sender, PaintEventArgs e)
        {

            LengthOfBuffer = this.Signal.LengthOfBuffer;
            MaxFromBuffor = (this.Signal.Course.Max()>=Math.Abs(this.Signal.Course.Min())) ?this.Signal.Course.Max() : Math.Abs(this.Signal.Course.Min());
            graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            int y1, y2; // poczatek i koniec lini w ukladzie wspolzednych y
            AutoscaleObserver();
            for (int iterator = 1; iterator < (horizontialZoom/100) * this.LengthOfBuffer; iterator++)
            {
                y1 = Convert.ToInt32(signal.Course[shift + iterator - 1] * yPercent * (-1)) + RealHeightOfChart / 2;
                y2 = Convert.ToInt32(signal.Course[shift + iterator] * yPercent * (-1)) + RealHeightOfChart / 2;

                if (horizontialZoom < 100)
                {
                    graphics.DrawLine(pen, (int)((iterator - 1) * (100 / horizontialZoom)) + widthOfVerticalAxis + leftPadding, y1, (int)(iterator * (100 / horizontialZoom)) + widthOfVerticalAxis + leftPadding, y2);
                }
                    
                else
                    graphics.DrawLine(pen, iterator - 1 + widthOfVerticalAxis + leftPadding, y1, iterator + widthOfVerticalAxis + leftPadding, y2);
                if ((iterator-shift) > (100.0 / horizontialZoom) * Signal.LengthOfBuffer)
                    break;
            }
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
        public CourseGraph(int x, int y, int Height, int Width, string description) 
            : base(x, y, Height, Width,"Time","Amplitude", description, "COURSE") 
        {}

        #endregion //end Constructor

        #region Methods

        /// <summary>
        /// Ustawia wartość dla signal
        /// </summary>
        /// <param name="sourceSignal"></param>
        public override void setSignal(Signal sourceSignal)
        {
            this.signal = sourceSignal;
        }

        /// <summary>
        /// Zwraca próbkę z sygnału o podanym indeksie
        /// </summary>
        protected override double getData(int index)
        {
            return signal.Course[index];
        }

        #endregion //end Methods

        #region Propertis

        #endregion // end Propertis
    }
}
