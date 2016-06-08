using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTD1
{
    public class DigitalSignal : Signal
    {

        #region Private Fields

        /// <summary>
        /// Zwraca czas trwania jednego bitu
        /// </summary>
        private double timeOfBit = 0;

        /// <summary>
        /// Zwraca reprezentacje bitowa jako string
        /// </summary>
        private string bit = "";

        private static DigitalSignal operatorMultiplication(DigitalSignal firstSignal, DigitalSignal secondSignal)
        {
            DigitalSignal productOfSignals = firstSignal;
            double[] course;
            course = new double[productOfSignals.LengthOfBuffer];
            for (int time = 0; time < productOfSignals.LengthOfBuffer; time++)
            {
                productOfSignals.Course[time] = firstSignal.Course[time] * secondSignal.Course[time];
                // Sprawdzenie maksymalnej amplitudy i przypisanie jej do parametru
                if (productOfSignals.Course[time] > productOfSignals.Amplitude)
                    productOfSignals.Amplitude = course[time];
            }
            return productOfSignals;
        }
        /// <summary>
        /// Operator mnozenia
        /// </summary>
        public static DigitalSignal operator *(DigitalSignal firstSignal, DigitalSignal secondSignal)
        {
            return operatorMultiplication(firstSignal, secondSignal);
        }

        #endregion //end Private Fields

        #region Constructors

        public DigitalSignal(string bit, double samplingFrequency, double timeOfBit)
        {
            this.bit = bit;
            char[] arr = bit.ToCharArray();
            this.samplingFrequency = samplingFrequency;
            this.timeOfBit = timeOfBit;
            lengthOfBuffer = (int)(samplingFrequency * bit.Length * timeOfBit);
            int LengthOfBit = (int)(timeOfBit * samplingFrequency);
            course = new double[(int)(samplingFrequency * bit.Length * timeOfBit)];
            for (int iString = 0; iString < bit.Length; iString++)
            {
                for (int iSampel = 0; iSampel < LengthOfBit; iSampel++)
                {
                    Course[iString * LengthOfBit + iSampel] = arr[iString] - 48;
                }
            }
        }

        #endregion //end Constructors

        #region DigitalSignal Members

        #region Properties
        
        /// <summary>
        /// Zwraca czas trwania jednego bitu
        /// </summary>
        public double TimeOfBit
        {
            get
            {
                return this.timeOfBit;
            }
        }
        #endregion //end Properties



        #endregion //end DigitalSignal Members
    }
}
