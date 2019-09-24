using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace GameOfLifeLibrary
{
	class NeighbourStates
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
				 new Point( row - 1, col-1 ),    new Point( row - 1, col ),   new Point( row - 1, col + 1),
				 new Point( row, col-1 ),									  new Point( row, col + 1),
				 new Point( row + 1, col - 1 ),  new Point( row + 1, col ),   new Point( row + 1, col + 1)
			};
		}

		public List<string> GetNeighbourStates()
		{
			var states = new List<string>();

			// Get all valid neighbours - i.e. within grid boundaries
			foreach (var point in coords)
			{
				if ((point.X > 0 && point.X < grid.Length) &&
					(point.Y > 0 && point.Y < grid.Length)
					)
				{
					var cell = grid.Data[point.Y, point.X]; 
					states.Add(cell.State);
				}
			}	
			return states;
		}
	}
}
