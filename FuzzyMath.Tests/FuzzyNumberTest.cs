using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzyMath.Tests
{
    [TestClass]
    public class FuzzyNumberTest
    {

        [TestMethod]
        public void GetAlphaCut()
        {
            var fn = new FuzzyNumberFactory(11);

            Assert.AreEqual(
                    new Interval(2, 2),
                    fn.CreateTriangular(1, 2, 3).GetAlphaCut(1)
                );

            Assert.AreEqual(
                    new Interval(1.5, 2.5),
                    fn.CreateTriangular(1, 2, 3).GetAlphaCut(0.5)
                );

            Assert.AreEqual(
                    new Interval(1.6, 2.8),
                    fn.CreateTriangular(0, 2, 6).GetAlphaCut(0.8)
                );
        }

        [TestMethod]
        public void GetMembership()
        {
            var fn = new FuzzyNumberFactory();

            Assert.AreEqual(
                    0,
                    fn.CreateTriangular(1, 2, 10).GetMembership(1)
                );

            Assert.AreEqual(
                    0.25,
                    fn.CreateTriangular(1, 2, 10).GetMembership(8)
                );

            Assert.AreEqual(
                    1,
                    fn.CreateTriangular(1, 2, 10).GetMembership(2)
                );

            Assert.AreEqual(
                    0.1,
                    fn.CreateTriangular(1, 2, 3).GetMembership(1.1)
                );

            Assert.AreEqual(
                    0.8,
                    fn.CreateTriangular(1, 2, 3).GetMembership(1.8)
                );
        }

        [TestMethod]
        public void MapUnary()
        {
            var fn = new FuzzyNumberFactory();

            Assert.AreEqual(
                    fn.CreateTriangular(-3, -2, -1).ToString(),
                    FuzzyNumber.Map(fn.CreateTriangular(.1, .2, .3), x => x * -10).ToString()
                );
        }

        [TestMethod]
        public void MapBinary()
        {
            var fn = new FuzzyNumberFactory();

            Assert.AreEqual(
                    fn.CreateTriangular(1, 3, 5).ToString(),
                    FuzzyNumber.Map(fn.CreateTriangular(0, 1, 2), fn.CreateTriangular(1, 2, 3), (x, y) => x + y).ToString()
                );
        }

    }
}
