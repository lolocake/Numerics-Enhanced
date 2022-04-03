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
        ///     Initializes a new instance of the <see cref="Orbifold