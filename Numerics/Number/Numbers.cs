using System;
using System.Linq;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Number theory utility functions for integers.
	/// </summary>
	public static class Numbers
	{
		/// <summary>
		/// Find out whether the provided 32 bit integer is an even number.
		/// </summary>
		/// <param name="number">The number to very whether it's even.</param>
		/// <returns>True if and only if it is an even number.</returns>
		public static bool IsEven(this int number)
		{
			return (number & 0x1) == 0x0;
		}

        /// <summary>
        /// Truncates the specified number by dropping its decimals.
        /// </summary>
        /// <param name="number">The number.</param>
	    public static long Truncate(this double number)
        {
            if (Math.Abs(number) < Constants.Epsilon) return 0;
            return number < 0 ? (long)System.Math.Ceiling(number) : (long)System.Math.Floor(number);
        }

	    /// <summary>
		/// Find out whether the provided 32 bit integer is an odd number.
		/// </summary>
		/// <param name="number">The number to very whether it's odd.</param>
		/// <returns>True if and only if it is an odd number.</returns>
		public static bool IsOdd(this int number)
		{
			return (number &