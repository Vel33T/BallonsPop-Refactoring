using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaloonsPopGame;
using System.Collections.Generic;

namespace BaloonsPopTests
{
    [TestClass]
    public class ScoreTests
    {
        [TestMethod]
        public void TestAddPlayer()
        {
            Score scoresList = new Score();
            scoresList.AddPlayer("Gosho", 14);
            scoresList.AddPlayer("Pesho", 15);

            Assert.AreEqual(2, scoresList.players.Count);
        }

        [TestMethod]
        public void TestSort()
        {
            Score players = new Score();
            players.AddPlayer("Gosho", 14);
            players.AddPlayer("Pesho", 15);

            Assert.AreEqual(2, players.players[0].Name);
        }
    }
}
