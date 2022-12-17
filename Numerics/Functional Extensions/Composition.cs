using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Composition of functionals extensions.
	/// </summary>
	public static class Composition
	{
		/// <summary>
		/// Composes two functions together.
		/// </summary>
		/// <remarks>This is the pipe-forward |> operation in F#.</remarks>
		/// <typeparam name="TDomain">The type of the source.</typeparam>
		/// <typeparam name="TIntermediate">The type of the intermediate result.</typeparam>
		/// <typeparam name="TTarget">The type of the end result.</typeparam>
		/// <param name="fun1">The first function.</param>
		/// <param name="fun2">The second function.</param>
		/// <returns>The function composition.</returns>
		public static Func<TDomain, TTarget> Compose<TDomain, TIntermediate, TTarget>(this Func<TDomain, TIntermediate> fun1, Func<TIntermediate, TTarget> fun2)
		{
			return sourceParam => fun2(fun1(sourceParam));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TEndResult> Compose<TDomain1, TDomain2, TIntermediateResult, TEndResult>(this Func<TDomain1, TDomain2, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2) => func2(func1(p1, p2));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3) => func2(func1(p1, p2, p3));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TIntermediateResult">The type of the intermediate result.</typeparam>
		/// <typeparam name="TEndResult">The type of the end result.</typeparam>
		/// <param name="func1">The first functional.</param>
		/// <param name="func2">The second functional.</param>
		/// <returns></returns>
		public static Func<TDomain1, TDomain2, TDomain3, TDomain4, TEndResult> Compose<TDomain1, TDomain2, TDomain3, TDomain4, TIntermediateResult, TEndResult>(
		  this Func<TDomain1, TDomain2, TDomain3, TDomain4, TIntermediateResult> func1, Func<TIntermediateResult, TEndResult> func2)
		{
			return (p1, p2, p3, p4) => func2(func1(p1, p2, p3, p4));
		}

		/// <summary>
		/// Composition of the given functionals.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the secon