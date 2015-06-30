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
        private int pieces = 11;

        /// <summary>
        /// Creates a factory of piecewise-linear fuzzy numbers
        /// </summary>
        /// <param name="pieces">Nubmer of alpha-cuts</param>
        public FuzzyNumberFactory(int pieces)
        {
            this.pieces = pieces > 1 ? pieces : 2;
        }

        /// <summary>
        /// Creates a factory of piecewise-linear fuzzy numbers using 11 alpha-cuts
        /// </summary>
        public FuzzyNumberFactory() { }

        /// <summary>
        /// Creates a trapezoidal fuzzy number
        /// </summary>
        /// <param name="a">Support lower bound</param>
        /// <param name="b">Kernel lower bound</param>
        /// <param name="c">Kernel upper bound</param>
        /// <param name="d">Support upper bound</param>
        /// <returns></returns>
        public FuzzyNumber Create(double a, double b, double c, double d)
        {
            var intervals = new Interval[pieces];

            for (var i = 0; i < intervals.Length; i++)
            {
                intervals[i] = new Interval(
                        a + (b - a) * i / (pieces - 1),
                        d - (d - c) * i / (pieces - 1)
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
        public FuzzyNumber Create(double a, double b, double c)
        {
            return Create(a, b, b, c);
        }

        /// <summary>
        /// Creates a fuzzy number representing a crisp value
        /// </summary>
        public FuzzyNumber Create(double a)
        {
            return Create(a, a, a);
        }

    }
}
