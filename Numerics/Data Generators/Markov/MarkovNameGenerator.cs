using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A Markov chain implementation geared towards the generation of single words.
	/// See for example http://en.wikipedia.org/wiki/Markov_chain .
	/// </summary>
	public class MarkovNameGenerator
	{
		private readonly Dictionary<string, List<char>> chains = new Dictionary<string, List<char>>();

		private readonly int minLength;

		private readonly int order;

        private readonly System.Random rnd = new System.Random(Environment.TickCount);

		private readonly List<string> samples = new List<string>();

		private readonly List<string> used = new List<string>();

		/// <summary>
		/// Gets or sets whether the generated items are unique across the lifetime of this generator.
		/// </summary>
		/// <value>
		///   <c>true</c> if uniqueness should be ensured; otherwise, <c>false</c>.
		/// </value>
		public static bool EnsureUniqueness { get; set; }

		/// <summary>
		/// Initializes a new instance