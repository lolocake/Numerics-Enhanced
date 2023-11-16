using System;
using System.Diagnostics;

namespace Orbifold.Numerics
{
    internal static class MatrixUtil
    {
        /// <summary>
        /// TransformRect - Internal helper for perf
        /// </summary>
        /// <param name="rect"> The Rect to transform. </param>
        /// <param name="matrix"> The Matrix with which to transform the Rect. </param>
        internal static void TransformRect(ref Rect rect, ref Matrix matrix)
        {
 