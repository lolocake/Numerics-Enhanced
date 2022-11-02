using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Orbifold.Numerics
{
	/// <summary>
	/// An implementation of the Heap data structure.
	/// </summary>
	/// <remarks>See http://en.wikipedia.org/wiki/Heap_%28data_structure%29 .</remarks>
	/// <typeparam name="TData">The type of item stored in the <see cref="Heap{T}"/>.</typeparam>
	public class Heap<TData> : ICollection<TData>
	{
		private readonly IComparer<TData> comparer;

		private readonly List<TData> internalList;

		private readonly OrderType orderingType;

		/// <summary>
		/// Initializes a new instance of the <see cref="Heap&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public Heap(OrderType type)
			: this(type, Comparer<TData>.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Heap&lt;TData&gt;"/> class.
		/// </summary>