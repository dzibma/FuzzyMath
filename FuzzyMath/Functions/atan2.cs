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
            double[] values;

            if (rotated)
            {
                if (y.Contains(0) && x.Contains(0))
                    return new Interval(-1.5 * Math.PI, 0.5 * Math.PI);

                values = new double[4] {
                    Atan2Rotated(y.A, x.A),
                    Atan2Rotated(y.A, x.B),
                    Atan2Rotated(y.B, x.A),
                    Atan2Rotated(y.B, x.B)
                };
            }
            else
            {
                if (y.Contains(0) && x.Contains(0))
                    return new Interval(-Math.PI, Math.PI);

                values = new double[4] {
                    Math.Atan2(y.A, x.A),
                    Math.Atan2(y.A, x.B),
                    Math.Atan2(y.B, x.A),
                    Math.Atan2(y.B, x.B)
                };
            }

            Array.Sort(values);

            return new Interval(values[0], values[3]);
        }

    }
}
