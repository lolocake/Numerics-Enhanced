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
                return new Size(_width, _height);
            }
            set
            {
                if (value.IsEmpty)
                {
                    this = s_empty;
                }
                else
                {
                    if (IsEmpty)
                    {
                        throw new System.InvalidOperationException("Cannot modify empty rectangle.");
                    }

                    _width = value._width;
                    _height = value._height;
                }
            }
        }

        /// <summary>
        /// X - The X coordinate of the Location. 
        /// If this is the empty rectangle, the value will be positive infinity.
        /// If this rect is Empty, setting this property is illegal. 
        /// </summary> 
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                if (IsEmpty)
                {
                    throw new System.InvalidOperationException("Cannot modify empty rectangle.");
                }

                _x = value;
            }

        }

        /// <summary>
        /// Y - The Y coordinate of the Location 
        /// If this is the empty rectangle, the value will be positive infinity.
        /// If this rect is Empty, setting this property is illegal.
        /// </summary>
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (IsEmpty)
                {
                    throw new System.InvalidOperationException("Cannot modify empty rectangle.");
                }

                _y = value;
            }
        }

        /// <summary>
        /// Width - The Width component of the Size.  This cannot be set to negative, and will only 
        /// be negative if this is the empty rectangle, in which case it will be negative infinity.
        /// If this rect is Empty, setting this property is illegal. 
        /// </summary> 
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (IsEmpty)
                {
                    throw new System.InvalidOperationException("Cannot modify empty rectangle.");
                }

                if (value < 0)
                {
                    throw new System.ArgumentException("Width cannot be negative.");
                }

                _width = value;
            }
        }

        /// <summary>
        /// Height - The Height component of the Size.  This cannot be set to negative, and will only 
        /// be negative if this is the empty rectangle, in which case it will be negative infinity.
        /// If this rect is Empty, setting this property is illegal. 
        /// </summary> 
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (IsEmpty)
                {
                    throw new System.InvalidOperationException("Cannot modify empty rectangle.");
                }

                if (value < 0)
                {
                    throw new System.ArgumentException("Height cannot be negative.");
                }

                _height = value;
            }
        }

        /// <summary>
        /// Left Property - This is a read-only alias for X 
        /// If this is the empty rectangle, the value will be positive infinity.
        /// </summary> 
        public double Left
        {
            get
            {
                return _x;
            }
        }

        /// <summary> 
        /// Top Property - This is a read-only alias for Y 
        /// If this is the empty rectangle, the value will be positive infinity.
        /// </summary> 
        public double Top
        {
            get
            {
                return _y;
            }
        }

        /// <summary> 
        /// Right Property - This is a read-only alias for X + Width
        /// If this is the empty rectangle, the value will be negative infinity.
        /// </summary>
        public double Right
        {
            get
            {
                if (IsEmpty)
                {
                    return Double.NegativeInfinity;
                }

                return _x + _width;
            }
        }

        /// <summary>
        /// Bottom Property - This is a read-only alias for Y + Height 
        /// If this is the empty rectangle, the value will be negative infinity.
        /// </summary>
        public double Bottom
        {
            get
            {
                if (IsEmpty)
                {
                    return Double.NegativeInfinity;
                }

                return _y + _height;
            }
        }

        /// <summary> 
        /// TopLeft Property - This is a read-only alias for the Point which is at X, Y
        /// If this is the empty rectangle, the value will be positive infinity, positive infinity. 
        /// </summary>
        public Point TopLeft
        {
            get
            {
                return new Point(Left, Top);
            }
        }

        /// <summary>
        /// TopRight Property - This is a read-only alias for the Point which is at X + Width, Y
        /// If this is the empty rectangle, the value will be negative infinity, positive infinity.
        /// </summary> 
        public Point TopRight
        {
            get
            {
                return new Point(Right, Top);
            }
        }

        /// <summary> 
        /// BottomLeft Property - This is a read-only alias for the Point which is at X, Y + Height
        /// If this is the empty rectangle, the value will be positive infinity, negative infinity. 
        /// </summary> 
        public Point BottomLeft
        {
            get
            {
                return new Point(Left, Bottom);
            }
        }

        /// <summary> 
        /// BottomRight Property - This is a read-only alias for the Point which is at X + Width, Y + Height
        /// If this is the empty rectangle, the value will be negative infinity, negative infinity. 
        /// </summary>
        public Point BottomRight
        {
            get
            {
                return new Point(Right, Bottom);
            }
        }
        #endregion Public Properties

        #region Public Methods

        /// <summary> 
        /// Contains - Returns true if the Point is within the rectangle, inclusive of the edges.
        /// Returns false otherwise. 
        /// </summary> 
        /// <param name="point"> The point which is being tested </param>
        /// <returns> 
        /// Returns true if the Point is within the rectangle.
        /// Returns false otherwise
        /// </returns>
        public bool Contains(Point point)
        {
            return Contains(point._x, point._y);
        }

        /// <summary> 
       