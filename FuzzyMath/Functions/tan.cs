using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates tangent of the fuzzy angle.
        /// </summary>
        /// <param name="X">A fuzzy number represents the angle in radians.</param>
        public static FuzzyNumber Tan(FuzzyNumber X)
        {
            return FuzzyNumber.Map(X, x => Tan(x));
        }

        /// <summary>
        /// Calculates tangent of the interval of angles.
        /// </summary>
        /// <param name="x">An interval represents the angle in radians.</param>
        public static Interval Tan(Interval x)
        {
            if (Cos(x).Contains(0))
            {
                throw new ArgumentException();
            }

            return new Interval(Math.Tan(x.A), Math.Tan(x.B), x.Epsilon);
        }

    }
}
