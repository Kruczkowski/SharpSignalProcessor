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
    /// Klasa abstrakcyjna reprezentujaca wykres sygnalu.
    /// Dziedziczaca po PictureBox.
    /// </summary>
    public abstract class SignalGraph : PictureBox
    {
        
        #region Auxiliary Field

        /// <summary>
        /// Metoda wyświetlająca dowolny tekst
        /// </summary>
        public void SaySomething(String something, Point point)
        {
            graphics = this.CreateGraphics();
            TextRenderer.DrawText(graphics, something, this.Font, point, Color.Red);
        }

        #endregion // end Auxiliary Field

        #region Private Fileds

        /// <summary>
        /// Struktura prostokąta selekcji
        /// </summary>
        private struct Selection
        {
            public Rectangle scope;
            public Pen pen;
            public int begin;
            public int end;
        }

        /// <summary>
        /// Pole reprezentujące prostokąt selekcji
        /// </summary>
        private Selection selection;

        /// <summary>
        /// Flaga informująca czy zaznaczenie jest widoczne
        /// </summary>
        private bool ifShowMarked = false;

        #endregion //end Private Fileds

        #region Protected Fields

        /// <summary>
        /// Pole skladowe reprezentujace wykres
        /// </summary>
        protected Graphics graphics;

        /// <summary>
        /// Pole skladowe reprezentujacy rysowany sygnal
        /// </summary>
        protected Signal signal;

        /// <summary>
        /// podpis na osi Y
        /// </summary>
        protected string xLabel;

        /// <summary>
        /// podpis na osi X
        /// </summary>
        protected string yLabel;

        /// <summary>
        /// Opis wykresu
        /// </summary>
        protected string description;

        /// <summary>
        /// Granica osi y
        /// </summary>
        protected double yPercent = 1;

        /// <summary>
        /// Odstęp wykresu od lewej krawędzi
        /// </summary>
        protected int leftPadding;

        /// <summary>
        /// Szerokość pionowej osi
        /// </summary>
        protected int widthOfVerticalAxis;

        /// <summary>
        /// Odstęp wykresu od dolnej krawędzi
        /// </summary>
        protected int downPadding;

        /// <summary>
        /// Szerokość poziomej osi
        /// </summary>
        protected int widthOfHorizontialAxis;

        /// <summary>
        /// Informacja na temat przyblizenia (%)
        /// </summary>
        protected double horizontialZoom;

        /// <summary>
        /// Presunięcie wykresu
        /// </summary>
        protected int shift;

        /// <summary>
        /// Flaga decydująca o wykonaniu autoskali
        /// </summary>
        protected bool DoAutoscale = false;

        /// <summary>
        /// Typ sygnału
        /// </summary>
        protected string typeOfSignal = null;

        #region Buttons

        /// <summary>
        /// Pola przycisków
        /// </summary>
        protected Button
            moveChartButton,
            backChartButton,
            zoomInButton,
            zoomOutButton,
            autoscaleButton,
            showMaximumButton,
            showXValueButton;
            

        #endregion //end Buttons
        

        #endregion //end Protected Fields

        #region Events

        /// <summary>
        /// Zdarzenie rysujace sygnal na wykresie
        /// </summary>
        protected abstract void DrawPlotEvent(object sender, PaintEventArgs e);

        /// <summary>
        /// Zdarzenie przygotowuje pusty wykres z podpisanymi osiami
        /// </summary>
        protected abstract void PrepareChartEvent(object sender, PaintEventArgs e);


        private void moveChartButton_Click(object sender, EventArgs e)
        {
            moveChart();
        }
        private void backChartButton_Click(object sender, EventArgs e)
        {
            backChart();
        }
        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            zoomOut();
        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            if(ifShowMarked)
                showMarked();
            else
                zoomIn();
        }
        private void autoscaleButton_Click(object sender, EventArgs e)
        {
            Autoscale();
        }
        private void showMaximumButton_Click(object sender, EventArgs e)
        {
            showMaximum();
        }
        private void showXValueButton_Click(object sender, EventArgs e)
        {
            showXValue();
        }

        /// <summary>
        /// Zdarzenie wciśnięcia lewego przycisku myszy
        /// </summary>
        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DeleteSelection();
            }
            else
            {
                DrawSelection(e);
            }
            deleteMaximum();
            deleteXValue();
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveEvent);
            
        }

        /// <summary>
        /// Zdarzenie przesuwania kursora myszy
        /// </summary>
        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StretchSelection(e);
            }
        } 


        /// <summary>
        /// Zdarzenie opuszczenia lewego przycisku myszy
        /// </summary>
        private void MouseUpEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
            }
            this.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.MouseMoveEvent);
        }

        /// <summary>
        /// Zdarzenie rysowania prostokątu selekcji
        /// </summary>
        private void DrawSelectedEvent(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            graphics.DrawRectangle(selection.pen, selection.scope);
        }

        /// <summary>
        /// Zdarzenie rysowania krzyża przy maximum
        /// </summary>
        private void ShowMaximumPaint(object sender, PaintEventArgs e)
        {
            int shiftOfSamples = shift + (int)((selection.begin - (Width - RealWidthOfChart - 1)) * horizontialZoom / 100);
            double zoomFactor = horizontialZoom * ((selection.end - selection.begin) * 100.0 / RealWidthOfChart) / 100;
            if (zoomFactor >= 100)
                zoomFactor = 100;
            double [] maximum = new double[2];
            maximum[0] = getData(selection.begin - (Width - RealWidthOfChart - 1));
            for (int iterator = selection.begin - (Width - RealWidthOfChart - 1); iterator < selection.end - (Width - RealWidthOfChart - 1); iterator++)
            {
                if (maximum[0] < Math.Abs(getData(iterator)))
                {
                    maximum[0] = Math.Abs(getData(iterator));
                    maximum[1] = iterator;
                }
            }

            //linia pozioma
            if (typeOfSignal.Equals("SPECTRUM"))
            {
                graphics.DrawLine(
                    selection.pen,
                    new Point(leftPadding, (int)(maximum[0] * yPercent * (-1) + RealHeightOfChart)),
                    new Point(Width, (int)(maximum[0] * yPercent * (-1) + RealHeightOfChart)));
            }
            else
            {
                graphics.DrawLine(
                    selection.pen,
                    new Point(leftPadding, (int)(maximum[0] * yPercent * (-1) + RealHeightOfChart / 2)),
                    new Point(Width, (int)(maximum[0] * yPercent * (-1) + RealHeightOfChart / 2)));
            }

            //linia pionowa
            graphics.DrawLine(
                selection.pen,
                new Point((int)(maximum[1] + (Width - RealWidthOfChart)), 0),
                new Point((int)(maximum[1] + (Width - RealWidthOfChart)), RealHeightOfChart));

            e.Graphics.DrawString(getData((int)maximum[1]).ToString("E4"), this.Font, new SolidBrush(Color.Red), new Point((int)(maximum[1] + (Width - RealWidthOfChart - 1) + 10),(int)(maximum[0] * yPercent * (-1) + RealHeightOfChart / 2) + 10), new StringFormat());
            e.Graphics.DrawString((maximum[1]).ToString(), this.Font, new SolidBrush(Color.Red), new Point( (int)(maximum[1] + (Width - RealWidthOfChart - 1) + 10),(int)(maximum[0] * yPercent * (-1) + RealHeightOfChart / 2) + 23), new StringFormat());

            //TextRenderer.DrawText(graphics, signal.Course[maximum[1]].ToString("E4"), this.Font, new Point((int)(maximum[0] * yPercent * (-1) + RealHeightOfChart / 2) + 10, maximum[1] + (Width - RealWidthOfChart - 1) + 10), Color.Red);
            //TextRenderer.DrawText(graphics, maximum[1].ToString(), this.Font, new Point((int)(maximum[0] * yPercent * (-1) + RealHeightOfChart / 2) + 10, maximum[1] + (Width - RealWidthOfChart - 1) + 23), Color.Red);
            //Trzeba zrobić odnajdowanie próbki oraz rysowanie w odpowiednim miejscu
        }

        /// <summary>
        /// Zdarzenie rysowania markera na danej próbce
        /// </summary>
        private void showXValuePaint(object sender, PaintEventArgs e)
        {
            int shiftOfSamples = shift + (int)((selection.begin - (Width - RealWidthOfChart - 1)) * horizontialZoom / 100);
            double zoomFactor = horizontialZoom * ((selection.end - selection.begin) * 100.0 / RealWidthOfChart) / 100;
            if (zoomFactor >= 100)
                zoomFactor = 100;

            int showedValue = selection.begin - (Width - RealWidthOfChart - 1);
            //linia pionowa
            graphics.DrawLine(
                selection.pen,
                new Point(selection.begin, 0),
                new Point(selection.begin, RealHeightOfChart));

            e.Graphics.DrawString(getData(showedValue).ToString("E4"), this.Font, new SolidBrush(Color.Red), new Point(selection.begin +10, 5), new StringFormat());
            e.Graphics.DrawString((showedValue).ToString(), this.Font, new SolidBrush(Color.Red), new Point(selection.begin + 10, 23), new StringFormat());

            //TextRenderer.DrawText(graphics, signal.Course[maximum[1]].ToString("E4"), this.Font, new Point((int)(maximum[0] * yPercent * (-1) + RealHeightOfChart / 2) + 10, maximum[1] + (Width - RealWidthOfChart - 1) + 10), Color.Red);
            //TextRenderer.DrawText(graphics, maximum[1].ToString(), this.Font, new Point((int)(maximum[0] * yPercent * (-1) + RealHeightOfChart / 2) + 10, maximum[1] + (Width - RealWidthOfChart - 1) + 23), Color.Red);
            //Trzeba zrobić odnajdowanie próbki oraz rysowanie w odpowiednim miejscu
        }
        #endregion //end Events

        #region Methods

        

        #region Methods of View

        /// <summary>
        /// Rysuje nowy prostokąt selekcji
        /// </summary>
        private void DrawSelection(MouseEventArgs e)
        {
            if (e.X < Width - RealWidthOfChart-1)
                selection.begin = Width - RealWidthOfChart-1;
            else
                selection.begin = e.X;
            selection.scope = new Rectangle(selection.begin, 0, 1, RealHeightOfChart);
            Paint += new System.Windows.Forms.PaintEventHandler(DrawSelectedEvent);
        }

        /// <summary>
        /// Rozciąga prostokąt selekcji
        /// </summary>
        private void StretchSelection(MouseEventArgs e)
        {
            Paint -= new System.Windows.Forms.PaintEventHandler(DrawSelectedEvent);
            if (e.X > selection.begin)
            {
                if (e.X > Width)
                    selection.end = Width - 1;
                else
                    selection.end = e.X;

                selection.scope = new Rectangle(selection.begin, 0, selection.end - selection.begin, RealHeightOfChart);

                Paint += new System.Windows.Forms.PaintEventHandler(DrawSelectedEvent);
                ifShowMarked = true;
            }
            else
                ifShowMarked = false;
        }

        /// <summary>
        /// Usuwa prostokąt selekcji
        /// </summary>
        private void DeleteSelection()
        {
            Paint -= new System.Windows.Forms.PaintEventHandler(DrawSelectedEvent);
        }

        /// <summary>
        /// Pokazuje maksimum (dodatnie lub ujemne)
        /// </summary>
        private void showMaximum()
        {
            if (ifShowMarked)
            {
                DeleteSelection();
                Paint += new System.Windows.Forms.PaintEventHandler(ShowMaximumPaint);
            }
            else
            {

            }
        }

        /// <summary>
        /// Rysuje marker na próbce
        /// </summary>
        private void showXValue()
        {
            if (ifShowMarked)
            {
                DeleteSelection();
                Paint += new System.Windows.Forms.PaintEventHandler(showXValuePaint);
            }
        }

        /// <summary>
        /// Usuwa zaznaczenie maximum
        /// </summary>
        private void deleteMaximum()
        {
            Paint -= new System.Windows.Forms.PaintEventHandler(ShowMaximumPaint);
        }

        /// <summary>
        /// Usuwa marker
        /// </summary>
        private void deleteXValue()
        {
            Paint -= new System.Windows.Forms.PaintEventHandler(showXValuePaint);
        }
        /// <summary>
        /// Przesunięcie wykresu w przud 
        /// </summary>
        private void moveChart()
        {
            if ((shift + 10 < this.LengthOfBuffer - ((horizontialZoom/100) * this.LengthOfBuffer))
                && horizontialZoom < 100)
            {
                shift += 10;
                this.Image = null;
                DoAutoscale = false;
            }
        }

        /// <summary>
        /// Przesunięcie wykresu w tył
        /// </summary>
        private void backChart()
        {
            if (shift - 10 > 0 && horizontialZoom < 100)
            {
                shift -= 10;
                this.Image = null;
                DoAutoscale = false;
            }
        }

        /// <summary>
        /// Przybliżenie wykresu
        /// </summary>
        private void zoomOut()
        {
            DoAutoscale = false;
            shift = 0;
            if (horizontialZoom * 2 <= 100)
                horizontialZoom *= 2;
            this.Image = null;
        }

        /// <summary>
        /// Oddalenie wykresu
        /// </summary>
        private void zoomIn()
        {
            shift = 0;
            horizontialZoom /= 2;
            DoAutoscale = false;
            this.Image = null;
        }

        /// <summary>
        /// Ustawia parametry pozwalające pokazanie zaznaczonego fragmenu
        /// </summary>
        private void showMarked()
        {
            DoAutoscale = false;
            shift += (int)((selection.begin - (Width - RealWidthOfChart - 1)) * horizontialZoom/100);
            horizontialZoom *= ((selection.end - selection.begin)*100.0 / RealWidthOfChart)/100;
            if (horizontialZoom >= 100)
                horizontialZoom = 100;
            this.Image = null;
            DeleteSelection();
            ifShowMarked = false;
            
        }
        #endregion //end Methods of View

        /// <summary>
        /// Zwraca element z tablicy danych 
        /// </summary>
        protected abstract double getData(int index);

        /// <summary>
        /// Metoda abstrakcyjna ustawiająca pole Signal
        /// </summary>
        public abstract void setSignal(Signal sourceSignal);

        // Wykożystanie architektury obserwatora jest spowodowane tym, że pole MaxFromBuffor jest wypełniane wewnątrz zdarzenia,
        // a darzenia wywoływane są na samym końcu.
        /// <summary>
        /// Obserwator wykonujący autoskalę.
        /// </summary>
        protected void AutoscaleObserver()
        {
            if (DoAutoscale && MaxFromBuffor != 0) // wykluczenie dzielenia przez zero
            {
                yPercent = ((RealHeightOfChart / 2.0) / MaxFromBuffor)*0.98;
                horizontialZoom = 100;
                shift = 0;
            }
        }

        #endregion //end Methods

        #region SignalGraph Interface

        #region Constructor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public SignalGraph(int x, int y, int Height, int Width, string xLabel, string yLabel, string description, string type)
        {
            this.leftPadding = 40;
            this.widthOfVerticalAxis = 3;
            this.downPadding = 50;
            this.widthOfHorizontialAxis = 3;
            this.shift = 0;
            this.horizontialZoom = 100;

            this.typeOfSignal = type;

            Location = new System.Drawing.Point(x, y);
            this.Width = Width;
            this.Height = Height;

            BackColor = System.Drawing.Color.White;
            Paint += new System.Windows.Forms.PaintEventHandler(PrepareChartEvent);

            selection.pen = new Pen(Color.Red);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownEvent);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUpEvent);
            SetLabels(xLabel, yLabel, description);
            initializeButtons();
        }

        #endregion //end Constructor

        #region Properties

        /// <summary>
        /// Pole skladowe reprezentujacy rysowany sygnal
        /// </summary>
        public Signal Signal
        {
            get
            {
                return this.signal;
            }
            set
            {
                this.signal = value;
            }
        }

        /// <summary>
        /// podpis na osi X
        /// </summary>
        public string XLabel
        {
            get
            {
                return xLabel;
            }
        }

        /// <summary>
        /// podpis na osi Y
        /// </summary>
        public string YLabel
        {
            get
            {
                return yLabel;
            }
        }

        /// <summary>
        /// Opis wykresu
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }
        /// <summary>
        /// Rzeczywista szerokość wykresu
        /// </summary>
        public int RealHeightOfChart
        {
            get
            {
                return (this.Height - (this.downPadding+widthOfHorizontialAxis));
            }
        }
        /// <summary>
        /// Rzeczywista wysokość wykresu
        /// </summary>
        public int RealWidthOfChart
        {
            get
            {
                return (this.Width - (this.leftPadding + widthOfVerticalAxis));
            }
        }

        /// <summary>
        /// Długość bufora, przechowującego argumenty wykresu
        /// </summary>
        public int LengthOfBuffer;

        /// <summary>
        /// maksymalna wartość znajdująca się w buforze
        /// </summary>
        public double MaxFromBuffor;
        
        #endregion //end Properties

        #region Methods

        /// <summary>
        /// Funkcja rysujaca wykres
        /// </summary>
        public void DrawGraph()
        {
            Paint += new System.Windows.Forms.PaintEventHandler(DrawPlotEvent);
        }

        /// <summary>
        /// Funkcja ustawiajaca podpisy osi
        /// </summary>
        public void SetLabels(string xLabel, string yLabel, string description)
        {
            this.xLabel = xLabel;
            this.yLabel = yLabel;
            this.description = description;
        }

        /// <summary>
        /// Funkcja ustawiajaca granice na osi y
        /// Parametr to procent wyswietlanego zakresu od zera
        /// </summary>
        public void Limit(double Limit)
        {
            yPercent = Limit/100;
        }

        /// <summary>
        /// Autoskala
        /// </summary>
        public void Autoscale()
        {
            DoAutoscale = true;
        }

        /// <summary>
        /// Inicjalizacja przycisków
        /// </summary>
        private void initializeButtons()
        {
            moveChartButton = new Button();
            moveChartButton.Text = ">";
            moveChartButton.Location = new Point(leftPadding + 20, RealHeightOfChart + 20);
            moveChartButton.Height = 20;
            moveChartButton.Width = 20;
            moveChartButton.Click += new System.EventHandler(this.moveChartButton_Click);

            backChartButton = new Button();
            backChartButton.Text = "<";
            backChartButton.Location = new Point(leftPadding, RealHeightOfChart + 20);
            backChartButton.Height = 20;
            backChartButton.Width = 20;
            backChartButton.Click += new System.EventHandler(this.backChartButton_Click);

            zoomInButton = new Button();
            zoomInButton.Text = "+";
            zoomInButton.Location = new Point(leftPadding+60, RealHeightOfChart + 20);
            zoomInButton.Height = 20;
            zoomInButton.Width = 20;
            zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);

            zoomOutButton = new Button();
            zoomOutButton.Text = "-";
            zoomOutButton.Location = new Point(leftPadding+40, RealHeightOfChart + 20);
            zoomOutButton.Height = 20;
            zoomOutButton.Width = 20;
            zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);

            autoscaleButton = new Button();
            autoscaleButton.Text = "Autoscale";
            autoscaleButton.Location = new Point(leftPadding + 80, RealHeightOfChart + 20);
            autoscaleButton.Height = 20;
            autoscaleButton.Width = 70;
            autoscaleButton.Click += new System.EventHandler(this.autoscaleButton_Click);

            showMaximumButton = new Button();
            showMaximumButton.Text = "Maximum";
            showMaximumButton.Location = new Point(leftPadding + 150, RealHeightOfChart + 20);
            showMaximumButton.Height = 20;
            showMaximumButton.Width = 70;
            showMaximumButton.Click += new System.EventHandler(this.showMaximumButton_Click);


            showXValueButton = new Button();
            showXValueButton.Text = "Value";
            showXValueButton.Location = new Point(leftPadding + 220, RealHeightOfChart + 20);
            showXValueButton.Height = 20;
            showXValueButton.Width = 70;
            showXValueButton.Click += new System.EventHandler(this.showXValueButton_Click);


            this.Controls.Add(moveChartButton);
            this.Controls.Add(backChartButton);
            this.Controls.Add(zoomInButton);
            this.Controls.Add(zoomOutButton);
            this.Controls.Add(autoscaleButton);
            this.Controls.Add(showMaximumButton);

            this.Controls.Add(showXValueButton);
            

        }

        #endregion //end Methods

        #endregion //end SignalGraph Interface
    }
}
