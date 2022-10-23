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
		/// The sink of the edge.
		/// </param>
		/// <returns>
		/// The added edge.
		/// </returns>
		public TLink AddEdge(TNode source, TNode sink)
		{
			return this.AddEdge(new TLink { Source = source, Sink = sink });
		}

		/// <summary>
		/// Adds the givven edge to the graph. It will add the sink and source nodes to the <see cref="Nodes"/> collection if they are not yet
		/// part of it.
		/// </summary>
		/// <param name="edge">
		/// The edge to add.
		/// </param>
		/// <returns>
		/// The added edge.
		/// </returns>
		public TLink AddEdge(TLink edge)
		{
			if (edge == null) throw new ArgumentNullException("edge");
			/* Allowing multigraphs...
			 * if (this.AreConnected(edge.Source, edge.Sink, true)) throw new InvalidOperationException("The edge cannot be added; this structure does not support multigraphs and the given nodes are already connected.");*/
			this.Edges.Add(edge);
			edge.Source.AddOutgoingEdge(edge);
			edge.Sink.AddIncomingEdge(edge);
			if (!this.Nodes.Contains(edge.Source)) this.AddNode(edge.Source);
			if (!this.Nodes.Contains(edge.Sink)) this.AddNode(edge.Sink);
			return edge;
		}

		/// <summary>
		/// Adds the given node to the graph.
		/// </summary>
		/// <param name="node">The node to add.</param>
		public void AddNode(TNode node)
		{
			if (node == null) throw new ArgumentNullException("node");
			this.Nodes.Add(node);
			node.IsDirected = this.IsDirected;
		}

		/// <summary>
		/// Adds a series of nodes to the graph.
		/// </summary>
		/// <param name="nodes">
		/// The nodes.
		/// </param>
		public void AddNodes(params TNode[] nodes)
		{
			if (nodes == null) throw new ArgumentNullException("nodes");
			nodes.ToList().ForEach(n => this.Nodes.Add(n));
		}

		/// <summary>
		/// Returns whether the given nodes are connected in one direction or the other.
		/// </summary>
		/// <remarks>
		/// Because the structure allows multigraphs the connectedness means there is at
		/// least one edge between the given nodes.
		/// </remarks>
		/// <param name="n">A node.</param>
		/// <param name="m">Another node.</param>
		/// <param name="strict">If set to <c>true</c> the first node has to be the source of the edge and the second the sink..</param>
		/// <returns>
		/// <c>true</c> If there is a edge connecting the given nodes with the first one as source and the second as sink, <c>false</c> if both options have to be considered.
		/// </returns>
		public bool AreConnected(TNode n, TNode m, bool strict = false)
		{
			if (n == null) throw new ArgumentNullException("n");
			if (m == null) throw new ArgumentNullException("m");
			if (strict) return n.Outgoing.Any(l => l.GetComplementaryNode(n) == m);
			return n.Outgoing.Any(l => l.GetComplementaryNode(n) == m) || n.Incoming.Any(l => l.GetComplementaryNode(n) == m);
		}

		/// <summary>
		/// Returns whether the given nodes are connected in one direction or the other.
		/// </summary>
		/// <remarks>
		/// Because the structure allows multigraphs the connectedness means there is at
		/// least one edge between the given nodes.
		/// </remarks>
		/// <param name="i">The id of the first node.</param>
		/// <param name="j">The id of the second node.</param>
		/// <param name="strict">If set to <c>true</c> the first node has to be the source of the edge and the second the sink..</param>
		/// <returns>
		///   <c>true</c> If there is a edge connecting the given nodes with the first one as source and the second as sink, <c>false</c> if both options have to be considered.
		/// </returns>
		public bool AreConnected(int i, int j, bool strict = false)
		{
			var n = this.FindNode(i);
			var m = this.FindNode(j);
			if (n == null || m == null) return false;
			return this.AreConnected(n, m, strict);
		}

		/// <summary>
		/// Assigns to each edge and node an identifier based on their collection listIndex.
		/// </summary>
		public void AssignIdentifiers()
		{
			// guess some other way would work as well but let's keep it simple
			this.Nodes.ForEach(n => n.Id = this.Nodes.IndexOf(n));
			this.Edges.ForEach(l => l.Id = this.Edges.IndexOf(l));
		}

		/// <summary>
		/// Clones this instance.
		/// </summary>
		/// <returns></returns>
		public Graph<TNode, TLink> Clone()
		{
			var clone = new Graph<TNode, TLink>();
			this.Nodes.ForEach(n => clone.AddNode(n.Clone()));
			foreach (var edge in this.Edges)
			{
				var n = this.FindNode(edge.Source.Id);
				var m = this.FindNode(edge.Sink.Id);
				clone.AddEdge(new TLink { Id = edge.Id, Sink = m, Source = n });
			}
			return clone;
		}

		/// <summary>
		/// Finds the edge with the specified identifiers.
		/// </summary>
		/// <param name="i">The id of the source.</param>
		/// <param name="j">The id of the sink.</param>
		/// <param name="strict">If set to <c>true</c> the found edge has to go from i to j.</param>
		/// <returns></returns>
		public TLink FindEdge(int i, int j, bool strict)
		{
			if (!this.AreConnected(i, j, strict)) return null;

			var n = this.FindNode(i);
			if (n == null) return null;
			var m = this.FindNode(j);
			if (m == null) return null;
			if (strict) return n.Outgoing.FirstOrDefault(l => l.GetComplementaryNode(n) == m);

			var found = n.Outgoing.FirstOrDefault(l => l.GetComplementaryNode(n) == m);
			return found ?? m.Outgoing.FirstOrDefault(l => l.GetComplementaryNode(m) == n);
		}

		/// <summary>
		/// Finds the node with the specified identifier.
		/// </summary>
		/// <param name="id">The id to look for.</param>
		/// <returns></returns>
		public TNode FindNode(int id)
		{
			return this.Nodes.FirstOrDefault(n => n.Id == id);
		}

		/// <summary>
		/// Attempts to find a tree root by looking at the longest paths in the graph.
		/// </summary>
		/// <remarks>The algorithms looks for all shortest paths between all vertices, which means it will also function for disconnected graphs but will return the root
		/// of the tree with longest path.</remarks>
		/// <returns>A tree root or <c>null</c> is none was found.
		/// </returns>
		public TNode FindTreeRoot()
		{
			if (this.Nodes == null || !this.Nodes.Any()) return null;
			if (this.Nodes.Count == 1) return this.Nodes[0];
			var shortestPaths = this.ShortestPaths();
			TNode found = null;
			var max = 0;
			foreach (var node in this.Nodes)
			{
				var maxPathlengthStartingFromThisNode = this.Nodes.Select((otherNode, j) => shortestPaths[Tuple.Create(node, otherNode)]).Concat(new[] { int.MinValue }).Max();
				if (maxPathlengthStartingFromThisNode <= max) continue;
				max = maxPathlengthStartingFromThisNode;
				found = node;
			}
			return found;
		}

		/// <summary>
		/// Returns the connected components of this graph.
		/// </summary>
		/// <returns>
		/// The list of connected components.
		/// </returns>
		public IEnumerable<Graph<TNode, TLink>> GetConnectedComponents()
		{
			this.HaveUniqueIdentifiers();
			Dictionary<int, int> componentMap;
			var componentsCount = this.NumberOfComponents(out componentMap);

			// now convert it to a list of graphs
			var components = new List<Graph<TNode, TLink>>();
			for (var i = 0; i < componentsCount; ++i) components.Add(new Graph<TNode, TLink>());
			foreach (var nodeId in componentMap.Keys)
			{
				var graph = components[componentMap[nodeId]];
				var node = this.FindNode(nodeId);
				graph.Nodes.Add(node);
				foreach (var l in node.Outgoing) graph.Edges.Add(l);
			}
			return components;
		}

		/// <summary>
		/// Gets the next identifier of the nodes sequence.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public int GetNextIdInNodes(int id)
		{
			var found = this.FindNode(id);
			if (found == null) return -1;
			return this.Nodes[this.Nodes.IndexOf(found) + 1].Id;
		}

		/// <summary>
		/// Ensures the unique identifiers.
		/// </summary>
		public bool HaveUniqueIdentifiers()
		{
			var ids = new List<int>();
			foreach (var node in this.Nodes)
			{
				if (ids.Contains(node.Id)) return false;
				ids.Add(node.Id);
			}
			return true;
		}

		/// <summary>
		/// Ensures that the graph nodes all have a unique identifier assigned.
		/// </summary>
		/// <remarks>If the nodes do have unique identifiers nothing will be altered.</remarks>
		public void EnsureUniqueIdentifiers()
		{
			if (!this.HaveUniqueIdentifiers()) this.AssignIdentifiers();
		}

		/// <summary>
		/// Returns the number of (connected) components.
		/// </summary>
		/// <example>
		/// The following example create two components; 
		/// <para> </para>
		/// <code lang="C#">var g = new Graph&lt;Node, Edge&gt;();
		/// for (var i = 0; i &lt; 4; i++) g.AddNode(new Node(1, true));
		/// g.AddEdge(g.Nodes[0], g.Nodes[1]);
		/// g.AddEdge(g.Nodes[2], g.Nodes[3]);
		/// var count = g.NumberOfComponents();</code>
		/// .</example>
		public int NumberOfComponents()
		{
			Dictionary<int, int> componentMap;
			return this.NumberOfComponents(out componentMap);
		}

		/// <summary>
		/// Returns the number of connected components.
		/// </summary>
		/// <param name="componentMap">The component map as a dictionary where the key is the node identifier and the value is the number of the connected component to which the node belongs.</param>
		/// <returns></returns>
		public int NumberOfComponents(out Dictionary<int, int> componentMap)
		{
			this.EnsureUniqueIdentifiers();
			var componentIndex = 0;

			// the map tells to which component a node belongs
			componentMap = new Dictionary<int, int>(this.Nodes.Count);
			foreach (var t in this.Nodes) componentMap.Add(t.Id, -1);

			for (var i = 0; i < this.Nodes.Count; i++)
			{
				if (componentMap[this.Nodes[i].Id] != -1) continue; // means it already belongs to a component
				this.AssignConnectedComponent(componentMap, i, componentIndex);
				componentIndex++;
			}
			return componentIndex;
		}

		/// <summary>
		/// Detaches all Edges from from the given node and removes them from the graph structure.
		/// </summary>
		/// <param name="node">The node.</param>
		public void RemoveAllLinksFrom(TNode node)
		{
			if (node == null) throw new ArgumentNullException("node");

			// one cannot change the collection in the looped collection, hence this construction
			while (node.Incoming.Count > 0) this.RemoveLink(node.Incoming[0]);
			while (node.Outgoing.Count > 0) this.RemoveLink(node.Outgoing[0]);
		}

		/// <summary>
		/// Removes the edge from the graph.
		/// </summary>
		/// <param name="edge">
		/// The edge.
		/// </param>
		public void RemoveLink(TLink edge)
		{
			if (edge == null) throw new ArgumentNullException("edge");
			if (!this.Edges.Contains(edge)) return;
			edge.Source.RemoveLink(edge);
			edge.Sink.RemoveLink(edge);
			this.Edges.Remove(edge);
		}

		/// <summary>
		/// Removes the given node from this graph.
		/// </summary>
		/// <param name="node">The node to remove.</param>
		public void RemoveNode(TNode node)
		{
			if (node == null) throw new ArgumentNullException("node");
			if (!this.Nodes.Contains(node)) return;
			this.RemoveAllLinksFrom(node);
			this.Nodes.Remove(node);
		}

		/// <summary>
		/// Assigns a new identifier to the nodes.
		/// </summary>
		/// <param name="startId">The number to start the numbering from.</param>
		public void RenumberNodes(int startId = 0)
		{
			for (var i = 0; i < this.Nodes.Count; i++) this.Nodes[i].Id = startId + i;
		}

		/// <summary>
		/// Returns a string representation of the incidence structure of this graph.
		/// </summary>
		/// <example>
		/// A diagram with Edges between identifier 1 and 2, 2 and 3, 3 and 4 will result in
		/// a string
		/// <para><code lang="C#">{&quot;1,2&quot;, &quot;2,3&quot;,
		/// &quot;3,4&quot;}</code></para>.
		/// </example>
		/// <seealso cref="ToLinksList">ToLinksList</seealso>
		public string ToLinkListString()
		{
			var sb = new StringBuilder();
			var list = this.T