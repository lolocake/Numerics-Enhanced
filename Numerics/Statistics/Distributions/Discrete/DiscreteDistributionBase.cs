using System;

namespace Orbifold.Numerics
{

	/// <summary>
	/// Base class for all discrete stochastic distributions.
	/// </summary>
	public abstract class DiscreteDistributionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DiscreteDistributionBase"/> class, using a 
		/// <see cref="DefaultSource"/> as underlying random number generator.
		/// </summary>
		protected DiscreteDistributionBase()
			: this(new DefaultSource())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscreteDistributionBase"/> class, using the
		/// specified <see cref="Randomizer"/> as underlying random number generator.
		/// </summary>
		/// <param name="generator">A <see cref="Randomizer"/> object aka stochastic source.</param>
		protected DiscreteDistributionBase(IStochastic generator)
		{
			if (