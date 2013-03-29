using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RobinMaben.GameOfLife
{
    [DebuggerDisplay("{X}, {Y}, {IsAlive}")]
    public class Cell
    {
        public Cell(int x, int y, bool isAlive = false)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
        }

        private GameBoard _board;
        public Cell AttachToBoard(GameBoard board)
        {
            _board = board;
            return this;
        }

        public readonly int X;
        public readonly int Y;
        
        public bool IsAlive { get; private set; }

        private IEnumerable<Cell> GetNeighbors()
        {
            return from cell in _board.Grid
                   where
                       (Math.Abs(X - cell.X) <= 1 && Math.Abs(Y - cell.Y) <= 1) && //Neighbors in the range |x - x1| <= 1, |y - y1| <= 1
                       cell != this
                   select new Cell(cell.X, cell.Y, cell.IsAlive);                  //Get cells detached from a board
        }

        private bool ComputeNextState()
        {
            var aliveNeighbors = GetNeighbors().Count(n => n.IsAlive);

            /*Rules taken from http://en.wikipedia.org/wiki/Conway's_Game_of_Life#Rules
             *  1.Any live cell with fewer than two live neighbours dies, as if caused by under-population.
                2.Any live cell with two or three live neighbours lives on to the next generation.
                3.Any live cell with more than three live neighbours dies, as if by overcrowding.
                4.Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
             */
            if (IsAlive)
            {
                if (aliveNeighbors < 2) return false;

                return aliveNeighbors == 2 || aliveNeighbors == 3;
            }

            return aliveNeighbors == 3;
        }

        public Cell GetNextGeneration()
        {
            return new Cell(X, Y, ComputeNextState()).AttachToBoard(_board);
        }
    }
}
