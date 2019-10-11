using GameOfLifeLibrary;
using GameOfLifeUILibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace GameOfLife
{
	public partial class GameOfLifeUI : Form
	{
		private Game chosenGame = Game.CONWAY;
		private State chosenState = State.DEAD;
		private int tickCounter;
		private bool gameStarted = false;
		private bool gameRunning = false;
		private bool cellSelectionEnabled = false;
		private Grid grid;
		private int cellSize;
		private Dictionary<Point, State> cellsToUpdate = new Dictionary<Point, State>();
		public GameOfLifeUI()
		{
			InitializeComponent();
			grid = GetNewGrid(60, chosenGame);
			InitializeUI();
		}

		private Grid GetNewGrid(int size, Game chosenGame)
		{
			Grid grid;

			if (chosenGame == Game.CONWAY) { grid = new Grid(size); }
			else if (chosenGame == Game.LARGEST_NEIGHBOUR) { grid = new RGBGrid(size, false); }
			else { grid = new RGBGrid(size, true); }

			return grid;
		}
		private void InitializeUI()
		{
			cellSize = dataGridView.Size.Height / grid.Length;
			AddColumnsToDGV(grid, dataGridView, cellSize);
			GetDataFromGrid(grid, dataGridView, cellSize);
			SetDGVDoubleBuffered(dataGridView);
			StyleDGV(dataGridView);
			InitializeChooseColourBox();
			InitializeChooseGameComboBox();
		}

		private void GetDataFromGrid(Grid grid, DataGridView dataGridView, int rowHeight)
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

		private void AddColumnsToDGV(Grid grid, DataGridView dataGridView, int colWidth)
		{
			int numCols = grid.Data.GetLength(1);

			for (int i = 0; i < numCols; i++)
			{
				DataGridViewColumn col = new DataGridViewColumn();
				col.Width = colWidth;
				dataGridView.Columns.Add(col);
			}
		}

		private void SetDGVDoubleBuffered(DataGridView dataGridView)
		{
			typeof(DataGridView).InvokeMember("DoubleBuffered",
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.SetProperty, null, dataGridView, new object[] { true });
		}

		private void StyleDGV(DataGridView dataGridView)
		{
			dataGridView.ColumnHeadersVisible = false;
			dataGridView.RowHeadersVisible = false;
			dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
		}

		private void InitializeChooseGameComboBox()
		{
			chooseGameDropdown.DataSource =
			new DropdownItem[]
			{
				new DropdownItem {Game = Game.CONWAY, Text = "Conway Rules" },
				new DropdownItem {Game = Game.LARGEST_NEIGHBOUR, Text = "3-Colour Largest Neighbour" },
				new DropdownItem {Game = Game.CYCLIC_EATING, Text = "3-Colour Cyclic Eating" },
			};
			chooseGameDropdown.DisplayMember = "Text";
		}

		private void InitializeChooseColourBox()
		{
			chooseColourListBox.Items.Clear();
			List<State> states = grid.PossibleStates;
			foreach (var state in states)
			{
				chooseColourListBox.Items.Add(state);
			}
			HideChooseColourBox();
		}

		private Color StateToColour(State state)
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

		private void InitializeGame()
		{
			InitializeTimer();
			gameStarted = true;
			chooseColourListBox.Enabled = false;
			chooseColourLabel.Hide();
			chooseGameDropdown.Enabled = false;
	
		}

		private void InitializeTimer()
		{
			timer.Tick += new EventHandler(timer_Tick);
			timer.Interval = 200;
			timer.Enabled = false;
		}

		private void RunGame()
		{
			grid.SetInitialCells(cellsToUpdate);
			cellsToUpdate.Clear();
			gameRunning = true;
			timer.Enabled = true;
			timer.Start();
		}

		private void PauseGame()
		{
			gameRunning = false;
			startGameButton.Text = "Start Game";
			timer.Stop();
		}

		private void ResetGame()
		{
			PauseGame();
			gameStarted = false;
			grid.Clear();
			grid = GetNewGrid(60, chosenGame);
			ResetCellSelection();
			HideChooseColourBox();
			chooseGameDropdown.Enabled = true;
			tickCounter = 0;
			cyclesLabel.Text = $"Cycles: {tickCounter.ToString()}";
			GetDataFromGrid(grid, dataGridView, cellSize);
		}

		private void EnableCellSelection()
		{
			startGameButton.Enabled = false;
			cellSelectionEnabled = true;
			selectCellsButton.Text = "Confirm selected cells";
			ShowChooseColourBox();
		}

		private void DisableCellSelection()
		{
			cellSelectionEnabled = false;
			startGameButton.Enabled = true;
			selectCellsButton.Text = "Set Custom Cells";
			HideChooseColourBox();
		}

		private void ResetCellSelection()
		{
			cellsToUpdate.Clear();
			cellSelectionEnabled = false;
			chosenState = State.DEAD;
			startGameButton.Enabled = true;
			selectCellsButton.Text = "Set Custom Cells";
			selectCellsButton.Show();
			HideChooseColourBox();
			selectCellsButton.Enabled = true;
		}

		private void ShowChooseColourBox()
		{
			chooseColourLabel.Show();
			chooseColourListBox.Enabled = true;
			chooseColourListBox.Show();
		}

		private void HideChooseColourBox()
		{
			chooseColourLabel.Hide();
			chooseColourListBox.Enabled = false;
			chooseColourListBox.Hide();
		}

		private void SelectCells()
		{
			int cellRow = dataGridView.CurrentCell.RowIndex;
			int cellCol = dataGridView.CurrentCell.ColumnIndex;
			var cellCoordinates = new Point { X = cellCol, Y = cellRow };

			AddCellToUpdateList(cellCoordinates);
		}

		private void AddCellToUpdateList(Point cellCoordinates)
		{
			cellsToUpdate[cellCoordinates] = chosenState;
			dataGridView.CurrentCell.Style.BackColor = StateToColour(chosenState);
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
			if (gameStarted)
			{
				ToggleGame(gameRunning);
			}
			else if (gameStarted == false)
			{
				InitializeGame();
				RunGame();
			}
		}

		private void ToggleGame(bool gameRunning)
		{
			if (gameRunning == true) { PauseGame(); }
			else if (gameRunning == false) { RunGame(); }
		}

		private void selectCellsButton_Click(object sender, EventArgs e)
		{
			if (cellSelectionEnabled == false)
			{
				PauseGame();
				EnableCellSelection();

			}
			else { DisableCellSelection(); }
		}

		private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (cellSelectionEnabled) { SelectCells(); }
		}

		private void dataGridView_SelectionChanged(object sender, EventArgs e)
		{
			dataGridView.ClearSelection();
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

		private void chooseColourListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			chosenState = (State)chooseColourListBox.SelectedItem;
		}

		private void chooseGameDropdown_SelectedIndexChanged(object sender, EventArgs e)
		{
			chosenGame = (Game)chooseGameDropdown.SelectedIndex;
			ResetGame();
			InitializeChooseColourBox();
		}
	}
}
