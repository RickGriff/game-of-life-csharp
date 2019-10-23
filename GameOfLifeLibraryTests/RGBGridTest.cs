using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameOfLifeLibraryTests
{
	[TestClass]
	public class RGBGridTest
	{
		// Constructor tests

		[TestMethod]
		public void RGBGrid_NewRGBGrid_InitializesCorrectPossibleStates()
		{
			var grid = new RGBGrid(4, false);
			var expectedPossibleStates = new List<State> { State.DEAD, State.RED, State.GREEN, State.BLUE };
			Assert.IsTrue(expectedPossibleStates.SequenceEqual(grid.PossibleStates));
		}

		[TestMethod]
		public void RGBGrid_NewRGBGrid_InitializesIsCyclicProperty()
		{
			var grid = new RGBGrid(4, false);
			Assert.IsFalse(grid.IsCyclic);
		}

		[TestMethod]
		public void RGBGrid_NewCyclicRGBGrid_InitializesIsCyclicProperty()
		{
			var cyclicGrid = new RGBGrid(4, true);
			Assert.IsTrue(cyclicGrid.IsCyclic);
		}

		[TestMethod]
		public void RGBGrid_NewRGBGrid_HasCorrectNumberOfCells()
		{
			var grid = new RGBGrid(4, false);
			Assert.IsTrue(grid.Data.Length == 16);
		}

		[TestMethod]
		public void RGBGrid_NewCyclicRGBGrid_HasCorrectNumberOfCells()
		{
			var grid = new RGBGrid(4, true);
			Assert.IsTrue(grid.Data.Length == 16);
		}

		[TestMethod]
		public void RGBGrid_NewRGBGrid_HasCorrectLengthAndWidthOfCellArray()
		{
			var grid = new RGBGrid(4, false);

			Assert.IsTrue(grid.Data.GetLength(0) == 4);
			Assert.IsTrue(grid.Data.GetLength(1) == 4);
		}

		[TestMethod]
		public void RGBGrid_NewCyclicRGBGrid_HasCorrectLengthAndWidthOfCellArray()
		{
			var grid = new RGBGrid(4, true);

			Assert.IsTrue(grid.Data.GetLength(0) == 4);
			Assert.IsTrue(grid.Data.GetLength(1) == 4);
		}

		[TestMethod]
		public void RGBGrid_NewRGBGrid_ContainsRGBCells()
		{
			var grid = new RGBGrid(4, false);

			var expectedCell1 = grid.Data[0, 0];
			var expectedCell2 = grid.Data[2, 1];
			var expectedCell3 = grid.Data[1, 3];

			Assert.IsInstanceOfType(expectedCell1, typeof(RGBCell));
			Assert.IsInstanceOfType(expectedCell2, typeof(RGBCell));
			Assert.IsInstanceOfType(expectedCell3, typeof(RGBCell));
		}

		[TestMethod]
		public void RGBGrid_NewCyclicRGBGrid_ContainsCyclicRGBCells()
		{
			var grid = new RGBGrid(4, true);

			var expectedCell1 = grid.Data[0, 0];
			var expectedCell2 = grid.Data[2, 1];
			var expectedCell3 = grid.Data[1, 3];

			Assert.IsInstanceOfType(expectedCell1, typeof(CyclicRGBCell));
			Assert.IsInstanceOfType(expectedCell2, typeof(CyclicRGBCell));
			Assert.IsInstanceOfType(expectedCell3, typeof(CyclicRGBCell));
		}

		[TestMethod]
		public void RGBGrid_NewRGBGrid_InitializesCellsAsDead()
		{
			var grid = new RGBGrid(4, false);
			int deadCells = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(deadCells, grid.Data.Length);
		}

		[TestMethod]
		public void RGBGrid_NewCyclicRGBGrid_InitializesCellsAsDead()
		{
			var grid = new RGBGrid(4, true);
			int deadCells = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(deadCells, grid.Data.Length);
		}

		// RGBGrid methods

		[TestMethod]
		public void SetInitialCells_EmptyRGBGrid_CorrectlySetsChosenCellsStates()
		{
			var grid = new RGBGrid(4, false);

			Assert.IsTrue(grid.Data[1, 1].CurrentState == State.DEAD);
			Assert.IsTrue(grid.Data[2, 1].CurrentState == State.DEAD);
			Assert.IsTrue(grid.Data[3, 1].CurrentState == State.DEAD);

			// Set chosen cells
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 1, Y = 1 }, State.RED },
				{ new Point { X = 1, Y = 2 }, State.GREEN },
				{ new Point { X = 1, Y = 3 }, State.BLUE },
			};
			grid.SetInitialCells(coords);

			Assert.IsTrue(grid.Data[1, 1].CurrentState == State.RED);
			Assert.IsTrue(grid.Data[2, 1].CurrentState == State.GREEN);
			Assert.IsTrue(grid.Data[3, 1].CurrentState == State.BLUE);
		}

		[TestMethod]
		public void SetInitialCells_EmptyRGBGrid_OnlyChosenCellsStateChanged()
		{
			var grid = new RGBGrid(4, false);

			int deadCellsBefore = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(deadCellsBefore, grid.Data.Length); 

			// set chosen cells
			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 1, Y = 1 }, State.RED },
				{ new Point { X = 1, Y = 2 }, State.GREEN },
				{ new Point { X = 1, Y = 3 }, State.BLUE },
			};
			grid.SetInitialCells(coords);

			int redCellsAfter = GridTestHelper.GetStateCount(grid, State.RED);
			int greenCellsAfter = GridTestHelper.GetStateCount(grid, State.GREEN);
			int blueCellsAfter = GridTestHelper.GetStateCount(grid, State.BLUE);
			int deadCellsAfter = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(redCellsAfter, 1);
			Assert.AreEqual(greenCellsAfter, 1);
			Assert.AreEqual(blueCellsAfter, 1);
			Assert.AreEqual(deadCellsAfter, 13);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SetInitialCells_EmptyRGBGrid_ThrowsForCellCoordsOutOfRange()
		{
			var grid = new RGBGrid(4, false);

			var coords = new Dictionary<Point, State> { { new Point { X = 5, Y = 5 }, State.RED } };
			grid.SetInitialCells(coords);
		}

		[TestMethod]
		public void SetRandomCells_EmptyRGBGrid_GridHasSomeCellsInEachState()
		{
			var grid = new RGBGrid(100, false);

			int redCellsBefore = GridTestHelper.GetStateCount(grid, State.RED);
			int greenCellsBefore = GridTestHelper.GetStateCount(grid, State.GREEN);
			int blueCellsBefore = GridTestHelper.GetStateCount(grid, State.BLUE);
			int deadCellsBefore = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(redCellsBefore, 0);
			Assert.AreEqual(greenCellsBefore, 0);
			Assert.AreEqual(blueCellsBefore, 0);
			Assert.AreEqual(deadCellsBefore, 10000);

			grid.SetRandomInitialCells();

			int redCellsAfter = GridTestHelper.GetStateCount(grid, State.RED);
			int greenCellsAfter = GridTestHelper.GetStateCount(grid, State.GREEN);
			int blueCellsAfter = GridTestHelper.GetStateCount(grid, State.BLUE);
			int deadCellsAfter = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.IsTrue(redCellsAfter > 0);
			Assert.IsTrue(greenCellsAfter > 0);
			Assert.IsTrue(blueCellsAfter > 0);
			Assert.IsTrue(deadCellsAfter > 0);
		}

		[TestMethod]
		public void Clear_RGBGridWithLiveCells_AllCellsBecomeDead()
		{
			var grid = new RGBGrid(4, false);

			var coords = new Dictionary<Point, State>
			{
				{ new Point { X = 1, Y = 1 }, State.RED },
				{ new Point { X = 1, Y = 2 }, State.GREEN },
				{ new Point { X = 3, Y = 3 }, State.BLUE }
			};

			grid.SetInitialCells(coords);

			int deadCellsBefore = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(deadCellsBefore, 13);

			grid.Clear();

			var deadCellsAfter = GridTestHelper.GetStateCount(grid, State.DEAD);

			Assert.AreEqual(deadCellsAfter, grid.Data.Length);
		}

		[TestMethod]
		public void Clear_RGBGrid_CyclesResetToZero()
		{
			var grid = new RGBGrid(4, false);
			grid.Cycle();

			Assert.IsTrue(grid.Cycles == 1);
			grid.Clear();
			Assert.IsTrue(grid.Cycles == 0);
		}

		[TestMethod]
		public void Cycle_EmptyRGBGrid_IncrementsCyclesByOne()
		{
			var grid = new RGBGrid(10, false);
			Assert.IsTrue(grid.Cycles == 0);
			grid.Cycle();
			Assert.IsTrue(grid.Cycles == 1);
		}

		/* Grid evolutions for 'largest neighbour' rules  - check lone single-colours flood grid.
		See RGBCellTest.cs for more thorough tests of the 'largest neighbour' game mechanics. */

		[TestMethod]
		public void Cycle_3RedRGBCells_AllCellsEventuallyBecomeRed()
		{
			var grid = new RGBGrid(10, false);

			GridTestHelper.make3CellColumn(grid, State.RED, new Point { X = 5, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var redCells = GridTestHelper.GetStateCount(grid, State.RED);

			Assert.AreEqual(redCells, grid.Data.Length);
		}

		[TestMethod]
		public void Cycle_3GreenRGBCells_AllCellsEventuallyBecomeGreen()
		{
			var grid = new RGBGrid(10, false);
			GridTestHelper.make3CellColumn(grid, State.GREEN, new Point { X = 5, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var greenCells = GridTestHelper.GetStateCount(grid, State.GREEN);

			Assert.AreEqual(greenCells, grid.Data.Length);
		}

		[TestMethod]
		public void Cycle_3BlueRGBCells_AllCellsEventuallyBecomeBlue()
		{
			var grid = new RGBGrid(10, false);
			GridTestHelper.make3CellColumn(grid, State.BLUE, new Point { X = 5, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var blueCells = GridTestHelper.GetStateCount(grid, State.BLUE);

			Assert.AreEqual(blueCells, grid.Data.Length);
		}

		/* Grid evolutions for 'cyclical eating' rules: [R]ed eats [G]reen eats [B]lue eats [R]ed.
		  See CyclicRGBCellTest.cs for more thorough tests of these game mechanics. */

		// Grid exhibits 'largest neighbour' evolution for lone colours surrounded by dead cells.

		[TestMethod]
		public void Cycle_3RedCyclicRGBCells_AllCellsEventuallyBecomeRed()
		{
			var grid = new RGBGrid(10, true);
			GridTestHelper.make3CellColumn(grid, State.RED, new Point { X = 5, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var redCells = GridTestHelper.GetStateCount(grid, State.RED);

			Assert.AreEqual(redCells, grid.Data.Length);
		}

		[TestMethod]
		public void Cycle_3GreenCyclicRGBCells_AllCellsEventuallyBecomeGreen()
		{
			var grid = new RGBGrid(10, true);
			GridTestHelper.make3CellColumn(grid, State.GREEN, new Point { X = 5, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var greenCells = GridTestHelper.GetStateCount(grid, State.GREEN);

			Assert.AreEqual(greenCells, grid.Data.Length);
		}

		[TestMethod]
		public void Cycle_3BlueCyclicRGBCells_AllCellsEventuallyBecomeBlue()
		{
			var grid = new RGBGrid(10, true);
			GridTestHelper.make3CellColumn(grid, State.BLUE, new Point { X = 5, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var blueCells = GridTestHelper.GetStateCount(grid, State.BLUE);

			Assert.AreEqual(blueCells, grid.Data.Length);
		}

		// Cyclical eating with a pair of coloured columns. Check that the 'predator' colour dominates.

		[TestMethod]
		public void Cycle_3Red3GreenCyclicRGBCells_AllCellsEventuallyBecomeRed()
		{
			var grid = new RGBGrid(10, true);
			GridTestHelper.make3CellColumn(grid, State.RED, new Point { X = 5, Y = 5 });
			GridTestHelper.make3CellColumn(grid, State.GREEN, new Point { X = 6, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var redCells = GridTestHelper.GetStateCount(grid, State.RED);
			Assert.AreEqual(redCells, grid.Data.Length);

		}
		[TestMethod]
		public void Cycle_3Green3BlueCyclicRGBCells_AllCellsEventuallyBecomeGreen()
		{
			var grid = new RGBGrid(10, true);
			GridTestHelper.make3CellColumn(grid, State.GREEN, new Point { X = 5, Y = 5 });
			GridTestHelper.make3CellColumn(grid, State.BLUE, new Point { X = 6, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var greenCells = GridTestHelper.GetStateCount(grid, State.GREEN);
			Assert.AreEqual(greenCells, grid.Data.Length);
		}

		[TestMethod]
		public void Cycle_3Blue3RedCyclicRGBCells_AllCellsEventuallyBecomeBlue()
		{
			var grid = new RGBGrid(10, true);
			GridTestHelper.make3CellColumn(grid, State.BLUE, new Point { X = 5, Y = 5 });
			GridTestHelper.make3CellColumn(grid, State.RED, new Point { X = 6, Y = 5 });

			GridTestHelper.PerformGridCycles(grid, 100);

			var blueCells = GridTestHelper.GetStateCount(grid, State.BLUE);
			Assert.AreEqual(blueCells, grid.Data.Length);
		}

		// Test for simple 3-colour eating pattern 
		[TestMethod]
		public void Cycle_3Red3Green3BlueCyclicRGBCells_AllCellsEventuallyBecomeRed()
		{
			var grid = new RGBGrid(10, true);
			GridTestHelper.make3CellColumn(grid, State.RED, new Point { X = 0, Y = 5 });    // left edge
			GridTestHelper.make3CellColumn(grid, State.GREEN, new Point { X = 5, Y = 5 });  // central
			GridTestHelper.make3CellColumn(grid, State.BLUE, new Point { X = 9, Y = 5 });  // right edge

			GridTestHelper.PerformGridCycles(grid, 100);

			/* Since Red eats Green eats Blue, Red should finally dominate: 
			- Green eats 'rightwards', and eats all the Blue 
			- Red eats 'rightwards' at the same rate, finally eating all the Green. */

			var redCells = GridTestHelper.GetStateCount(grid, State.RED);
			Assert.AreEqual(redCells, grid.Data.Length);
		}
	}
}
