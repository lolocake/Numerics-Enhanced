using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Orbifold.Numerics
{
    /// <summary>
    /// Geometric intersection and overlap methods.
    /// </summary>
    public static class GeometryIntersections
    {
        /// <summary>
        /// Returns whether the two rectangles intersect.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool AreIntersecting(Rect rectangle, Rect rect)
        {
            rectangle.Intersect(rect);
            return !rectangle.IsEmpty;
        }

        /// <summary>
        /// Returns whether the specified rectangle and circle intersect.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns></returns>
        public static bool AreIntersecting(Rect rectangle, Point center, double radius)
        {
            var leftX = rectangle.Left - center.X;
            var rightX = rectangle.Right - center.X;
            var topY = rectangle.Top - center.Y;
            var bottomY = rectangle.Bottom - center.Y;

            if (rightX < 0)
            {
                // SW
                if (topY > 0) return (rightX * rightX) + (topY * topY) < radius * radius;
   