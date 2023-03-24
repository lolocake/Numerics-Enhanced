﻿using System;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Extensions related to partial functional applications.
	/// </summary>
	public static class PartialFunction
	{
		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The type of the 1.</typeparam>
		/// <typeparam name="TDomain2">The type of the 2.</typeparam>
		/// <typeparam name="TRange">The type of the R.</typeparam>
		/// <param name="func">The functional.</param>
		/// <param name="x">The value on which the functional will be partially applied.</param>
		/// <returns></returns>
		public static Func<TDomain2, TRange> Partial<TDomain1, TDomain2, TRange>(Func<TDomain1, TDomain2, TRange> func, TDomain1 x)
		{
			return y => func(x, y);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TRange> Partial<TDomain1, TDomain2, TDomain3, TRange>(Func<TDomain1, TDomain2, TDomain3, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3) => function(arg1, arg2, arg3);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TRange> Partial<TDomain1, TDomain2, TDomain3, TRange>(Func<TDomain1, TDomain2, TDomain3, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return arg3 => function(arg1, arg2, arg3);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4) => function(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4) => function(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return arg4 => function(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TDomain5, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5) => function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TDomain5, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5) => function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TDomain5, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5) => function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <param name="arg4">The fourth argument.</param>
		/// <returns></returns>
		public static Func<TDomain5, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3, TDomain4 arg4)
		{
			return arg5 => function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <returns></returns>
		public static Func<TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> function, TDomain1 arg1)
		{
			return (arg2, arg3, arg4, arg5, arg6) => function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <returns></returns>
		public static Func<TDomain3, TDomain4, TDomain5, TDomain6, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> function, TDomain1 arg1, TDomain2 arg2)
		{
			return (arg3, arg4, arg5, arg6) => function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth parameter.</typeparam>
		/// <typeparam name="TRange">The type of the range.</typeparam>
		/// <param name="function">The function.</param>
		/// <param name="arg1">The first argument.</param>
		/// <param name="arg2">The second argument.</param>
		/// <param name="arg3">The third argument.</param>
		/// <returns></returns>
		public static Func<TDomain4, TDomain5, TDomain6, TRange> Partial<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange>(Func<TDomain1, TDomain2, TDomain3, TDomain4, TDomain5, TDomain6, TRange> function, TDomain1 arg1, TDomain2 arg2, TDomain3 arg3)
		{
			return (arg4, arg5, arg6) => function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Partial application of the given functional.
		/// </summary>
		/// <typeparam name="TDomain1">The data type of the first parameter.</typeparam>
		/// <typeparam name="TDomain2">The data type of the second parameter.</typeparam>
		/// <typeparam name="TDomain3">The data type of the thrid parameter.</typeparam>
		/// <typeparam name="TDomain4">The data type of the fourth parameter.</typeparam>
		/// <typeparam name="TDomain5">The data type of the fifth parameter.</typeparam>
		/// <typeparam name="TDomain6">The data type of the sixth par