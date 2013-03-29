using System;
using System.IO;
using System.Threading;

namespace RobinMaben.GameOfLife
{
    static class Program
    {
        static void Main()
        {
            var gameBoard = new GameBoard(new InitialState(File.ReadAllText("Input.csv")));

            while (gameBoard.Generation < 100)
            {
                Console.Clear();
                Console.WriteLine("Genration " + gameBoard.Generation);

                gameBoard.Draw();
                Thread.Sleep(500);

                gameBoard.Evolve();
                
            }
        }
    }
}
