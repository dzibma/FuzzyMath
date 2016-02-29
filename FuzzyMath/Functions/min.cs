using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        public static FuzzyNumber Min(FuzzyNumber X, FuzzyNumber Y)
        {
            return FuzzyNumber.Map(X, Y, (x, y) => Min(x, y));
        }

        public static Interval Min(Interval x, Interval y)
        {
            return new Interval(Math.Min(x.A, y.A), Math.Min(x.B, y.B));
        }

    }
}
