namespace Orbifold.Numerics
{
	/// <summary>
	/// An item/node of the <see cref="RedBlackTree{TKey,TValue}"/> can obviously only have at most two children, hence the inheritance from the <see cref="BinaryTree{T}"/>.
	/// </summary>
	/// <typeparam name="T">The data type in the node.</typeparam>
	internal class RedBlackTreeNode<T> : BinaryTree<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RedBlackTreeNode&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="data">The data contained in this node.</param>
		internal RedBlackTreeNode(T data)
			: base(data)
		{
			this.Color = NodeColor.Red;
		}

		/// <summary>
		/// Gets or sets the color of the current node.
		/// </summary>
		/// <value>The color of the node.</value>
		internal NodeC