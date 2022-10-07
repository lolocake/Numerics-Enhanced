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
						runner = runner.Parent;
					else
						return runner;
				}
				return this;
			}
		}

		/// <summary>
		/// Gets the parent.
		/// </summary>
		ITree<TData> ITree<TData>.Parent {
			get {
				return this.Parent;
			}
		}

		/// <summary>
		/// Gets the <see cref="BinaryTree{T}"/> at the specified index.
		/// </summary>
		public BinaryTree<TData> this[int index] {
			get {
				return this.GetChild(index);
			}
		}

		/// <summary>
		/// Adds the given item to this tree.
		/// </summary>
		/// <param name="item">The item to add.</param>
		public virtual void Add(TData item)
		{
			this.AddItem(new BinaryTree<TData>(item));
		}

		/// <summary>
		/// Adds an item to the <see cref="ICollection{T}"/>.
		/// </summary>
		/// <param name="subtree">The subtree.</param>
		/// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
		/// <exception cref="InvalidOperationException">The <see cref="BinaryTree{T}"/> is full.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="subtree"/> is null (Nothing in Visual Basic).</exception>
		public void Add(BinaryTree<TData> subtree)
		{
			this.AddItem(subtree);
		}

		/// <summary>
		/// Performs a breadth first traversal on this tree with the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <exception cref="ArgumentNullException"><paramref name="visitor"/> is a null reference (<c>Nothing</c> in Visual Basic).</exception>
		public virtual void BreadthFirstTraversal(IVisitor<TData> visitor)
		{
			var queue = new Queue<BinaryTree<TData>>();

			queue.Enqueue(this);

			while(queue.Count > 0) {
				if(visitor.HasCompleted) {
					break;
				}
				var binaryTree = queue.Dequeue();
				visitor.Visit(binaryTree.Data);

				for(var i = 0; i < binaryTree.Degree; i++) {
					var child = binaryTree.GetChild(i);
					if(child != null) {
						queue.Enqueue(child);
					}
				}
			}
		}

		/// <summary>
		/// Clears this tree of its content.
		/// </summary>
		public virtual void Clear()
		{
			if(this.leftSubtree != null) {
				this.leftSubtree.Parent = null;
				this.leftSubtree = null;
			}
			if(this.rightSubtree != null) {
				this.rightSubtree.Parent = null;
				this.rightSubtree = null;
			}
		}

		/// <summary>
		/// Returns whether the given item is contained in this collection.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>
		///   <c>true</c> if is contained in this collection; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(TData item)
		{
			return Enumerable.Contains(this, item);
		}

		/// <summary>
		/// Copies the tree to the given array.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="arrayIndex">Index of the array.</param>
		public void CopyTo(TData[] array, int arrayIndex)
		{
			foreach(var item in this) {
				if(arrayIndex >= array.Length) {
					throw new ArgumentException(Resource.ArrayTooSmall, "array");
				}
				array[arrayIndex++] = item;
			}
		}

		/// <summary>
		/// Performs a depth first traversal on this tree with the specified visitor.
		/// </summary>
		/// <param name="visitor">The ordered visitor.</param>
		/// <exception cref="ArgumentNullException"><paramref name="visitor"/> is a null reference (<c>Nothing</c> in Visual Basic).</exception>
		public virtual void DepthFirstTraversal(IVisitor<TData> visitor)
		{
			if(visitor.HasCompleted)
				return;
			var prepost = visitor as IPrePostVisitor<TData>;
			if(prepost != null)
				prepost.PreVisit(this.Data);
			if(this.leftSubtree != null)
				this.leftSubtree.DepthFirstTraversal(visitor);
			visitor.Visit(this.Data);
			if(this.rightSubtree != null)
				this.rightSubtree.DepthFirstTraversal(visitor);
			if(prepost != null)
				prepost.PostVisit(this.Data);
		}

		/// <summary>
		/// Seeks the tree node containing the given data.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public BinaryTree<TData> Find(TData value)
		{
			// we do it the easy way here and use an ad hoc BFT 
			var queue = new Queue<BinaryTree<TData>>();
			queue.Enqueue(this.Root);
			while(queue.Count > 0) {
				var binaryTree = queue.Dequeue();
				if(EqualityComparer<TData>.Default.Equals(binaryTree.Data, value)) {
					return binaryTree;
				}
				for(var i = 0; i < binaryTree.Degree; i++) {
					var child = binaryTree.GetChild(i);
					if(child != null) {
						queue.Enqueue(child);
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Finds the node with the specified condition.  If a node is not found matching
		/// the specified condition, null is returned.
		/// </summary>
		/// <param name="condition">The condition to test.</param>
		/// <returns>The first node that matches the condition supplied.  If a node is not found, null is returned.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="condition"/> is a null reference (<c>Nothing</c> in Visual Basic).</exception>
		public BinaryTree<TData> FindNode(Predicate<TData> condition)
		{
			if(condition(this.Data)) {
				return this;
			}
			if(this.leftSubtree != null) {
				var ret = this.leftSubtree.FindNode(condition);
				if(ret != null)
					return ret;
			}
			if(this.rightSubtree != null) {
				var ret = this.rightSubtree.FindNode(condition);
				if(ret != null)
					return ret;
			}
			return null;
		}

		/// <summary>
		/// Gets the left (index zero) or right (index one) subtree.
		/// </summary>
		/// <param name="index">The index of the child in question.</param>
		/// <returns>The child at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>There are at most two children at each level of a binary tree, the index can hence only be zero or one.</exception>
		public BinaryTree<TData> GetChild(int index)
		{
			switch(index) {
			case 0:
				return this.leftSubtree;
			case 1:
				return this.rightSubtree;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<TData> GetEnumerator()
		{
			var stack = new Stack<BinaryTree<TData>>();
			stack.Push(this);
			while(stack.Count > 0) {
				var tree = stack.Pop();
				yield return tree.Data;
				if(tree.leftSubtree != null) {
					stack.Push(tree.leftSubtree);
				}
				if(tree.rightSubtree != null) {
					stack.Push(tree.rightSubtree);
				}
			}
		}

		/// <summary>
		/// Removes the specified item from the tree.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		/// <returns></returns>
		public virtual bool Remove(TData item)
		{
			if(this.left