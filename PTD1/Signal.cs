using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTD1
{
    public abstract class Signal
    {
        #region Static Fileds

        #endregion // end Ststic Fileds
        #region Private Fields


        /// <summary>
        /// Zwraca czestotliwosc probkowania
        /// </summary>
        protected double samplingFrequency = 0;

        /// <summary>
        /// Zwraca amplitude
        /// </summary>
        protected double amplitude = 0;

        /// <summary>
        /// Zwraca przebieg sygnalu w czasie sygnalu
        /// </summary>
        protected double[] course = null;

        /// <summary>
        /// Zwraca dlugosc bufora
        /// </summary>
        protected int lengthOfBuffer;

        #endregion //end Private Fields

        #region Properties
        /// <summary>
        /// Zwraca czestotliwosc probkowania
        /// </summary>
        public double SamplingFrequency
        {
            get
            {
                return this.samplingFrequency;
            }
        }
        /// <summary>
        /// Zwraca amplitude i ustawia
        /// </summary>
        public double Amplitude
        {
            get
            {
                return this.amplitude;
            }
            set
            {
                this.amplitude = value;
            }
        }
        /// <summary>
        /// Zwraca przebieg sygnalu w czasie sygnalu
        /// </summary>
        public double[] Course
        {
            get
            {
                return this.course;
            }
        }
        
        /// <summary>
        /// Zwraca dlugosc bufora
        /// </summary>
        public int LengthOfBuffer
        {
            get
            {
                return this.lengthOfBuffer;
            }
        }

        #endregion //end Properties

        #region Methods


        /// <summary>
        /// Funkcja wzmacniajaca (tlumiaca) sygnal
        /// </summary>
        public void Amplifier(double drive)
        {
            for (int time = 0; time < LengthOfBuffer; time++)
            {
                course[time] = course[time] * drive;
            }
        }

        #endregion //end Methods
    }
}
