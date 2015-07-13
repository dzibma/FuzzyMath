using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates value of fuzzy number 'X' raised to specified power
        /// </summary>
        /// <param name="X">Base</param>
        /// <param name="exp">Exponent</param>
        public static FuzzyNumber Pow(FuzzyNumber X, double power)
        {
            return FuzzyNumber.Map(X, x => Pow(x, power));
        }

        public static Interval Pow(Interval x, double power)
        {
            var a = Math.Pow(x.A, power);
            var b = Math.Pow(x.B, power);
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

            return new Interval(a, b);
        }

    }
}
