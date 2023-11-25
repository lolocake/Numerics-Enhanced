using System;
using System.Diagnostics;
using System.Globalization;

namespace Orbifold.Numerics
{
    public struct Rect : IFormattable
    {
        #region Constructors

        /// <summary> 
        /// Constructor which sets the initial values to the values of the parameters 
        /// </summary>
        public Rect(Point location,
                    Size size)
        {
            if (size.IsEmpty)
            {
                this = s_empty;
            }
            else
            {
                _x = location._x;
                _y = location._y;
                _width = size._width;
                _height = size._height;
            }
        }

        /// <summary> 
        /// Constructor which sets the initial values to the values of the parameters.
        /// Width and Height must be non-negative 
        /// </summary>
        public Rect(double x,
                    double y,
                    double width,
                    double height)
        {
            if (width < 0 || height < 0)
            {
                throw new System.ArgumentException("Width and height cannot be negative.");
            }

            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        /// <summary> 
        /// Constructor which sets the initial values to bound the two points provided.
        /// </summary>
        public Rect(Point point1,
                    Point point2)
        {
            _x = Math.Min(point1._x, point2._x);
            _y = Math.Min(point1._y, point2._y);

            //  Max with 0 to prevent double weirdness from causing us to be (-epsilon..0) 
            _width = Math.Max(Math.Max(point1._x, point2._x) - _x, 0);
            _height = Math.Max(Math.Max(point1._y, point2._y) - _y, 0);
        }

        /// <summary>
        /// Constructor which sets the initial values to bound the point provided and the point 
        /// which results from point + vector. 
        /// </summary>
        public Rect(Point point,
                    Vector2D vector)
            : this(point, vector+point)
        {
        }

        /// <summary>
        /// Constructor which sets the initial values to bound the (0,0) point and the point 
        /// that results from (0,0) + size. 
        /// </summary>
        public Rect(Size size)
        {
            if (size.IsEmpty)
            {
                this = s_empty;
            }
            else
            {
                _x = _y = 0;
                _width = size.Width;
                _height = size.Height;
            }
        }

        #endregion Constructors

        #region Statics

        /// <summary> 
        /// Empty - a static property which provides an Empty rectangle.  X and Y are positive-infinity
        /// and Width and Height are negative infinity.  This is the only situation where Width or
        /// Height can be negative.
        /// </summary> 
        public static Rect Empty
        {
            get
            {
                return s_empty;
            }
        }

        #endregion Statics

        #region Public Properties

        /// <summary>
        /// IsEmpty - this returns true if this rect is the Empty rectangle. 
        /// Note: If width or height are 0 this Rectangle still contains a 0 or 1 dimensional set
        /// of points, so this method should not be used to check for 0 area.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                // The funny width and height tests are to handle NaNs
                Debug.Assert((!(_width < 0) && !(_height < 0)) || (this == Empty));

                return _width < 0;
            }
        }

        /// <summary> 
        /// Location - The Point representing the origin of the Rectangle 
        /// </summary>
        public Point Location
        {
            get
            {
                return new Point(_x, _y);
            }
            set
            {
                if (IsEmpty)
                {
                    throw new System.InvalidOperationException("Cannot modify empty rectangle.");
                }

                _x = value._x;
                _y = value._y;
            }
        }

        /// <summary> 
        /// Size - The Size representing the area of the Rectangle
        /// </summary>
        public Size Size
        {
            get
            {
                if (IsEmpty)
                    return Size.Empty;
 