using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaloonsPopGame;

namespace BaloonsPopTests
{
    [TestClass]
    public class TestScore
    {
        [TestMethod]
        public void Compare14To19Score()
        {
            Score pesho = new Score(14, "Pesho");
            Score gosho = new Score(19, "Gosho");
            int actualResult = pesho.CompareTo(gosho);

            Assert.AreEqual(-1, actualResult);
        }

        [TestMethod]
        public void CompareEqualScore()
        {
            Score pesho = new Score(15, "Pesho");
            Score gosho = new Score(15, "Gosho");
            int actualResult = pesho.CompareTo(gosho);

            Assert.AreEqual(0, actualResult);
        }

        [TestMethod]
        public void Compare18To12Score()
        {
            Score pesho = new Score(18, "Pesho");
            Score gosho = new Score(12, "Gosho");
            int actualResult = pesho.CompareTo(gosho);

            Assert.AreEqual(1, actualResult);
        }

        [TestMethod]
        public void TestToStringMethod ()
        {
            Score pesho = new Score(18, "Pesho");
            string peshoToString = pesho.ToString();

            Assert.AreEqual("Pesho with 18 moves.", peshoToString);
        }


    }
}
