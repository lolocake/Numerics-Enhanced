﻿using System;
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
        public static bool IntersectsCircle(this Rect rectangle, Point center, double radius)
        {
            return AreIntersecting(rectangle, center, radius);
        }

        /// <summary>
        /// Returns whether the line (line segments) intersect and returns in the crossingPoint the actual crossing
        /// point if they do.
        /// </summary>
        /// <param name="a">The first point of the first line.</param>
        /// <param name="b">The second point of the first line.</param>
        /// <param name="c">The first point of the second line.</param>
        /// <param name="d">The second point of the second line.</param>
        /// <param name="intersectionPoint">The crossing point, if any.</param>
        /// <returns></returns>
        public static bool AreLinesIntersecting(Point a, Point b, Point c, Point d, ref Point intersectionPoint)
        {
            var tangensdiff = ((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X));
            if (tangensdiff.IsEqualTo(0)) return false;
            var num1 = ((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y));
            var num2 = ((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y));
            var r = num1 / tangensdiff;
            var s = num2 / tangensdiff;

            if (r < 0 || r > 1 || s < 0 || s > 1) return false;
            intersectionPoint = new Point(a.X + (r * (b.X - a.X)), a.Y + (r * (b.Y - a.Y)));
            return true;
        }
        /// <summary>
        /// Returns whether the rectangle and the segment intersect.
        /// </summary>
        /// <param name="rect">A rectangle.</param>
        /// <param name="a">An endpoint of the segment.</param>
        /// <param name="b">The complementary endpoint of the segment.</param>
        /// <param name="intersectionPoint">The crossing point, if any.</param>
        /// <returns></returns>
        public static bool IntersectsLineSegment(this Rect rect, Point a, Point b, ref Point intersectionPoint)
        {
            return AreLinesIntersecting(a, b, rect.TopLeft(), rect.TopRight(), ref intersectionPoint) ||
                   AreLinesIntersecting(a, b, rect.TopRight(), rect.BottomRight(), ref intersectionPoint) ||
                   AreLinesIntersecting(a, b, rect.BottomRight(), rect.BottomLeft(), ref intersectionPoint) ||
                   AreLinesIntersecting(a, b, rect.BottomLeft(), rect.TopLeft(), ref intersectionPoint);
        }

        /// <summary>
        /// Returns whether the polyline segment intersect the rectangle.
        /// </summary>
        /// <param name="rect">A rectangle</param>
        /// <param name="polyline">A polyline.</param>
        /// <returns></returns>
        /// <seealso cref="IntersectsLineSegment"/>
        public static bool IntersectsLine(this Rect rect, IList polyline)
        {
            var intersectsWithRect = false;
            for (var s = 0; s < polyline.Count - 1; ++s)
            {
                var p1 = (Point)polyline[s];
                var p2 = (Point)polyline[s + 1];
                var intersectionPoint = new Point();
                intersectsWithRect = GeometryIntersections.IntersectsLineSegment(rect, p1, p2, ref intersectionPoint);
                if (intersectsWithRect) break;
            }
            return intersectsWithRect;
        }

        /// <summary>
        /// Returns whether the given rectangle intersects the current one.
        /// </summary>
        /// <param name="r1">The first rectangle.</param>
        /// <param name="r2">The queried rectangle which potentially intersects.</param>
        /// <returns></returns>
        public static bool IntersectsWith(this Rect r1, Rect r2)
        {
            r1.Intersect(r2);
            return !r1.IsEmpty;
        }

        /// <summary>
        /// Checks whether the segments defined by the specified
        /// point pairs intersect and returns the intersection point.
        /// </summary>
        public static bool SegmentIntersect(Point s1, Point s2, Point l1, Point l2, ref Point p)
        {
            p = FindLinesIntersection(s1, s2, l1, l2);
            var p1 = (p.X - s1.X) * (p.X - s2.X);
            var p2 = (p.Y - s1.Y) * (p.Y - s2.Y);
            if (p1 > Constants.Epsilon || p2 > Constants.Epsilon) return false;
            var pl1 = (p.X - l1.X) * (p.X - l2.X);
            var pl2 = (p.Y - l1.Y) * (p.Y - l2.Y);
            return pl1 <= Constants.Epsilon && pl2 <= Constants.Epsilon;
        }

        /// <summary>
        /// Finds the intersection point of the lines defined by the point pairs.
        /// </summary>
        /// <returns>
        /// The intersection point. If acceptNaN is <c>true</c> a <c>double.NaN</c> is returned if they don't intersect.
        /// </return