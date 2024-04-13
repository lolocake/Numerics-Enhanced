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
				new Student { Name = "Jackson", Age = 26, Friends = 0, GPA = 0.47, Grade = Grade.C, Tall = true, Nice = false },
				new Student { Name = "Madeline", Age = 40, Friends = 6, GPA = 3.63, Grade = Grade.C, Tall = true, Nice = true },
				new Student { Name = "Landon", Age = 24, Friends = 1, GPA = 0.40, Grade = Grade.D, Tall = false, Nice = false },
				new Student { Name = "Lily", Age = 26, Friends = 14, GPA = 3.40, Grade = Grade.B, Tall = true, Nice = true },
				new Student { Name = "Jacob", Age = 20, Friends = 3, GPA = 1.02, Grade = Grade.F, Tall = false, Nice = false },
				new Student { Name = "Abigail", Age = 32, Friends = 6, GPA = 3.10, Grade = Grade.A, Tall = true, Nice = true },
				new Student { Name = "Caleb", Age = 37, Friends = 3, GPA = 1.52, Grade = Grade.C, Tall = true, Nice = false },
				new Student { Name = "Chloe", Age = 32, Friends = 10, GPA = 3.59, Grade = Grade.B, Tall = false, Nice = true },
				new Student { Name = "Lucas", Age = 28, Friends = 2, GPA = 1.66, Grade = Grade.F, Tall = true, Nice = false },
				new Student { Name = "Emma", Age = 25, Friends = 13, GPA = 3.30, Grade = Grade.A, Tall = true, Nice = true },
				new Student { Name = "Braden", Age = 27, Friends = 2, GPA = 2.09, Grade = Grade.D, Tall = true, Nice = false },
				new Student { Name = "Charlotte", Age = 31, Friends = 10, GPA = 3.22, Grade = Grade.B, Tall = true, Nice = true },
				new Student { Name = "Benjamin", Age = 26, Friends = 3, GPA = 0.69, Grade = Grade.F, Tall = false, Nice = false },
				new Student { Name = "Ella", Age = 38, Friends = 12, GPA = 3.78, Grade = Grade.C, Tall = true, Nice = true },
				new Student { Name = "Gavin", Age = 18, Friends = 3, GPA = 1.63, Grade = Grade.C, Tall = false, Nice = false },
				new Student { Name = "Addison", Age = 27, Friends = 8, GPA = 3.65, Grade = Grade.A, Tall = true, Nice = true },
				new Student { Name = "Connor", Age = 16, Friends = 0, GPA = 1.67, Grade = Grade.C, Tall = false, Nice = false },
				new Student { Name = "Elizabeth", Age = 30, Friends = 10, GPA = 2.67, Grade = Grade.B, Tall = false, Nice = true },
				new Student { Name = "Elijah", Age = 27, Friends = 1, GPA = 0.68, Grade = Grade.F, Tall = true, Nice = false },
				new Student { Name = "Grace", Age = 27, Friends = 7, GPA = 2.54, Grade = Grade.C, Tall = true, Nice = true },
				new Student { Name = "Ol