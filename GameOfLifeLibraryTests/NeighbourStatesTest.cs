using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLifeLibraryTests
{
	[TestClass]
	public class NeighbourStatesTest
	{
		// Check corner cell neighbour states counts
		[TestMethod]
		public void GetNeighbourStates_CornerCellTopLeft_Has3Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var cornerCell = grid.Data[0, 0];

			var neighbours = new NeighbourStates(grid, cornerCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 3);
		}

		[TestMethod]
		public void GetNeighbourStates_CornerCellTopRight_Has3Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var cornerCell = grid.Data[0, 3];

			var neighbours = new NeighbourStates(grid, cornerCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 3);

		}

		[TestMethod]
		public void GetNeighbourStates_CornerCellBottomLeft_Has3Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var cornerCell = grid.Data[3, 0];

			var neighbours = new NeighbourStates(grid, cornerCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 3);

		}

		[TestMethod]
		public void GetNeighbourStates_CornerCellBottomRight_Has3Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var cornerCell = grid.Data[3, 3];

			var neighbours = new NeighbourStates(grid, cornerCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 3);
		}

		// Check edge cell neighbour states counts
		[TestMethod]
		public void GetNeighbourStates_EdgeCellTop_Has5Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var edgeCell = grid.Data[0, 2];

			var neighbours = new NeighbourStates(grid, edgeCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 5);
		}

		[TestMethod]
		public void GetNeighbourStates_EdgeCellRight_Has5Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var edgeCell = grid.Data[2, 3];

			var neighbours = new NeighbourStates(grid, edgeCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 5);
		}

		[TestMethod]
		public void GetNeighbourStates_EdgeCellBottom_Has5Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var edgeCell = grid.Data[3, 2];

			var neighbours = new NeighbourStates(grid, edgeCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 5);
		}

		[TestMethod]
		public void GetNeighbourStates_EdgeCellLeft_Has5Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var edgeCell = grid.Data[2, 0];

			var neighbours = new NeighbourStates(grid, edgeCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 5);
		}

		// Check interior cell neighbour states counts
		[TestMethod]
		public void GetNeighbourStates_InteriorCell1_Has8Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var interiorCell = grid.Data[2, 1];

			var neighbours = new NeighbourStates(grid, interiorCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 8);
		}

		public void GetNeighbourStates_InteriorCell2_Has8Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var interiorCell = grid.Data[1, 2];

			var neighbours = new NeighbourStates(grid, interiorCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 8);
		}

		[TestMethod]
		public void GetNeighbourStates_InteriorCell3_Has8Neighbours()
		{
			var grid = new RGBGrid(4, false);
			var interiorCell = grid.Data[2, 2];

			var neighbours = new NeighbourStates(grid, interiorCell);
			var states = neighbours.GetNeighbourStates();

			Assert.IsTrue(states.Count() == 8);
		}

		// Check neighbourStates values around cell in an empty grid.

		[TestMethod]
		public void GetNeighbourStates_CornerCellInEmptyGrid_HasAllDeadNeighbours()
		{
			var grid = new RGBGrid(4, false);
			var cell = grid.Data[0, 0];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.DEAD, 3 } };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}
		[TestMethod]
		public void GetNeighbourStates_EdgeCellInEmptyGrid_HasAllDeadNeighbours()
		{
			var grid = new RGBGrid(4, false);
			var cell = grid.Data[2, 3];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.DEAD, 5 } };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}

		[TestMethod]
		public void GetNeighbourStates_InteriorCellInEmptyGrid_HasAllDeadNeighbours()
		{
			var grid = new RGBGrid(4, false);
			var cell = grid.Data[2, 2];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.DEAD, 8 } };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}

		/* Check neighbourStates values for a cell in a filled, alternating 4x4 grid.
		Cell states, by row, are:  Dead, Red, Green, Blue. */

		[TestMethod]
		public void GetNeighbourStates_CornerCellTopLeftInFilledGrid_Neighbours1Dead2Red()
		{
			var grid = NeighbourStatesTestHelper.MakeFilled4x4RGBGrid();
			var cell = grid.Data[0, 0];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.DEAD, 1 }, { State.RED, 2 } };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}

		[TestMethod]
		public void GetNeighbourStates_CornerCellBottomRightInFilledGrid_Neighbours1Blue2Green()
		{
			var grid = NeighbourStatesTestHelper.MakeFilled4x4RGBGrid();
			var cell = grid.Data[3, 3];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.BLUE, 1 }, { State.GREEN, 2 } };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}

		[TestMethod]
		public void GetNeighbourStates_EdgeCellTopInFilledGrid_Neighbours2Red1Green2Blue()
		{
			var grid = NeighbourStatesTestHelper.MakeFilled4x4RGBGrid();
			var cell = grid.Data[0, 2];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.BLUE, 2 }, { State.GREEN, 1 }, { State.RED, 2 } };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}

		[TestMethod]
		public void GetNeighbourStates_EdgeCellLeftInFilledGrid_Neighbours2Dead3Red()
		{
			var grid = NeighbourStatesTestHelper.MakeFilled4x4RGBGrid();
			var cell = grid.Data[2, 0];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.DEAD, 2 }, { State.RED, 3 } };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}

		[TestMethod]
		public void GetNeighbourStates__InteriorCell_1_1_InFilledGrid__Neighbours3Dead2Red3Green()
		{
			var grid = NeighbourStatesTestHelper.MakeFilled4x4RGBGrid();
			var cell = grid.Data[1, 1];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.DEAD, 3 }, { State.RED, 2 }, { State.GREEN, 3 }, };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}

		[TestMethod]
		public void GetNeighbourStates__InteriorCell_2_2_InFilledGrid__Neighbours3Red2Green3Blue()
		{
			var grid = NeighbourStatesTestHelper.MakeFilled4x4RGBGrid();
			var cell = grid.Data[2, 2];

			var neighbours = new NeighbourStates(grid, cell);
			var actualStates = neighbours.GetNeighbourStates();

			var actualStateCounts = NeighbourStatesTestHelper.GetStateCountsDict(actualStates);
			var expectedStateCounts = new Dictionary<State, int> { { State.RED, 3 }, { State.GREEN, 2 }, { State.BLUE, 3 }, };

			Assert.IsTrue(NeighbourStatesTestHelper.DictsAreEqual(actualStateCounts, expectedStateCounts));
		}
	}
}
