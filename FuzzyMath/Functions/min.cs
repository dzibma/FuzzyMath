using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Returns the smaller of two fuzzy numbers.
        /// </summary>
        public static FuzzyNumber Min(FuzzyNumber X, FuzzyNumber Y)
        {
            return FuzzyNumber.Map(X, Y, (x, y) => Min(x, y));
        }

        /// <summary>
        /// Returns the smaller of two intervals.
        /// </summary>
        public static Interval Min(Interval x, Interval y)
        {
            return new Interval(Math.Min(x.A, y.A), Math.Min(x.B, y.B), Math.Max(x.Epsilon, y.Epsilon));
        }

    }
}
