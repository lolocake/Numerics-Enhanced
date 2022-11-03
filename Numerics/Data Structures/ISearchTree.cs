using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Describes data structures used for searching.
	/// </summary>
	/// <typeparam name="T">The data type.</typeparam>
	public interface ISearchTree<T> : ICollection<T>
	{
		/// <summary>
		/// Gets the maximal item in the tree.
		/// </summary>
		/// <value>The maximum item in the tree.</value>
		T Maximum { get; }

		/// <summary>
		/// Gets the smallest item in the tree.
		/// </summary>
		/// <value>The smallest item in the tree.</value>
		/// <exception cref="InvalidOperationException">The <see cref="ISearchTree{T}"/> is empty.</exception>
		T Minimum { get; }

		/// <summary>
		/// Performs a depth first traversal on the searc