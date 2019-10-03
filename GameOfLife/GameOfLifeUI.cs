using GameOfLifeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{

	public partial class GameOfLifeUI : Form
	{

		private int tickCounter;
		private Grid grid;
		private int cellSize;

		public GameOfLifeUI()
		{
			InitializeComponent();

			grid = new Grid(60);
			grid.SetRandomInitialCells();

			cellSize = dataGridView.Size.Height / grid.Length;

			SetDoubleBuffered(dataGridView);
			dataGridView.ColumnHeadersVisible = false;
			dataGridView.RowHeadersVisible = false;
			dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;

			AddColumns(grid, dataGridView, cellSize);
			GetDataFromGrid(grid, dataGridView, cellSize);
			StartTimer();
		}

		public void AddColumns(Grid grid, DataGridView dataGridView, int colWidth)
		{
			int numCols = grid.Data.GetLength(1);

			for (int i = 0; i < numCols; i++)
			{
				DataGridViewColumn col = new DataGridViewColumn();
				col.Width = colWidth;
				dataGridView.Columns.Add(col);
			}
		}

		void GetDataFromGrid(Grid grid, DataGridView dataGridView, int rowHeight)
		{
			int numRows = grid.Data.GetLength(0);
			int numCols = grid.Data.GetLength(1);

			dataGridView.Rows.Clear();

			for (int row = 0; row < numRows; row++)
			{
				var dvgRow = new DataGridViewRow();

				for (int col = 0; col < numCols; col++)
				{
					var cell = new DataGridViewTextBoxCell();
					cell.Tag = grid.Data[row, col].State;

					dvgRow.Cells.Add(cell);
					int lastCellIdx = dvgRow.Cells.Count - 1;
					dvgRow.Cells[lastCellIdx].Style.BackColor = StateToColour((States)cell.Tag);
					

				}
				dvgRow.Height = rowHeight;
				dataGridView.Rows.Add(dvgRow);
			}
		}

		void SetDoubleBuffered(DataGridView dataGridView)
		{
			typeof(DataGridView).InvokeMember("DoubleBuffered",
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.SetProperty, null, dataGridView, new object[] { true });
		}

		Color StateToColour(States state)
		{
			var stateColorDict = new Dictionary<States, Color> {
				 { States.ALIVE, Color.Black },
				 { States.DEAD, Color.White }
			};
			return stateColorDict[state];
		}

		private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void StartTimer()
		{
			
			timer.Tick += new EventHandler(timer_Tick);
			timer.Interval = 500;
			timer.Start();

			
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			tickCounter += 1;
			grid.Cycle();
			GetDataFromGrid(grid, dataGridView, cellSize);

			cyclesLabel.Text = $"Cycles: {tickCounter.ToString()}";
		}

		private void Label1_Click(object sender, EventArgs e)
		{
			

		}
	}
}
