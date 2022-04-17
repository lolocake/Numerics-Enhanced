﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Orbifold.Numerics
{
    /// <summary>
    ///     A real-valued matrix.
    /// </summary>
    public struct RMatrix : ICloneable
    {
        private readonly int colCount;

        private readonly int rowCount;

        private bool isTransposed;

        private double[,] matrix;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix" /> struct.
        /// </summary>
        /// <param name="rowCount">Row count.</param>
        /// <param name="colCount">Col count.</param>
        public RMatrix(int rowCount, int colCount)
        {
            this.rowCount = rowCount;
            this.colCount = colCount;
            this.matrix = new double[rowCount, colCount];
            this.isTransposed = false;
            for (var i = 0; i < rowCount; i++) for (var j = 0; j < colCount; j++) this.matrix[i, j] = 0.0;
        }

        public RMatrix(int size)
            : this(size, size)
        {
        }

        public RMatrix(IReadOnlyList<double> a, IReadOnlyList<double> b)
        {
            this.matrix = new double[a.Count, 2];
            this.isTransposed = false;
            for (var i = 0; i < a.Count; i++)
            {
                this.matrix[i, 0] = a[i];
                this.matrix[i, 1] = b[i];
            }
            this.rowCount = a.Count;
            this.colCount = 2;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix" /> struct.
        /// </summary>
        /// <param name="matrix">A matrix specified as doubles.</param>
        /// <remarks>
        ///     Note that the notation {{1,2},{3,4}} represents a matrix
        ///     | 1  2|
        ///     | 3  4|
        ///     and so on. It maps identically to the Mathematica notation.
        /// </remarks>
        public RMatrix(double[,] matrix)
        {
            this.matrix = matrix;
            this.rowCount = matrix.GetLength(0);
            this.colCount = matrix.GetLength(1);
            this.isTransposed = false;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Orbifold.Numerics.RMatrix" /> struct.
        /// </summary>
        /// <param name="m">M.</param>
        public RMatrix(RMatrix m)
        {
            this.rowCount = m.RowCount;
            this.colCount = m.ColumnCount;
            this.matrix = m.matrix;
            this.isTransposed = false;
        }

        public RMatrix T
        {
            get
            {
                return new RMatrix(this.colCount, this.rowCount)
                           {
                               isTransposed = true,
                               matrix = this.matrix
                           };
            }
        }

        public double this[int m, int n]
        {
            get
            {
                if (this.isTransposed)
                {
                    if (m < 0 || m > this.ColumnCount) throw new Exception("m-th row is out of range!");
                    if (n < 0 || n > this.RowCount) throw new Exception("n-th col is out of range!");
                    return this.matrix[n, m];
                }
                if (m < 0 || m > this.RowCount) throw new Exception("m-th row is out of range!");
                if (n < 0 || n > this.ColumnCount) throw new Exception("n-th col is out of range!");
                return this.matrix[m, n];
            }
            set
            {
                if (this.isTransposed) throw new Exception("The transosed matrix is read-only.");
                this.matrix[m, n] = value;
            }
        }

        public Vector this[int i]
        {
            get
            {
                return this[i, VectorType.Row];
            }
            set
            {
                this[i, VectorType.Row] = value;
            }
        }

        public Vector this[int i, VectorType t]
        {
            get
            {
                // switch it up if using a transposed version
                if (this.isTransposed) t = t == VectorType.Row ? VectorType.Col : VectorType.Row;

                if (t == VectorType.Row)
                {
                    if (i >= this.RowCount) throw new IndexOutOfRangeException();

                    return new Vector(this.matrix, i, true);
                }
                if (i >= this.ColumnCount) throw new IndexOutOfRangeException();

                return new Vector(this.matrix, i);
            }
            set
            {
                if (this.isTransposed) throw new InvalidOperationException("Cannot modify matrix in read-only transpose mode!");

                if (t == VectorType.Row)
                {
                    if (i >= this.RowCount) throw new IndexOutOfRangeException();

                    if (value.Dimension > this.ColumnCount) throw new InvalidOperationException(string.Format("Given vector has larger dimension than the {0} columns available.", this.ColumnCount));

                    for (var k = 0; k < this.ColumnCount; k++) this.matrix[i, k] = value[k];
                }
                else
                {
                    if (i >= this.ColumnCount) throw new IndexOutOfRangeException();

                    if (value.Dimension > this.RowCount) throw new InvalidOperationException(string.Format("Given vector has larger dimension than the {0} rows available.", this.RowCount));

                    for (var k = 0; k < this.RowCount; k++) this.matrix[k, i] = value[k];
                }
            }
        }

        /// <summary>
        ///     Gets the number or rows in this matrix.
        /// </summary>
        /// <value>The row count.</value>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>
        ///     Gets the number or columns in this matrix.
        /// </summary>
        /// <value>The column count.</value>
        public int ColumnCount
        {
            get
            {
                return this.colCount;
            }
        }

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public IEnumerable<Vector> GetRows()
        {
            for (var i = 0; i < this.RowCount; i++) yield return this[i, VectorType.Row];
        }

        public IEnumerable<Vector> GetCols()
        {
            for (var i = 0; i < this.ColumnCount; i++) yield return this[i, VectorType.Col];
        }

        public Vector Col(int i)
        {
            return this[i, VectorType.Col];
        }

        public Vector Row(int i)
        {
            return this[i, VectorType.Row];
        }

        public static RMatrix Identity(int size)
        {
            return Identity(size, size);
        }

        public static RMatrix Identity(int n, int d)
        {
            var m = new double[n, d];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < d; j++)
                {
                    if (i == j) m[i, j] = 1;
                    else m[i, j] = 0;
                }
            }

            return new RMatrix(n, d)
                       {
                           matrix = m,
                           isTransposed = false
                       };
        }

        public RMatrix IdentityMatrix()
        {
            var m = new RMatrix(this.rowCount, this.colCount);
            for (var i = 0; i < this.rowCount; i++) for (var j = 0; j < this.colCount; j++) if (i == j) m[i, j] = 1;
            return m;
        }

        public static implicit operator RMatrix(double[,] m)
        {
            return new RMatrix(m);
        }

        public RMatrix Center(VectorType t)
        {
            var max = t == VectorType.Row ? this.RowCount : this.ColumnCount;
            for (var i = 0; i < max; i++) this[i, t] -= this[i, t].Mean();
            return this;
        }

        public void Normalize(VectorType t)
        {
            var max = t == VectorType.Row ? this.RowCount : this.ColumnCount;
            for (var i = 0; i < max; i++) this[i, t] /= this[i, t].Norm();
        }

        /// <summary>
        ///     Returns a clone of this matrix.
        /// </summary>
        public RMatrix Clone()
        {
            return new RMatrix(this.matrix)
                       {
                           matrix = (double[,])this.matrix.Clone()
                       };
        }

        public Vector ToVector()
        {
            if (this.RowCount == 1) return this[0, VectorType.Row].Clone();

            if (this.ColumnCount == 1) return this[0, VectorType.Col].Clone();

            throw new InvalidOperationException("Only one-dimensional matrices can be converted to a vector.");
        }

        public override string ToString()
        {
            var strMatrix = "(";
            for (var i = 0; i < this.rowCount; i++)
            {
                var str = "";
                for (var j = 0; j < this.colCount - 1; j++) str += this.matrix[i, j].ToString(CultureInfo.InvariantCulture) + ", ";
                str += this.matrix[i, this.colCount - 1].ToString(CultureInfo.InvariantCulture);
                if (i != this.rowCount - 1 && i == 0) strMatrix += str + "\n";
                else if (i != this.rowCount - 1 && i != 0) strMatrix += " " + str + "\n";
                else strMatrix += " " + str + ")";
            }
            return strMatrix;
        }

      