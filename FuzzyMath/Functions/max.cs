using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Returns the greater of two fuzzy numbers.
        /// </summary>
        public static FuzzyNumber Max(FuzzyNumber X, FuzzyNumber Y)
        {
            return FuzzyNumber.Map(X, Y, (x, y) => Max(x, y));
        }

        /// <summary>
        /// Returns the greater of intervals.
        /// </summary>
        public static Interval Max(Interval x, Interval y)
        {
            return new Interval(Math.Max(x.A, y.A), Math.Max(x.B, y.B), Math.Max(x.Epsilon, y.Epsilon));
        }

    }
}
