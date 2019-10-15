using GameOfLifeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace GameOfLifeUILibrary
{
	public static class DGVSetter
	{

		private static Dictionary<State, Color> stateColorDict = new Dictionary<State, Color> {
				 { State.DEAD, Color.White },
				 { State.ALIVE, Color.Black },
				 { State.RED, Color.Red },
				 { State.GREEN, Color.Green },
				 { State.BLUE, Color.Blue }
			};
		public static void GetDataFromGrid(Grid grid, DataGridView dataGridView, int rowHeight)
		{
			int numRows = grid.Data.GetLength(0);
			int numCols = grid.Data.GetLength(1);

			dataGridView.Rows.Clear();

			for (int row = 0; row < numRows; row++)
			{
				var dgvRow = new DataGridViewRow();

				for (int col = 0; col < numCols; col++)
				{
					var cell = new DataGridViewTextBoxCell();
					cell.Tag = grid.Data[row, col].CurrentState;

					dgvRow.Cells.Add(cell);
					int lastCellIdx = dgvRow.Cells.Count - 1;
					dgvRow.Cells[lastCellIdx].Style.BackColor = StateToColour((State)cell.Tag);
				}

				dgvRow.Height = rowHeight;
				dataGridView.Rows.Add(dgvRow);
			}
		}
		
		public static void ConfigureDGV(Grid grid, DataGridView dataGridView, int colWidth)
		{
			AddColumnsToDGV(grid, dataGridView, colWidth);
			SetDGVDoubleBuffered(dataGridView);
			StyleDGV(dataGridView);
		}

		public static void AddColumnsToDGV(Grid grid, DataGridView dataGridView, int colWidth)
		{
			int numCols = grid.Data.GetLength(1);

			for (int i = 0; i < numCols; i++)
			{
				DataGridViewColumn col = new DataGridViewColumn();
				col.Width = colWidth;
				dataGridView.Columns.Add(col);
			}
		}

		private static void SetDGVDoubleBuffered(DataGridView dataGridView)
		{
			typeof(DataGridView).InvokeMember("DoubleBuffered",
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.SetProperty, null, dataGridView, new object[] { true });
		} 

		private static void StyleDGV(DataGridView dataGridView) 
		{
			dataGridView.ColumnHeadersVisible = false;
			dataGridView.RowHeadersVisible = false;
			dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
		}

		public static Color StateToColour(State state)
		{
			
			return stateColorDict[state];
		}
	}
}
