﻿using System;

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
        ///     Upper limit of a random variable with this probability distribution.
        /// </summary>
        public override double Maximum
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Gets the mean for this distribution.
        /// </summary>
        /// <remarks>
        ///     The χ² distribution mean is the number of degrees of freedom.
        /// </remarks>
        public override double Mean
        {
            get
            {
                return this.DegreesOfFreedom;
            }
            set
            {
                this.DegreesOfFreedom = (int)value;
            }
        }

        /// <summary>
        ///     The value separating the lower half part from the upper half part of a random variable with this probability
        ///     distribution.
        /// </summary>
        public override double Median
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Gets the variance for this distribution.
        /// </summary>
        /// <remarks>
        ///     The χ² distribution variance is twice its degrees of freedom.
        /// </remarks>
        public override double Variance
        {
            get
            {
                return 2.0 * this.DegreesOfFreedom;
            }
        }

        /// <summary>
        ///     Measure of the asymmetry of this probability distribution.
        /// </summary>
        public override double Skewness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Gets the complementary cumulative distribution function
        ///     (ccdf) for the χ² distribution evaluated at point <c>x</c>.
        ///     This function is also known as the Survival function.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The Complementary Cumulative Distribution Function (CCDF) is
        ///         the complement of the Cumulative Distribution Function, or 1
        ///         minus the CDF.
        ///     </para>
        ///     <para>
        ///         The χ² complementary distribution function is defined in terms of the
        ///         <see cref="Gamma