using System;
using System.Collections.Generic;
using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeLibraryTests
{
	[TestClass]
	public class RGBCellTest

	{
		// Helper methods
		public Cell MakeDeadCell()
		{
			var cell = new RGBCell(1, 1);
			cell.CurrentState = State.DEAD;
			return cell;
		}
		public Cell MakeRedCell()
		{
			var cell = new RGBCell(1, 1);
			cell.CurrentState = State.RED;
			return cell;
		}

		public Cell MakeGreenCell()
		{
			var cell = new RGBCell(1, 1);
			cell.CurrentState = State.GREEN;
			return cell;
		}

		public Cell MakeBlueCell()
		{
			var cell = new RGBCell(1, 1);
			cell.CurrentState = State.BLUE;
			return cell;
		}


		// 0 live neighbours. Expected output: cell's state does not change

		[TestMethod]
		public void GetNextState_0LiveNeighbours_CellStateDoesNotChange()
		{
			var deadCell = MakeDeadCell();
			var redCell = MakeRedCell();
			var greenCell = MakeGreenCell();
			var blueCell = MakeBlueCell();

			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(0, 0, 0);

			CellTestHelper.EvolveCell(deadCell, deadNeighbours);
			CellTestHelper.EvolveCell(redCell, deadNeighbours);
			CellTestHelper.EvolveCell(greenCell, deadNeighbours);
			CellTestHelper.EvolveCell(blueCell, deadNeighbours);

			Assert.IsTrue(deadCell.CurrentState == State.DEAD);
			Assert.IsTrue(redCell.CurrentState == State.RED);
			Assert.IsTrue(greenCell.CurrentState == State.GREEN);
			Assert.IsTrue(blueCell.CurrentState == State.BLUE);
		}

		// Tests for assortment of different neighbour states. 
		// Expected output: Cell takes the state of it's largest neighbour.

		// R 1, G 0, B 0 --> R

		[TestMethod]
		public void GetNextState_Neighbours_1Red0Green0Blue_CellBecomesRed()
		{
			var deadCell = MakeDeadCell();
			var redCell = MakeRedCell();
			var greenCell = MakeGreenCell();
			var blueCell = MakeBlueCell();

			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(1, 0, 0);

			CellTestHelper.EvolveCell(deadCell, deadNeighbours);
			CellTestHelper.EvolveCell(redCell, deadNeighbours);
			CellTestHelper.EvolveCell(greenCell, deadNeighbours);
			CellTestHelper.EvolveCell(blueCell, deadNeighbours);

			Assert.IsTrue(deadCell.CurrentState == State.RED);
			Assert.IsTrue(redCell.CurrentState == State.RED);
			Assert.IsTrue(greenCell.CurrentState == State.RED);
			Assert.IsTrue(blueCell.CurrentState == State.RED);
		}

		// R 1, G 6, B 2 --> G

		[TestMethod]
		public void GetNextState__Neighbours_1Red_6Green_2Blue__CellBecomesGreen()
		{
			var deadCell = MakeDeadCell();
			var redCell = MakeRedCell();
			var greenCell = MakeGreenCell();
			var blueCell = MakeBlueCell();

			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(1, 6, 2);

			CellTestHelper.EvolveCell(deadCell, deadNeighbours);
			CellTestHelper.EvolveCell(redCell, deadNeighbours);
			CellTestHelper.EvolveCell(greenCell, deadNeighbours);
			CellTestHelper.EvolveCell(blueCell, deadNeighbours);

			Assert.IsTrue(deadCell.CurrentState == State.GREEN);
			Assert.IsTrue(redCell.CurrentState == State.GREEN);
			Assert.IsTrue(greenCell.CurrentState == State.GREEN);
			Assert.IsTrue(blueCell.CurrentState == State.GREEN);
		}

		// R 3, G 2, B 4 --> B
		[TestMethod]
		public void GetNextState__Neighbours_3Red_2Green_4Blue__CellBecomesBlue()
		{
			var deadCell = MakeDeadCell();
			var redCell = MakeRedCell();
			var greenCell = MakeGreenCell();
			var blueCell = MakeBlueCell();

			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(3, 2, 4);

			CellTestHelper.EvolveCell(deadCell, deadNeighbours);
			CellTestHelper.EvolveCell(redCell, deadNeighbours);
			CellTestHelper.EvolveCell(greenCell, deadNeighbours);
			CellTestHelper.EvolveCell(blueCell, deadNeighbours);

			Assert.IsTrue(deadCell.CurrentState == State.BLUE);
			Assert.IsTrue(redCell.CurrentState == State.BLUE);
			Assert.IsTrue(greenCell.CurrentState == State.BLUE);
			Assert.IsTrue(blueCell.CurrentState == State.BLUE);
		}


		// Tiebreak tests

		public void GetNextState__Neighbours_3Red_3Green_3Blue__CellBecomesRedOrGreenOrBlue()
		{
			var cell = MakeDeadCell();
	
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(3, 3, 3);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			
			Assert.IsTrue(cell.CurrentState == State.RED || cell.CurrentState == State.GREEN || cell.CurrentState == State.BLUE);
		}

		public void GetNextState__Neighbours_4Red_4Green_2Blue__CellBecomesRedOrGreen()
		{
			var cell = MakeDeadCell();

			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(4, 4, 2);

			CellTestHelper.EvolveCell(cell, deadNeighbours);

			Assert.IsTrue(cell.CurrentState == State.RED || cell.CurrentState == State.GREEN );
		}

		public void GetNextState___Neighbours_2Red_4Green_4Blue__CellBecomesGreenOrBlue()
		{
			var cell = MakeDeadCell();

			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(2, 4, 4);

			CellTestHelper.EvolveCell(cell, deadNeighbours);

			Assert.IsTrue(cell.CurrentState == State.GREEN || cell.CurrentState == State.BLUE);
		}

		public void GetNextState__Neighbours_4Red_2Green_4Blue__CellBecomesRedOrBlue()
		{
			var cell = MakeDeadCell();

			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(4, 2, 4);

			CellTestHelper.EvolveCell(cell, deadNeighbours);

			Assert.IsTrue(cell.CurrentState == State.RED || cell.CurrentState == State.BLUE);
		}









	}
}
