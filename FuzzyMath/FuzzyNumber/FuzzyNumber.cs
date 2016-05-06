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

        /// <summary>
        /// List of alpha-cuts
        /// </summary>
        public ReadOnlyCollection<Interval> AlphaCuts;

        /// <summary>
        /// Creates a fuzzy number from list of intervals
        /// </summary>
        public FuzzyNumber(IEnumerable<Interval> intervals)
        {
            var alphaCuts = new List<Interval>();

            var maxA = double.NegativeInfinity;
            var minB = double.PositiveInfinity;

            foreach (var interval in intervals)
            {
                maxA = Math.Max(interval.A, maxA);
                minB = Math.Min(interval.B, minB);

                if (interval.Contains(maxA) && interval.Contains(minB))
                {
                    alphaCuts.Add(interval);
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            if (alphaCuts.Count > 0)
            {
                AlphaCuts = new ReadOnlyCollection<Interval>(alphaCuts);
            }
            else
            {
                throw new ArgumentException("Fuzzy number expects at least one alpha-cut");
            }
        }

        /// <summary>
        /// Alpha-cut at level of membership 1
        /// </summary>
        public Interval Kernel
        {
            get { return AlphaCuts.Last(); }
        }

        /// <summary>
        /// Alpha-cut at level of membership 0
        /// </summary>
        public Interval Support
        {
            get { return AlphaCuts.First(); }
        }

        /// <summary>
        /// Creates an alpha-cut at corresponding level of membership
        /// </summary>
        /// <param name="alpha">Degree of membership</param>
        /// <returns></returns>
        public Interval GetAlphaCut(double alpha)
        {
            if (alpha >= 0 && alpha <= 1)
            {
                var pos = alpha * (AlphaCuts.Count - 1);
                var upper = (int)Math.Ceiling(pos);
                var lower = (int)Math.Floor(pos);

                var a = AlphaCuts[lower].A == AlphaCuts[upper].A
                    ? AlphaCuts[lower].A
                    : AlphaCuts[upper].A - (upper - pos) * (AlphaCuts[upper].A - AlphaCuts[lower].A);

                var b = AlphaCuts[lower].B == AlphaCuts[upper].B
                    ? AlphaCuts[lower].B
                    : AlphaCuts[upper].B + (upper - pos) * (AlphaCuts[lower].B - AlphaCuts[upper].B);

                return new Interval(a, b);
            }

            throw new ArgumentException("Membership degree must be from the interval [0, 1]");
        }

        /// <summary>
        /// Calculates membership degree of 'x' to the fuzzy number
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>Membership degree from 0 to 1</returns>
        public double GetMembership(double x)
        {
            if (Kernel.Contains(x))
            {
                return 1;
            }

            if (Support.Contains(x)) 
            {
                for (var i = 1; i < AlphaCuts.Count; i++)
                {
                    if (AlphaCuts[i].Contains(x))
                    {
                        continue;
                    }

                    if (AlphaCuts[i].B < x)
                    {
                        return (i - 1 + (AlphaCuts[i - 1].B - x) / (AlphaCuts[i - 1].B - AlphaCuts[i].B)) / (AlphaCuts.Count - 1);
                    }
                    else
                    {
                        return (i - 1 + (x - AlphaCuts[i - 1].A) / (AlphaCuts[i].A - AlphaCuts[i - 1].A)) / (AlphaCuts.Count - 1);
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Concatenates alpha-cuts into a string
        /// </summary>
        public override string ToString()
        {
            return String.Format("[{0}]", String.Join(", ", AlphaCuts));
        }

        /// <summary>
        /// How much is greater than other fuzzy number?
        /// </summary>
        /// <param name="other"></param>
        /// <returns>A Presumption (0 - 1) that the fuzzy number is greater</returns>
        public double GreaterThan(FuzzyNumber other)
        {
            if (other.AlphaCuts.Count == AlphaCuts.Count)
            {               
                var sum = 0d;
                for (var i = 0; i < AlphaCuts.Count; i++)
                {
                    sum += AlphaCuts[i].GreaterThan(other.AlphaCuts[i]);
                }

                return sum / AlphaCuts.Count;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Maps an unary operation on a fuzzy number
        /// </summary>
        public static FuzzyNumber Map(FuzzyNumber X, Func<Interval, Interval> operation)
        {
            return new FuzzyNumber(X.AlphaCuts.Select(operation));
        }

        /// <summary>
        /// Maps a binary operation on two fuzzy numbers
        /// (Performs the operation over each pair of corresponding alpha-cuts)
        /// </summary>
        public static FuzzyNumber Map(FuzzyNumber X, FuzzyNumber Y, Func<Interval, Interval, Interval> operation)
        {
            if (X.AlphaCuts.Count == Y.AlphaCuts.Count)
            {
                return new FuzzyNumber(X.AlphaCuts.Zip(Y.AlphaCuts, operation));
                
            }

            throw new NotImplementedException();
        }

    }
}
