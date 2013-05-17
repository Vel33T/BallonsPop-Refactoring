using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaloonsPopGame;
using System.Collections.Generic;

namespace BaloonsPopTests
{
    [TestClass]
    public class TestScore
    {
        [TestMethod]
        public void Compare14To19Score()
        {
            Score players = new Score();
            players.AddPlayer("Gosho", 14);
            players.AddPlayer("Pesho", 15);

            string expected = "Gosho";
            Assert.AreEqual(2, players.);
        }
    }
}
