using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife_CR
{
    class Game
    {
        private bool[,] Grid { get; set; }
        private int Rows { get; }
        private int Cols { get; }

        public Game(string initialGridText)
        {
            this.Grid = MakeGrid(initialGridText);
            this.Rows = this.Grid.GetLength(0);
            this.Cols = this.Grid.GetLength(1);
        }

        public void NextGeneration()
        {

            Func<int, int, bool> getCellStatus =
                (row, col) =>
                {
                    int aliveNeighbours = GetNeighbours(row, col)
                                            .Where(x => x)
                                            .Count();

                    // If it's alive, it dies if it doesn't have 2 or 3 neighbours
                    if (this.Grid[row, col])
                        return aliveNeighbours > 1 && aliveNeighbours < 4;

                    // If it's dead, it turns alive if it has 3 neighbours
                    return aliveNeighbours == 3;
                };

            bool[,] newGrid = new bool[this.Rows, this.Cols];

            loopIndex(this.Rows, rowIndex =>
                loopIndex(this.Cols, colIndex =>
                    newGrid[rowIndex, colIndex] = getCellStatus(rowIndex, colIndex)));

            this.Grid = newGrid;
        }

        public bool[] GetNeighbours(int row, int col)
        {
            // Left upper corner
            if (col == 0 && row == 0)
                return new bool[3] {
                    this.Grid[0, 1],
                    this.Grid[1, 0],
                    this.Grid[1, 1]
                };

            // Left bottom corner
            if (col == 0 && row == this.Rows - 1)
                return new bool[3] {
                    this.Grid[row, 1],
                    this.Grid[row - 1, 0],
                    this.Grid[row - 1, 1]
                };

            // Right upper corner
            if (col == this.Cols - 1 && row == 0)
                return new bool[3] {
                    this.Grid[row, col - 1],
                    this.Grid[row + 1, col],
                    this.Grid[row + 1, col - 1]
                };

            // Right bottom corner
            if (col == this.Cols - 1 && row == this.Rows - 1)
                return new bool[3] {
                    this.Grid[row - 1, col],
                    this.Grid[row, col - 1],
                    this.Grid[row - 1, col - 1]
                };

            // Left side
            if (col == 0)
                return new bool[5] {
                    this.Grid[row - 1, col],
                    this.Grid[row + 1, col],
                    this.Grid[row, col + 1],
                    this.Grid[row - 1, col + 1],
                    this.Grid[row + 1, col + 1]
                };

            // Right side
            if (col == this.Cols - 1)
                return new bool[5] {
                    this.Grid[row - 1, col],
                    this.Grid[row + 1, col],
                    this.Grid[row, col - 1],
                    this.Grid[row + 1, col - 1],
                    this.Grid[row - 1, col - 1]
                };

            // Top side
            if (row == 0)
                return new bool[5] {
                    this.Grid[row + 1, col],
                    this.Grid[row, col - 1],
                    this.Grid[row, col + 1],
                    this.Grid[row + 1, col - 1],
                    this.Grid[row + 1, col + 1]
                };

            // Down side
            if (row == this.Rows - 1)
                return new bool[5] {
                    this.Grid[row - 1, col],
                    this.Grid[row, col - 1],
                    this.Grid[row, col + 1],
                    this.Grid[row - 1, col + 1],
                    this.Grid[row - 1, col - 1]
                };

            // Middle
            return new bool[8] {
                this.Grid[row - 1, col],
                this.Grid[row + 1, col],
                this.Grid[row, col - 1],
                this.Grid[row, col + 1],
                this.Grid[row + 1, col + 1],
                this.Grid[row + 1, col - 1],
                this.Grid[row - 1, col + 1],
                this.Grid[row - 1, col - 1]
            };
        }

        public string ShowGrid()
        {
            string grid = "";

            loopIndex(this.Rows, rowIndex =>
            {
                loopIndex(this.Cols, colIndex =>
                {
                    // We use 2 characters because console shows us
                    // twice as tall as they are wide
                    grid += this.Grid[rowIndex, colIndex] ? "XX" : "..";
                });

                grid += '\n';
            });

            return grid;
        }

        private static bool[,] MakeGrid(string gridText)
        {
            string[] rowsText = gridText.Split('\n');


            int rows = rowsText.Length;
            int cols = rowsText[0].Length;

            bool[,] newGrid = new bool[rows, cols];

            loopIndex(rows, rowIndex =>
                loopIndex(cols, colIndex =>
                    newGrid[rowIndex, colIndex] =
                        rowsText[rowIndex][colIndex] != '.'));

            return newGrid;
        }

        private static void loopIndex(int slots, Action<int> map) =>
            Enumerable.Range(0, slots).ToList().ForEach(map);

    }
}
