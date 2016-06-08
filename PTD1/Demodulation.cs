using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTD1
{
    class Demodulation
    {
        #region Private Members

        #region Private Fields

        /// <summary>
        /// Pole reprezentujące sygnał
        /// </summary>
        private AnalogSignal analogSignal;

        #endregion //end Private Fields

        #endregion //end Private Members

        #region Public Members

        #region Public Methods

        /// <summary>
        /// Funkcja realuzująca demodulację sygnałów zmodulowanych amplitudowo i fazowo
        /// </summary>
        /// <param name="carrier">Sygnał nośny - kod</param>
        /// <param name="timeOfBit">Czas trwania jednego bitu</param>
        /// <param name="limit">Granica komparatora</param>
        /// <param name="unipolar">Określa, czy iloczyn sygnałów jest jednobiegunowy</param>
        /// <returns>Zwraca ciąg bitów zawartych w sygnale nośnym</returns>
        public DigitalSignal demodulationASKandPSK(AnalogSignal carrier, double timeOfBit,long limit, bool unipolar)
        {
            int numberOfBit;
            int lengthOfWord = 8;
            long [] heuristic = new long[lengthOfWord];

            analogSignal = new AnalogSignal(carrier.Amplitude, carrier.Frequency, carrier.SamplingFrequency, carrier.Length);

            //Generowanie sygnału
            analogSignal.GenerateSinusSignal();

            //Pomnożenie sygnału nośnego przez  informację
            AnalogSignal multi = analogSignal * carrier;
            string word = "";

            for (int i = 0; i < carrier.LengthOfBuffer; i++)
            {
                numberOfBit = (int)(i/carrier.SamplingFrequency / timeOfBit);
                heuristic[numberOfBit] += (long)multi.Course[i];
            }
            for (int i = 0; i < lengthOfWord; i++)
            {
                if (heuristic[i] > limit && unipolar) //jeżeli sygnał jest jednobiegunowy
                {
                    word += "1";
                }
                else if (heuristic[i] < limit && !unipolar) // jeżeli sygnał jest dwubiegunowy
                {
                    word += "1";
                }
                else
                    word += "0";
            }
            return new DigitalSignal(word, carrier.SamplingFrequency, timeOfBit);
        }
        /// <summary>
        /// Funkcja realuzująca demodulację sygnałów zmodulowanych częstotliwościowo
        /// </summary>
        /// <param name="carrier">Sygnał nośny - kod</param>
        /// <param name="timeOfBit">Czas trwania jednego bitu</param>
        /// <param name="n">Liczba okresów przypadających dla jednego bitu w sygnale nośnym o mniejszej częstotliwośi</param>
        /// <returns></returns>
        public DigitalSignal demodulationFSK(AnalogSignal carrier, double timeOfBit, int n)
        {
            int numberOfBit;
            int lengthOfWord = 8;
            long [] heuristic = new long[lengthOfWord];

            //Obiczenie częstotliwości na podstawie liczby okresów na czas trawnia bitu
            double frequency1 = n / timeOfBit;
            double frequency2 = n * 2 / timeOfBit;

            AnalogSignal signal1 = new AnalogSignal(carrier.Amplitude, frequency1, carrier.SamplingFrequency, carrier.Length);
            AnalogSignal signal2 = new AnalogSignal(carrier.Amplitude, frequency2, carrier.SamplingFrequency, carrier.Length);

            //Wygenerowanie sygnałów nośnych
            signal1.GenerateSinusSignal();
            signal2.GenerateSinusSignal();

            //Pomnożenie sygnałów nośnych przez zakodowaną informację
            AnalogSignal multi1 = signal1 * carrier;
            AnalogSignal multi2 = signal2 * carrier;

            //Zsumowanie sygnałów
            AnalogSignal sumOfMulti = multi1 + multi1;
            string word = "";
            for (int i = 0; i < carrier.LengthOfBuffer; i++)
            {
                numberOfBit = (int)(i / carrier.SamplingFrequency / timeOfBit);
                heuristic[numberOfBit] += (long)sumOfMulti.Course[i];
            }
            for (int i = 0; i < lengthOfWord; i++)
            {
                if (heuristic[i] < 0)
                {
                    word += "1";
                }
                else
                    word += "0";
            }
            return new DigitalSignal(word, carrier.SamplingFrequency, timeOfBit);
        }

        #endregion //end Public Methods

        #endregion //end Public Members

        #region Constructor

        public Demodulation()
        {
            
        }

        #endregion //end Constructor
    }
}
