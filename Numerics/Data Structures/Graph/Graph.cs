using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Base graph class for the various incarnations in the graph analysis.
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item>
	/// <description>The graph is directed by default (<code lang="C#">IsDirected =
	/// true</code>)</description></item>
	/// <item>
	/// <description>The adjacency structure is not centralized but resides in the
	/// Outgoing and Incoming collection attached to the
	/// Nodes.</description></item></list>
	/// </remarks>
	/// <typeparam name="TNode">The data type of the node which should be an
	/// implementation of the <see cref="INode{TNode,TLink}">INode{TNode,TLink}</see>
	/// interface and have a parameterless constructor.</typeparam>
	/// <typeparam name="TLink">The data type of the edge which should be an
	/// implementation of the <see cref="IEdge{TNode,TLink}">IEdge{TNode,TLink}</see>
	/// interface and have a parameterless constructor.</typeparam>
	public class Graph<TNode, TLink>
		where TNode : class, INode<TNode, TLink>, new()
		where TLink : class, IEdge<TNode, TLink>, new()
	{
		/// <summary>
		/// The <see cref="IsDirected"/> field.
		/// </summary>
		private bool isDirected;

		/// <summary>
		/// Initializes a new instance of the <see cref="Graph{TNode,TLink}"/> class. 
		/// </summary>
		public Graph()
		{
			this.Nodes = new List<TNode>();
			this.Edges = new List<TLink>();
			this.isDirected = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Graph{TNode,TLink}"/> class.
		/// </summary>
		/// <param name="graph">The graph content to start with. Note that references will be added, not clones.</param>
		public Graph(Graph<TNode, TLink> graph)
			: this()
		{
			graph.Nodes.ForEach(n => this.Nodes.Add(n));
			graph.Edges.ForEach(l => this.Edges.Add(l));
		}

		/// <summary>
		/// Gets whether this graph is connected.
		/// See also this article;  http://en.wikipedia.org/wiki/Connected_graph. 
		/// </summary>
		/// <remarks>
		/// A graph is connected if every two vertices are connected by a path. A connected
		/// graph has only one component.
		/// </remarks>
		/// <value>
		/// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		public bool IsConnected
		{
			get
			{
				return this.GetConnectedComponents().Count() == 1;
			}
		}

		/// <summary>
		/// Gets whether the graph is acyclic.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>If there are no cycles in a graph it's acyclic. A cycle means a
		/// closed path or loop.</description></item>
		/// <item>
		/// <description>See also the article;
		/// http://en.wikipedia.org/wiki/Directed_acyclic_graph
		/// .</description></item></list>
		/// </remarks>
		/// <value>
		/// <c>true</c> if this instance is acyclic; otherwise, <c>false</c>.
		/// </value>
		public bool IsAcyclic
		{
			get
			{
				return !this.FindCycles().Any();
			}
		}

		/// <summary>
		/// Gets whether the graph is hamiltonian.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description>An Hamitonian cycle is a cycle which contains all nodes of the
		/// graph. If there is at least one such cycle the graph is called
		/// Hamiltonian.</description></item>
		/// <item>
		/// <description>See also the article;http://en.wikipedia.org/wiki/Hamiltonian_graph
		/// .</description></item></list>
		/// </remarks>
		/// <value>
		/// <c>true</c> if this instance is acyclic; otherwise, <c>false</c>.
		/// </value>
		public bool IsHamiltonian
		{
			get
			{
				throw new NotImplementedException("This is work in progress, please hold your horses.");
			}
		}

		/// <summary>
		/// Gets or sets the links of this graph.
		/// </summary>
		/// <value>
		/// The links collection.
		/// </value>
		public List<TLink> Edges { get; protected set; }

		/// <summary>
		/// Gets whether this graph is directed.
		/// </summary>
		public bool IsDirected
		{
			get
			{
				return this.isDirected;
			}
			set
			{
				if (value == this.isDirected) return;

				/*Here's the thing; our adjacency structure doesn't reside in a global entity but in each node's Incoming/Outgoing collections.
				 So, if the graph becomes (un)direct the nodes need to know since they contain the linkage.
				 */
				this.Nodes.ForEach(n => n.IsDirected = value);
				this.isDirected = value;
			}
		}

		/// <summary>
		/// Gets or sets the nodes of this graph.
		/// </summary>
		/// <value>
		/// The nodes collection.
		/// </value>
		public List<TNode> Nodes { get; protected set; }

		/// <summary>
		/// Adds a edge to this graph.
		/// </summary>
		/// <param name="source">
		/// The source of the edge.
		/// </param>
		/// <param name="sink">
		/// The sink of the 