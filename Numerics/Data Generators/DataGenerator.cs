
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Orbifold.Numerics
{
	/// <summary>
	/// A collection of methods related to random data generation.
	/// </summary>
	public static class DataGenerator
	{
		private static MarkovNameGenerator markovFemaleNameGenerator;
		private static MarkovNameGenerator markovFamilyNameGenerator;
		private static MarkovNameGenerator markovTaskNameGenerator;

		/// <summary>
		/// The randomizer at the basis of all.
		/// </summary>
        private static readonly System.Random Rand = new System.Random(Environment.TickCount);

		/// <summary>
		/// Returns a random paragraph of lipsum text of the specified length.
		/// </summary>
		/// <param name="numWords">The num words; default is 25 and must be more than 5.</param>
		/// <returns></returns>
		public static string RandomLipsum(int numWords = 25)
		{
			numWords = System.Math.Max(numWords, 5);
			var sb = new StringBuilder();
			sb.Append("Lorem ipsum dolor sit amet");
			var counter = 5;
			var capitalize = false;
			for(var i = 0; i <= (numWords - 6); i++) {
				var newWord = DataStore.LipsumData[Rand.Next(DataStore.LipsumData.Length)];
				if(capitalize) {
					newWord = newWord.Capitalize();
					capitalize = false;
				}
				sb.AppendFormat(" {0}", newWord);
				counter++;

				// create sentences of random length;
				if(counter >= 15 && Rand.NextDouble() < .7) {
					sb.Append(".");
					counter = 0;
					capitalize = true;
				}
			}

			sb.Append(".");
			return sb.ToString();
		}

		/// <summary>
		/// Returns a collection of person names.
		/// </summary>
		/// <param name="type">The option specifying the format of the returned names.</param>
		/// <param name="count">The amount of person names to be returned.</param>
		/// <returns></returns>
		public static IEnumerable<string> RandomPersonNameCollection(PersonDataType type = PersonDataType.FullNameWithMiddleInitial, int count = 15)
		{
			if(count < 1)
				throw new Exception("The amount of names to generate should be bigger than one.");
			if(count == 1)
				return new List<string> { RandomPersonName(type) };
			var list = new List<string>();
			Range.Create(1, count).ForEach(() => list.Add(RandomPersonName(type)));
			return list;
		}

		/// <summary>
		/// Returns a random person name.
		/// </summary>
		/// <param name="type">The option specifying the format of the returned name. Default is <see cref="PersonDataType.FullNameWithMiddleInitial"/>.</param>
		public static string RandomPersonName(PersonDataType type = PersonDataType.FullNameWithMiddleInitial)
		{
			switch(type) {
			case PersonDataType.FirstName:
				return RandomFirstName();
			case PersonDataType.MaleFirstName:
				return RandomMaleName();
			case PersonDataType.FemalFirstName:
				return RandomFemaleName();
			case PersonDataType.FamilyName:
				return RandomFamilyName();
			case PersonDataType.FullName:
				return String.Format("{0} {1}", RandomFirstName(), RandomFamilyName());
			case PersonDataType.FullNameWithMiddleInitial:
				return String.Format("{0} {1}. {2}", RandomFirstName(), RandomLetter(CaseType.UpperCase), RandomFamilyName());
			default:
				throw new ArgumentOutOfRangeException("type");
			}
		}

		/// <summary>
		/// Returns a random (English) male or female first name.
		/// </summary>
		/// <returns>The first name.</returns>
		public static string RandomFirstName()
		{
			return Rand.NextDouble() < .5 ? RandomFemaleName() : RandomMaleName();
		}

		/// <summary>
		/// Returns a random (English) male name (first name).
		/// </summary>
		/// <returns>The male name.</returns>
		public static string RandomMaleName()
		{
			return DataStore.EnglishMaleNames[Rand.Next(DataStore.EnglishMaleNames.Length)];
		}

		public static List<string> RandomMaleNames(int count = 15)
		{
			if(count < 1)
				throw new Exception("The amount of names to generate should be bigger than one.");

			if(count == 1)
				return new List<string> { RandomMaleName() };
			var list = new List<string>();
			Range.Create(1, count).ForEach(() => list.Add(RandomMaleName()));
			return list;
		}

		public static string RandomFemaleName()
		{
			if(markovFemaleNameGenerator == null)
				markovFemaleNameGenerator = new MarkovNameGenerator(DataStore.EnglishFemaleNames);
			return markovFemaleNameGenerator.NextName;
		}

		public static string RandomSalesTerm()
		{
			return DataStore.SalesTerms[Rand.Next(DataStore.SalesTerms.Length), 0];
		}

		public static Concept RandomSalesConcept()
		{
			var i = Rand.Next(DataStore.SalesTerms.GetUpperBound(0));
			return new Concept {
				Name = DataStore.SalesTerms[i, 0],
				Description = DataStore.SalesTerms[i, 1]
			};
		}

		public static string RandomTaskName()
		{
			if(markovTaskNameGenerator == null)
				markovTaskNameGenerator = new MarkovNameGenerator(DataStore.TaskNames);
			return markovTaskNameGenerator.NextName;
		}


		public static List<string> RandomTaskNames(int count = 15)
		{
			if(count < 1)
				throw new Exception("The amount of names to generate should be bigger than one.");
			if(count == 1)
				return new List<string> { RandomTaskName() };

			if(markovTaskNameGenerator == null)
				markovTaskNameGenerator = new MarkovNameGenerator(DataStore.TaskNames);
			var list = new List<string>();
			Range.Create(1, count).ForEach(() => list.Add(markovTaskNameGenerator.NextName));
			return list;
		}

		/// <summary>
		/// Returns a random family name.
		/// </summary>
		/// <returns>The family name.</returns>
		public static string RandomFamilyName()
		{
			if(markovFamilyNameGenerator == null)
				markovFamilyNameGenerator = new MarkovNameGenerator(DataStore.FamilyNam