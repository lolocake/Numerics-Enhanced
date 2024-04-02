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

		[Feature]
		public decimal SepalWidth { get; set; }

		[Feature]
		public decimal PetalLength { get; set; }

		[Feature]
		public decimal PetalWidth { get; set; }

		[StringLabel]
		public string Class { get; set; }

		public static Iris[] Load()
		{
			return new [] {
				new Iris { SepalLength = 5.1m, SepalWidth = 3.5m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.9m, SepalWidth = 3m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.7m, SepalWidth = 3.2m, PetalLength = 1.3m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.6m, SepalWidth = 3.1m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 5m, SepalWidth = 3.6m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 5.4m, SepalWidth = 3.9m, PetalLength = 1.7m, PetalWidth = 0.4m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.6m, SepalWidth = 3.4m, PetalLength = 1.4m, PetalWidth = 0.3m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 5m, SepalWidth = 3.4m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.4m, SepalWidth = 2.9m, PetalLength = 1.4m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.9m, SepalWidth = 3.1m, PetalLength = 1.5m, PetalWidth = 0.1m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 5.4m, SepalWidth = 3.7m, PetalLength = 1.5m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.8m, SepalWidth = 3.4m, PetalLength = 1.6m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.8m, SepalWidth = 3m, PetalLength = 1.4m, PetalWidth = 0.1m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 4.3m, SepalWidth = 3m, PetalLength = 1.1m, PetalWidth = 0.1m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 5.8m, SepalWidth = 4m, PetalLength = 1.2m, PetalWidth = 0.2m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 5.7m, SepalWidth = 4.4m, PetalLength = 1.5m, PetalWidth = 0.4m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 5.4m, SepalWidth = 3.9m, PetalLength = 1.3m, PetalWidth = 0.4m, Class = "Iris-setosa" }, 
				new Iris { SepalLength = 5.1m, SepalWidth = 3.