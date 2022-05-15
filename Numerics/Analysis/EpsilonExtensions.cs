
namespace Orbifold.Numerics
{
	public static class EpsilonExtensions
	{
		#region Done/Reviewed

		/// <summary>
		/// Returns whether the given value is less than an <see cref="Constants.Epsilon"/>.
		/// </summary>
		/// <param name="value">A value.</param>
		/// <returns>
		///   <c>true</c> if less than <see cref="Constants.Epsilon"/>; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsVerySmall(this double value)
		{
			return System.Math.Abs(value) < Constants.Epsilon;
		}

		/// <summary>
		/// Checks whether two values are close, i.e. the absolute value of their difference is less than <see cref="Constants.Epsilon"/>.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns></returns>
		public static bool AreClose(double value1, double value2)
		{
			return value1.IsEqualTo(value2) || (value1 - value2).IsVerySmall();
		}

		/// <summary>
		/// Checks whether two values are not close, i.e. the absolute value of their difference is larger than <see cref="Constants.Epsilon"/>.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns></returns>
		public static bool AreNotClose(double value1, double value2)
		{
			return !AreClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly less than another one in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than the second or only an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessThanOrClose(double value1, double value2)
		{
			return value1 < value2 || AreClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly less than another one in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than the second or only an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessThanOrCloseTo(this double value1, double value2)
		{
			return IsLessThanOrClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is less than or equal to another one in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than or equal to the second or only an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessOrEqual(double value1, double value2)
		{
			return value1 < (value2 + Constants.Epsilon);
		}

		/// <summary>
		/// Determines whether a given value is less than or equal to another one in an epsilon sense.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than or equal to the second or only an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessOrEqualTo(this double value1, double value2)
		{
			return IsLessOrEqual(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly less than another one  and not an epsilon away.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less than the second and not an epsilon apart; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsLessThan(double value1, double value2)
		{
			return (value1 < value2) && !AreClose(value1, value2);
		}

		/// <summary>
		/// Determines whether a given value is strictly bigger than another one and not an epsilon away.
		/// </summary>
		/// <param name="value1">A value.</param>
		/// <param name="value2">Another value.</param>
		/// <returns>
		///   <c>True</c> if the first value is less th