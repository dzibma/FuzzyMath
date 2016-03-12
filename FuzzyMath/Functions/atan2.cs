using System;

namespace FuzzyMath
{
    public static partial class Functions
    {

        public static FuzzyNumber Atan2(FuzzyNumber Y, FuzzyNumber X)
        {
            var rotated = Y.Support.Contains(0) && !X.Support.Contains(0);

            return FuzzyNumber.Map(Y, X, (y, x) => Atan2(y, x, rotated));
        }

        private static double Atan2Rotated(double y, double x)
        {
            if (x < 0 && y >= 0)
            {
                return Math.Atan(y / x) - Math.PI;
            }

            return Math.Atan2(y, x);
        }

        private static Interval Atan2(Interval y, Interval x, bool rotated)
        {
            double a1, a2, a3, a4;
            var epsilon = Math.Max(y.Epsilon, x.Epsilon);

            if (rotated)
            {
                if (y.Contains(0) && x.Contains(0))
                {
                    return new Interval(-1.5 * Math.PI, 0.5 * Math.PI, epsilon);
                }

                a1 = Atan2Rotated(y.A, x.A);
                a2 = Atan2Rotated(y.A, x.B);
                a3 = Atan2Rotated(y.B, x.B);
                a4 = Atan2Rotated(y.B, x.A);
            }
            else
            {
                if (y.Contains(0) && x.Contains(0))
                {
                    return new Interval(-Math.PI, Math.PI, epsilon);
                }
                    
                a1 = Math.Atan2(y.A, x.A);
                a2 = Math.Atan2(y.A, x.B);
                a3 = Math.Atan2(y.B, x.B);
                a4 = Math.Atan2(y.B, x.A);
            }

            return new Interval(
                    Math.Min(Math.Min(a1, a2), Math.Min(a3, a4)),
                    Math.Max(Math.Max(a1, a2), Math.Max(a3, a4)),
                    epsilon
                );
        }

    }
}
