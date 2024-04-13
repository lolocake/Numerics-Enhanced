using System;
using NUnit.Framework;
using System.Linq;

namespace Orbifold.Numerics.Tests
{
	public enum Grade
	{
		A,
		B,
		C,
		D,
		F
	}

	public class Student
	{
		[StringFeature(SplitType = StringSplitType.Character)]
		public string Name { get; set; }

		[Feature]
		public Grade Grade { get; set; }

		[Feature]
		public double GPA { get; set; }

		[Feature]
		public int Age { get; set; }

		public bool Tall { get; set; }

		[Feature]
		public int Friends { get; set; }

		[Label]
		public bool Nice { get; set; }

		public static Student[] GetData()
		{
			return new [] {
				new Student { Name = "Aidan", Age = 27, Friends = 3, GPA = 0.54, Grade = Grade.F, Tall = true, Nice = false },
				new Student { Name = "2A!@m3el3ia", Age = 40, Friends = 7, GPA = 2.60, Grade = Grade.A, Tall = true, Nice = true },
				new Student { Name = "Noah", Age = 26, Friends = 0, GPA = 2.39, Grade = Grade.C, Tall = false, Nice = false },
				new Student { Name = "Isabella", Age = 21, Friends = 9, GPA = 3.83, Grade = Grade.A, Tall = true, Nice = true },
				new Student { Name = "Liam", Age = 19, Friends = 3, GPA = 1.06, Grade = Grade.F, Tall = true, Nice = false },
				new Student { Name = "Ava", Age = 16, Friends = 7, GPA = 3.31, Grade = Grade.B, Tall = false, Nice = true },
				new Student { Name = "Cayden", Age = 34, Friends = 2, GPA = 1.01, Grade = Grade.D, Tall = false, Nice = false },
				new Student { Name = "Sophia", Age = 28, Friends = 13, GPA = 3.33, Grade = Grade.A, Tall = true, Nice = true },
				new Student { Name = "Ethan", Age = 21, Friends = 3, GPA = 0.33, Grade = Grade.F, Tall = true, Nice = false },
				new Student { Name = "Olivia", Age = 32, Friends = 5, GPA = 3.12, Grade = Grade.C, Tall = true, Nice = true },
				new Student { Name = "Jackson", Age = 26, Friends = 0, GPA = 0.47, Grade = Grade.C, Tall = tr