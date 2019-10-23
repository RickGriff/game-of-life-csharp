using System.Collections.Generic;
using System.Drawing;

namespace GameOfLifeLibrary
{
	public class NeighbourStates
	{
		private Grid grid;
		private int row;
		private int col;
		private Point[] coords;

		public NeighbourStates(Grid _grid, Cell _cell)
		{
			grid = _grid;
			row = _cell.Row;
			col = _cell.Col;

			coords = new[] {
				 new Point(col-1, row-1),   new Point(col, row - 1),    new Point(col+1, row-1),
				 new Point(col-1, row),                                 new Point(col+1, row),
				 new Point(col-1, row+1),   new Point(col, row + 1),    new Point(col+1, row+1)
			};
		}

		public List<State> GetNeighbourStates()
		{
			var states = new List<State>();

			// Get all neighbours within grid boundaries
			foreach (var point in coords)
			{
				if ((point.X >= 0) && (point.X < grid.Length) &&
					 (point.Y >= 0) && (point.Y < grid.Length)
					)
				{
					var cell = grid.Data[point.Y, point.X];
					states.Add(cell.CurrentState);
				}
			}
			return states;
		}
	}
}
