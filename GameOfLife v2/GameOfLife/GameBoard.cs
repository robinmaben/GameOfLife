using System;
using System.Collections.Generic;
using System.Linq;

namespace RobinMaben.GameOfLife
{
    public class GameBoard : IGameOfLife
    {
        private GameBoard()
        {
            Generation = 0;
            Grid = new List<Cell>();
        }

        public GameBoard(InitialState initialState)
            : this()
        {
            InitBoard(initialState);
        }

        public List<Cell> Grid { get; private set; }
        public long Generation { get; private set; }

        private void InitBoard(InitialState initialState)
        {
            if (initialState == null) throw new ArgumentNullException("initialState");

            var seed = initialState.AliveCells;

            for (var i = 1; i < initialState.InitialDimension + 1; i++)
                for (var j = 1; j < initialState.InitialDimension + 1; j++)
                {
                    var isAlive = seed.Any(cell => cell.X == j && cell.Y == i);
                    Grid.Add(new Cell(i, j, isAlive).AttachToBoard(this));
                }
        }

        public void Evolve()
        {
            var newGrid = Grid.Select(cell => cell.GetNextGeneration()).ToList();
            Grid = newGrid;

            Generation++;
        }

        public void Draw()
        {
            foreach (var cell in Grid.Where(cell => cell.IsAlive))
            {
                Console.CursorTop = cell.Y + 10;
                Console.CursorLeft = cell.X + 10;
                Console.Write("■");  //Using the Alt + 254 symbol to show an alive cell
            }
        }
    }
}
