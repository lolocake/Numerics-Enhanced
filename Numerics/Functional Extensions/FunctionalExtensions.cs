
﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	#if PCL
	public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
	#endif
	/// <summary>
	/// A collection of utility methods inspired by functional programming techniques in F#.
	/// </summary>
	public static class FunctionalExtensions
	{
		/*Swa: need to be able to replace the Random with IStochastic*/

		/// <summary>
		/// The randomizer.
		/// </summary>
        private static readonly System.Random Rand = new System.Random(Environment.TickCount);

		/// <summary>
		/// Appends the given list to the current one.
		/// </summary>
		/// <param name="list">The one.</param>
		/// <param name="otherList">The other.</param>
		/// <returns></returns>
		public static FunctionalList<T> Append<T>(this FunctionalList<T> list, FunctionalList<T> otherList)
		{
			return list.IsEmpty ? otherList : list.Reverse().Aggregate(otherList, (current, element) => current.Cons(element));
		}

		/// <summary>
		/// Returns whether all the elements are different in the given sequence.
		/// </summary>
		/// <typeparam name="T">The data type contained in the list.</typeparam>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public static bool AreAllDifferent<T>(this IEnumerable<T> list)
		{
			if(list == null) {
				throw new ArgumentNullException("list");
			}
			return !list.Any(m => list.Count(l => EqualityComparer<T>.Default.Equals(l, m)) > 1);
		}

		/// <summary>
		/// Collects elements into a new sequence by applying the given converter to each element.
		/// </summary>
		/// <typeparam name="TDomain">The data type of the list.</typeparam>
		/// <typeparam name="TRange">The data type of the target list.</typeparam>
		/// <param name="converter">The converter.</param>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public static IEnumerable<TRange> Collect<TDomain, TRange>(this IEnumerable<TDomain> list, Converter<TDomain, IEnumerable<TRange>> converter)
		{
			var listOfLists = Map(list, converter);
			return Concat(listOfLists);
		}

		/// <summary>
		/// Concatenates the sequence of sequences into one sequence.
		/// </summary>
		/// <typeparam name="T">The data type of the list.</typeparam>