using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates the cosine of a fuzzy number
        /// </summary>
        /// <param name="X">Fuzzy angle in radians</param>
        public static FuzzyNumber Cos(FuzzyNumber X)
        {
            return FuzzyNumber.Map(X, x => Cos(x));
        }

        public static Interval Cos(Interval interval)
        {
            double a, b, x;
            x = Math.Floor(interval.A / Math.PI) * Math.PI;

            if (interval.Contains(x))
            {
                a = Math.Cos(x);
            }
            else
            {
                x += Math.PI;

                if (interval.Contains(x))
                {
                    a = Math.Cos(x);
                }
                else
                {
                    a = Math.Cos(interval.A);
                    b = Math.Cos(interval.B);

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
                    ? Math.Min(Math.Cos(interval.A), Math.Cos(interval.B))
                    : Math.Max(Math.Cos(interval.A), Math.Cos(interval.B));
            }

            return b > a ? new Interval(a, b) : new Interval(b, a);
        }

    }
}
