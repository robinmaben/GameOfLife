
using System.Collections.Generic;

namespace RobinMaben.GameOfLife
{
    interface IGameOfLife
    {
        List<Cell> Grid { get; }
        void Evolve();
    }
}
