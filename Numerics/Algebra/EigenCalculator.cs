using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Calculates the eigenvalues and eigenvectors.
	/// </summary>
	public class EigenCalculator
	{
		private RMatrix A;

		private RMatrix V;
		private Func<double,double> sqr = d => Math.Pow(d, 2);

		public RMatrix EigenVectors {
			get {
				return V;
			}
		}

		public Vector EigenValues { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Orbifold.Numerics.Evd"/> class.
		/// </summary>
		/// <param name="a">A matrix.</param>
		public EigenCalculator(RMatrix a)
		{
			A = a.Clone();
			V = RMatrix.Identity(A.RowCount);
		}

		/// <summary>Offs the given a.</summary>
		/// <param name="a">The int to process.</param>
		/// <returns>A double.</returns>
		public double Offset(RMatrix a)
		{
			double sum = 0;
			for(int i = 0; i < a.RowCount; i++)
				for(int j = 0; j < a.ColumnCount; j++)
					if(i != j)
						sum += sqr(a[i, j]);
			return Math.Sqrt(sum);
		}

		/// <summary>Schurs.</summary>
		/// <param name="a">The int to process.</param>
		/// <param name="p">The int to process.</param>
		/// <param name="q">The int to process.</param>
		/// <returns>A Tuple&lt;double,double&gt;</retur