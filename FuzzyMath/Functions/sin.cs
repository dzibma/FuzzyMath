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
            double a, b, extreme;
            extreme = Math.Floor(x.A / Math.PI) * Math.PI + Math.PI / 2;

            if (x.Contains(extreme))
            {
                a = Math.Sin(extreme);
            }
            else
            {
                extreme += Math.PI;

                if (x.Contains(extreme))
                {
                    a = Math.Sin(extreme);
                }
                else
                {
                    a = Math.Sin(x.A);
                    b = Math.Sin(x.B);

                    return b > a ? new Interval(a, b) : new Interval(b, a);
                }
            }

            extreme += Math.PI;

            if (x.Contains(extreme))
            {
                b = Math.Sin(extreme);
            }
            else
            {
                b = a > 0
                    ? Math.Min(Math.Sin(x.A), Math.Sin(x.B))
                    : Math.Max(Math.Sin(x.A), Math.Sin(x.B));
            }

            return b > a ? new Interval(a, b, x.Epsilon) : new Interval(b, a, x.Epsilon);
        }

    }
}
