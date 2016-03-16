using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates a sine of the fuzzy angle.
        /// </summary>
        /// <param name="X">A fuzzy number represents the angle in radians.</param>
        public static FuzzyNumber Sin(FuzzyNumber X)
        {
            return FuzzyNumber.Map(X, x => Sin(x));
        }

        /// <summary>
        /// Calculates a sine of the interval of angles.
        /// </summary>
        /// <param name="x">A interval represents the angle in radians.</param>
        public static Interval Sin(Interval x)
        {
            return Cos(x - Math.PI / 2);
        }

    }
}
