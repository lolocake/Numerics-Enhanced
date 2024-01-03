using System;

namespace Orbifold.Numerics
{
    /// <summary>
	/// Gaussian or Normal probability distribution.
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// <description>See <see cref="http://en.wikipedia.org/wiki/Normal_distribution">
	/// Wikipedia - Normal distribution</see>.</description></item>
	/// <item>
	/// <description>The Box-Muller transformation: http://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform .</description></item>
	/// <item>
	/// <description>See the <i>Box-Muller</i> algorithm for generating random deviates
	/// with a normal distribution: <see cref="http://www.library.cornell.edu/nr/">
	/// Numerical recipes in C</see> (chapter 7)</description></item></list>
	/// </remarks>
	public sealed class GaussianDistribution : ContinuousDistribution
	{
		private double mean;
		private double stdDev;

		/// <summary>
		/// Initializes a new instance of the <see cref="GaussianDistribution"/> class.
		/// </summary>
		public GaussianDistribution()
		{
		}

		public GaussianDistribution(double mean, double standardDeviation)
		{
			SetParameters(mean, standardDeviation);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GaussianDistribution"/> class.
		/// </summary>
		/// <param name="random">The random.</param>
		public GaussianDistribution(IStochastic random)
			: base(random)
		{
		}

		/// <summary>
		/// Gets the maximum possible value of the distribution.
		/// </summary>
		public override double Maximum {
			get {
				return double.MaxValue;
			}
		}

		/// <