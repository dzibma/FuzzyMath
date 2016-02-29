using System;

namespace FuzzyMath
{
    public partial struct Interval
    {

        public static Interval operator +(Interval left, Interval right)
        {
            return new Interval(left.A + right.A, left.B + right.B, Math.Max(left.Epsilon, right.Epsilon));
        }

        public static Interval operator +(Interval left, double right)
        {
            return new Interval(left.A + right, left.B + right, left.Epsilon);
        }

        public static Interval operator +(double left, Interval right)
        {
            return right + left;
        }

        public static Interval operator -(Interval left, Interval right)
        {
            return new Interval(left.A - right.B, left.B - right.A, Math.Max(left.Epsilon, right.Epsilon));
        }

        public static Interval operator -(Interval left, double right)
        {
            return new Interval(left.A - right, left.B - right, left.Epsilon);
        }

        public static Interval operator -(double left, Interval right)
        {
            return new Interval(left - right.B, left - right.A, right.Epsilon);
        }

        public static Interval operator *(Interval left, Interval right)
        {
            var x1 = left.A * right.A;
            var x2 = left.A * right.B;
            var x3 = left.B * right.B;
            var x4 = left.B * right.A;

            return new Interval(
                    Math.Min(Math.Min(x1, x2), Math.Min(x3, x4)),
                    Math.Max(Math.Max(x1, x2), Math.Max(x3, x4)),
                    Math.Max(left.Epsilon, right.Epsilon)
                );
        }

        public static Interval operator *(Interval left, double right)
        {
            return left * new Interval(right, right);
        }

        public static Interval operator *(double left, Interval right)
        {
            return new Interval(left, left) * right;
        }

        public static Interval operator /(Interval left, Interval right)
        {
            if (right.Contains(0))
            {
                throw new InvalidOperationException("Can not divide by an interval containing zero");
            }

            return left * new Interval(1 / right.B, 1 / right.A, Math.Max(left.Epsilon, right.Epsilon));
        }

        public static Interval operator /(Interval left, double right)
        {
            return left / new Interval(right, right);
        }

        public static Interval operator /(double left, Interval right)
        {
            return new Interval(left, left) / right;
        }

        public static double operator >(Interval left, Interval right)
        {
            return left.GreaterThan(right);
        }

        public static double operator >(Interval left, double right)
        {
            return left > new Interval(right, right);
        }

        public static double operator >(double left, Interval right)
        {
            return new Interval(left, left, right.Epsilon) > right;
        }

        public static double operator <(Interval left, Interval right)
        {
            return right > left;
        }

        public static double operator <(Interval left, double right)
        {
            return right > left;
        }

        public static double operator <(double left, Interval right)
        {
            return right > left;
        }

    }
}
