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
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
		/// </value>
		public bool IsEmpty {
			get {
				return this.Count == 0;
			}
		}

		/// <summary>
		/// Returns <c>false</c>; the heap is never read-only.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is read only; otherwise, <c>false</c>.
		/// </value>
		public bool IsReadOnly {
			get {
				return false;
			}
		}

		/// <summary>
		/// Gets the root of the heap.
		/// </summary>
		public TData Root {
			get {
				if(this.Count == 0) {
					throw new InvalidOperationException(Resource.IsEmpty);
				}
				return this.internalList[1];
			}
		}

		/// <summary>
		/// Gets the type of heap.
		/// </summary>
		/// <value>The type of heap.</value>
		public OrderType Type {
			get {
				return this.orderingType;
			}
		}

		/// <summary>
		/// Adds the given item to the heap.
		/// </summary>
		/// <param name="item">The item to be added.</param>
		public void Add(TData item)
		{
			this.AddItem(item);
		}

		/// <summary>
		/// Clears this heap of all data.
		/// </summary>
		public void Clear()
		{
			this.ClearItems();
		}

		/// <summary>
		/// Returns whether the given item is in the heap.
		/// </summary>
		/// <param name="item">The item to test.</param>
		/// <returns>
		///   <c>true</c> if contained in this heap; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(TData item)
		{
			return this.internalList.Contains(item);
		}

		/// <summary>
		/// Copies the heap data to an array.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="position">Index of the array from which the copying starts.</param>
		public void CopyTo(TData[] array, int position)
		{
			if((array.Length - position) < this.Count) {
				throw new ArgumentException(Resource.ArrayTooSmall, "array");
			}
			for(var i = 1; i < this.internalList.Count; i++) {
				array[position++] = this.internalList[i];
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<TData> GetEnumera