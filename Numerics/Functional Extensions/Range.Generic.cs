using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Generic range implementation.
	/// </summary>
	/// <typeparam name="TData">The data type of the range.</typeparam>
	public class Range<TData> : IEnumerable<TData>
	{
		private readonly Comparison<TData> compare;

		private readonly TData end;

		private readonly IEnumerable<TData> sequence;

		private readonly TData start;

		/// <summary>
		/// Initializes a new instance of the <see cref="Range&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="start">The start-element of the range.</param>
		/// <param name="end">The end-element of the range.</param>
		/// <param name="nextElement">The functional mapping from one element to the next one.</param>
		/// <param name="compare">The comparison between two items.</param>
		public Range(TData start, TData end, Func<TData, TData> nextElement, Comparison<TData> compare)
		{
			this.start = start;
			this.end = end;
			this.compare = compare;
			if(compare(start, end) == 0)
				this.sequence = new []{ start };
			else if(compare(start, end) < 0)
				this.sequence = Sequence.CreateSequence(nextElement, start, v => compare(nextElement(v), end) > 0);
			else
				this.sequence = Sequence.CreateSequence(nextElement, start, v => compare(nextElement(v), end) < 0);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Range&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <param name="nextElement">The get next.</param>
		public Range(TData start, TData end, Func<TData, TData> nextElement)
			: this(start, end, nextElement, Compare)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.Range`1"/> class.
		/// </summary>
		/// <param name="source">Source.</param>
		public Range(IEnumerable<TData> source)
		{
			this.start = source.First();
			this.end = source.Last();
			this.sequence = source;
			this.compare = Compare;
		}

		/// <summary>
		/// Gets the end of the range.
		/// </summary>
		public TData End {
			get {
				return this.end;
			}
			 
		}

		/// <summary>
		//