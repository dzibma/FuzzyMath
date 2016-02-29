using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzyMath.Tests
{
    [TestClass]
    public class IntervalTest
    {

        [TestMethod]
        public void Interval()
        {
            var epsilon = 1E-12;
            Interval x;

            x = new Interval(-1, 1);
            Assert.AreEqual(-1, x.A);
            Assert.AreEqual(1, x.B);

            x = new Interval(1 - 2 * epsilon, 1, epsilon);
            Assert.AreEqual(1 - 2 * epsilon, epsilon, x.A);
            Assert.AreEqual(1, x.B);

            x = new Interval(1 - epsilon / 2, 1, epsilon);
            Assert.AreEqual(x.A, x.B);

            x = new Interval(1, 1 + epsilon / 2, epsilon);
            Assert.AreEqual(x.A, x.B);

            x = new Interval(1, 1 + 2 * epsilon, epsilon);
            Assert.AreEqual(1, x.A);
            Assert.AreEqual(1 + 2 * epsilon, x.B);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidInterval()
        {
            Interval x = new Interval(-1, -1 - 1E-12);
        }

        [TestMethod]
        public void ContainsValue()
        {
            var epsilon = 1E-12;
            Interval x;

            x = new Interval();
            Assert.IsTrue(x.Contains(0));
            Assert.IsFalse(x.Contains(0 + 2 * epsilon));

            x = new Interval(-1, 1, epsilon);
            Assert.IsTrue(x.Contains(0));
            Assert.IsTrue(x.Contains(-1 - epsilon / 2));
            Assert.IsFalse(x.Contains(3));
            Assert.IsFalse(x.Contains(1 + 2 * epsilon));
        }

        [TestMethod]
        public void ContainsInterval()
        {
            var epsilon = 1E-3;

            Assert.IsTrue(
                (new Interval(-1, 1)).Contains(new Interval(-1, 1))
            );
            Assert.IsTrue(
                (new Interval(-1, 1, epsilon)).Contains(new Interval(-1 - epsilon / 2, 1 + epsilon / 2))
            );
            Assert.IsFalse(
                (new Interval(-1, 1)).Contains(new Interval(-1 - epsilon / 2, 1 + epsilon / 2, epsilon))
            );
            Assert.IsFalse(
                (new Interval(-1 + 2 * epsilon, 1, epsilon)).Contains(new Interval(-1, 1))
            );
        }

        [TestMethod]
        public void Intersects()
        {
            var epsilon = 1E-5;

            Assert.IsTrue(
                (new Interval(-1, 2)).Intersects(new Interval(-3, 3))
            );
            Assert.IsTrue(
                (new Interval(-3, 3)).Intersects(new Interval(-1, 2))
            );
            Assert.IsTrue(
                (new Interval(-1, 2)).Intersects(new Interval(1, 3))
            );
            Assert.IsTrue(
                (new Interval(-1, 2, epsilon)).Intersects(new Interval(-3, -1 - epsilon / 2))
            );
            Assert.IsTrue(
                (new Interval(-1, 2)).Intersects(new Interval(-3, -1 - epsilon / 2, epsilon))
            );
            Assert.IsFalse(
               (new Interval(-3, -1)).Intersects(new Interval(1, 3))
            );
        }

        [TestMethod]
        public void GreaterThan()
        {
            var epsilon = 1E-5;

            Assert.AreEqual(
                .25,
                new Interval(1, 3) > new Interval(2, 4)
            );
            Assert.AreEqual(
                1,
                (new Interval(1, 3) > new Interval(2, 4)) + (new Interval(1, 3) < new Interval(2, 4))
            );
            Assert.AreEqual(
                0,
                new Interval(1, 3) > new Interval(3, 4)
            );
            Assert.AreEqual(
                1,
                new Interval(3, 4) > new Interval(1, 3)
            );
            Assert.AreEqual(
                .5,
                new Interval(1, 3) < new Interval(2, 2)
            );
            Assert.AreEqual(
                1,
                (2 > new Interval(1, 3)) + (new Interval(1, 3) > 2)
            );
            Assert.AreEqual(
                .5,
                new Interval(0 - epsilon / 2, 0, epsilon) > new Interval(0, 0)
            );
            Assert.AreEqual(
                .5,
                new Interval(0, 0) > new Interval(0 - epsilon / 2, 0, epsilon)
            );
        }

        [TestMethod]
        public void Summation()
        {
            Assert.AreEqual(
                new Interval(1.1, 25),
                new Interval(.1, 23) + new Interval(1, 2)
            );
            Assert.AreEqual(
                new Interval(-22.5, 23.5),
                new Interval(-23, 23) + .5
            );
            Assert.AreEqual(
                    new Interval(-26.333, -23),
                    -23 + new Interval(-3.333, 0)
                );
        }

        [TestMethod]
        public void Substraction()
        {
            Assert.AreEqual(
                new Interval(-1.9, 22),
                new Interval(.1, 23) - new Interval(1, 2)
            );
            Assert.AreEqual(
                new Interval(-23.5, 22.5),
                new Interval(-23, 23) - .5
            );
            Assert.AreEqual(
                new Interval(-23, -19.667),
                -23 - new Interval(-3.333, 0)
            );
        }

        [TestMethod]
        public void Multiplication()
        {
            Assert.AreEqual(
                new Interval(1, 46),
                new Interval(1, 23) * new Interval(1, 2)
            );
            Assert.AreEqual(
                new Interval(-12, 12),
                2 * new Interval(-2, 2) * new Interval(2, 3)
            );
            Assert.AreEqual(
                new Interval(-6, 6),
                new Interval(-2, 2) * new Interval(2, 3) * -1
            );
            Assert.AreEqual(
                new Interval(-2, 1),
                new Interval(0, 1) * new Interval(-2, 1)
            );
        }

        [TestMethod]
        public void Division()
        {
            Assert.AreEqual(
                new Interval(0.5, 23),
                new Interval(1, 23) / new Interval(1, 2)
            );
            Assert.AreEqual(
                new Interval(-1, 1),
                new Interval(-2, 2) / new Interval(2, 3)
            );
            Assert.AreEqual(
                new Interval(1, 2),
                8 / new Interval(2, 4) / 2
            );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DivisionByZero()
        {
            Interval x;

            x = new Interval(3, 3) / new Interval(-1, 1);
        }

    }
}
