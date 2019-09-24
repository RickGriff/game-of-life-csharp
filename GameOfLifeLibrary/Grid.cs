using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLibrary
{
	// arrays-of-arrays vs matrices:
	// grid should prob be a matrix
	//  coordinates should prob be array-of-points.
	public class Grid

	{
		public int Length { get; private set; }
		public Cell[,] Data { get; private set; }
		public int Cycles { get; private set; }

		public Grid(int length)
		{
			Length = length;
			Cycles = 0;
			Data = new Cell[length, length]; // 2d array of cells

			// populate grid
			for (var row = 0; row < Length; row++)
			{
				for (var col = 0; col < Length; col++)
				{
					Data[row, col] = new Cell(row, col);
				}
			}
		}
		void calcNextStates()
		{
			for(var row = 0; row < Data.Length; row++ )
			{
				for(var col = 0; col < Data.Length; col++)
				{
					var cell = Data[row, col];
					var neighbourStates = (new NeighbourStates(this, cell)).GetNeighbourStates();

					cell.GetNextState(neighbourStates);
				}
			}
		}

		public void SetInitialCells(Point[] initialCells)
		{
		
		}
		void Cycle()
		{

		}

		public void Display()
		{
			var gridValues = new int[,] {
				{ 1, 2, 3, 4, 5 },
				{ 1, 2, 3, 4, 5 },
				{ 1, 2, 3, 4, 5 },
				{ 1, 2, 3, 4, 5 },
				{ 1, 2, 3, 4, 5 },
			};

			for (int row = 0; row < gridValues.GetLength(0); row++)
			{
				StringBuilder line = new StringBuilder();
				line.Append($" {row + 1}. [");
				for (int col = 0; col < gridValues.GetLength(1); col++)
				{
					line.Append($" {gridValues[row, col]}");
				}
				line.Append(" ]");
				Console.WriteLine(line);
			}
		}
	}
}
