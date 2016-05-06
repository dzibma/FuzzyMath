namespace FuzzyMath
{
    /// <summary>
    /// Piecewise-linear fuzzy numbers factory
    /// </summary>
    public class FuzzyNumberFactory
    {

        /// <summary>
        /// Number of alpha-cuts 
        /// </summary>
        public int NumberOfAlphaCuts
        {
            get;
            private set;
        }

        /// <summary>
        /// Tolerance in equality comparison 
        /// </summary>
        public double Epsilon
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a factory of piecewise-linear fuzzy numbers
        /// </summary>
        /// <param name="pieces">Nubmer of alpha-cuts</param>
        /// <param name="epsilon">Tolerance in equality comparison</param>
        public FuzzyNumberFactory(int numberOfAlphaCuts = 11, double epsilon = 1E-6)
        {
            NumberOfAlphaCuts = numberOfAlphaCuts > 1 ? numberOfAlphaCuts : 2;
            Epsilon = epsilon;
        }

        /// <summary>
        /// Creates a trapezoidal fuzzy number
        /// </summary>
        /// <param name="a">Support lower bound</param>
        /// <param name="b">Kernel lower bound</param>
        /// <param name="c">Kernel upper bound</param>
        /// <param name="d">Support upper bound</param>
        /// <returns></returns>
        public FuzzyNumber CreateTrapezoidal(double a, double b, double c, double d)
        {
            var intervals = new Interval[NumberOfAlphaCuts];

            for (var i = 0; i < NumberOfAlphaCuts; i++)
            {
                intervals[i] = new Interval(
                        a + (b - a) * i / (NumberOfAlphaCuts - 1),
                        d - (d - c) * i / (NumberOfAlphaCuts - 1),
                        Epsilon
                    );
            }

            return new FuzzyNumber(intervals);
        }

        /// <summary>
        /// Creates a triangular fuzzy number (TFN)
        /// </summary>
        /// <param name="a">Support lower bound</param>
        /// <param name="b">Kernel</param>
        /// <param name="c">Support upper bound</param>
        /// <returns></returns>
        public FuzzyNumber CreateTriangular(double a, double b, double c)
        {
            return CreateTrapezoidal(a, b, b, c);
        }

        /// <summary>
        /// Creates a fuzzy number representing a crisp value
        /// </summary>
        public FuzzyNumber CreateCrisp(double a)
        {
            return CreateTrapezoidal(a, a, a, a);
        }

    }
}
