﻿using System;
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
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). 
        /// The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static long BIN2DEC(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return 0L;

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return Convert.ToInt64(number, 2) - 512;
            }
            return Convert.ToInt64(number, 2);
        }

        /// <summary>
        /// Converts a binary number to hexadecimal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places"> The number of characters to use. If places is omitted, BIN2HEX uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        public static string BIN2HEX(string number, int places)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";
            return OCT2HEX(BIN2OCT(number), places);
        }

        /// <summary>
        /// Converts a binary number to hexadecimal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static string BIN2HEX(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";
            return OCT2HEX(BIN2OCT(number));
        }

        /// <summary>
        /// Converts a binary number to octal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        /// <param name="places">The number of characters to use. If places is omitted, BIN2OCT uses the minimum number of characters necessary. Places is useful for padding the return value with leading 0s (zeros).</param>
        public static string BIN2OCT(string number, int places)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return DEC2OCT(Convert.ToInt64(number, 2) - 512, places);
            }
            return DEC2OCT(Convert.ToInt64(number, 2), places);
        }

        /// <summary>
        /// Converts a binary number to octal.
        /// </summary>
        /// <param name="number">The binary number you want to convert. Number cannot contain more than 10 characters (10 bits). The most significant bit of number is the sign bit. The remaining 9 bits are magnitude bits. Negative numbers are represented using two's-complement notation.</param>
        public static string BIN2OCT(string number)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");
            if (number.Length > 10) throw new Exception("The given number cannot be longer than 10 characters.");
            if (number == "0") return "0";

            if (number.Length == 10)
            {
                number = number.Substring(1);
                return DEC2OCT(Convert.ToInt64(number, 2) - 512);
            }
            return DEC2OCT(Convert.ToInt64(number, 2));
        }

        /// <summary>
        /// Returns a bitwise 'AND' of two numbers.
        /// </summary>
        /// <param name="number1">Must be in decimal form and greater than or equal to 0.</param>
        /// <param name="number2">Must be in decimal form and greater than or equal to 0.</param>
        /// <returns></returns>
        public static long BITAND(long number1, long number2)
        {
            return number1 & number2;
        }

        /// <summary>
        /// Returns a number shifted left by the specified number of bits.
        /// </summary>
        /// <param name="number">Must be an integer greater than or equal to 0.</param>
        /// <param name="amount">Must be an integer.</param>
        public static long BITLSHIFT(long number, int amount)
        {
            return amount < 0 ? BITRSHIFT(number, -amount) : number << amount;
        }

        /// <summary>
        /// Returns a bitwise 'OR' of two numbers.
        /// </summary>
        /// <param name="number1"> Must be in decimal form and greater than or equal to 0.</param>
        /// <param name="number2"> Must be in decimal form and greater than or equal to 0.</param>
        public static long BITOR(long number1, long number2)
        {
            return number1 | number2;
        }

        /// <summary>
        /// Returns a number shifted right by the specified number of bits.
        /// </summary>
        /// <param name="number">Must be an integer greater than or equal to 0.</param>
        /// <param name="amount">Must be an integer.</param>
        public static long BITRSHIFT(long number, int amount)
        {
            return amount < 0 ? BITLSHIFT(number, -amount) : number >> amount;
        }

        /// <summary>
        /// Returns a bitwise 'XOR' of two numbers.
        /// </summary>
        /// <param name="number1">Must be greater than or equal to 0.</param>
        /// <param name="number2">Must be greater than or equal to 0.</param>
        public static long BITXOR(long number1, long number2)
        {
            return number1 ^ number2;
        }

        /// <summary>
        /// Converts real and imaginary coefficients into a complex number of the form x + yi or x + yj.
        /// </summary>
        /// <param name="a">The real coefficient of the complex number.</param>
        /// <param name="b">The imaginary coefficient of th