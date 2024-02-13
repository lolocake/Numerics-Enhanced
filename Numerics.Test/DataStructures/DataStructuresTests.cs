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
			var ordered = arr