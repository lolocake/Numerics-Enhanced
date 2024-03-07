using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

using NUnit.Framework;

namespace Orbifold.Numerics.Tests.Math
{
	/// <summary>
	/// Unit tests of diverse special functions and polynomials.
	/// </summary>
	[TestFixture]
	public class MathTests
	{
		private const double Accuracy = 1E-6;

		[Test]
		[Category("Diverse functions")]
		public void GammaTest()
		{
			// gamma
			Assert.AreEqual(2.41605085099579, Functions.Gamma(3.19672372937202), Accuracy);
			Assert.AreEqual(13.8825126879496, Functions.Gamma(4.62595878839493), Accuracy);
			Assert.AreEqual(2.13271882732642, Functions.Gamma(0.415676707029343), Accuracy);
			Assert.AreEqual(3.69810387443817, Functions.Gamma(3.59550366401672), Accuracy);
			Assert.AreEqual(1.77273235949519, Functions.Gamma(2.86533065438271), Accuracy);
			Assert.AreEqual(0.948430702927698, Functions.Gamma(1.85917609930038), Accuracy);
			Assert.AreEqual(4.55022977456423, Functions.Gamma(3.77391051650047), Accuracy);
			Assert.AreEqual(5.44572548650429, Functions.Gamma(3.92214500904083), Accuracy);
			Assert.AreEqual(0.901097590334103, Functions.Gamma(1.65637829899788), Accuracy);
			Assert.AreEqual(0.918635851663489, Functions.Gamma(1.74811812639236), Accuracy);

			// gamma negative values
//			Assert.AreEqual(2.9452661069313324820932987, Functions.Gamma(-1.3478), Accuracy);
//			Assert.AreEqual(-1.39822215681271E-03, Functions.Gamma(-6.66904813051224), Accuracy);
//			Assert.AreEqual(-0.976666670015353, Functions.Gamma(-2.73450392484665), Accuracy);
//			Assert.AreEqual(0.277448639195839, Functions.Gamma(-3.76867443323135), Accuracy);
//			Assert.AreEqual(-5.29530743048645, Functions.Gamma(-0.225512206554413), Accuracy);
//			Assert.AreEqual(3.10