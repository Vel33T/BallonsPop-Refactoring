using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaloonsPopGame;

namespace BaloonsPopTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void PlayerConstructorTest()
        {
            Player georgi = new Player("Georgi", 14);
            Assert.AreEqual("Georgi", georgi.Name);
        }

        [TestMethod]
        public void PlayerConstructorTest2()
        {
            Player georgi = new Player("Georgi", 14);
            Assert.AreEqual(14, georgi.Points);
        }

        [TestMethod]
        public void PlayerConstructorTest3()
        {
            Player georgi = new Player("Georgi", 14);
            bool isInstanceOfPlayer=false;
            if (georgi.GetType() == typeof(Player))
            {
                isInstanceOfPlayer = true;
            }
            Assert.IsTrue(isInstanceOfPlayer);
        }
       
    }
}
