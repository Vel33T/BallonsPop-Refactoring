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
        public void TestInputedPlayer()
        {
            Score scoresList = new Score();
            scoresList.AddPlayer("Gosho", 14);
            scoresList.AddPlayer("Pesho", 15);

            Assert.AreEqual("Gosho", scoresList.players[0].Name);
        }

        [TestMethod]
        public void TestAddingMoreThanFivePlayers()
        {
            Score scoresList = new Score();
            scoresList.AddPlayer("Gosho", 11);
            scoresList.AddPlayer("Pesho", 12);
            scoresList.AddPlayer("Peter", 13);
            scoresList.AddPlayer("Georgi", 13);
            scoresList.AddPlayer("Ivan", 18);

            scoresList.AddPlayer("Maria", 15);

            Assert.AreEqual("Maria", scoresList.players[4].Name);
        }

        [TestMethod]
        public void TestIsPlayerGoodEnoughLessThanFivePlayers()
        {
            Score scoresList = new Score();
            scoresList.AddPlayer("Gosho", 11);
            scoresList.AddPlayer("Pesho", 12);
            scoresList.AddPlayer("Peter", 13);
            scoresList.AddPlayer("Ivan", 18);

            bool result = scoresList.IsGoodEnough(21);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestIsPlayerGoodEnoughFivePlayersMorePoints()
        {
            Score scoresList = new Score();
            scoresList.AddPlayer("Gosho", 11);
            scoresList.AddPlayer("Pesho", 12);
            scoresList.AddPlayer("Peter", 13);
            scoresList.AddPlayer("Ivan", 18);
            scoresList.AddPlayer("Maria", 15);

            bool result = scoresList.IsGoodEnough(21);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestIsPlayerGoodEnoughFivePlayersLessPoints()
        {
            Score scoresList = new Score();
            scoresList.AddPlayer("Gosho", 11);
            scoresList.AddPlayer("Pesho", 12);
            scoresList.AddPlayer("Peter", 13);
            scoresList.AddPlayer("Ivan", 18);
            scoresList.AddPlayer("Maria", 15);

            bool result = scoresList.IsGoodEnough(12);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestSort()
        {
            Score scoresList = new Score();
            scoresList.AddPlayer("Pesho", 14);
            scoresList.AddPlayer("Peter", 13);
            scoresList.AddPlayer("Ivan", 18);
            scoresList.AddPlayer("Gosho", 11);
            scoresList.AddPlayer("Maria", 15);
            
            scoresList.Sort();

            Assert.AreEqual("Gosho", scoresList.players[0].Name);
        }

        [TestMethod]
        public void TestGetScoreBoard()
        {
            Score scoresList = new Score();
            scoresList.AddPlayer("Pesho", 14);
            string actual = scoresList.GetScoreBoard();
            string expected = "---------TOP FIVE SCORES-----------\n1.Pesho - 14\n-----------------------------------";

            Assert.AreEqual(expected, actual);
        }
    }
}
