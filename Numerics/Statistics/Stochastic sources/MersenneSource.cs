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
		private const double UIntToDoubleMultiplier = 1.0 / (uint.MaxValue + 1D);

		/// <summary>
		/// Represents the most significant w-r bits. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 0x80000000.</remarks>
		private const uint UpperMask = 0x80000000U;

		/// <summary>
		/// Represents the constant vector a. This field is constant.
		/// </summary>
		/// <remarks>The value of this constant is 0x9908b0dfU.</remarks>
		private const uint VectorA = 0x9908b0dfU;

		/// <summary>
		/// Stores the state vector array.
		/// </summary>
		private readonly long[] mt;

		/// <summary>
		/// Stores the used seed value.
		/// </summary>
		private readonly int seed;

		/// <summary>
		/// Stores the used seed array.
		/// </summary>
		private readonly int[] seedArray;

		/// <summary>
		/// Stores an <see cref="uint"/> used to generate up to 32 random <see cref="bool"/> values.
		/// </summary>
		private int bitBuffer;

		/// <summary>
		/// Stores how many random <see cref="bool"/> values still can be generated from <see cref="bitBuffer"/>.
		/// </summary>
		private int bitCount;

		/// <summary>
		/// Stores an index for the state vector array element that will be accessed next.
		/// </summary>
		private uint mti;

		/// <summary>
		/// Initializes a new instance of the <see cref="MersenneSource"/> class, using a time-dependent default 
		///   seed value.
		/// </summary>
		public MersenneSource()
			: this(System.Math.Abs(Environment.TickCount))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MersenneSource"/> class, using the specified seed value.
		/// </summary>
		/// <param name="seed">
		/// An unsigned number used to calculate a starting value for the pseudo-random number sequence.
		/// </param>
		public MersenneSource(int seed)
		{

			this.mt = new long[N];
			this.seed = System.Math.Abs(seed);
			this.Reset();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MersenneSource"/> class, using the specified seed array.
		/// </summary>
		/// <param name="seedArray">
		/// An array of numbers used to calculate a starting values for the pseudo-random number sequence.
		/// If negative numbers are specified, the absolute values of them are used. 
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="seedArray"/> is NULL (<see langword="Nothing"/> in Visual Basic).
		/// </exception>
		public MersenneSource(IList<int> seedArray)
		{
			if (seedArray == null) throw new ArgumentNullException("seedArray");

			this.mt = new long[N];
			this.seed = 19650218;

			this.seedArray = new int[seedArray.Count];
			for (var index = 0; index < seedArray.Count; index++) this.seedArray[index] = System.Math.Abs(seedArray[index]);

			this.Reset();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MersenneSource"/> class, using the specified seed array.
		/// </summary>
		/// <param name="seedArray">
		/// An array of unsigned numbers used to calculate a starting values for the pseudo-random number sequence.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="seedArray"/> is NULL (<see langword="Nothing"/> in Visual Basic).
		/// </exception>
		public MersenneSource(int[] seedArray)
		{
			if (seedArray == null)
			{
				throw new ArgumentNullException("seedArray");
			}
			this.mt = new long[N];
			this.seed = 19650218;
			this.seedArray = seedArray;
			this.Reset();
		}

		/// <summary>
		/// Returns a nonnegative random number less than <see cref="Int32.MaxValue"/>.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to 0, and less than <see cref="Int32.MaxValue"/>; that is, 
		///   the range of return values includes 0 but not <paramref>
		///                                                   <name>Int32.MaxValue</name>
		///                                                 </paramref> .
		/// </returns>
		public override int Next()
		{
			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N)
			{
				// generate N words at one time
				this.GenerateUnsignedInts();
			}

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			var result = (int)(y >> 1);

			// Exclude Int32.MaxValue from the range of return values.
			return result == int.MaxValue ? this.Next() : result;
		}

		/// <summary>
		/// Returns a nonnegative random number less than the specified maximum.
		/// </summary>
		/// <param name="maxValue">
		/// The exclusive upper bound of the random number to be generated. 
		/// <paramref name="maxValue"/> must be greater than or equal to 0. 
		/// </param>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to 0, and less than <paramref name="maxValue"/>; that is, 
		///   the range of return values includes 0 but not <paramref name="maxValue"/>. 
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="maxValue"/> is less than 0. 
		/// </exception>
		public override int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue");
			}

			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N)
			{
				// generate N words at one time
				this.GenerateUnsignedInts();
			}

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 11;
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= y >> 18;

			// The shift operation and extra int cast before the first multiplication give better performance.
			// See comment in NextDouble().
			return (int)((int)(y >> 1) * IntToDoubleMultiplier * maxValue);
		}

		/// <summary>
		/// Returns a random number within the specified range. 
		/// </summary>
		/// <param name="minValue">
		/// The inclusive lower bound of the random number to be generated. 
		/// </param>
		/// <param name="maxValue">
		/// The exclusive upper bound of the random number to be generated. 
		/// <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>. 
		/// </param>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to <paramref name="minValue"/>, and less than 
		///   <paramref name="maxValue"/>; that is, the range of return values includes <paramref name="minValue"/> but 
		///   not <paramref name="maxValue"/>. 
		/// If <paramref name="minValue"/> equals <paramref name="maxValue"/>, <paramref name="minValue"/> is returned.  
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="minValue"/> is greater than <paramref name="maxValue"/>.
		/// </exception>
		public override int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue) throw new ArgumentOutOfRangeException("maxValue");

			// Its faster to explicitly calculate the unsigned random number than simply call NextUInt().
			if (this.mti >= N) this.GenerateUnsignedInts();

			var y = this.mt[this.mti++];

			// Tempering
			y ^= y >> 1