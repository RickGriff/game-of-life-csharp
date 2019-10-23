using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameOfLifeLibraryTests
{

	[TestClass]
	public class GridTest
	{
		// Constructor tests

		[TestMethod]
		public void Grid_NewGrid_InitializesLengthAndCyclesProperties()
		{
			var grid = new Grid(4);

			Assert.IsTrue(grid.Length == 4);
			Assert.IsTrue(grid.Cycles == 0);
		}

		[TestMethod]
		public void Grid_NewGrid_InitializesCorrectPossibleStates()
		{
			var grid = new Grid(4);
			var expectedPossibleStates = new List<State> { State.DEAD, State.ALIVE };
			Assert.IsTrue(expectedPossibleStates.SequenceEqual(grid.PossibleStates));
		}

		[TestMethod]
		public void Grid_NewGrid_HasCorrectNumberOfCells()
		{
			var grid = new Grid(4);
			Assert.IsTrue(grid.Data.Length == 16);
		}

		[TestMethod]
		public void Grid_NewGrid_HasCorrectLengthAndWidthOfCellArray()
		{
			var grid = new Grid(4);

			Assert.IsTrue(grid.Data.GetLength(0) == 4);
			Assert.IsTrue(grid.Data.GetLength(1) == 4);
		}

		[TestMethod]
		public void Grid_NewGrid_ContainsCells()
		{
			var grid = new Grid(4);

			var expectedCell1 = grid.Data[0, 0];
			var expectedCell2 = grid.Data[2, 1];
			var expectedCell3 = grid.Data[1, 3];

			Assert.IsInstanceOfType(expectedCell1, typeof(Cell));
			Assert.IsInstanceOfType(expectedCell2, typeof(Cell));
			Assert.IsInstanceOfType(expectedCell3, typeof(Cell));
		}

		[TestMethod]
		public void Grid_NewGrid_InitializesCellsAsDead()
		{
			var grid = new Grid(4);
			int deadCells = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(deadCells, grid.Data.Length);
		}

		// SetInitialCells()
		[TestMethod]
		public void SetInitialCells_EmptyGrid_CorrectlySetsChosenCellsState()
		{
			var grid = new Grid(4);

			Assert.IsTrue(grid.Data[1, 1].CurrentState == State.DEAD);
			Assert.IsTrue(grid.Data[2, 1].CurrentState == State.DEAD);

			// set chosen cells
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 1, Y = 1 }, State.ALIVE },
				{ new Point { X = 1, Y = 2 }, State.ALIVE }
			};
			grid.SetInitialCells(coords);

			Assert.IsTrue(grid.Data[1, 1].CurrentState == State.ALIVE);
			Assert.IsTrue(grid.Data[2, 1].CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void SetInitialCells_EmptyGrid_OnlyChosenCellsStateChanged()
		{
			var grid = new Grid(4);

			int liveCellsBefore = GridTestHelper.GetStateCount(grid, State.ALIVE);
			int deadCellsBefore = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(liveCellsBefore, 0);
			Assert.AreEqual(deadCellsBefore, 16);

			// set chosen cells
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 1, Y = 1 }, State.ALIVE },
				{ new Point { X = 1, Y = 2 }, State.ALIVE }
			};
			grid.SetInitialCells(coords);

			int liveCellsAfter = GridTestHelper.GetStateCount(grid, State.ALIVE);
			int deadCellsAfter = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(liveCellsAfter, 2);
			Assert.AreEqual(deadCellsAfter, 14);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SetInitialCells_EmptyGrid_ThrowsForCellCoordsOutOfRange()
		{
			var grid = new Grid(4);

			var coords = new Dictionary<Point, State> { { new Point { X = 5, Y = 5 }, State.ALIVE } };
			grid.SetInitialCells(coords);
		}

		// SetRandomCells()
		[TestMethod]
		public void SetRandomCells_EmptyGrid_GridHasLiveAndDeadCells()
		{
			var grid = new Grid(50);

			var liveCellsBefore = GridTestHelper.GetStateCount(grid, State.ALIVE);
			var deadCellsBefore = GridTestHelper.GetStateCount(grid, State.DEAD);
			Assert.AreEqual(liveCellsBefore, 0);
			Assert.AreEqual(deadCellsBefore, 2500);

			grid.SetRandomInitialCells();

			var liveCellsAfter = GridTestHelper.GetStateCount(grid, State.ALIVE);
			var deadCellsAfter = GridTestHelper.GetStateCount(grid, State.DEAD);
			Assert.IsTrue(liveCellsAfter > 0);
			Assert.IsTrue(deadCellsAfter > 0);
		}

		// Clear()
		[TestMethod]
		public void Clear_GridWithLiveCells_AllCellsBecomeDead()
		{
			var grid = new Grid(4);

			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 1, Y = 1 }, State.ALIVE },
				{ new Point { X = 1, Y = 2 }, State.ALIVE },
				{ new Point { X = 3, Y = 3 }, State.ALIVE }
			};

			grid.SetInitialCells(coords);
		
			var deadCellsBefore = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(deadCellsBefore, 13);

			grid.Clear();

			var deadCellsAfter = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(deadCellsAfter, grid.Data.Length);
		}

		[TestMethod]
		public void Clear_CyclesResetToZero()
		{
			var grid = new Grid(4);
			grid.Cycle();

			Assert.IsTrue(grid.Cycles == 1);
			grid.Clear();
			Assert.IsTrue(grid.Cycles == 0);
		}

		// Cycle()

		// Test grid evolution for various archetypal Game Of Life cell patterns (Conway Rules)
		public void Cycle_EmptyGrid_IncrementsCyclesByOne()
		{
			var grid = new Grid(10);
			Assert.IsTrue(grid.Cycles == 0);
			grid.Cycle();
			Assert.IsTrue(grid.Cycles == 1);
		}

		[TestMethod]
		public void Cycle_3CellColumn_Becomes3CellRow()
		{
			var grid = new Grid(10);
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 5, Y = 4 }, State.ALIVE },
				{ new Point { X = 5, Y = 5 }, State.ALIVE },
				{ new Point { X = 5, Y = 6 }, State.ALIVE }
			};
			grid.SetInitialCells(coords);

			grid.Cycle();

			/* check grid evolves from a 3-cell live column to a 3-cell live row, 
			  and that no other cells become live */
			var expectedLiveCellsRow = new List<Cell> { grid.Data[5, 4], grid.Data[5, 5], grid.Data[5, 6] };
			var numLiveCellsInRow = expectedLiveCellsRow.Where(cell => cell.CurrentState == State.ALIVE).Count();

			Assert.IsTrue(numLiveCellsInRow == 3);

			Assert.AreEqual(GridTestHelper.GetStateCount(grid, State.ALIVE), 3);
			Assert.AreEqual(GridTestHelper.GetStateCount(grid, State.DEAD), 97);
		}

		[TestMethod]
		public void Cycle_3CellRow_Becomes3CellColumn()
		{
			var grid = new Grid(10);
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 4, Y = 5 }, State.ALIVE },
				{ new Point { X = 5, Y = 5 }, State.ALIVE },
				{ new Point { X = 6, Y = 5 }, State.ALIVE }
			};
			grid.SetInitialCells(coords);

			grid.Cycle();

			/* check grid evolves from a 3-cell live row to a 3-cell live column, 
			  and that no other cells become live */
			var expectedLiveCellsRow = new List<Cell> { grid.Data[4, 5], grid.Data[5, 5], grid.Data[6, 5] };
			var numLiveCellsInRow = expectedLiveCellsRow.Where(cell => cell.CurrentState == State.ALIVE).Count();

			Assert.IsTrue(numLiveCellsInRow == 3);

			Assert.AreEqual(GridTestHelper.GetStateCount(grid, State.ALIVE), 3);
			Assert.AreEqual(GridTestHelper.GetStateCount(grid, State.DEAD), 97);
		}
		[TestMethod]
		public void Cycle_4CellBlock_RemainsStable4CellBlock()
		{
			var grid = new Grid(10);
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 4, Y = 4 }, State.ALIVE },
				{ new Point { X = 4, Y = 5 }, State.ALIVE },
				{ new Point { X = 5, Y = 4 }, State.ALIVE },
				{ new Point { X = 5, Y = 5 }, State.ALIVE }
			};
			grid.SetInitialCells(coords);

			grid.Cycle();
			// check block remains live, and no other cells become live
			var expectedLiveCellsBlock = new List<Cell> { grid.Data[4, 4], grid.Data[4, 5], grid.Data[5, 4], grid.Data[5, 5] };
			var numLiveCellsInBlock = expectedLiveCellsBlock.Where(cell => cell.CurrentState == State.ALIVE).Count();

			Assert.IsTrue(numLiveCellsInBlock == 4);

			Assert.AreEqual(GridTestHelper.GetStateCount(grid, State.ALIVE), 4);
			Assert.AreEqual(GridTestHelper.GetStateCount(grid, State.DEAD), 96);
		}

		[TestMethod]
		public void Cycle_ScatteredLoneCells_AllCellsDie()
		{
			var grid = new Grid(10);
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 9, Y = 8 }, State.ALIVE },
				{ new Point { X = 1, Y = 2 }, State.ALIVE },
				{ new Point { X = 5, Y = 4 }, State.ALIVE },
				{ new Point { X = 7, Y = 1}, State.ALIVE }
			};
			grid.SetInitialCells(coords);

			grid.Cycle();

			Assert.AreEqual(GridTestHelper.GetStateCount(grid, State.ALIVE), 0);
			Assert.AreEqual(GridTestHelper.GetStateCount(grid, State.DEAD), 100);
		}
	}
}
