using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using NUnit.Framework;

 
namespace Orbifold.Numerics.Tests.DataStructures
{
	/// <summary>
	/// Summary description for DataStructuresTests
	/// </summary>
	[TestFixture]
	public class DataStructuresTests
	{
        private static System.Random Rand = new System.Random(Environment.TickCount);

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

		#region Heap

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void HeapTest1()
		{
			// heap with the minimum on top
			var heap = new Heap<int>(OrderType.Ascending);
			// add a range in random order
			Range.Create(1, 50).Scramble().ToList().ForEach(heap.Add);
			Assert.AreEqual(50, heap.Count);
			Assert.AreEqual(1, heap.Root);
			heap.Add(-5);
			Assert.AreEqual(-5, heap.Root);
			heap.RemoveRoot();
			var array = new int[50];
			heap.CopyTo(array, 0);
			var ordered = array.OrderBy(i => i).ToList();
			for(var i = 0; i < ordered.Count; i++)
				Assert.AreEqual(i + 1, ordered[i], string.Format("Wrong number at position {0}", i));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void HeapTest2()
		{
			// max value on top of the heap
			var heap = new Heap<KeyValuePair<int, string>>(OrderType.Descending, (a, b) => a.Key.CompareTo(b.Key));
			Range.Create('a', 'z').Scramble().ToList().ForEach(kv => heap.Add(new KeyValuePair<int, string>(Convert.ToInt32(kv), kv.ToString(CultureInfo.InvariantCulture))));
			Assert.AreEqual(Convert.ToInt32('z'), heap.Root.Key, "Root should be 'z'.");
			Console.WriteLine(heap.ToString());
		}

		#endregion

		#region PriorityQueue

		public class PriorityNode : IComparable
		{
			public int Priority { get; set; }

			public PriorityNode(int priority)
			{
				this.Priority = priority;
			}

			public int CompareTo(object obj)
			{
				if(obj is PriorityNode)
					return this.Priority.CompareTo((obj as PriorityNode).Priority);
				throw new ArgumentException("obj");
			}
		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void PriorityTest1()
		{
			var pq = new PriorityQueue<PriorityNode, int>();
			var prios = new[] { 12, 2, 33, 12, 2, -3, 20 }.ToList();
			prios.ForEach(i => pq.Push(new PriorityNode(i), i));
			var ordered = prios.OrderByDescending(q => q);
			foreach(var i in ordered) {
				var node = pq.Pop();
				Debug.WriteLine(node.Priority);
				Assert.AreEqual(i, node.Priority, "Wrong priority at this level.");
			}

		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void PriorityTest2()
		{
			var pq = new PriorityQueue<string, string>();
			var prios = new[] { "a", "b", "p", "3" }.ToList();
			prios.ForEach(i => pq.Push(i, i));
			var ordered = prios.OrderByDescending(q => q);
			foreach(var i in ordered) {
				var item = pq.Pop();
				Debug.WriteLine(item);
				Assert.AreEqual(i, item, "Wrong priority at this level.");
			}
		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void PriorityTest3()
		{
			var pq = new PriorityQueue<string, int>(OrderType.Ascending);
			