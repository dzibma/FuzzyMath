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
            double tmp, a, b;

            a = b = left.A * right.A;

            tmp = left.B * right.B;
            if (tmp < a)
            {
                a = tmp;
            }
            else
            {
                b = tmp;
            }

            tmp = left.A * right.B;
            if (tmp < a)
            {
                a = tmp;
            }
            else if (tmp > b)
            {
                b = tmp;
            }

            tmp = left.B * right.A;
            if (tmp < a)
            {
                a = tmp;
            }
            else if (tmp > b)
            {
                b = tmp;
            }

            return new Interval(a, b, Math.Max(left.Epsilon, right.Epsilon));
        }

        public static Interval operator *(Interval left, double right)
        {
            return left * new Interval(right, right, left.Epsilon);
        }

        public static Interval operator *(double left, Interval right)
        {
            return new Interval(left, left, right.Epsilon) * right;
        }

        public static Interval operator /(Interval left, Interval right)
        {
            if (right.Contains(0))
            {
                throw new InvalidOperationException("Can not divide by an interval containing zero");
            }

            return left * new Interval(1 / right.B, 1 / right.A, Math.Max(left.Epsilon, right.Epsilon)); // 1/[a, b] = [1/b, 1/a]
        }

        public static Interval operator /(Interval left, double right)
        {
            return left / new Interval(right, right, left.Epsilon);
        }

        public static Interval operator /(double left, Interval right)
        {
            return new Interval(left, left, right.Epsilon) / right;
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
            return new Interval(left, left) > right;
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
