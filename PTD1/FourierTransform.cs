using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using FFTWSharp;

namespace PTD1
{
    /// <summary>
    /// Klada zawierająca metody wykonujące transformaty
    /// </summary>
    public class FourierTransform
    {
        public double maxFrequency;
        public FourierTransform() { }
        
        /// <summary>
        /// Dyskretna transformata Fouriera
        /// </summary>
        public Complex[] DFT(Signal signal)
        {
            int N = Convert.ToInt32(signal.SamplingFrequency / 2);
            
            double max = 0;
            Complex[] complexFrequency = new Complex[N];
            // X(k) = sum(x(n) * e^(-i*2PI*n*k/N), od n=0, do N-1
            // X(k) = sum(x(n) * cos(-2PI*n*k/N) + isin(-2PI*n*k/N), od n=0, do N-1

            for (int n1 = 0; n1 < N; n1++)
            {
                complexFrequency[n1] = 0;
                for (int n2 = 0; n2 < N; n2++)
                {
                    complexFrequency[n1] += 
                        new Complex( 
                            signal.Course[n2]* Math.Cos(-2 * Math.PI * n2 * n1 / N),
                            signal.Course[n2]* Math.Sin(-2 * Math.PI * n2 * n1 / N));
                    
                }
                if (complexFrequency[n1].Magnitude > max)
                    max = complexFrequency[n1].Magnitude;
            }
            maxFrequency = max;
            return complexFrequency;
        }
        /// <summary>
        /// Dyskretna transformata Fouriera
        /// </summary>
        public Complex[] DFT(Complex[] signal, int N)
        {
            Complex[] complexFrequency = new Complex[N];
            for (int n1 = 0; n1 < N; n1++)
            {
                complexFrequency[n1] = 0;
                for (int n2 = 0; n2 < N; n2++)
                {
                    complexFrequency[n1] +=
                        new Complex(
                            signal[n2].Real * Math.Cos(-2 * Math.PI * n2 * n1 / N),
                            signal[n2].Real * Math.Sin(-2 * Math.PI * n2 * n1 / N));
                }
            }
            return complexFrequency;
        }
        /// <summary>
        /// Odwrotna transformata Fouriera
        /// </summary>
        public Complex[] iDFT(Complex[] Frequency, int N)
        {
            Complex[] resultSignal = new Complex[N];
            for (int n1 = 0; n1 < N; n1++)
            {
                resultSignal[n1] = 0;
                for (int n2 = 0; n2 < N; n2++)
                {
                    resultSignal[n1] +=
                        new Complex(
                            Frequency[n2].Real * Math.Cos(2 * Math.PI * n2 * n1 / N),
                            Frequency[n2].Imaginary * Math.Sin(2 * Math.PI * n2 * n1 / N));
                }
                resultSignal[n1] *= 1.0 / N;
            }
            return resultSignal;
        }
    }
}
