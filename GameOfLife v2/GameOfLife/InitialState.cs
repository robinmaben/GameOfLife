using System;
using System.Collections.Generic;

namespace RobinMaben.GameOfLife
{
    public class InitialState
    {
        public readonly List<Cell> AliveCells = new List<Cell>();
        public readonly int InitialDimension;

        public InitialState(string csvMatrix)
        {
            /*Rows are separated by "\n" and Columns are separated by ","
             * Eg:
             *      0,0,0,0
             *      0,1,0,0
             *      0,1,1,0
             *      0,0,0,0
             * */

            var rows = csvMatrix.Split('\n');

            for (var i = 0; i < rows.Length; i++)
            {
                var values = rows[i].Split(',');
                //if (values.Length != rows.Length) throw new InvalidOperationException("The Configuration is invalid");
                
                for (var j = 0; j < values.Length; j++)
                {
                    if (values[j] == "1")
                    {
                        AliveCells.Add(new Cell(i + 1, j + 1, true));
                    }
                }
            }

            InitialDimension = Math.Max(rows.Length, 8);
        }
    }
}
