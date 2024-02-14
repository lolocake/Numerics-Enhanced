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
			var prios = new Dictionary<string, int> { { "a", 1 }, { "k", -2 }, { "j", 9 }, { "v", 234 } };
			prios.Keys.ToList().ForEach(i => pq.Push(i, prios[i]));
			var ordered = prios.OrderBy(q => q.Value);
			foreach(var i in ordered) {
				var item = pq.Pop();
				Debug.WriteLine(item);
				Assert.AreEqual(i.Key, item, "Wrong priority at this level.");
			}

		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void StringLengthQueueTest()
		{
			var queue = new PriorityQueue<string, string>(OrderType.Ascending, new StringLengthComparer());
			queue.Push("A", "short");
			queue.Push("B", "longer");
			queue.Push("E", "much longer");
			queue.Push("D", "much longer");
			queue.Push("C", "tiny");

			Assert.AreEqual("C", queue.Pop(), "Not the correct head of the queue.");
		}

		public sealed class StringLengthComparer : IComparer<string>
		{
			public int Compare(String x, String y)
			{
				var lx = 0;
				if(!string.IsNullOrEmpty(x))
					lx = x.Length;

				var ly = 0;
				if(!string.IsNullOrEmpty(y))
					ly = y.Length;

				if(lx < ly)
					return -1;
				if(lx > ly)
					return 1;
				return 0;
			}
		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void RemoveQueueTest()
		{
			var queue = new PriorityQueue<string, int>();
			Assert.IsFalse(queue.Contains("Swa"));
			queue.Enqueue("Swa");
			Assert.IsTrue(queue.Contains("Swa"));
			queue.Remove("Swa");
			Assert.IsFalse(queue.Contains("Swa"));
		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void PeekPriorityTest()
		{
			var priorityQueue = new PriorityQueue<string, int>(OrderType.Ascending);
			int priority;
			priorityQueue.Enqueue("Microsoft", 13);
			var nextItem = priorityQueue.Peek(out priority);
			Assert.AreEqual("Microsoft", nextItem);
			Assert.AreEqual(13, priority, "Peeked priority is wrong.");
			priorityQueue.Enqueue("Google", 44);
			nextItem = priorityQueue.Peek(out priority);
			Assert.AreEqual("Microsoft", nextItem);
			Assert.AreEqual(13, priority);
			priorityQueue.Enqueue("Apple", 12);
			nextItem = priorityQueue.Peek(out priority);
			Assert.AreEqual("Apple", nextItem);
			Assert.AreEqual(12, priority);
		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public void QueueCopyArrayTest()
		{
			var queue = new PriorityQueue<string, int>(OrderType.Ascending);
			queue.Enqueue("B");
			queue.Enqueue("S");
			queue.Enqueue("Z");
			queue.Enqueue("X");
			queue.Enqueue("3");
			queue.Enqueue("G");
			queue.Enqueue("2");

			var array = new string[queue.Count];
			queue.CopyTo(array, 0);
			var index = 0;
			Assert.AreEqual(queue.Count, array.Length);
			while(queue.Any()) {
				var item = queue.Pop();
				Assert.AreEqual(item, array[index], string.Format("Wrong at position {0}", index));
				index++;
			}
		}

		#endregion

		#region RedBlackTree

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
#endif
		[ExpectedException(typeof(KeyNotFoundException))]
		public void KeyValuePairTest()
		{
			var tree = new RedBlackTree<string, double>();
			var count = Rand.Next(17, 123);
			for(var i = 0; i < count; i++) {
				tree.Add(new KeyValuePair<string, double>(i.ToString(CultureInfo.InvariantCulture), Rand.NextDouble()));
			}
			var shouldraiseexception = tree["W"];
			var maxShouldBe = tree.Max(m => m.Value);
			var minShouldBe = tree.Min(m => m.Value);
			var sumShouldBe = tree.Sum(m => m.Value);
			Assert.AreEqual(count, tree.Count);
			var visitor = new SumVisitor();
			tree.DepthFirstTraversal(visitor);
			Assert.AreEqual(sumShouldBe, visitor.Sum, "Sum of the visitor is wrong.");
			tree.Clear();
			Assert.AreEqual(0, tree.Count);
			Assert.IsFalse(tree.ContainsKey("17"));
			Assert.IsTrue(tree.IsEmpty);
			Assert.AreEqual(maxShouldBe, tree.Maximum.Value, "Maximum is not correct.");
			Assert.AreEqual(minShouldBe, tree.Minimum.Value, "Minimum is not correct.");
		}

		/// <summary>
		/// Gets a basic red-black tree consisting of int-string pairs.
		/// </summary>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		private static RedBlackTree<int, string> GetBasicRedBlackTree(int count)
		{
			var redBlackTree = new RedBlackTree<int, string>();
			Range.Create(1, count).ForEach(i => redBlackTree.Add(i, i.ToString(CultureInfo.InvariantCulture)));
			return redBlackTree;
		}

		[Test]
#if SILVERLIGHT
        [Tag("Data Structures")]
#else
		[Category("Data Structures")]
		#endif
		public v