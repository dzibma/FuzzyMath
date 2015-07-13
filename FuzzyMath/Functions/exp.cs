using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates value of e raised to fuzzy number 'X'
        /// </summary>
        /// <param name="X">Exponent</param>
        public static FuzzyNumber Exp(FuzzyNumber X)
        {
            return FuzzyNumber.Map(X, x => Exp(x));
        }

        public static Interval Exp(Interval x)
        {
            return new Interval(Math.Exp(x.A), Math.Exp(x.B));
        }

    }
}
