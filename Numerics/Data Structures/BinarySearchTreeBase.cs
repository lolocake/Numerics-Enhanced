
using System;
using System.Collections;
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Base implementation of the <see cref="ISearchTree{T}"/> interface.
	/// </summary>
	/// <typeparam name="T">The data type contained in this collection.</typeparam>
	/// <seealso cref="RedBlackTree{TKey,TValue}"/>
	public abstract class BinarySearchTreeBase<T> : ISearchTree<T>
	{
		private readonly IComparer<T> comparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="BinarySearchTreeBase{T}"/> class.
		/// </summary>
		protected BinarySearchTreeBase()
		{
			this.comparer = Comparer<T>.Default;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinarySearchTreeBase{T}"/> class.
		/// </summary>
		/// <param name="comparer">The comparer to use when comparing items.</param>
		/// <exception cref="ArgumentNullException"><paramref name="comparer"/> is a null reference (<c>Nothing</c> in Visual Basic).</exception>
		protected BinarySearchTreeBase(IComparer<T> comparer)
		{
			this.comparer = comparer;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinarySearchTreeBase&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="comparison">The comparison.</param>
		protected BinarySearchTreeBase(Comparison<T> comparison)
		{
			this.comparer = new ComparisonComparer<T>(comparison);
		}

		/// <summary>
		/// A custom comparison between some search value and the type of item that is kept in the tree.
		/// </summary>
		/// <typeparam name="TSearch">The type of the search.</typeparam>
		protected delegate int CustomComparison<in TSearch>(TSearch value, T item);

		/// <summary>
		/// Gets the comparer.
		/// </summary>
		/// <value>The comparer.</value>
		public IComparer<T> Comparer {
			get {
				return this.comparer;
			}
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		public int Count { get; private set; }

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
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is read only; otherwise, <c>false</c>.
		/// </value>
		public bool IsReadOnly {
			get {
				return false;
			}
		}

		public int Height {
			get {
				if(this.Tree.IsEmpty)
					return 0;
				return System.Math.Max(this.Tree.Left == null ? 0 : this.Tree.Left.Height, this.Tree.Right == null ? 0 : this.Tree.Right.Height);
			}
		}

		/// <summary>
		/// Gets the maximum.
		/// </summary>
		public virtual T Maximum {
			get {
				this.ValidateEmpty();
				return this.FindMaximumNode().Data;
			}
		}

		/// <summary>
		/// Gets the minimum.
		/// </summary>
		public virtual T Minimum {
			get {
				this.ValidateEmpty();
				return this.FindMinimumNode().Data;
			}
		}

		/// <summary>
		/// Gets or sets the tree.
		/// </summary>
		/// <value>
		/// The tree.
		/// </value>
		protected BinaryTree<T> Tree { get; set; }

		/// <summary>
		/// Adds the specified item.
		/// </summary>
		/// <param name="item">The item.</param>
		public void Add(T item)
		{
			this.AddItem(item);
			this.Count++;
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public void Clear()
		{
			this.ClearItems();
		}

		/// <summary>
		/// Determines whether the item is in this tree.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>
		///   <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool Contains(T item)
		{
			var node = this.FindNode(item);
			return node != null;
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="arrayIndex">Index of the array.</param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if((array.Length - arrayIndex) < this.Count)
				throw new ArgumentException(Resource.ArrayTooSmall, "array");
			foreach(var association in this.Tree)
				array[arrayIndex++] = association;
		}

		/// <summary>
		/// Depthes the first traversal.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		public void DepthFirstTraversal(IVisitor<T> visitor)
		{
			VisitNode(this.Tree, visitor);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<T> GetEnumerator()
		{
			if(this.Tree != null) {
				var stack = new Stack<BinaryTree<T>>();
				stack.Push(this.Tree);
				while(stack.Count > 0) {
					var binaryTree = stack.Pop();
					yield return binaryTree.Data;
					if(binaryTree.Left != null)
						stack.Push(binaryTree.Left);
					if(binaryTree.Right != null)
						stack.Push(binaryTree.Right);
				}
			}
		}

		/// <summary>
		/// Gets the ordered enumerator.
		/// </summary>
		/// <returns></returns>