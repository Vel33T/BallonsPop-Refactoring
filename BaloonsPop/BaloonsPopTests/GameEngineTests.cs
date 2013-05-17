using System;
using BaloonsPopGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaloonsPopTests
{
    [TestClass]
    public class GameEngineTests
    {
        private GameEngine engineEasy = new GameEngine("easy");
        private GameEngine engineMedium = new GameEngine("medium");
        private GameEngine engineHard = new GameEngine("hard");
        private byte[,] matrix = new byte[4, 5] { 
            { 1, 1, 1, 1, 1 }, 
            { 2, 2, 2, 2, 2 },
            { 3, 3, 3, 3, 3 }, 
            { 4, 4, 4, 4, 4 } };

        [TestMethod]
        public void TestGetMatrixImage()
        {
            engineEasy.Matrix = matrix;
            string expected = "    0 1 2 3 4 \n   -----------\n0 | 1 1 1 1 1 | \n1 | 2 2 2 2 2 | \n2 | 3 3 3 3 3 | \n3 | 4 4 4 4 4 | \n   -----------\n";
            Assert.AreEqual(expected, engineEasy.GetMatrixImage());
        }

        [TestMethod]
        public void TestInputRestart()
        {
            engineEasy.ProcessGame("RESTART");
            engineEasy.Matrix = matrix;
            string expected = "    0 1 2 3 4 \n   -----------\n0 | 1 1 1 1 1 | \n1 | 2 2 2 2 2 | \n2 | 3 3 3 3 3 | \n3 | 4 4 4 4 4 | \n   -----------\n";
            Assert.AreEqual(expected, engineEasy.GetMatrixImage());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException),"There is no such field! Try again!")]
        public void TestInputIncorrect()
        {
            engineEasy.ProcessGame("alskjdhjas");
        }

        [TestMethod]
        public void TestDropDownMatrix()
        {
            engineEasy.Matrix = matrix;
            engineEasy.ProcessGame("3 3");
            string expected = "    0 1 2 3 4 \n   -----------\n0 |           | \n1 | 1 1 1 1 1 | \n2 | 2 2 2 2 2 | \n3 | 3 3 3 3 3 | \n   -----------\n";
            Assert.AreEqual(expected, engineEasy.GetMatrixImage());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This baloon is popped!")]
        public void TestIsPopped()
        {
            engineEasy.Matrix = matrix;
            engineEasy.ProcessGame("3 3");
            engineEasy.ProcessGame("0 0");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "There is no such field! Try again!")]
        public void TestInvalidField()
        {
            engineEasy.Matrix = matrix;
            engineEasy.ProcessGame("3 10");
        }
    }
}
