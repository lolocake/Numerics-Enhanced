using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Standard implementation of a binary tree.
	/// </summary>
	/// <typeparam name="TData">The data type on which the tree is based.</typeparam>
	/// <seealso cref="RedBlackTree{TKey,TValue}"/>
	public class BinaryTree<TData> : ICollection<TData>, ITree<TData>
	{
		private BinaryTree<TData> leftSubtree;

		private BinaryTree<TData> rightSubtree;

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryTree&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		public BinaryTree(TData data, TData left, TData right)
			: this(data, new BinaryTree<TData>(left), new BinaryTree<TData>(right))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryTree&lt;TData&gt;"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		public BinaryTree(TData data, BinaryTree<TData> left = null, BinaryTree<TData> right = null)
		{
			this.leftSubtree = left;
			if(left != null)
				left.Parent = this;
			this.rightSubtree = right;
			if(right != null)
				right.Parent = this;
			this.Data = data;
		}

		/// <summary>
		/// Gets the number of children at this level, which can be at most two.
		/// </summary>
		public int Count {
			get {
				var count = 0;
				if(this.leftSubtree != null)
					count++;
				if(this.rightSubtree != null)
					count++;
				return count;
			}
		}

		/// <summary>
		/// Gets or sets the data of this tree.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public TData Data { get; set; }

		/// <summary>
		/// Gets the degree.
		/// </summary>
		public int Degree {
			get {
				return this.Count;
			}
		}

		/// <summary>
		/// Gets the height.
		/// </summary>
		public virtual int Height {
			get {
				if(this.Degree == 0)
					return 0;
				return 1 + this.FindMaximumChildHeight();
			}
		}

		