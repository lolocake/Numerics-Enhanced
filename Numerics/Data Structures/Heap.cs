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
		/// <param name="type">The type.</param>
		/// <param name="capacity">The capacity.</param>
		public Heap(OrderType type, int capacity)
			: this(type, capacity, Comparer<TData>.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Heap&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="comparer">The comparer.</param>
		public Heap(OrderType type, Comparison<TData> comparer)
			: this(type, new ComparisonComparer<TData>(comparer))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Heap&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="capacity">The capacity.</param>
		/// <param name="comparer">The comparer.</param>
		public Heap(OrderType type, int capacity, Comparison<TData> comparer)
			: this(type, capacity, new ComparisonComparer<TData>(comparer))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Heap&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="comparer">The comparer.</param>
		public Heap(OrderType type, IComparer<TData> comparer)
		{
			if((type != OrderType.Ascending) && (type != OrderType.Descending)) {
				throw new ArgumentOutOfRangeException("type");
			}
			this.orderingType = type;
			this.internalList = new List<TData> { default(TData) };
			this.comparer = type == OrderType.Ascending ? comparer : new ReverseComparer<TData>(comparer);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Heap&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="capacity">The capacity.</param>
		/// <param name="comparer">The comparer.</param>
		public Heap(OrderType type, int capacity, IComparer<TData> comparer)
		{
			if((type != OrderType.Ascending) && (type != OrderType.Descending)) {
				throw new ArgumentOutOfRangeException("type");
			}
			this.orderingType = type;
			this.internalList = new List<TData>(capacity) { default(TData) };
			this.comparer = type == OrderType.Ascending ? comparer : new ReverseComparer<TData>(comparer);
		}

		/// <summary>
		/// Gets the number of items in this heap structure.
		/// </summary>
		public int Count {
			get {
				return this.internalList.Count - 1;
			}
		}

		/// <summary>
