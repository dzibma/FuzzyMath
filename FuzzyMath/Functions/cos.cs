﻿using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        /// <summary>
        /// Calculates a cosine of the fuzzy angle.
        /// </summary>
        /// <param name="X">A fuzzy number represents the angle in radians.</param>
        public static FuzzyNumber Cos(FuzzyNumber X)
        {
            return FuzzyNumber.Map(X, x => Cos(x));
        }

        /// <summary>
        /// Calculates a cosine of the interval.
        /// </summary>
        /// <param name="x">A fuzzy number represents the angle in radians.</param>
        public static Interval Cos(Interval x)
        {
            double a, b, extreme;
            extreme = Math.Floor(x.A / Math.PI) * Math.PI;

            if (x.Contains(extreme))
            {
                a = Math.Cos(extreme);
            }
            else
            {
                extreme += Math.PI;

                if (x.Contains(extreme))
                {
                    a = Math.Cos(extreme);
                }
                else
                {
                    a = Math.Cos(x.A);
                    b = Math.Cos(x.B);

                    return b > a ? new Interval(a, b) : new Interval(b, a);
                }
            }

            extreme += Math.PI;

            if (x.Contains(extreme))
            {
                b = Math.Cos(extreme);
            }
            else
            {
                b = a > 0
                    ? Math.Min(Math.Cos(x.A), Math.Cos(x.B))
                    : Math.Max(Math.Cos(x.A), Math.Cos(x.B));
            }

            return b > a ? new Interval(a, b, x.Epsilon) : new Interval(b, a, x.Epsilon);
        }

    }
}
