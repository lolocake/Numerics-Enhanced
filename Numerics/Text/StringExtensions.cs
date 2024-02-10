
﻿using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Diverse extensions related to strings and text.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>The empty string.</summary>
		public const string EmptyStringHash = "#EMPTY#";

		/// <summary>Number of strings.</summary>
		public const string NumHash = "#NUM#";

		/// <summary>The symbol string.</summary>
		public const string SymbolHash = "#SYM#";

		/// <summary>
		/// Capitalizes the first character of the given string.
		/// </summary>
		/// <param name="s">The string to capiatalize.</param>
		public static string Capitalize(this string s)
		{
			if(string.IsNullOrEmpty(s))
				return string.Empty;
			var trimmed = s.Trim();
			return char.ToUpper(trimmed[0]) + trimmed.Substring(1);
		}

		/// <summary>
		/// Capitalizes the first letter after a period ('.').
		/// </summary>
		/// <param name="text">The text to be processes.</param>
		/// <returns></returns>
		public static string ToSentenceCase(string text)
		{
			// start by converting entire string to lower case
			var lowerCase = text.ToLower();
			// matches the first sentence of a string, as well as subsequent sentences
			var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
			// MatchEvaluator delegate defines replacement of setence starts to uppercase
			var result = r.Replace(lowerCase, s => s.Value.ToUpper());
			return result;
		}

		/// <summary>
		/// Ensures that the string does not contain punctuation, separators...
		/// 
		/// </summary>
		/// <param name="s">string.</param>
		/// <param name="checkNumber">(Optional) true to check number.</param>
		/// <returns>A string.</returns>
		public static string Sanitize(this string s, bool checkNumber = true)
		{
			if(string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
				return EmptyStringHash;

			s = s.Trim().ToUpperInvariant();
			var item = s.Trim();

			// kill inlined stuff that creates noise (like punctuation etc.)
			item = item.ToCharArray().Aggregate("",
				(x, a) => {
					return char.IsSymbol(a) || char.IsPunctuation(a) || char.IsSeparator(a) ? x : x + a;
				}
			);

			// since we killed everything
			// and it is still empty, it
			// must be a symbol
			if(string.IsNullOrEmpty(item))
				return SymbolHash;

			// number check
			if(checkNumber) {
				double check;
				if(double.TryParse(item, out check))
					return NumHash;
			}

			return item;
		}

		/// <summary>Lazy list of available characters in a given string.</summary>
		/// <param name="s">string.</param>
		/// <param name="exclusions">(Optional) characters to ignore.</param>
		/// <returns>returns key value.</returns>
		public static IEnumerable<string> GetChars(string s, string[] exclusions = null)
		{
			s = s.Trim().ToUpperInvariant();
			foreach(char a in s) {
				var key = a.ToString();
				if(string.IsNullOrWhiteSpace(key))
					continue;