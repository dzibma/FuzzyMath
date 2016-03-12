using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates e raised to the specified fuzzy number.
        /// </summary>
        /// <param name="X">Exponent</param>
        public static FuzzyNumber Exp(FuzzyNumber X)
        {
            return FuzzyNumber.Map(X, x => Exp(x));
        }

        /// <summary>
        /// Calculates e raised to the specified interval.
        /// </summary>
        /// <param name="X">Exponent</param>
        public static Interval Exp(Interval x)
        {
            return new Interval(Math.Exp(x.A), Math.Exp(x.B), x.Epsilon);
        }

    }
}
