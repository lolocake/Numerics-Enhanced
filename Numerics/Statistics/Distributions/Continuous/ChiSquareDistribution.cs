using System;

namespace Orbifold.Numerics
{
    /// <summary>
    ///     http://en.wikipedia.org/wiki/Chi-squared_distribution
    /// </summary>
    public class ChiSquareDistribution : ContinuousDistribution
    {
        // Distribution parameters

        // Distribution measures
        private double? entropy;

        /// <summary>
        ///     Constructs a new Chi-Square distribution
        ///     with given degrees of freedom.
        /// </summary>
        public ChiSquareDistribution() : this(1)
        {
        }

        /// <summary>
        ///     Constructs a new Chi-Square distribution
        ///     with given degrees of freedom.
        /// </summary>
        /// <param name="degreesOfFreedom">The degrees of freedom for the distribution. Default is 1.</param>
        public ChiSquareDistribution(int degreesOfFreedom)
        {
            if (degreesOfFreedom <= 0) throw new ArgumentOutOfRangeException("degreesOfFreedom", "The number of degrees of freedom must be higher than zero.");

            this.DegreesOfFreedom = degreesOfFreedom;
        }

        /// <summary>
        ///     Gets the Degrees of Freedom for this distribution.
        /// </summary>
        public int DegreesOfFreedom { get; private set; }

        /// <summary>
        ///     Upper limit of a random