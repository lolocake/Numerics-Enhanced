using System;
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
        