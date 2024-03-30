using System;
using NUnit.Framework;
using System.Linq;

namespace Orbifold.Numerics.Tests
{
	/// <summary>
	/// This is a classification problem dating back to 1936. Its originator, R. A. Fisher, developed the problem to test clustering analysis 
	/// and other types of classification programs prior to the development of computerized decision tree generation programs. 
	/// The dataset is small consisting of 150 records. The target variable is categorical specifying the species of iris. 
	/// The predictor variables are measurements of plant dimensions.
	/// </summary>
	public class Iris
	{
		[Feature]
		public decimal SepalLength { get; set; }

		[