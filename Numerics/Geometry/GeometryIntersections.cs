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
                // NW
                if (bottomY < 0) return (rightX * rightX) + (bottomY * bottomY) < radius * radius;
                return System.Math.Abs(rightX) < radius;
            }
            if (leftX > 0)
            {
                // SE
                if (topY > 0) return (leftX * leftX) + (topY * topY) < radius * radius;
                // NE
                if (bottomY < 0) return (leftX * leftX) + (bottomY * bottomY) < radius * radius;
                return System.Math.Abs(leftX) < radius;
            }
            if (topY > 0) return System.Math.Abs(topY) < radius;
            if (bottomY < 0) return System.Math.Abs(bottomY) < radius;
            return true;
        }

        /// <summary>
        /// Returns whether the specified point is inside the ellipse defined by the specified rectangle.
        /// </summary>
        public static bool IsPointInEllipse(Point point, Rect rectangle)
        {
            var rct = rectangle;
            var width = (rct.Right - rct.Left) / 2;
            var height = (rct.Bottom - rct.Top) / 2;
            var x = point.X - ((rct.Left + rct.Right) / 2);
            var y = point.Y - ((rct.Top + rct.Bottom) / 2);
            ////basic geometry again
            return ((x * x) / (width * width)) + ((y * y) / (height * height)) <= 1;
        }

        /// <summary>
        /// Determines whether [is point in rectangle] [the specified point].
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="size">The size.</param>
        /// <returns>
        ///   <c>true</c> if [is point in rectangle] [the specified point]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPointInRectangle(Point point, Size size)
        {
            return Utils.BetweenOrEqualSorted(point.X, 0, size.Width) && Utils.BetweenOrEqualSorted(point.Y, 0, size.Height);
        }

        /// <summary>
        /// Returns whether the specified rectangle and circle intersect.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns></returns>
        public static bool IntersectsCircle(this Rect rectangle, Point center, double radi