using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobinMaben.GameOfLife;

namespace Tests
{
    [TestClass]
    public class UnitTests
    {
        /*Test Plan - 
         * Tests for finite steps
        /* 1. We can verify results by using 2 of the the 4 still states described here - http://en.wikipedia.org/wiki/Conway's_Game_of_Life#Examples_of_patterns
        /* 2. Also, we can test 1 known oscillation pattern at a given generation - 
         * 3. Finally, an arbitrary initial state that we know will change
         * */

        [TestMethod]
        public void RunAllTests()
        {
            TestStillStateBlock();
            TestStillStateBeeHive();
            
            TestOscillationStateBlinker();

            TestRandomStateChange();
        }
        
        [TestMethod]
        public void TestStillStateBlock()
        {
            var gameBoard = new GameBoard(new InitialState(File.ReadAllText("Block.csv")));
            var snapShot = gameBoard.Grid;
            gameBoard.Evolve();

            Assert.IsTrue(AreStatesEqual(snapShot, gameBoard.Grid));
        }

        [TestMethod]
        public void TestStillStateBeeHive()
        {
            var gameBoard = new GameBoard(new InitialState(File.ReadAllText("BeeHive.csv")));
            var snapShot = gameBoard.Grid;
            gameBoard.Evolve();

            Assert.IsTrue(AreStatesEqual(snapShot, gameBoard.Grid));
        }

        private static bool AreStatesEqual(IEnumerable<Cell> oldState, IEnumerable<Cell> newState)
        {
            //For every cell, check if its counterpart is in the same state
            return oldState.All(cell => newState.Single(c => c.X == cell.X && c.Y == cell.Y).IsAlive == cell.IsAlive);
        }

        [TestMethod]
        public void TestOscillationStateBlinker()
        {
            var blinker = new GameBoard(new InitialState(File.ReadAllText("Blinker.csv")));
            var snapShot1 = blinker.Grid;
            
            blinker.Evolve();
            Assert.IsFalse(AreStatesEqual(snapShot1, blinker.Grid)); //Assert that states have changed

            blinker.Evolve();
            Assert.IsTrue(AreStatesEqual(snapShot1, blinker.Grid)); //Assert that states have changed back to original position
        }

        [TestMethod]
        public void TestRandomStateChange()
        {
            var gameBoard = new GameBoard(new InitialState(File.ReadAllText("Arbitrary.csv")));
            var snapShot = gameBoard.Grid;
            gameBoard.Evolve();

            Assert.IsFalse(AreStatesEqual(snapShot, gameBoard.Grid));
        }

    }
}
