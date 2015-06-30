using System;
using System.Globalization;

namespace FuzzyMath
{
    /// <summary>
    /// Closed interval
    /// </summary>
    public partial struct Interval
    {

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
                throw new ArgumentException("Lower bound must be less than or equal to the upper");
            }

            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// Creates a degenerate interval
        /// </summary>
        public Interval(double value)
        {
            a = b = value;
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
            return a <= value && value <= b;
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
            if (a >= other.A && a <= other.B)
            {
                return true;
            }

            if (a <= other.A && b >= other.A)
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

    }
}

