
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A collection of methods related to random data generation.
	/// </summary>
	public static class DataGenerator
	{
		private static MarkovNameGenerator markovFemaleNameGenerator;
		private static MarkovNameGenerator markovFamilyNameGenerator;
		private static MarkovNameGenerator markovTaskNameGenerator;

		/// <summary>
		/// The randomizer at the basis of all.
		/// </summary>
        private static readonly System.Random Rand = new System.Random(Environment.TickCount);

		/// <summary>
		/// Returns a random paragraph of lipsum text of the specified length.
		/// </summary>
		/// <param name="numWords">The num words; default is 25 and must be more than 5.</param>
		/// <returns></returns>
		public static string RandomLipsum(int numWords = 25)
		{
			numWords = System.Math.Max(numWords