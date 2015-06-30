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

    }
}
