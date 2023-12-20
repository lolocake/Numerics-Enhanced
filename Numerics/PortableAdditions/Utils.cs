
#region Copyright

// Copyright (c) 2007-2011, Orbifold bvba.
// 
// For the complete license agreement see http://orbifold.net/EULA or contact us at sales@orbifold.net.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Orbifold.Numerics
{
	/// <summary>
	/// Contains common helper methods.
	/// </summary>
	public static class Utils
	{

		/// <summary>
		/// Linear interpolation between the given values.
		/// </summary>
		/// <remarks>See http://en.wikipedia.org/wiki/Linear_interpolation .</remarks>
		/// <param name="x">A value.</param>
		/// <param name="y">Another value.</param>
		/// <param name="fraction">A value in the [0,1] interval. At zero the interpolation returns the first value, at one it results in the second value.</param>
		/// <returns></returns>
		public static double Lerp(double x, double y, double fraction)
		{
			return (x * (1.0 - fraction)) + (y * fraction);
		}

		/// <summary>
		/// Gets the bezier coefficients, see http://processingjs.nihongoresources.com/bezierinfo/ .
		/// </summary>
		/// <param name="a0">The a0.</param>
		/// <param name="a1">The a1.</param>
		/// <param name="a2">The a2.</param>
		/// <param name="a3">The a3.</param>
		/// <param name="b0">The b0.</param>
		/// <param name="b1">The b1.</param>
		/// <param name="b2">The b2.</param>
		/// <param name="b3">The b3.</param>
		/// <param name="u">The u.</param>
		/// <param name="s">The s.</param>
		/// <param name="z">The z.</param>
		/// <param name="x4">The x4.</param>
		/// <param name="y4">The y4.</param>
		public static void GetBezierCoefficients(ref double a0, ref double a1, ref double a2, ref double a3, ref double b0, ref double b1, ref double b2, ref double b3, ref double u, ref double s, ref double z, ref double x4, ref double y4)
		{
			var x = a0 + (u * (a1 + (u * (a2 + (u * a3)))));
			var dx4 = x - x4;
			var dy = b1 + (u * ((2 * b2) + (3 * u * b3)));
			var y = b0 + (u * (b1 + (u * (b2 + (u * b3)))));
			var dy4 = y - y4;
			var dx = a1 + (u * ((2 * a2) + (3 * u * a3)));
			s = (dx4 * dx4) + (dy4 * dy4);
			z = (dx * dx4) + (dy * dy4);
		}

		/// <summary>
		/// Similar to Math.Sign but zero is a range instead of point
		/// </summary>
		/// <param name="val"></param>
		/// <param name="zeroLow"></param>
		/// <param name="zeroHigh"></param>
		/// <returns></returns>
		public static int StairValue(double val, double zeroLow, double zeroHigh)
		{
			if(val < zeroLow)
				return -1;
			if(val < zeroHigh)
				return 0;
			return 1;
		}

		/// <summary>
		/// 2D version of Sign()
		/// </summary>
		/// <param name="p"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Vector2D StairValue(Point p, Rect r)
		{
			return new Vector2D(StairValue(p.X, r.Left, r.Right), StairValue(p.Y, r.Top, r.Bottom));
		}

		/// <summary>
		/// Approximation of a Bezier segment by a polyline.
		/// </summary>
		/// <param name="bezierPoints">The points defining the Bezier curve.</param>
		/// <param name="index">The index at which the four Bezier start.</param>
		/// <param name="quality">The quality of the approximation.</param>
		/// <returns></returns>
		/// <seealso cref="http://en.wikipedia.org/wiki/B%C3%A9zier_curve">Bezier curves on Wikipedia.</seealso>
		public static Point[] ApproximateBezierCurve(Point[] bezierPoints, int index, int quality)
		{
			var polyline = new Point[quality];
			var epsilon = 1.0 / quality;

			var x0 = bezierPoints[0 + index].X;
			var y0 = bezierPoints[0 + index].Y;
			var x1 = bezierPoints[1 + index].X;
			var y1 = bezierPoints[1 + index].Y;
			var x2 = bezierPoints[2 + index].X;
			var y2 = bezierPoints[2 + index].Y;
			var x3 = bezierPoints[3 + index].X;
			var y3 = bezierPoints[3 + index].Y;

			for(var i = 0; i < quality; ++i) {
				var t = i * epsilon;
				//these are just the classic polynomials related to Bezier
				var q0 = (1 - t) * (1 - t) * (1 - t);
				var q1 = 3 * t * (1 - t) * (1 - t);
				var q2 = 3 * t * t * (1 - t);
				var q3 = t * t * t;
				var xt = q0 * x0 + q1 * x1 + q2 * x2 + q3 * x3;
				var yt = q0 * y0 + q1 * y1 + q2 * y2 + q3 * y3;

				polyline[i] = new Point(xt, yt);
			}
			return polyline;
		}

		/// <summary>
		/// Returns whether the line (line segments) intersect and returns in the <see cref="crossingPoint"/> the actual crossing 
		/// point if they do.
		/// </summary>
		/// <param name="a">The first point of the first line.</param>
		/// <param name="b">The second point of the first line.</param>
		/// <param name="c">The first point of the second line.</param>
		/// <param name="d">The second point of the second line.</param>
		/// <param name="crossingPoint">The crossing point.</param>
		/// <returns></returns>
		public static bool AreLinesIntersecting(Point a, Point b, Point c, Point d, ref Point crossingPoint)
		{
			var tangensdiff = (b.X - a.X) * (d.Y - c.Y) - (b.Y - a.Y) * (d.X - c.X);
			if(tangensdiff == 0)
				return false;
			var num1 = (a.Y - c.Y) * (d.X - c.X) - (a.X - c.X) * (d.Y - c.Y);
			var num2 = (a.Y - c.Y) * (b.X - a.X) - (a.X - c.X) * (b.Y - a.Y);
			var r = num1 / tangensdiff;
			var s = num2 / tangensdiff;
			if(r < 0 || r > 1 || s < 0 || s > 1)
				return false; //parallel cases
			crossingPoint = new Point(a.X + r * (b.X - a.X), a.Y + r * (b.Y - a.Y));

			return true;
		}

		/// <summary>
		/// Returns the barycentric coordinates as percentages with respect to the given rectangle.
		/// </summary>
		/// <param name="realPoint">The real point.</param>
		/// <param name="rectangle">The rectangle which acts as a barycentric coordinate system.</param>
		/// <returns>The percentages wrapped in a Point.</returns>
		/// <see cref="PointFromBarycentricPercentage(Point,System.Windows.Size)">The complementary method.</see>
		/// <seealso cref="http://en.wikipedia.org/wiki/Barycentric_coordinate_system_%28mathematics%29">Barycentric coordinates.</seealso>
		public static Point BarycentricPercentageFromPoint(Point realPoint, Rect rectangle)
		{
			var percentage = new Point(50, 50);

			var w = rectangle.Right - rectangle.Left;
			var h = rectangle.Bottom - rectangle.Top;
			if(w != 0 && h != 0) {
				percentage.X = (realPoint.X - rectangle.Left) * 100 / w;
				percentage.Y = (realPoint.Y - rectangle.Top) * 100 / h;
			}

			return percentage;
		}

		/// <summary>
		/// Returns the squared distance between the given points.
		/// </summary>
		public static double DistanceSquared(Point a, Point b)
		{
			return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
		}

		/// <summary>
		/// Finds the intersection point of the lines defined by the
		/// specified point pairs.
		/// </summary>
		/// <returns>
		/// The intersection point of the specified lines or
		/// Point(float.MinValue, float.MinValue) if the lines
		/// do not intersect.
		/// </returns>
		public static Point FindLinesIntersection(Point a, Point b, Point c, Point d, bool acceptNaN = false)
		{
			var pt = acceptNaN ? new Point(Double.NaN, Double.NaN) : new Point(Double.MinValue, Double.MinValue);

			if(a.X == b.X && c.X == d.X)
				return pt;

			// Check if the first line is vertical
			if(a.X == b.X) {
				pt.X = a.X;
				pt.Y = (c.Y - d.Y) / (c.X - d.X) * pt.X + (c.X * d.Y - d.X * c.Y) / (c.X - d.X);

				return pt;
			}

			// Check if the second line is vertical
			if(c.X == d.X) {
				pt.X = c.X;
				pt.Y = (a.Y - b.Y) / (a.X - b.X) * pt.X + (a.X * b.Y - b.X * a.Y) / (a.X - b.X);

				return pt;
			}

			var a1 = (a.Y - b.Y) / (a.X - b.X);
			var b1 = (a.X * b.Y - b.X * a.Y) / (a.X - b.X);

			var a2 = (c.Y - d.Y) / (c.X - d.X);
			var b2 = (c.X * d.Y - d.X * c.Y) / (c.X - d.X);

			if((a1 != a2) || acceptNaN) {
				pt.X = (b2 - b1) / (a1 - a2);
				pt.Y = a1 * (b2 - b1) / (a1 - a2) + b1;
			}

			return pt;
		}

		/// <summary>
		/// Inflates the given rectangle with the specified amount.
		/// </summary>
		public static Rect Inflate(Rect rect, double deltaX, double deltaY)
		{
			//border case when the delta is negative and larger than the actual rectangle size
			if(rect.Width + 2 * deltaX < 0)
				deltaX = -rect.Width / 2;
			if(rect.Height + 2 * deltaY < 0)
				deltaY = -rect.Height / 2;
			return new Rect(rect.X - deltaX, rect.Y - deltaY, rect.Width + 2 * deltaX, rect.Height + 2 * deltaY);
		}

		/// <summary>
		/// Returs the middle point between the given points.
		/// </summary>
		/// <param name="p1">A point</param>
		/// <param name="p2">Another point</param>
		/// <returns></returns>
		public static Point Mid(Point p1, Point p2)
		{
			return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
		}

		/// <summary>
		/// Returns the mirrored vector with respect to the X-coordinate.
		/// </summary>
		/// <param name="v">The v.</param>
		/// <returns></returns>
		//public static Vector MirrorHorizontally(this Vector v)
		//{
		//    return new Vector(v.Y, -v.X);
		//}

		public static Vector2D MirrorVertically(this Vector2D v)
		{
			return new Vector2D(-v.Y, v.X);
		}

		/// <summary>
		/// Given a percentage and a rectangle this will return the coordinates corresponding
		/// to the percentages given.
		/// </summary>
		/// <param name="percentage">A couple of values in percentage, e.g. a value of (50,50) will return the center of the rectangle.</param>
		/// <param name="size">The size from which the point will be extracted.</param>
		/// <returns></returns> 
		/// <seealso cref="http://en.wikipedia.org/wiki/Barycentric_coordinate_system_%28mathematics%29">Barycentric coordinates.</seealso>
		public static Point PointFromBarycentricPercentage(Point percentage, Size size)
		{
			return new Point(percentage.X / 100D * size.Width, percentage.Y / 100D * size.Height);
		}

		/// <summary>
		/// Given a percentage and a rectangle this will return the coordinates corresponding
		/// to the percentages given.
		/// </summary>
		/// <param name="percentage">A couple of values in percentage, e.g. a value of (50,50) will return the center of the rectangle.</param>
		/// <param name="rectangle">The rectangle which acts as the barycentric system.</param>
		/// <returns></returns>
		/// <seealso cref="http://en.wikipedia.org/wiki/Barycentric_coordinate_system_%28mathematics%29">Barycentric coordinates.</seealso>
		public static Point PointFromBarycentricPercentage(Point percentage, Rect rectangle)
		{
			return new Point(rectangle.Left + percentage.X / 100D * rectangle.Width, rectangle.Top + percentage.Y / 100D * rectangle.Height);
		}

		public static void CopyTo<T>(this T[] src, IList<T> dest)
		{
			for(int i = 0, n = src.Length; i < n; ++i)
				dest.Add(src[i]);
		}

		/// <summary>
		/// Converts the specified value from degrees to radians.
		/// </summary>
		public static double ToDegrees(this double radians)
		{
			return radians / Math.PI * 180;
		}

		/// <summary>
		/// Converts the specified value from degrees to radians.
		/// </summary>
		//public static double ToRadians(this double degrees)
		//{
		//    return degrees / 180 * Math.PI;
		//}

		public static Rect ToRect(this Size s)
		{
			return new Rect(0, 0, s.Width, s.Height);
		}

       
		/// <summary>
		/// Checks if the specified rectangle and circle intersect.
		/// </summary>
		public static bool CheckIntersect(Point pt, Rect rc, double rad)
		{
			//Translating coordinates, placing percentage at origin
			var leftX = rc.Left - pt.X;
			var rightX = rc.Right - pt.X;
			var topY = rc.Top - pt.Y;
			var bottomY = rc.Bottom - pt.Y;

			if(rightX < 0) {
				if(topY > 0) { //rect is SW from origin
					return (rightX * rightX + topY * topY < rad * rad);
				} else if(bottomY < 0) { //rect is NW from origin
					return (rightX * rightX + bottomY * bottomY < rad * rad);
				} else { //rect is W from circle
					return (Math.Abs(rightX) < rad);
				}
			} else if(leftX > 0) {
				if(topY > 0) { // rect is SE from origin
					return (leftX * leftX + topY * topY < rad * rad);
				} else if(bottomY < 0) { //rect is NE from origin
					return (leftX * leftX + bottomY * bottomY < rad * rad);
				} else { //rect is E from circle
					return (Math.Abs(leftX) < rad);
				}
			} else {
				if(topY > 0) { //rect is S from circle
					return (Math.Abs(topY) < rad);
				} else if(bottomY < 0) { // rect is N from circle
					return (Math.Abs(bottomY) < rad);
				} else { // rect contains origin
					return true;
				}
			}
		}

		public static List<Point> ApproximateBezierCurve(Point[] bezierPoints, int quality)
		{
			var approximation = new List<Point>();
			var epsilon = 1.0 / quality;

			// get the points defining the curve
			var x0 = bezierPoints[0].X;
			var y0 = bezierPoints[0].Y;
			var x1 = bezierPoints[1].X;
			var y1 = bezierPoints[1].Y;
			var x2 = bezierPoints[2].X;
			var y2 = bezierPoints[2].Y;
			var x3 = bezierPoints[3].X;
			var y3 = bezierPoints[3].Y;

			for(double t = 0; t <= 1.0; t += epsilon) {
				var q0 = (1 - t) * (1 - t) * (1 - t);
				var q1 = 3 * t * (1 - t) * (1 - t);
				var q2 = 3 * t * t * (1 - t);
				var q3 = t * t * t;
				var xt = q0 * x0 + q1 * x1 + q2 * x2 + q3 * x3;
				var yt = q0 * y0 + q1 * y1 + q2 * y2 + q3 * y3;

				// Draw straight line between last two calculated points
				approximation.Add(new Point(xt, yt));
			}

			return approximation;
		}

		public static void ArcConvert(Rect r, double startAngle, double sweep, out Point startPoint, out Point outsidePoint, out bool largeArc, out SweepDirection dir)
		{
			startPoint = ArcPoint(r, startAngle);
			outsidePoint = ArcPoint(r, startAngle + sweep);
			largeArc = Math.Abs(sweep) > 180;
			dir = sweep > 0 ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
		}

		/// <summary>
		/// Returns the point at an angle on the ellipse with axes specified by the given rectangle.
		/// </summary>
		public static Point ArcPoint(this Rect r, double angle)
		{
			var rads = angle.ToRadians();
			var cos = Math.Cos(rads);
			var rx = r.Width / 2;
			var ry = r.Height / 2;
			double x, y;

			if(cos == 0) {
				x = 0;
				y = ry * Math.Sin(rads); // for sign only
			} else {
				var sin = Math.Sin(rads);
				var r1 = 1 / Math.Sqrt(Sqr(cos / rx) + Sqr(sin / ry));
				x = r1 * cos;
				y = r1 * sin;
			}

			return new Point(r.Left + rx + x, r.Top + ry + y);
		}

		public static bool BetweenOrEqual(double n, double boundary1, double boundary2)
		{
			Sort(ref boundary1, ref boundary2);
			return BetweenOrEqualSorted(n, boundary1, boundary2);
		}

		internal static bool BetweenOrEqualSorted(double n, double boundary1, double boundary2)
		{
			return boundary1 <= n && n <= boundary2;
		}


		/// <summary>
		/// Calculates the bezier coefficients in the equation
		/// of the specified bezier curve.
		/// </summary>
		public static void CalcBezierCoef(ref double a0, ref double a1,
		                                        ref double a2, ref double a3, ref double b0, ref double b1,
		                                        ref double b2, ref double b3, ref double u, ref double s,
		                                        ref double z, ref double x4, ref double y4)
		{
			var x = a0 + u * (a1 + u * (a2 + u * a3));
			var y = b0 + u * (b1 + u * (b2 + u * b3));
			var dx4 = x - x4;
			var dy4 = y - y4;
			var dx = a1 + u * (2 * a2 + 3 * u * a3);
			var dy = b1 + u * (2 * b2 + 3 * u * b3);

			z = dx * dx4 + dy * dy4;
			s = dx4 * dx4 + dy4 * dy4;
		}

		/// <summary>
		/// Calculates the point of the the specified line segment which determines the distance from the specified
		/// point to the line segment
		/// </summary>
		public static Point DistancePoint(Point p, Point a, Point b)
		{
			if(a == b)
				return a;

			var dx = b.X - a.X;
			var dy = b.Y - a.Y;

			var dotProduct = (p.X - a.X) * dx + (p.Y - a.Y) * dy;
			if(dotProduct < 0)
				return a;

			dotProduct = (b.X - p.X) * dx + (b.Y - p.Y) * dy;
			if(dotProduct < 0)
				return b;

			var lf = VectorExtensions.MirrorHorizontally(new Vector2D(a.X - b.X, a.Y - b.Y));
			var n = new Point(p.X + lf.X, p.Y + lf.Y);
			return FindLinesIntersection(a, b, p, n, true);
		}

		/// <summary>
		/// Converts dekart coordinates to the corresponding
		/// polar coordinates, using the specified point as
		/// a center of the coordinate system.
		/// </summary>
		public static void CarteseanToPolar(Point coordCenter, Point dekart, ref double a, ref double r)
		{
			if(coordCenter == dekart) {
				a = 0;
				r = 0;
				return;
			}

			var dx = dekart.X - coordCenter.X;
			var dy = dekart.Y - coordCenter.Y;
			r = Distance(dx, dy);

			a = Math.Atan(-dy / dx) * 180 / Math.PI;
			if(dx < 0)
				a += 180;
		}

		/// <summary>
		/// Determines, given three points, if when travelling from the first to
		/// the second to the third, we travel in a counterclockwise direction.
		/// </summary>
		/// <remarks>
		/// 1 if the movement is in a counterclockwise direction, -1 if not.
		/// </remarks>
		public static int IsCounterClockWise(Point p0, Point p1, Point p2)
		{
			var dx1 = p1.X - p0.X;
			var dx2 = p2.X - p0.X;
			var dy1 = p1.Y - p0.Y;
			var dy2 = p2.Y - p0.Y;

			// This is basically a slope comparison: we don't do divisions
			// because of divide by zero possibilities with pure horizontal
			// and pure vertical lines