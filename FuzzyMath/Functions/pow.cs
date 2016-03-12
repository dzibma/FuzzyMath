using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates a value of the fuzzy number raised to the specified power.
        /// </summary>
        /// <param name="X">Base</param>
        /// <param name="y">Exponent</param>
        public static FuzzyNumber Pow(FuzzyNumber X, double y)
        {
            return FuzzyNumber.Map(X, x => Pow(x, y));
        }

        /// <summary>
        /// Calculates a value of the interval raised to the specified power.
        /// </summary>
        /// <param name="x">Base</param>
        /// <param name="y">Exponent</param>
        public static Interval Pow(Interval x, double y)
        {
            var a = Math.Pow(x.A, y);
            var b = Math.Pow(x.B, y);
            if (a > b)
            {
                var tmp = a;
                a = b;
                b = tmp;
            }

            if (x.Contains(0) && a > 0)
            {
                a = 0;
            }

            return new Interval(a, b, x.Epsilon);
        }

    }
}
