using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLibrary
{
	
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
		void CalcNextStates()
		{
			for(var row = 0; row < Length; row++ )
			{
				for(var col = 0; col < Length; col++)
				{
					var cell = Data[row, col];
					var neighbourStates = (new NeighbourStates(this, cell)).GetNeighbourStates();

					cell.GetNextState(neighbourStates);
				}
			}
		}

		void UpdateStates()
		{
			for (var row = 0; row < Length; row++)
			{
				for (var col = 0; col < Length; col++)
				{
					var cell = Data[row, col];
					cell.UpdateState();
				}
			}
		}

		public void SetInitialCells(Point[] initialCells)
		{
			foreach(var point in initialCells)
			{
				Data[point.Y, point.X].State = States.ALIVE;

			}
		}

		public void Cycle()
		{
			Cycles += 1;
			Console.WriteLine($"Performing Cycle {Cycles}");
			CalcNextStates();
			UpdateStates();
			Display();

		}

		char StatesToChar(States state)
		{
			if (state == States.ALIVE)
			{
				return 'X';
			} else
			{
				return 'o';
			}
		}
		public void Display()
		{
			for (int row = 0; row < Data.GetLength(0); row++)
			{
				StringBuilder line = new StringBuilder();
				line.Append($"  [");

				for (int col = 0; col < Data.GetLength(1); col++)
				{
					char letter = StatesToChar(Data[row, col].State);
					line.Append($" {letter}");
				}
				line.Append($" ]   row:{row}");
				Console.WriteLine(line);
			}
			Console.WriteLine("\n");
		}
	}
}
