using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates the sine of a fuzzy number
        /// </summary>
        /// <param name="X">Fuzzy angle in radians</param>
        public static FuzzyNumber Sin(FuzzyNumber X)
        {
            return FuzzyNumber.Map(X, x => Sin(x));
        }

        public static Interval Sin(Interval interval)
        {
            double a, b, x;
            x = Math.Floor(interval.A / Math.PI) * Math.PI + Math.PI / 2;

            if (interval.Contains(x))
            {
                a = Math.Sin(x);
            }
            else
            {
                x += Math.PI;

                if (interval.Contains(x))
                {
                    a = Math.Sin(x);
                }
                else
                {
                    a = Math.Sin(interval.A);
                    b = Math.Sin(interval.B);

                    return b > a ? new Interval(a, b) : new Interval(b, a);
                }
            }

            x += Math.PI;

            if (interval.Contains(x))
            {
                b = Math.Sin(x);
            }
            else
            {
                b = a > 0
                    ? Math.Min(Math.Sin(interval.A), Math.Sin(interval.B))
                    : Math.Max(Math.Sin(interval.A), Math.Sin(interval.B));
            }

            return b > a ? new Interval(a, b) : new Interval(b, a);
        }

    }
}
