using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameOfLifeLibrary
{
	public enum GameType { CONWAY, LARGEST_NEIGHBOUR, CYCLIC_EATING };

	public class Grid
	{
		public int Length { get; internal set; }
		public Cell[,] Data { get; internal set; }
		public int Cycles { get; internal set; }
		public List<State> PossibleStates { get; internal set; }
		
		public Grid(int length)
		{
			Length = length;
			Cycles = 0;
			Data = new Cell[length, length];
			PossibleStates = new List<State> { State.DEAD, State.ALIVE };

			// populate grid
			for (var row = 0; row < Length; row++)
			{
				for (var col = 0; col < Length; col++)
				{
					Data[row, col] = new Cell(row, col);
				}
			}
		}

		public void Clear()
		{
			foreach(var cell in Data)
			{
				cell.CurrentState = State.DEAD;
			}

			Cycles = 0;
		}
		void CalcNextStates()
		{
			for (var row = 0; row < Length; row++)
			{
				for (var col = 0; col < Length; col++)
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

		public void SetInitialCells(Dictionary<Point, State> initialCells)
		{
			foreach (var pair in initialCells)
			{
				var point = pair.Key;
				var state = pair.Value;

				if (!PossibleStates.Contains(state))
				{
					throw new ArgumentException("state is not valid for this grid");
				}

				Data[point.Y, point.X].CurrentState = state;
			}
		}

		public void SetRandomInitialCells()
		{
			var rand = new Random();
			int numStates = PossibleStates.Count;
			foreach (var cell in Data)
			{
				cell.CurrentState = PossibleStates[rand.Next(0, numStates)];
			}
		}

		public void Cycle()
		{
			Cycles += 1;
			LogCycle();
			CalcNextStates();
			UpdateStates();
			//LogCycleData();
		}

		void LogCycle()
		{
			Console.WriteLine($"Performing Cycle {Cycles}");
		}
		public void LogCycleData()
		{
			// Displays cell states in the console
			for (int row = 0; row < Data.GetLength(0); row++)
			{
				StringBuilder line = new StringBuilder();
				line.Append($"  [");

				for (int col = 0; col < Data.GetLength(1); col++)
				{
					char letter = Data[row, col].StateChar;
					line.Append($" {letter}");
				}
				line.Append($" ]   row:{row}");
				Console.WriteLine(line);
			}
			Console.WriteLine("\n");
		}
	}

	public class RGBGrid : Grid
	{
		public bool IsCyclic { get; private set; }
		public RGBGrid(int length, bool isCyclic) : base(length)
		{
			IsCyclic = isCyclic;
			Length = length;
			Cycles = 0;
			Data = new RGBCell[length, length]; // 2d array of cells
			PossibleStates = new List<State> { State.DEAD, State.RED, State.GREEN, State.BLUE };

			// populate grid
			for (var row = 0; row < Length; row++)
			{
				for (var col = 0; col < Length; col++)
				{
					Data[row, col] = NewCellInstance(row, col);
				}
			}
		}

		private Cell NewCellInstance(int row, int col)
		{
			var cell = IsCyclic ? new CyclicRGBCell(row, col) : new RGBCell(row, col);
			return cell;
		}
	}
}

