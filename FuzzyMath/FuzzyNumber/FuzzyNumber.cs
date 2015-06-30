using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FuzzyMath
{
    /// <summary>
    /// Implementation of piecewise-linear fuzzy number
    /// </summary>
    public partial class FuzzyNumber
    {

        private Interval[] alphaCuts;

        /// <summary>
        /// Creates a Fuzzy number from a list of confidence intervals
        /// </summary>
        public FuzzyNumber(IEnumerable<Interval> alphaCuts)
        {
            this.alphaCuts = alphaCuts.ToArray();

            for (var i = 1; i < this.alphaCuts.Length; i++)
            {
                if (!this.alphaCuts[i - 1].Contains(this.alphaCuts[i]))
                {
                    throw new ArgumentException("Fuzzy number expects an ordered list of alpha-cuts where (n-1)th cut contains (n)th");
                }
            }

            if (this.alphaCuts.Length == 0)
            {
                throw new ArgumentException("Fuzzy number expects at least one alpha-cut");
            }
        }

        /// <summary>
        /// List of alpha-cuts
        /// </summary>
        public ReadOnlyCollection<Interval> AlphaCuts
        {
            get { return Array.AsReadOnly(alphaCuts); }
        }

        /// <summary>
        /// Alpha-cut at level of membership 1
        /// </summary>
        public Interval Kernel
        {
            get { return alphaCuts[alphaCuts.Length - 1]; }
        }

        /// <summary>
        /// Alpha-cut at level of membership 0
        /// </summary>
        public Interval Support
        {
            get { return alphaCuts[0]; }
        }

        /// <summary>
        /// Creates an alpha-cut at corresponding level of membership
        /// </summary>
        /// <param name="alpha">Value from the interval [0, 1]</param>
        public Interval GetAlphaCut(double membership)
        {
            if (membership > 1 || membership < 0)
            {
                throw new ArgumentException("Membership degree must be from the interval [0, 1]");
            }

            var i = membership * (alphaCuts.Length - 1);
            if (i % 1 == 0)
            {
                return alphaCuts[(int)i];
            }

            var upper = (int)Math.Ceiling(i);
            var lower = (int)Math.Floor(i);

            var a = alphaCuts[lower].A == alphaCuts[upper].A
                ? alphaCuts[lower].A
                : alphaCuts[upper].A - (upper - i) * (alphaCuts[upper].A - alphaCuts[lower].A);

            var b = alphaCuts[lower].B == alphaCuts[upper].B
                ? alphaCuts[lower].A
                : alphaCuts[upper].B + (upper - i) * (alphaCuts[lower].B - alphaCuts[upper].B);

            return new Interval(a, b);
        }

        /// <summary>
        /// Gets membership degree of a given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Value from the interval [0, 1]</returns>
        public double GetMembership(double value)
        {
            if (Kernel.Contains(value))
            {
                return 1;
            }
            
            if (Support.Contains(value)) 
            {
                for (var i = 1; i < alphaCuts.Length; i++)
                {
                    if (!alphaCuts[i].Contains(value))
                    {
                        if (alphaCuts[i].B < value)
                        {
                            return (i - 1 + (alphaCuts[i - 1].B - value) / (alphaCuts[i - 1].B - alphaCuts[i].B)) / (alphaCuts.Length - 1);
                        }
                        else
                        {
                            return (i - 1 + (value - alphaCuts[i - 1].A) / (alphaCuts[i].A - alphaCuts[i - 1].A)) / (alphaCuts.Length - 1);
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Concatenates alpha-cuts of the Fuzzy Number into a string
        /// </summary>
        public override string ToString()
        {
            return String.Format("[{0}]", String.Join(", ", alphaCuts));
        }

        /// <summary>
        /// Maps an unary operation on a fuzzy number
        /// </summary>
        public static FuzzyNumber Map(FuzzyNumber number, Func<Interval, Interval> operation)
        {
            return new FuzzyNumber(number.AlphaCuts.Select(operation));
        }

        /// <summary>
        /// Maps a binary operation on two fuzzy numbers
        /// (Performs the operation over each pair of corresponding alpha-cuts)
        /// </summary>
        public static FuzzyNumber Map(FuzzyNumber left, FuzzyNumber right, Func<Interval, Interval, Interval> operation)
        {
            if (left.AlphaCuts.Count != right.AlphaCuts.Count)
            {
                throw new NotImplementedException();
            }

            return new FuzzyNumber(left.AlphaCuts.Zip(right.AlphaCuts, operation));
        }

    }
}
