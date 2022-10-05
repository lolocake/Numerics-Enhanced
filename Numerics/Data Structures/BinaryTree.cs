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

		/// <summary>
		/// Gets whether both sides are occupied, i.e. the left and right positions are filled.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is full; otherwise, <c>false</c>.
		/// </value>
		public bool IsComplete {
			get {
				return (this.leftSubtree != null) && (this.rightSubtree != null);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this tree is empty.
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
		/// Gets whether this is a leaf node, i.e. it doesn't have children nodes.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is leaf node; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsLeafNode {
			get {
				return this.Degree == 0;
			}
		}

		/// <summary>
		/// Returns <c>false</c>; this tree is never read-only.
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
		/// Gets or sets the left subtree.
		/// </summary>
		/// <value>The left subtree.</value>
		public virtual BinaryTree<TData> Left {
			get {
				return this.leftSubtree;
			}
			set {
				if(this.leftSubtree != null)
					this.RemoveLeft();
				if(value != null) {
					if(value.Parent != null)
						value.Parent.Remove(value);
					value.Parent = this;
				}

				this.leftSubtree = value;
			}
		}

		/// <summary>
		/// Gets the parent of the current node.
		/// </summary>
		/// <value>The parent of the current node.</value>
		public BinaryTree<TData> Parent { get; set; }

		/// <summary>
		/// Gets or sets the right subtree.
		/// </summary>
		/// <value>The right subtree.</value>
		public virtual BinaryTree<TData> Right {
			get {
				return this.rightSubtree;
			}
			set {
				if(this.rightSubtree != null)
					this.RemoveRight();
				if(value != null) {
					if(value.Parent != null)
						value.Parent.Remove(value);
					value.Parent = this;
				}
				this.rightSubtree = value;
			}
		}

		/// <summary>
		/// Gets the root of the binary tree.
		/// </summary>
		public BinaryTree<TData> Root {
			get {
				var runner = this.Parent;
				while(runner != null) {
					if(runner.Parent != null)
						runner