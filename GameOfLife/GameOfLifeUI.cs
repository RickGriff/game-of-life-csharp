using GameOfLifeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace GameOfLife
{
	public partial class GameOfLifeUI : Form
	{
		private int tickCounter;
		private bool gameStarted = false;
		private bool gameRunning = false;
		private bool cellSelectionEnabled = false;
		private Grid grid;
		private int cellSize;
		private List<Point> selectedCells = new List<Point>();
		public GameOfLifeUI()
		{
			InitializeComponent();

			grid = new Grid(60);
			//grid.SetRandomInitialCells();

			//List<Point> initialCells = new List<Point> { new Point { X = 5, Y = 5 }, new Point { X = 5, Y = 6 }, new Point { X = 5, Y = 7 } };
			//grid.SetInitialCells(initialCells, State.ALIVE);

			cellSize = dataGridView.Size.Height / grid.Length;

			SetDoubleBuffered(dataGridView);
			ConfigureGrid(dataGridView);

			AddColumns(grid, dataGridView, cellSize);
			GetDataFromGrid(grid, dataGridView, cellSize);
			InitializeTimer();
		}
		public void ConfigureGrid(DataGridView dataGridView)
		{
			dataGridView.ColumnHeadersVisible = false;
			dataGridView.RowHeadersVisible = false;
			dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
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

		void SetDoubleBuffered(DataGridView dataGridView)
		{
			typeof(DataGridView).InvokeMember("DoubleBuffered",
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.SetProperty, null, dataGridView, new object[] { true });
		}

		Color StateToColour(State state)
		{
			var stateColorDict = new Dictionary<State, Color> {
				 { State.DEAD, Color.White },
				 { State.ALIVE, Color.Black },
				 { State.RED, Color.Red },
				 { State.GREEN, Color.Green },
				 { State.BLUE, Color.Blue }
			};
			return stateColorDict[state];
		}

		private void InitializeTimer()
		{
			timer.Tick += new EventHandler(timer_Tick);
			timer.Interval = 200;
			timer.Enabled = false;
		}

		private void InitializeGame()
		{
			//selectCellsButton.Hide();
			grid.SetInitialCells(selectedCells, State.ALIVE);
			selectedCells.Clear();
			gameStarted = true;
		}
		private void RunGame()
		{
			grid.SetInitialCells(selectedCells, State.ALIVE);
			selectedCells.Clear();
			gameRunning = true;
			//selectCellsButton.Enabled = false;
			timer.Enabled = true;
			timer.Start();
			startGameButton.Text = "Pause Game";
		}
		private void StopGame()
		{
			gameRunning = false;
			startGameButton.Text = "Start Game";
			timer.Stop();
		}

		private void ResetGame()
		{
			StopGame();
			grid.Clear();
			ResetCellSelection();
			gameStarted = false;
			tickCounter = 0;
			cyclesLabel.Text = $"Cycles: {tickCounter.ToString()}";
			GetDataFromGrid(grid, dataGridView, cellSize);
		}

		private void EnableCellSelection()
		{
			startGameButton.Enabled = false;
			cellSelectionEnabled = true;
			selectCellsButton.Text = "Confirm selected cells";
		}

		private void DisableCellSelection()
		{
			cellSelectionEnabled = false;
			startGameButton.Enabled = true;
			selectCellsButton.Text = "Select cells";
			//selectCellsButton.Enabled = false;
		}

		private void ResetCellSelection()
		{
			selectedCells.Clear();
			cellSelectionEnabled = false;
			startGameButton.Enabled = true;
			selectCellsButton.Text = "Select cells";
			selectCellsButton.Show();
			selectCellsButton.Enabled = true;
		}
		private void SelectCells()
		{
			int cellRow = dataGridView.CurrentCell.RowIndex;
			int cellCol = dataGridView.CurrentCell.ColumnIndex;
			var cellCoordinates = new Point { X = cellCol, Y = cellRow };
			//selectedCells.Add(cellCoordinates);

			SwitchSelected(cellCoordinates);
			//var colour = dataGridView.CurrentCell.Style.BackColor;
			//dataGridView.CurrentCell.Style.BackColor = (colour == Color.Black) ? Color.White : Color.Black;
			//selectedCells.Add(cellCoordinates);
			//selectedCells.Re
		}

		private void SwitchSelected(Point cellCoordinates)
		{
			if (selectedCells.Contains(cellCoordinates))
			{
				dataGridView.CurrentCell.Style.BackColor = Color.White;
				selectedCells.Remove(cellCoordinates);
			}
			else
			{
				dataGridView.CurrentCell.Style.BackColor = Color.Black;
				selectedCells.Add(cellCoordinates);
			}	
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			tickCounter += 1;
			grid.Cycle();
			GetDataFromGrid(grid, dataGridView, cellSize);

			cyclesLabel.Text = $"Cycles: {tickCounter.ToString()}";
		}
		
		private void startGameButton_Click(object sender, EventArgs e)
		{
			if (gameStarted == false)
			{
				InitializeGame();
			}
			
			if (gameRunning == false) { RunGame(); }
			else { StopGame(); }
		}

		private void selectCellsButton_Click(object sender, EventArgs e)
		{
			// if (gameStarted == true) { continue };

			if (cellSelectionEnabled == false)
			{
				StopGame();
				EnableCellSelection();
			}

			else { DisableCellSelection(); }
		}

		private void cyclesLabel_Click(object sender, EventArgs e) { }

		private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (cellSelectionEnabled == true) { SelectCells(); }
		}

		private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
		}

		private void resetButton_click(object sender, EventArgs e)
		{
			ResetGame();
		}

		private void setRandomCellsButton_Click(object sender, EventArgs e)
		{
			grid.SetRandomInitialCells();
			GetDataFromGrid(grid, dataGridView, cellSize);
		}
	}
}
