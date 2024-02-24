using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using NUnit.Framework;
using Edge = Orbifold.Numerics.Edge<object, object>;
using Graph = Orbifold.Numerics.Graph<Orbifold.Numerics.Node<object, object>, Orbifold.Numerics.Edge<object, object>>;
using Node = Orbifold.Numerics.Node<object, object>;

namespace Orbifold.Numerics.Tests.DataStructures
{
	/// <summary>
	/// Summary description for GraphAnalysisTests
	/// </summary>
	[TestFixture]
	public class GraphAnalysisTests
	{
        private static readonly System.Random Rand = new System.Random(Environment.TickCount);

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }

		#region Additional test attributes

		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//

		#endregion

		#region Prim's

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void PrimTest1()
		{
			var g = GraphExtensions.Parse(new[] { "1,2", "1,3", "2,4", "3,4", "4,5" });
			var tree = g.PrimsSpanningTree(g.FindNode(1));
			Assert.AreNotEqual(g.Edges.Count, tree.Edges.Count, "One edge should have been removed to have a spanning tree.");
			var cycles = tree.FindCycles();
			Assert.AreEqual(0, cycles.Count, "There shouldn't be any cycles now.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void PrimUndirectedTest()
		{
			var howmany = Rand.Next(5, 13);

			// we'll do the same test with different random graphs a few times
			for(var i = 0; i < howmany; i++) {
				var size = Rand.Next(10, 40);
				var g = GraphExtensions.CreateRandomConnectedGraph(size);

				// making it undirected means the spanning tree should reach all the nodes since the graph is connected
				g.IsDirected = false;

				var tree = g.PrimsSpanningTree(g.FindNode(1), true);
				Assert.AreEqual(g.Nodes.Count, tree.Nodes.Count, "Seems the spanning tree is not so spanning things.");
				tree.IsDirected = true;
				Assert.IsFalse(tree.Nodes.Any(n => n.Parents.Count() > 1), "Some nodes have more than one parent.");
			}

		}

		#endregion

		#region Kruskal

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void KruskalTest1()
		{
			var g = GraphExtensions.Parse(new[] { "1,2", "1,3", "2,4", "3,4", "4,5" });
			var tree = g.KruskalsSpanningTree(true);
			Assert.AreNotEqual(g.Edges.Count, tree.Edges.Count, "One edge should have been removed to have a spanning tree.");
			var cycles = tree.FindCycles();
			Assert.AreEqual(0, cycles.Count, "There shouldn't be any cycles now.");
		}

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void KruskalTest2()
		{
			// Penrose diagram
			var g = GraphExtensions.Parse(new[] { "1,3", "2,1", "2,4", "2,5", "3,4", "3,6", "4,5", "4,6", "7,5", "7,6" });

			// by specifyin 'true' the tree will be a flow
			var tree = g.KruskalsSpanningTree(true);
			var cycles = tree.FindCycles();
			Assert.AreEqual(0, cycles.Count, "There shouldn't be any cycles now.");
			Assert.IsFalse(tree.Nodes.Any(n => n.Parents.Count() > 1), "Some nodes have more than one parent.");

		}

		/// <summary>
		/// Kruskal over some random graphs.
		/// Note that Kruskal works only with directed graphs.
		/// </summary>
		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void KruskalRandomTest()
		{

			var howmany = Rand.Next(5, 13);

			// we'll do the same test with different random graphs a few times
			for(var i = 0; i < howmany; i++) {
				var size = Rand.Next(10, 40);
				var g = GraphExtensions.CreateRandomConnectedGraph(size);

				var tree = g.KruskalsSpanningTree(true);
				
				Assert.IsTrue(tree.IsAcyclic, "There shouldn't be any cycles now.");
				Assert.AreEqual(g.Nodes.Count, tree.Nodes.Count, "Seems the spanning tree is not so spanning things.");
				tree.IsDirected = true;
				Assert.IsFalse(tree.Nodes.Any(n => n.Parents.Count() > 1), "Some nodes have more than one parent.");
			}

		}

		#endregion


		#region Cycles

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void FindCyclesTest()
		{
			var g = GraphExtensions.Parse(new[] { "1,2", "3,1", "2,4", "4,3", "4,5", "10,11", "11,12", "12,10" });
			var cycles = g.FindCycles();
			Assert.AreEqual(2, cycles.Count, "There are two cycles, one in each of the two components.");
			var expectedCycle0 = new[] { 1, 2, 3, 4 };
			var expectedCycle1 = new[] { 10, 11, 12 };

			var cycle0 = cycles[0].Select(n => n.Id).OrderBy(i => i).ToList();
			var cycle1 = cycles[1].Select(n => n.Id).OrderBy(i => i).ToList();
			Assert.IsTrue(cycle0.Contains(1) || cycle1.Contains(1), "One of the cycles should contain id=1.");

			if(cycle1.Contains(1)) {
				// we want to have id=1 in the first cycle, so let's swap things
				var temp = cycle1;
				cycle1 = cycle0;
				cycle0 = temp;
			}
			Assert.AreEqual(expectedCycle0.Count(), cycle0.Count, "The first cycle has not the expected length.");
			Assert.AreEqual(expectedCycle1.Count(), cycle1.Count, "The second cycle has not the expected length.");

			var runner = 0;
			while(cycle0.Count > 0) {
				var id = cycle0[0];
				cycle0.RemoveAt(0);
				Assert.AreEqual(expectedCycle0[runner], id, string.Format("Mismatch of the first cycle at {0}", id));
				runner++;
			}

			runner = 0;
			while(cycle1.Count > 0) {
				var id = cycle1[0];
				cycle1.RemoveAt(0);
				Assert.AreEqual(expectedCycle1[runner], id, string.Format("Mismatch of the second cycle at {0}", id));
				runner++;
			}

			var tree = g.PrimsSpanningTree(g.FindNode(1));
			cycles = tree.FindCycles();
			Assert.AreEqual(0, cycles.Count, "A spanning tree should not have cycles...");
		}

		#endregion

		#region Traversals

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void BFTTest1()
		{
			var g = GraphExtensions.Parse(new[] { "1,2", "1,3", "2,4", "3,4", "4,5" });
			var trail = new TrailVisitor<Node>();
			var start = g.FindNode(1);
			g.BreadthFirstTraversal(trail, start);
			Assert.AreEqual(5, trail.Trail.Count, "Not all nodes were visited.");
			for(var i = 0; i < trail.Trail.Count; i++)
				Assert.AreEqual(i + 1, trail.Trail[i].Id, string.Format("Wrong visite at {0}", i));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void BFTTest2()
		{
			// tadpole aka lolly diagram
			var g = GraphExtensions.Parse(new[] { "1,2", "1,3", "2,4", "3,4", "4,5" });
			var start = g.FindNode(1);

			var trail = new List<Node>();
			g.BreadthFirstTraversal(new ActionVisitor<Node>(trail.Add), start);
			Assert.AreEqual(5, trail.Count, "Not all nodes were visited.");
			for(var i = 0; i < trail.Count; i++)
				Assert.AreEqual(i + 1, trail[i].Id, string.Format("Wrong visite at {0}", i));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void DFTTest()
		{
			var g = GraphExtensions.Parse(new[] { "1,2", "1,3", "2,4", "3,4", "4,5" });
			var trail = new TrailVisitor<Node>();
			var start = g.FindNode(1);
			g.DepthFirstTraversal(trail, start);
			Assert.AreEqual(5, trail.Trail.Count, "Not all nodes were visited.");
			var shouldbe = new List<int> { 1, 2, 4, 5, 3 };

			for(var i = 0; i < trail.Trail.Count; i++)
				Assert.AreEqual(shouldbe[i], trail.Trail[i].Id, string.Format("Wrong visite at {0}", i));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void DFTSpecificGraphTest()
		{
			var g = GraphExtensions.Parse(new[] { "1,2", "2,3", "1,3" });
			var nodeCount = g.Nodes.Count;

			// pretend the directions don't matter
			g.IsDirected = false;

			// any node can serve as root
			var root = g.FindNode(2);
			var tree = g.PrimsSpanningTree(root, true);
			tree.IsDirected = true;
			Console.WriteLine(tree.ToLinkListString());
			Assert.AreEqual(nodeCount, tree.Nodes.Count, "The spanning tree doesn't span the whole graph.");
			var trailVisitor = new TrailVisitor<Node>();
			tree.DepthFirstTraversal(trailVisitor, root);
			Assert.AreEqual(nodeCount, trailVisitor.Trail.Count, "DFT didn't visit the whole tree.");
			foreach(var node in tree.Nodes)
				Assert.IsTrue(node.Parents.Count() <= 1, string.Format("Node {0} has more than one parent.", node.Id));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Graph Analysis")]
#else
		[Category("Graph Analysis")]
		#endif
		public void FindDuplicatesInListTest()
		{
			var list = new List<Point> {
				new Point(0, 2),
				new Point(4, 55),
				new Point(0, 5),
				new Point(0, 2),
				new Point(845, 4),
				new Point(0, 5)
			};
	