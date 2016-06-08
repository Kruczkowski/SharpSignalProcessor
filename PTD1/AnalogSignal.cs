using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PTD1
{
    /// <summary>
    /// Klasa reprezentujaca sygnal.
    /// </summary>
    public class AnalogSignal : Signal
    {
        #region Private Fields

        /// <summary>
        /// Zwraca czestotliwosc
        /// </summary>
        private double frequency = 0;

        /// <summary>
        /// Zwraca dlugosc sygnalu w sekundach
        /// </summary>
        private double length;


        #endregion //end Private Fields

        #region Private Methods

        /// <summary>
        /// Funkcja realizujaca dodawanie sygnalow
        /// </summary>
        /// <param name="firstSignal">Pierwszy sygnal.</param>
        /// <param name="secondSignal">Drugi sygnal.</param>
        private static AnalogSignal operatorAddition(AnalogSignal firstSignal, AnalogSignal secondSignal)
        {
            double[] course;
            AnalogSignal sumOfSignals = new AnalogSignal(firstSignal, secondSignal);

            course = new double[sumOfSignals.LengthOfBuffer];

            for (int time = 0; time < sumOfSignals.LengthOfBuffer; time++)
            {
                course[time] = firstSignal.Course[time] + secondSignal.Course[time];
                // Sprawdzenie maksymalnej amplitudy i przypisanie jej do parametru
                if (course[time] > sumOfSignals.Amplitude)
                    sumOfSignals.Amplitude = course[time];
            }
            
            sumOfSignals.SetCourse(course);
            return sumOfSignals;
        }

        /// <summary>
        /// Funkcja wykonujaca mnozenie sygnalow
        /// </summary>
        private static AnalogSignal operatorMultiplication(AnalogSignal firstSignal, AnalogSignal secondSignal)
        {
            AnalogSignal productOfSignals = new AnalogSignal(firstSignal,secondSignal);
            double[] course;
            course = new double[productOfSignals.LengthOfBuffer];
            for (int time = 0; time < productOfSignals.LengthOfBuffer; time++)
            {
                course[time] = firstSignal.Course[time] * secondSignal.Course[time];
                // Sprawdzenie maksymalnej amplitudy i przypisanie jej do parametru
                if (course[time] > productOfSignals.Amplitude)
                    productOfSignals.Amplitude = course[time];
            }
            productOfSignals.SetCourse(course);
            return productOfSignals;
        }
        /// <summary>
        /// Funkcja wykonujaca konkadenacje sygnałów
        /// </summary>
        private static AnalogSignal operatorConcadenation(AnalogSignal firstSignal, AnalogSignal secondSignal)
        {
            AnalogSignal concadenateOfSignals = new AnalogSignal(firstSignal, secondSignal);
            double[] course;
            concadenateOfSignals.lengthOfBuffer = firstSignal.LengthOfBuffer + secondSignal.LengthOfBuffer;
            course = new double[concadenateOfSignals.LengthOfBuffer];
            for (int time = 0; time < firstSignal.LengthOfBuffer; time++)
            {
                course[time] = firstSignal.Course[time];
                // Sprawdzenie maksymalnej amplitudy i przypisanie jej do parametru
                if (course[time] > concadenateOfSignals.Amplitude)
                    concadenateOfSignals.Amplitude = course[time];
            }
            for (int time = firstSignal.LengthOfBuffer; time < concadenateOfSignals.LengthOfBuffer; time++)
            {
                course[time] = secondSignal.Course[time - firstSignal.LengthOfBuffer];
                // Sprawdzenie maksymalnej amplitudy i przypisanie jej do parametru
                if (course[time] > concadenateOfSignals.Amplitude)
                    concadenateOfSignals.Amplitude = course[time];
            }
            concadenateOfSignals.SetCourse(course);
            return concadenateOfSignals;
        }

        /// <summary>
        /// Funkcja ustawia przebieg sygnalu
        /// </summary>
        private double[] SetCourse(double[] course)
        {
            this.course = (double[])course.Clone();
            return course;
        }

        #endregion //end Private Methods

        #region Constructor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AnalogSignal()
        {
            this.samplingFrequency = 0;
            this.amplitude = 0;
            this.frequency = 0;
            this.length = 0;
            this.course = null;
        }

        /// <summary>
        /// Konstruktor
        /// <param name=""></param>
        /// <param name="amplitude">Amplitude[db]</param>
        /// <param name="frequency">Czestotliwosc[Hz]</param>
        /// <param name="samplingFrequency">Czestotliwosc Probkowania[Hz]</param>
        /// <param name="maxLength">Maksymalna dlugosc fali[s]</param>
        /// </summary>
        public AnalogSignal(double amplitude, double frequency, double samplingFrequency, double length)
        {
            if (frequency <= (samplingFrequency / 2))
            {
                this.samplingFrequency = samplingFrequency;
            }
            else
            {
                this.samplingFrequency = frequency * 2;
            }
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.length = length;
            this.lengthOfBuffer = (int)(samplingFrequency * length);
            course = new double[LengthOfBuffer];
        }

        /// <summary>
        /// Konstruktor kopiujacy
        /// </summary>
        public AnalogSignal(AnalogSignal parent)
        {
            this.samplingFrequency = parent.SamplingFrequency;
            this.amplitude = parent.Amplitude;
            this.frequency = parent.Frequency;
            this.length = parent.Length;
            this.lengthOfBuffer = parent.LengthOfBuffer;
            this.course = parent.Course;
        }

        /// <summary>
        /// Konstruktor przyjmujacy 2 obiekty zrodlowe,
        /// z ktorych przyjmuje odpowiednie wartosci
        /// </summary>
        public AnalogSignal(AnalogSignal firstSignal, AnalogSignal secondSignal)
        {
            
            double frequency, samplingFrequency, length, amplitude = 0;

            //MaxLength przyjmuje dlugosc krotszego sygnalu
            if (firstSignal.LengthOfBuffer > secondSignal.LengthOfBuffer)
            {
                length = secondSignal.Length;
            }
            else
            {
                length = firstSignal.Length;
            }
            course = new double[LengthOfBuffer];

            //Frequency przyjmuje wartosc wyzszej czestotliwosci
            if (firstSignal.Frequency > secondSignal.Frequency)
            {
                frequency = secondSignal.Frequency;
                samplingFrequency = secondSignal.SamplingFrequency;
            }
            else
            {
                frequency = firstSignal.Frequency;
                samplingFrequency = firstSignal.SamplingFrequency;
            }

            this.samplingFrequency = samplingFrequency;
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.length = length;
            this.lengthOfBuffer = (int)(samplingFrequency * length);
        }

        #endregion //end Constuctor

        #region AnalogSignal Members

        #region Properties

        /// <summary>
        /// Zwraca czestotliwosc
        /// </summary>
        public double Frequency
        {
            get
            {
                return this.frequency;
            }
        }
        /// <summary>
        /// Zwraca dlugosc sygnalu w sekundach
        /// </summary>
        public double Length
        {
            get
            {
                return this.length;
            }
        }

        /// <summary>
        /// Omega = 2 * PI * f
        /// </summary>
        public double Omega
        {
            get
            {
                return 2 * Math.PI * frequency / samplingFrequency;
            }
        }

        #endregion // end   #region Properties

        #region Methods

        #region Generate Filters

        public AnalogSignal GenerateFilter1()
        {
           double [] filter= new double[44]{ 
               -0.0223244681359,  0.04221844153037,  0.01441358896298,-0.007046282746312,
                -0.0117208475257, 0.004404907720284,  0.01812357331811, 0.006151592233128,
               -0.01754295637898, -0.01684454788301,  0.01210498404351,  0.02733588904158,
              0.0001106893521507, -0.03459849190906,  -0.0196014944298,  0.03495239038254,
                 0.0475284654854, -0.02247718800958, -0.08989790769832, -0.02105652290547,
                  0.193947121439,   0.3911161363837,   0.3911161363837,    0.193947121439,
               -0.02105652290547, -0.08989790769832, -0.02247718800958,   0.0475284654854,
                0.03495239038254,  -0.0196014944298, -0.03459849190906,0.0001106893521507,
                0.02733588904158,  0.01210498404351, -0.01684454788301, -0.01754295637898,
               0.006151592233128,  0.01812357331811, 0.004404907720284,  -0.0117208475257,
              -0.007046282746312,  0.01441358896298,  0.04221844153037,  -0.0223244681359
           };
           for(int i=0;i<lengthOfBuffer;i++)
           {
               if(i<44)
                this.course[i] = filter[i];
               else
                   this.course[i] = 0;
           }
           return this;
        }
        public List<double[]> GenerateFilter_IIR()
        {
            double[] a = new double[23]
                {
                   -0.07647616756963,   0.1798016180051,   0.1110182110253,  -0.4338055002043,
                    -0.1644896288601,   0.8020935496436,   0.1545833164631,   -1.121221738553,
                    -0.03347802094561,    1.142109862862, -0.02603970408658,   -1.065103248102,
                    0.1140659703703,   0.8248904998642,  -0.1054434434232,  -0.6876172771196,
                    0.08669601172289,   0.5919288234189,  -0.3549665502009,   0.3051458100092,
                    -0.1521524658379,  0.08018317873768, -0.02966747386078
                };
            double[] b = new double[23]
                {
                     0,-6.792448165671e-17,-0.0005348563199297,3.112396950235e-05,
                    0.001303905409624,-7.076446895391e-05, -0.00242458077271,0.0001107040770681,
                    0.003402547346913,-0.000134301687249,-0.003443647055742, 0.000206274436792,
                    0.003227467989557,-0.000224758711492,-0.002469865607626,0.0002976282637693,
                    0.002051195099439,-0.0003596772494643, -0.00196755442239,-6.485332282413e-05,
                    -0.0009533939059231,3.396071712562e-06,-0.0002433114755957
                };
            List<double[]> result = new List<double[]>();
            result.Add(a);
            result.Add(b);
            return result;
        }

        public List<double[]> GenerateFilter_HighBand_IIR()
        {
            double[] a = new double[5]
            {
                1,   -1.992827311651,     2.50618600096,   -1.650101933144, 0.6178375649279
            };
            double[] b = new double[5]
            {
                    -0.05949158336122, -0.01580752695141, -0.09132932498393, -0.01629184712267,-0.05972831513952
            };
            List<double[]> result = new List<double[]>();
            result.Add(a);
            result.Add(b);
            return result;
        }

        #endregion //end Generate Window

        #region Generate Signal

        /// <summary>
        /// Funkcja obliczajaca przebieg sygnalu w czasie, o podanej dlugosci
        /// </summary>
        public AnalogSignal GenerateSinusSignal(int maxTime)
        {
            if (maxTime <= LengthOfBuffer)
            {
                for (int time = 0; time < maxTime; time++)
                {
                    course[time] = Amplitude * Math.Sin(2*Math.PI * Frequency * time / SamplingFrequency);
                }
                for (int time = 0; maxTime < LengthOfBuffer; time++)
                {
                    course[time] = 0;
                }
            }
            else
            {
                GenerateSinusSignal();
            }
            return this;
        }

        /// <summary>
        /// Funkcja obliczajaca przebieg sygnalu sinusoidalnego w czasie, o maksymalnej dlugosci
        /// </summary>
        public AnalogSignal GenerateSinusSignal()
        {
            for (int time = 0; time < LengthOfBuffer; time++)
            {
                course[time] = Amplitude * Math.Sin(2*Math.PI * Frequency * time / SamplingFrequency);
            }
            return this;
        }

        /// <summary>
        /// Funkcja obliczajaca przebieg sygnalu sinc w czasie, o maksymalnej dlugosci
        /// </summary>
        public AnalogSignal GenerateSincSignal(int N)
        {
            if (N % 2 == 0)
                N++;

            course[N] = 1;
            for (int time = 1; time < N; time++)
            {
                course[N+time] = Amplitude * Math.Sin(2 * Math.PI * Frequency * time / SamplingFrequency) / (2 * Math.PI * Frequency * time / SamplingFrequency);
                course[N-time] = Amplitude * Math.Sin(2 * Math.PI * Frequency * time / SamplingFrequency) / (2 * Math.PI * Frequency * time / SamplingFrequency);
            }
            for (int time = N*2; time < lengthOfBuffer; time++)
                course[time] = 0;
            return this;
        }

        /// <summary>
        /// Funkcja obliczajaca przebieg sygnalu kosinusoidalnego w czasie, o maksymalnej dlugosci
        /// </summary>
        public AnalogSignal GenerateCosinusSignal()
        {
            for (int time = 0; time < LengthOfBuffer; time++)
            {
                course[time] = Amplitude * Math.Cos(2 * Math.PI * Frequency * time / SamplingFrequency);
            }
            return this;
        }
        /// <summary>
        /// Generuje przebieg prostokątny
        /// </summary>
        /// <param name="M">Ilość harmonicznych</param>
        /// <returns></returns>
        public AnalogSignal GenerateSquareSignal(int M)
        {
            double tmp = 0; // zmienna tymczasowa
            for (int time = 0; time < LengthOfBuffer; time++)
            {
                tmp = 0;
                for (int m = 1; m <= M*2; m+=2)
                {
                    tmp+= Math.Sin(Omega * time*m)/m;
                }
                course[time] = (4 * Amplitude / Math.PI) * tmp;
            }
            return this;
        }

        /// <summary>
        /// Generuje przebieg piłokształtny
        /// </summary>
        /// <param name="M">Ilość harmonicznych</param>
        /// <returns></returns>
        public AnalogSignal GenerateSawSignal(int M)
        {
            double tmp = 0; // zmienna tymczasowa
            bool minus; // zmienna decydująca znaku matematycznym wyrażenia

            for (int time = 0; time < LengthOfBuffer; time++)
            {
                tmp = 0;
                minus = false;
                for (int m = 1; m <= M; m++)
                {
                    if (minus)
                        tmp += Math.Sin(Omega * time * m) / m * (-1);
                    else
                        tmp += Math.Sin(Omega * time * m) / m;
                    minus = !minus;
                }
                course[time] = (2 * Amplitude / Math.PI) * tmp;
            }
            return this;
        }

        /// <summary>
        /// Generuje przebieg trójkątny
        /// </summary>
        /// <param name="M">Ilość harmonicznych</param>
        /// <returns></returns>
        public AnalogSignal GenerateTriangleSignal(int M)
        {
            double tmp = 0; // zmienna tymczasowa
            bool minus; // zmienna decydująca znaku matematycznym wyrażenia

            for (int time = 0; time < LengthOfBuffer; time++)
            {
                tmp = 0;
                minus = false;
                for (int m = 1; m <= M*2; m += 2)
                {
                    if (minus)
                        tmp += Math.Sin(Omega * time * m) / (m * m) * (-1);
                    else
                        tmp += Math.Sin(Omega * time * m) / (m * m);
                    minus = !minus;
                }
                course[time] = (8 * Amplitude / Math.PI) * tmp;
            }
            return this;
        }
        /// <summary>
        /// Generuje szum
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public AnalogSignal GenerateNoise(double u)
        {
            Random rand = new Random();
            for (int time = 0; time < LengthOfBuffer; time++)
            {
                this.Course[time] = (rand.Next(1000) / 1000.0 * u)-(u/2);
            }
            return this;
        }

        /// <summary>
        /// Generuje Peak
        /// </summary>
        public AnalogSignal GeneratePeak(double Amplitude, double Delay)
        {
            int delay = (int)(Delay * samplingFrequency);
            bool zero = false;
            bool grows = true;
            int dlugosc = (int)(lengthOfBuffer/100);
            for (int time = 1; time < LengthOfBuffer; time++)
            {
                if (!zero)
                {
                    
                    if (grows)
                    {
                        Course[time] = Course[time - 1] + (Amplitude/(dlugosc*2));
                        if (Course[time] > Amplitude)
                        {
                            Course[time] = Course[time-1];
                            grows = false;
                        }
                    }
                    else
                    {
                        Course[time] = Course[time - 1] - (Amplitude / (dlugosc * 2));
                        if (Course[time] <= 0)
                        {
                            zero = true;
                            Course[time] = 0;
                        }
                    }
                }
                else
                    Course[time] = 0;

            }
            return this;
        }
        /// <summary>
        /// Generuje sygnał delta
        /// </summary>
        public AnalogSignal GenerateDelta(double amp)
        {
            this.Course[0] = amp;
            for (int time = 1; time < LengthOfBuffer; time++)
            {
                this.Course[time] = 0;
            }
            return this;
        }

        /// <summary>
        /// Generuje sygnał delta
        /// </summary>
        public AnalogSignal GenerateDelta()
        {
            this.Course[0] = 1;
            for (int time = 1; time < LengthOfBuffer; time++)
            {
                this.Course[time] = 0;
            }
            return this;
        }
        #endregion //end Generate Signal

        #region Inverted DFT

        /// <summary>
        /// Odwrotna transformata Fouriera
        /// </summary>
        /// <param name="frequencyBuffer"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        public AnalogSignal iDFT(Complex[] frequencyBuffer, FourierTransform ft)
        {
            int N = frequencyBuffer.Length;
            Complex[] resultCourse = ft.iDFT(frequencyBuffer, N);

            for (int i = 0; i < N; i++)
            {
                this.course[i] = resultCourse[i].Real + resultCourse[i].Imaginary;
            }
            return this;
        }
        public double[] iDFT2(Complex[] frequencyBuffer, FourierTransform ft)
        {
            int N = frequencyBuffer.Length;
            double[] resultArray = new double[lengthOfBuffer];
            Complex[] resultCourse = ft.iDFT(frequencyBuffer, N);

            for (int i = 0; i < N; i++)
            {
                resultArray[i] = resultCourse[i].Real + resultCourse[i].Imaginary;
            }
            return resultArray;
        }

        #endregion // end Inverted DFT

        #region Generate Model Signals


        /// <summary>
        /// Funkcja generuje wzorowy sygnał prostokątny
        /// </summary>
        /// <returns></returns>
        public AnalogSignal GenerateModelSquare()
        {
            /// zmienna decydujaca o parzystości potęgi -1
            int minus; 

            /// interwal - połowa okresu
            int interval = (int)(samplingFrequency / frequency * length); 
            for (int time = 0; time < LengthOfBuffer; time++)
            {
                minus = time / interval;
                course[time] = Amplitude*(Math.Pow(-1, minus));
            }
            return this;
        }

        /// <summary>
        /// Funckja generująca wzorowy sygnał trójkątny
        /// </summary>
        /// <param name="maxAmplitude"></param>
        /// <returns></returns>
        public AnalogSignal GenerateModelTriangle(double maxAmplitude)
        {
            amplitude = maxAmplitude;

            /// zmienna ustalająca parzystość potęgi dla -1
            int minus1, minus2;
            /// interwal - połowa okresu
            int interval = (int)(samplingFrequency / frequency * length);
            for (int time = 1; time < LengthOfBuffer; time++)
            {
                minus1 = time / (interval);
                minus2 = time / (interval / 2);
                course[time] = course[time - 1] + (amplitude / (interval/2)) *Math.Pow(-1, minus1) * Math.Pow(-1, minus2);
            }
            return this;
        }

        /// <summary>
        /// Funkcja generująca wzorowy sygnał piłokształtny
        /// </summary>
        /// <param name="maxAmplitude"></param>
        /// <returns></returns>
        public AnalogSignal GenerateModelSawer(double maxAmplitude)
        {
            amplitude = maxAmplitude;

            /// interwal - połowa okresu
            int interval = (int)(samplingFrequency / frequency * length);
            for (int time = 1; time < LengthOfBuffer; time++)
            {
                if (time%interval == 0)
                    course[time] = -course[time - 1];
                else
                    course[time] = course[time - 1] + (amplitude / interval);
            }
            return this;
        }

        #endregion // end Generate Model Signals

        #region Modulation

        /// <summary>
        /// Funkcja realizujaca modulacje amplitudowa [AM]
        /// </summary>
        /// course[time] = Amplitude * Math.Sin(2*Math.PI * Frequency * time / SamplingFrequency);
        public void AmplitudeModulation(AnalogSignal carrier, AnalogSignal informaion, double k)
        {
            frequency = informaion.Frequency;
            samplingFrequency = informaion.samplingFrequency;
            length = carrier.Length;
            lengthOfBuffer = (int)(samplingFrequency * length);
            course = new double[informaion.LengthOfBuffer];

            for (int time = 0; time < LengthOfBuffer; time++)
            {
                course[time] = (1 + k * informaion.Course[time]) * Math.Sin(2*Math.PI * carrier.Frequency * time / carrier.SamplingFrequency);

                // Sprawdzenie maksymalnej amplitudy i przypisanie jej do parametru
                if (course[time] > amplitude)
                    amplitude = course[time];
            }
        }

        /// <summary>
        /// Funkcja realizujaca modulacje fazowa [PM]
        /// </summary>
        public void PhaseModulation(AnalogSignal carrier, AnalogSignal informaion, double k)
        {
            frequency = informaion.Frequency;
            samplingFrequency = informaion.samplingFrequency;
            length = carrier.Length;
            lengthOfBuffer = (int)(samplingFrequency * length);
            course = new double[informaion.LengthOfBuffer];

            for (int time = 0; time < LengthOfBuffer; time++)
            {
                course[time] = carrier.Amplitude * Math.Sin(2 * Math.PI * carrier.Frequency * time / carrier.SamplingFrequency + k * informaion.Course[time]);
                // Sprawdzenie maksymalnej amplitudy i przypisanie jej do parametru
                if (course[time] > amplitude)
                    amplitude = course[time];
            }
        }
        /// <summary>
        /// Funkcja realizujaca modulacje cyfrową ASK
        /// </summary>
        public void ASK(AnalogSignal carrier, DigitalSignal informaion)
        {
            course = new double[informaion.LengthOfBuffer];
            for (int time = 0; time < informaion.LengthOfBuffer; time++)
            {
                if (informaion.Course[time] == 0)
                {
                    course[time] = 0;
                }
                else
                {
                    course[time] = carrier.Course[time];
                }
            }
        }
        /// <summary>
        /// Funkcja realizujaca modulacje cyfrową FSK
        /// </summary>
        public void FSK(DigitalSignal informaion, int n)
        {
            double frequency1 = n / informaion.TimeOfBit;
            double frequency2 = n*2 / informaion.TimeOfBit;
            course = new double[informaion.LengthOfBuffer];
            for (int time = 0; time < informaion.LengthOfBuffer; time++)
            {
                if (informaion.Course[time] == 0)
                {
                    course[time] = Math.Sin(2 * Math.PI * frequency1 * time / SamplingFrequency);
                }
                else
                {
                    course[time] = Math.Sin(2 * Math.PI * frequency2 * time / SamplingFrequency);
                }
            }
        }
        /// <summary>
        /// Funkcja realizujaca modulacje cyfrową PSK
        /// </summary>
        public void PSK(DigitalSignal informaion)
        {
            double frequency = 1 / informaion.TimeOfBit;
            this.frequency = frequency;
            lengthOfBuffer = informaion.LengthOfBuffer;
            course = new double[informaion.LengthOfBuffer];
            for (int time = 0; time < informaion.LengthOfBuffer; time++)
            {
                if (informaion.Course[time] == 0)
                {
                    course[time] = Math.Sin(2 * Math.PI * frequency / SamplingFrequency * time);
                }
                else
                {
                    course[time] = Math.Sin(2 * Math.PI * frequency * time / SamplingFrequency + Math.PI);
                }
            }
        }

        #endregion //end Modulation

        #region Correlation

        /// <summary>
        /// Wyznacza sygnał koralacji
        /// </summary>
        /// <param name="signal1"></param>
        /// <param name="signal2"></param>
        /// <returns></returns>
        public AnalogSignal Correlation(Signal signal1, Signal signal2)
        {
            for(int tau = 0; tau < signal1.LengthOfBuffer;tau++)
            {
                for (int t = 0; t < signal1.LengthOfBuffer; t++)
                {
                    if(t - tau >= 0)
                        this.Course[tau] += signal1.Course[t-tau] * signal2.Course[t];
                }
            }
            return this;
        }

        #endregion //end Correlation

        #region Filtering

        /// <summary>
        /// Wykonuje operację splotu
        /// </summary>
        public AnalogSignal Weave(Signal sourceSignal, Signal filter)
        {
            for (int k = 0; k < sourceSignal.LengthOfBuffer; k++)
            {
                for (int n = 0; n < sourceSignal.LengthOfBuffer; n++)
                {
                    if (k-n >= 0)
                        this.Course[k] += sourceSignal.Course[k-n] * filter.Course[n];
                }
            }
            return this;
        }
        public AnalogSignal IIR(Signal sourceSignal, Signal filter)
        {
            for (int k = 0; k < sourceSignal.LengthOfBuffer; k++)
            {
                for (int n = 0; n < sourceSignal.LengthOfBuffer-1; n++)
                {
                    if (k-n >= 0 )
                    {
                        if (n == 0)
                            this.Course[k] += sourceSignal.Course[k-n] * filter.Course[n];

                        if (n > 0 && k-(n+1)>=0)
                            this.Course[k] += sourceSignal.Course[k-n] * filter.Course[n]
                                - sourceSignal.Course[k-(n+1)] * filter.Course[n+1];
                    }
                }
            }
            return this;
        }

        public AnalogSignal IIR(Signal sourceSignal, List<double[]> filter)
        {
            double[] a = filter[0];
            double[] b = filter[1];
            int M = a.Length;
            int N = b.Length;
            for (int m = 0; m < sourceSignal.LengthOfBuffer; m++)
            {
                for (int n = 0; n < M; n++)
                {
                    if (m - n >= 0)
                    {
                        if(m==0)
                            this.Course[m] += sourceSignal.Course[m - n] * b[n];
                        else
                            if(m - (n+1) >= 0 && n+1<N)
                                this.Course[m] += sourceSignal.Course[m - n] * b[n]
                                    - this.Course[m - (n+1)] * a[n+1];

                    }
                }
            }
            return this;
        }

        #endregion //end Filtering

        #endregion //end Methods

        #region Operators

        /// <summary>
        /// Operator dodawania
        /// </summary>
        public static AnalogSignal operator +(AnalogSignal firstSignal, AnalogSignal secondSignal)
        {
            return operatorAddition(firstSignal,secondSignal);
        }

        /// <summary>
        /// Operator mnozenia
        /// </summary>
        public static AnalogSignal operator *(AnalogSignal firstSignal, AnalogSignal secondSignal)
        {
            return operatorMultiplication(firstSignal, secondSignal);
        }

        /// <summary>
        /// Operator konkadenacji
        /// </summary>
        public static AnalogSignal operator |(AnalogSignal firstSignal, AnalogSignal secondSignal)
        {
            return operatorConcadenation(firstSignal, secondSignal);
        }

        #endregion //end Operators

        #endregion //end AnalogSignal Members
    }
}
