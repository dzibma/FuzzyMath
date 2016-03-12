using System;
using System.Globalization;

namespace FuzzyMath
{
    /// <summary>
    /// Closed interval to represent an alpha-cut
    /// </summary>
    public partial struct Interval
    {

        private double a, b, epsilon;

        /// <summary>
        /// Create a closed interval [a, b]
        /// </summary>
        /// <param name="a">Lower bound</param>
        /// <param name="b">Upper bound</param>
        /// <param name="epsilon">Equality comparison tolerance</param>
        public Interval(double a, double b, double epsilon = 0)
        {
            this.epsilon = epsilon;

            if (Math.Abs(a - b) <= epsilon)
            {
                this.a = this.b = a;
            }
            else if (a < b)
            {
                this.a = a;
                this.b = b;
            }
            else
            {
                throw new ArgumentException("Lower bound must be less than or equal to the upper");
            }
        }

        /// <summary>
        /// Tolerance in equality comparison (of two doubles) 
        /// </summary>
        public double Epsilon
        {
            get { return epsilon; }
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
            get { return (a + b) / 2; }
        }

        /// <summary>
        /// Containts a value?
        /// </summary>
        public bool Contains(double x)
        {
            return x >= a - epsilon && x <= b + epsilon;
        }

        /// <summary>
        /// Containts an interval?
        /// </summary>
        public bool Contains(Interval other)
        {
            return other.A >= a - epsilon && other.B <= b + epsilon;
        }

        /// <summary>
        /// Intersects with another interval?
        /// </summary>
        public bool Intersects(Interval other)
        {
            if (other.Epsilon > epsilon)
            {
                return other.Intersects(this);
            }

            return Contains(other.A) || Contains(other.B) || other.Contains(this);
        }

        /// <summary>
        /// How much is greater than other interval?
        /// </summary>
        /// <returns>Presumption level from 0 to 1</returns>
        public double GreaterThan(Interval other)
        {
            var epsilon = Math.Max(this.epsilon, other.Epsilon);

            if (b < other.A - epsilon)
            {
                return 0;
            }

            if (a > other.B + epsilon)
            {
                return 1;
            }

            var with = Width + other.Width;
            if (with <= epsilon)
            {
                return .5;
            }

            return (b - other.A) / with;
        }

        /// <summary>
        /// Builds a string representation of the interval
        /// </summary>
        /// <returns>[a, b]</returns>
        public override string ToString()
        {
            return String.Format("[{0}, {1}]", a.ToString(CultureInfo.InvariantCulture), b.ToString(CultureInfo.InvariantCulture));
        } 

    }
}

