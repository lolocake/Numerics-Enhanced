
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Histogram extensions.
	/// </summary>
	public static class HistogramExtensions
	{
		/// <summary>
		/// Creates the histogram within the specified interval.
		/// </summary>
		/// <param name="data">The data to partition in the histogram.</param>
		/// <param name="min">The start-value of the histogram.</param>
		/// <param name="max">The end-value of the histogram.</param>
		/// <param name="partitionCount">The nLumber of partitions.</param>
		public static double[] MakeHistogram(this IEnumerable<double> data, double min, double max, int partitionCount)
		{
			var histogram = new double[partitionCount];
			var binWidth = (max - min) / partitionCount;
			var doubles = data as double[] ?? data.ToArray();
			for(var i = 0; i < partitionCount; i++) {
				var nCounts = doubles.Count(t => t >= min + (i) * binWid