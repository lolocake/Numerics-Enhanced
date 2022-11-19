using System;
using System.Numerics;

namespace Orbifold.Numerics
{
    /// <summary>
    /// This class contains all the Excel engineering functions.
    /// </summary>
    public sealed class EngineeringFunctions
    {
        /// <summary>
        /// Returns the modified Bessel function, which is equivalent to the Bessel function evaluated for purely imaginary arguments.
        /// </summary>
        /// <param name="x">The value at which to evaluate the function.</param>
        /// <param name="n">The order of the Bessel function. If n is not an integer, it is truncated.</param>
        /// <returns></returns>
        public static double BESSELI(double x, double n)
        {
            // note tha Excel truncates the order
            // note also that Mathematica switches the argumens and the order is the first parameter there
            return Functions.BesselI(x, n.Truncate());
        }

        /// <summary>
        /// Returns the Bessel function.
        /// </summary>
        /// <param name="x">The value at which to evaluate the function.</param>
        /// <param name="n">The order of the Bessel function. If n is not an integer, it is truncated.</param>
        /// <returns></returns>
        public static double BESSELJ(double x, double n)
        {
            return Functions.BesselJ(x, n.Truncate());
        }

        /// <summary>
        /// Returns the modified Bessel function, which is equivalent to the Bessel functions evaluated for purely imaginary arguments.
        /// </summary>
        /// <param name="x">The value at which to evaluate the function.</param>
        /// <param name="n">The order of the Bessel function. If n is not an integer, it is truncated.</param>
        /// <returns></returns>
        public static double BESSELK(double x, double n)
        {
            return Functions.BesselK(x, n.Truncate());
        }

        /// <summary>
        /// Returns the Bessel function, which is also called the Weber function or the Neumann function.
        /// </summary>
        /// <param name="x">The value at which to evaluate the function.</param>
        /// <param name="n">The order of the Bessel function. If n is not an integer, it is truncated.</param>
        /// <returns></returns>
        public static double BESSELY(double x, double n)
        {
            return Functions.BesselY(x, n.Truncate());
        }

        /// <summary>
        /// Converts a binary number to decimal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 ch