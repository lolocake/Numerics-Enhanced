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

				var tree =