using System;
using System.Globalization;

namespace FuzzyMath
{
    public partial struct Interval
    {

        private double a, b, epsilon;

        /// <summary>
        /// Creates a closed interval [a, b]
        /// </summary>
        /// <param name="a">Lower bound</param>
        /// <param name="b">Upper bound</param>
        /// <param name="epsilon">Equality comparison tolerance</param>
        public Interval(double a, double b, double epsilon = 0)
        {
            if (a > b)
            {
                if (a - b > epsilon)
                {
                    throw new ArgumentException("Lower bound must be less than or equal to the upper");
                }

                b = a;
            }

            this.epsilon = epsilon;
            this.a = a;
            this.b = b;
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
            get { return a + Width / 2; }
        }

        /// <summary>
        /// Containts a value?
        /// </summary>
        public bool Contains(double value)
        {
            return (a < value || a - value <= epsilon) && (value < b || value - b <= epsilon);
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
            if ((a > other.A || other.A - a <= epsilon) && (a < other.B || a - other.B <= epsilon))
            {
                return true;
            }

            if ((a < other.A || a - other.A <= epsilon) && (b > other.A || other.A - b <= epsilon))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// How much is greater than other interval?
        /// </summary>
        /// <returns>Presumption level from 0 to 1</returns>
        public double GreaterThan(Interval other)
        {
            if (b < other.A)
            {
                return 0;
            }

            if (a > other.B)
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

