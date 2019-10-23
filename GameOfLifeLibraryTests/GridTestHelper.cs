using GameOfLifeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLibraryTests
{
	public static class GridTestHelper
	{
		public static int GetStateCount(Grid grid, State state)
		{
			int count = 0;
			foreach (var cell in grid.Data)
			{
				if (cell.CurrentState == state) { count += 1; }
			}
			return count;
		}

		public static void make3CellColumn(Grid grid, State state, Point center)
		{
			int row = center.Y;
			int col = center.X;
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = col, Y = row - 1 }, state },
				{ new Point { X = col, Y = row }, state },
				{ new Point { X = col, Y = row + 1 }, state }
			};

			grid.SetInitialCells(coords);
		}

		public static void make3CellRow(Grid grid, State state, Point center)
		{
			int row = center.Y;
			int col = center.X;

			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = col - 1, Y = row }, state },
				{ new Point { X = col,   Y = row }, state },
				{ new Point { X =  col + 1, Y = row }, state }
			};

			grid.SetInitialCells(coords);
		}

		public static void PerformGridCycles(Grid grid, int times)
		{
			int counter = 0;
			while (counter < times)
			{
				grid.Cycle();
				counter += 1;
			}
		}
	}
}
