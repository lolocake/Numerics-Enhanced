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
//			Assert.AreEqual(3.10564420824069, Functions.Gamma(-1.79224115610123), Accuracy);
//			Assert.AreEqual(0.289605666043831, Functions.Gamma(-3.78734856843948), Accuracy);
//			Assert.AreEqual(-1.97041451487914E-03, Functions.Gamma(-6.43006724119186), Accuracy);

			// GammaLn
			Assert.AreEqual(1.50856818610322, Functions.GammaLn(3.76835145950317), Accuracy);
			Assert.AreEqual(1.52395510070524, Functions.GammaLn(3.78128070831299), Accuracy);
			Assert.AreEqual(3.51639004045872, Functions.GammaLn(5.22110624313355), Accuracy);
			Assert.AreEqual(1.05593856001418, Functions.GammaLn(3.36578979492187), Accuracy);
			Assert.AreEqual(2.93885210191772, Functions.GammaLn(4.83925867080688), Accuracy);
			Assert.AreEqual(0.513590205904634, Functions.GammaLn(2.79629344940186), Accuracy);
			Assert.AreEqual(0.429146817643342, Functions.GammaLn(2.69286489486694), Accuracy);
			Assert.AreEqual(2.59403131257292, Functions.GammaLn(4.60012321472168), Accuracy);
			Assert.AreEqual(9.01512217041147E-02, Functions.GammaLn(2.18743028640747), Accuracy);
			Assert.AreEqual(1.78957799295296, Functions.GammaLn(3.9982629776001), Accuracy);

			Assert.AreEqual(39.3398841872, Functions.GammaLn(20), Accuracy);
			Assert.AreEqual(365.123, Functions.GammaLn(101.3), 0.01);
			Assert.AreEqual(1.82781, Functions.GammaLn(0.15), 0.01);

			Assert.AreEqual(0.864664716763, Functions.GammaRegularized(1, 2), Accuracy);
			Assert.AreEqual(0.999477741950, Functions.GammaRegularized(3, 12), Accuracy);
			Assert.AreEqual(0.714943499683, Functions.GammaRegularized(5, 6), Accuracy);

			Assert.AreEqual(6227020800, Functions.Gamma(14), Accuracy); // equal 13!
			Assert.AreEqual(7.1099858780486345E74, Functions.Gamma(57), Accuracy); // equal 56!
			Assert.AreEqual(9.3326215443944153E155, Functions.Gamma(100), Accuracy); // equal 99!
			Assert.AreEqual(3.0414093201713378E64, Functions.Gamma(51), Accuracy); // equal 50!
		}

		[Test]
		[Category("Diverse functions")]
		public void BinomialTest()
		{
			Assert.AreEqual(225792840, Functions.BinomialCoefficient(32, 12), Accuracy);
			Assert.AreEqual(792, Functions.BinomialCoefficient(12, 5), Accuracy);
			Assert.AreEqual(53130, Functions.BinomialCoefficient(25, 5), Accuracy);
			Assert.AreEqual(53130, Functions.BinomialCoefficient(25, 5), Accuracy);
			Assert.AreEqual(170544, Functions.BinomialCoefficient(22, 7), Accuracy);
			Assert.AreEq