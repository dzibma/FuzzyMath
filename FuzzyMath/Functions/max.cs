using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        public static FuzzyNumber Max(FuzzyNumber X, FuzzyNumber Y)
        {
            return FuzzyNumber.Map(X, Y, (x, y) => Max(x, y));
        }

        public static Interval Max(Interval x, Interval y)
        {
            return new Interval(Math.Max(x.A, y.A), Math.Max(x.B, y.B));
        }

    }
}
