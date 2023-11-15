using System;
using System.Linq.Expressions;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Basic implementation of an angle and conversation between radials and degrees.
	/// </summary>
	public class Angle
	{
		public static Angle Zero { get { return new Angle(0d); } }

		public static Angle Pi { get { return new Angle(Math.PI); } }

		public static Angle TwoPi { get { return new Angle(2 * Math.PI); } }

		public static Angle PiHalf { get { return new Angle(Math.PI / 2d); } }

		public static Angle ThreePiHalf { get { return new Angle(3 * Math.PI / 2d); } }

		public static Angle Bisect { get { return new Angle(Math.PI / 4d); } }

		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.Angle"/> class.
		/// </summary>
		public Angle()
		{
			this.Radians = 0.0;
		}

		/// <summary>
		/// Initializes a new ins