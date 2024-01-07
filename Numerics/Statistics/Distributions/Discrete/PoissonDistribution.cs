using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Generates Poisson distributed values.
	/// </summary>
	/// <remarks> 
	/// <para>See http://en.wikipedia.org/wiki/Poisson_distribution .</para>
	/// <para>See http://www.lkn.ei.tum.de/lehre/scn/cncl/doc/html/cncl_toc.html .</para>
	/// </remarks>
	public sealed class PoissonDistribution : DiscreteDistributionBase
	{
		do