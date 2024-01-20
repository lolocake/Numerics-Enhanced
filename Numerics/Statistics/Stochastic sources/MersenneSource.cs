using System;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// The Mersenne twister is a pseudorandom number generator developed in 1997 by Makoto Matsumoto 
	/// and Takuji Nishimura that is based on a matrix linear recurrence over a finite binary field.
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// <description>For implementation details see the Mersenne Twister Home Page: http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html .</description></item>
	/// <item>
	/// <description>See Wikipedia for general info on this topic: http://en.wikipedia.org/wiki/Mersenne_twister .</description></item></list>
	/// </remarks>
	public class MersenneSource : StochasticBase
	{
		/// <summary>
		/// Represents the multiplier that computes a double-precision floating point number greater than or equal to 0.0 
		///   and less than 1.0 when it gets applied to a nonnegative 32-bit signed integer.
		/// </summary>
		private const double IntToDoubleMultiplier = 1.0 / (int.MaxValue + 1D);

		/// <summary>
		/// Represents the least significant r bits. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 0x7fffffff.</remarks>
		private const uint LowerMask = 0x7fffffffU;

		/// <summary>
		/// Represents a constant used for generation of unsigned random numbers. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 397.</remarks>
		private const int M = 397;

		/// <summary>
		/// Represents the number of unsigned random numbers generated at one time. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 624.</remarks>
		private const int N = 624;

		/// <summary>
		/// Represents the multiplier that computes a double-precision floating point number greater than or equal to 0.0 
		///   and less than 1.0  when it gets applied to a 32-bit unsigned integer.
		/// </summary>
		private const doub