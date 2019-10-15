using GameOfLifeLibrary;
using GameOfLifeUILibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLife
{
	public partial class GameOfLifeUI : Form
	{
		private GameType chosenGame = GameType.CONWAY;
		private Grid grid;
		private int cellSize;

		private bool gameStarted = false;
		private bool gameRunning = false;
		private int tickCounter;

		private State chosenState = State.DEAD;

		private bool cellSelectionEnabled = false;
		private Dictionary<Point, State> cellsToUpdate = new Dictionary<Point, State>();

		public GameOfLifeUI()
		{
			InitializeComponent();

			grid = GetNewGrid(60, chosenGame);
			InitializeUI();
		}

		private Grid GetNewGrid(int size, GameType chosenGame)
		{
			Grid grid;

			if (chosenGame == GameType.CONWAY) { grid = new Grid(size); }
			else if (chosenGame == GameType.LARGEST_NEIGHBOUR) { grid = new RGBGrid(size, false); }
			else { grid = new RGBGrid(size, true); }

			return grid;
		}

		private void SetupChooseGameDropdown()
		{
			chooseGameDropdown.DataSource = 
			new DropdownItem[]
			{
				new DropdownItem {GameType = GameType.CONWAY, Text = "Conway Rules" },
				new DropdownItem {GameType = GameType.LARGEST_NEIGHBOUR, Text = "3-Colour Largest Neighbour" },
				new DropdownItem {GameType = GameType.CYCLIC_EATING, Text = "3-Colour Cyclic Eating" },
			};
			chooseGameDropdown.DisplayMember = "Text";
		}

		private void InitializeUI()
		{
			cellSize = dataGridView.Size.Height / grid.Length; 
			DGVSetter.ConfigureDGV(grid, dataGridView, cellSize);
			DGVSetter.GetDataFromGrid(grid, dataGridView, cellSize); 
			SetupChooseColourBox(); 
			SetupChooseGameDropdown(); 
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
			startGameButton.Text = "Pause Game";
		}

		private void ToggleGame(bool gameRunning)  
		{
			if (gameRunning == true) { PauseGame(); }
			else if (gameRunning == false) { RunGame(); }
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
			DGVSetter.GetDataFromGrid(grid, dataGridView, cellSize);
		}

		private void SetupChooseColourBox()
		{
			chooseColourListBox.Items.Clear();  
			List<State> states = grid.PossibleStates;  
			foreach (var state in states)
			{
				chooseColourListBox.Items.Add(state);
			}
			HideChooseColourBox();
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
			dataGridView.CurrentCell.Style.BackColor = DGVSetter.StateToColour(chosenState); 
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			tickCounter += 1; 
			grid.Cycle();
			DGVSetter.GetDataFromGrid(grid, dataGridView, cellSize);  

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
			DGVSetter.GetDataFromGrid(grid, dataGridView, cellSize);
		}

		private void chooseColourListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			chosenState = (State)chooseColourListBox.SelectedItem;
		}

		private void chooseGameDropdown_SelectedIndexChanged(object sender, EventArgs e)
		{
			chosenGame = (GameType)chooseGameDropdown.SelectedIndex;
			ResetGame();
			SetupChooseColourBox();
		}
	}
}
