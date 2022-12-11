using System;
using System.Numerics;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Complex Fast (FFT) Implementation of the Discrete Fourier Transform (DFT).
    /// </summary>
    /// <remarks>Note that there is some arbitraryness where to put the normalization factor. It was put in the inverse transform in both approaches.</remarks>
    public class DiscreteFourierTransform
    {

        /// <summary>
        /// Plain forward discrete Fourier transformation.
        /// </summary>
        /// <remarks>Forward means that the k-space vector is returned.</remarks>
        /// <returns>Corresponding frequency-space vector.</returns>
        public static Complex[] PlainForward(Complex[] samples)
        {
            if (samples.Length == 0) return new Complex[] { };
            var w0 = -2 * Constants.Pi / samples.Length;
            var spectrum = new Complex[samples.Length];
            for (var i = 0; i < samples.Length; i++)
            {
                var sum = Complex.Zero;
                for (var n = 0; n < samples.Length; n++)
                {
                    sum += samples[n] * new Complex(Math.Cos(n * w0 * i), Math.Sin(n * w0 * i));
                }
                spectrum[i] = sum;
            }
            return spectrum;
        }

        /// <summary>
        /// Plain inverse discrete Fourier transformation.
        /// </summary>
        /// <remarks>Forward means that the k-space vector is returned.</remarks>
        public static Complex[] PlainInverse(Complex[] samples)
        {
            if (samples.Length == 0) return new Complex[] { };
            var w0 = 2 * Constants.Pi / samples.Length;
            var spectrum = new Complex[samples.Length];
            for (var i = 0; i < samples.Length; i++)
            {
                var sum = Complex.Zero;
                for (var n = 0; n < samples.Length; n++)
                {
                    sum += samples[n] * new Complex(Math.Cos(n * w0 * i), M