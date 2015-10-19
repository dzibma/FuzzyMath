using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzyMath.Tests
{
    [TestClass]
    public class IntervalTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidIntervalCreation()
        {
            new Interval(-1, -2);
        }

        [TestMethod]
        public void Contains()
        {
            var x = new Interval(-2, 0);
            var y = new Interval(-5, 5);

            Assert.IsTrue(y.Contains(x));
            Assert.IsTrue(x.Contains(0));
            Assert.IsFalse(x.Contains(y));
            Assert.IsFalse(x.Contains(.1));
        }

        [TestMethod]
        public void GreaterThan()
        {
            var x = new Interval(1, 3);
            var y = new Interval(2, 4);
            var z = new Interval(3, 4);
            var w = new Interval(2);

            Assert.AreEqual(.25, Interval.GreaterThan(x, y));
            Assert.AreEqual(1, Interval.GreaterThan(x, y) + Interval.GreaterThan(y, x));
            Assert.AreEqual(0, Interval.GreaterThan(x, z));
            Assert.AreEqual(1, Interval.GreaterThan(z, x));
            Assert.AreEqual(.5, Interval.GreaterThan(x, w));
        }

        [TestMethod]
        public void Intersects()
        {
            var x = new Interval(-2, 0);
            var y = new Interval(-5, 5);
            var z = new Interval(4, 10);

            Assert.IsTrue(y.Intersects(x));
            Assert.IsTrue(x.Intersects(y));
            Assert.IsTrue(y.Intersects(z));
            Assert.IsFalse(x.Intersects(z));
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
            var blackHole = new Interval(3) / new Interval(-1, 1);
        }

    }
}
