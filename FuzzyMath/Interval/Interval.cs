using System;
using System.Globalization;

namespace FuzzyMath
{
    /// <summary>
    /// Closed interval
    /// </summary>
    public partial struct Interval
    {

        internal static double Epsilon = 1E-12;

        private double a, b;

        /// <summary>
        /// Creates a closed interval [a, b]
        /// </summary>
        /// <param name="a">Lower bound</param>
        /// <param name="b">Upper bound</param>
        public Interval(double a, double b)
        {

            if (a > b)
            {
                if (Math.Abs(a - b) > Epsilon)
                {
                    throw new ArgumentException("Lower bound must be less than or equal to the upper");
                }

                b = a;
            }

            if (double.IsInfinity(a) || double.IsInfinity(b))
            {
                throw new ArgumentException("Interval must be finite.");
            }

            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// Creates a degenerate interval
        /// </summary>
        public Interval(double a)
        {
            if (double.IsInfinity(a))
            {
                throw new ArgumentException("Interval must be finite.");
            }

            this.a = b = a;
        }

        /// <summary>
        /// Lower bound
        /// </summary>
        public double A
        {
            get { return a; }
        }

        /// <summary>
        /// Upper bound
        /// </summary>
        public double B
        {
            get { return b; }
        }

        /// <summary>
        /// Diameter of the interval
        /// </summary>
        public double Width
        {
            get { return b - a; }
        }

        /// <summary>
        /// Centre of the interval
        /// </summary>
        public double Midpoint
        {
            get { return a + Width / 2; }
        }

        /// <summary>
        /// Containts a value?
        /// </summary>
        public bool Contains(double value)
        {
            return (a < value || a - value < Epsilon) && (value < b || value - b < Epsilon);
        }

        /// <summary>
        /// Containts an interval?
        /// </summary>
        public bool Contains(Interval other)
        {
            return Contains(other.A) && Contains(other.B);
        }

        /// <summary>
        /// Intersects with another interval?
        /// </summary>
        public bool Intersects(Interval other)
        {
            if ((a > other.A || other.A - a < Epsilon) && (a < other.B || a - other.B < Epsilon))
            {
                return true;
            }

            if ((a < other.A || a - other.A < Epsilon) && (b > other.A || other.A - b < Epsilon))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a string representation of the interval
        /// </summary>
        /// <returns>[a, b]</returns>
        public override string ToString()
        {
            return String.Format("[{0}, {1}]", a.ToString(CultureInfo.InvariantCulture), b.ToString(CultureInfo.InvariantCulture));
        }

        public double GreaterThan(Interval other)
        {
            return GreaterThan(this, other);
        }

        /// <summary>
        /// How much is interval 'x' greater than 'y'?
        /// </summary>
        /// <returns>Presumption level from the interval [0, 1]</returns>
        public static double GreaterThan(Interval x, Interval y)
        {
            if (x.B < y.A)
            {
                return 0;
            }

            if (x.A > y.B)
            {
                return 1;
            }

            var width = x.Width + y.Width;
            if (width < Epsilon)
            {
                return .5;
            }

            return (x.B - y.A) / width;
        }

    }
}

