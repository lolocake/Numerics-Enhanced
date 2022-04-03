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
		/// <returns>A Tuple&lt;double,double&gt;</returns>
		private Tuple<double, double> Schur(RMatrix a, int p, int q)
		{
			double c, s = 0;
			if(a[p, q] != 0) {
				var tau = (a[q, q] - a[p, p]) / (2 * a[p, q]);
				var t = 0d;
				if(tau >= 0)
					t = 1 / (tau + Math.Sqrt(tau + sqr(tau)));
				else
					t = -1 / (-tau + Math.Sqrt(1 + sqr(tau)));

				c = 1 / Math.Sqrt(1 + sqr(t));
				s = t * c;
			} else {
				c = 1;
				s = 0;
			}

			return new Tuple<double, double>(c, s);
		}

		/// <summary>Sweeps.</summary>
		/// <param name="p">The int to process.</param>
		/// <param name="q">The int to process.</param>
		private void Sweep(int p, int q)
		{
			// set jacobi rotation matrix
			var cs = Schur(A, p, q);
			double c = cs.Item1;
			double s = cs.Item2;

			if(c != 1 || s != 0) { // if rotation

				/*************************
                 * perform jacobi rotation
                 *************************/
				// calculating intermediate J.T * A
				var pV = Vector.Create(A.ColumnCount, i => A[p, i] * c + A[q, i] * -s);
				var qV = Vector.Create(A.ColumnCount, i => A[q, i] * c + A[p, i] * s);

				// calculating A * J for inner p, q square
				var App = pV[p] * c + pV[q] * -s;
				var Apq = pV[q] * c + pV[p] * s;
				var Aqq = qV[q] * c + qV[p] * s;

				// fill in changes along box
				pV[p] = App;
				pV[q] = qV[p] = Apq;
				qV[q] = Aqq;

				/***************************
                 * store accumulated results
                 ***************************/
				var pE = Vector.Create(V.RowCount, i => V[i, p] * c + V[i, q] * -s);
				var qE = Vector.Create(V.RowCount, i => V[i, q] * c + V[i, p] * s);

				/****************
                 * matrix updates
                 ****************/
				// Update A
				A[p, VectorType.Col] = pV;
				A[p, VectorType.Row] = pV;
				A[q, VectorType.Col] = qV;
				A[q, VectorType.Row] = qV;

				// Update V - not critical 
				V[p, VectorType.Col] = pE;
				V[q, VectorType.Col] = qE;
			}
		}

		/// <summary>Parallels this object.</summary>
		public void Parallel()
		{
			//Console.WriteLine("Starting new sweep!");
			int N = A.ColumnCount;
			// make even pairings
			int n = N % 2 == 0 ? N : N + 1;

			//varp round-iness of the robin
			var queue = new Queue<int>(n - 1);

			// fill queue
			for(int i = 1; i < N; i++)
				queue.Enqueue(i);
			// add extra for odd pairings
			if(N % 2 == 1)
				queue.Enqueue(-1);

			for(int i = 0; i < n - 1; i++) {
				System.Threading.Tasks.Parallel.For(0, n / 2, j => {
					int p, q, k = n - 1