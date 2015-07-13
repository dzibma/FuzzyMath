using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates tangent of the fuzzy number
        /// </summary>
        /// <param name="X">Fuzzy angle in radians</param>
        public static FuzzyNumber Tan(FuzzyNumber X)
        {
            return FuzzyNumber.Map(X, x => Tan(x));
        }

        public static Interval Tan(Interval x)
        {
            if (Cos(x).Contains(0))
            {
                throw new ArgumentException();
            }

            return new Interval(Math.Tan(x.A), Math.Tan(x.B));
        }

    }
}
