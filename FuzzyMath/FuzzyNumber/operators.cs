using System;
using System.Linq;

namespace FuzzyMath
{
    public partial class FuzzyNumber
    {

        public static FuzzyNumber operator +(FuzzyNumber left, FuzzyNumber right)
        {
            return Map(left, right, (x, y) => x + y);
        }

        public static FuzzyNumber operator +(FuzzyNumber left, double right)
        {
            return Map(left, x => x + right);
        }

        public static FuzzyNumber operator +(double left, FuzzyNumber right)
        {
            return right + left;
        }

        public static FuzzyNumber operator -(FuzzyNumber left, FuzzyNumber right)
        {
            return Map(left, right, (x, y) => x - y);
        }

        public static FuzzyNumber operator -(FuzzyNumber left, double right)
        {
            return Map(left, x => x - right);
        }

        public static FuzzyNumber operator -(double left, FuzzyNumber right)
        {
            return Map(right, x => left - x);
        }

        public static FuzzyNumber operator *(FuzzyNumber left, FuzzyNumber right)
        {
            return Map(left, right, (x, y) => x * y);
        }

        public static FuzzyNumber operator *(FuzzyNumber left, double right)
        {
            return Map(left, x => x * right);
        }

        public static FuzzyNumber operator *(double left, FuzzyNumber right)
        {
            return right * left;
        }

        public static FuzzyNumber operator /(FuzzyNumber left, FuzzyNumber right)
        {
            return Map(left, right, (x, y) => x / y);
        }

        public static FuzzyNumber operator /(FuzzyNumber left, double right)
        {
            return Map(left, x => x / right);
        }

        public static FuzzyNumber operator /(double left, FuzzyNumber right)
        {
            return Map(right, x => left / x);
        }

        public static double operator >(FuzzyNumber left, FuzzyNumber right)
        {
            return left.GreaterThan(right);
        }

        public static double operator >(FuzzyNumber left, double right)
        {
            var cuts = Enumerable.Repeat(new Interval(right, right), left.AlphaCuts.Count);
            return left > new FuzzyNumber(cuts);
        }

        public static double operator >(double left, FuzzyNumber right)
        {
            var cuts = Enumerable.Repeat(new Interval(left, left), right.AlphaCuts.Count);
            return new FuzzyNumber(cuts) > right;
        }

        public static double operator <(FuzzyNumber left, FuzzyNumber right)
        {
            return right > left;
        }

        public static double operator <(FuzzyNumber left, double right)
        {
            return right > left;
        }

        public static double operator <(double left, FuzzyNumber right)
        {
            return right > left;
        }

    }
}
