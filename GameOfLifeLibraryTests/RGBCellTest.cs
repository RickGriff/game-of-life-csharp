using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameOfLifeLibraryTests
{
	[TestClass]
	public class RGBCellTest

	{
		// 0 live neighbours. Expected behaviour: cell's state does not change

		[TestMethod]
		public void GetNextState_0LiveNeighbours_CellStateDoesNotChange()
		{
			var deadCell = CellTestHelper.MakeDeadRGBCell();
			var redCell = CellTestHelper.MakeRedRGBCell();
			var greenCell = CellTestHelper.MakeGreenRGBCell();
			var blueCell = CellTestHelper.MakeBlueRGBCell();

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

		/* Assortment of different live neighbour states. 
		/ Expected behaviour: cell takes the state of it's largest neighbour. */

		[TestMethod]
		public void GetNextState_Neighbours_1Red0Green0Blue_CellBecomesRed()
		{
			var deadCell = CellTestHelper.MakeDeadRGBCell();
			var redCell = CellTestHelper.MakeRedRGBCell();
			var greenCell = CellTestHelper.MakeGreenRGBCell();
			var blueCell = CellTestHelper.MakeBlueRGBCell();

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

		[TestMethod]
		public void GetNextState__Neighbours_1Red_6Green_2Blue__CellBecomesGreen()
		{
			var deadCell = CellTestHelper.MakeDeadRGBCell();
			var redCell = CellTestHelper.MakeRedRGBCell();
			var greenCell = CellTestHelper.MakeGreenRGBCell();
			var blueCell = CellTestHelper.MakeBlueRGBCell();

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

		[TestMethod]
		public void GetNextState__Neighbours_3Red_2Green_4Blue__CellBecomesBlue()
		{
			var deadCell = CellTestHelper.MakeDeadRGBCell();
			var redCell = CellTestHelper.MakeRedRGBCell();
			var greenCell = CellTestHelper.MakeGreenRGBCell();
			var blueCell = CellTestHelper.MakeBlueRGBCell();

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

		/* Tiebreak  - equal number of neighbours.  
		 Expected behaviour: cell takes the states of one of the tied neighbours */

		[TestMethod]
		public void GetNextState__Neighbours_3Red_3Green_3Blue__CellBecomesRedOrGreenOrBlue()
		{
			var cell = CellTestHelper.MakeDeadRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(3, 3, 3);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.RED || cell.CurrentState == State.GREEN || cell.CurrentState == State.BLUE);
		}

		[TestMethod]
		public void GetNextState__Neighbours_4Red_4Green_2Blue__CellBecomesRedOrGreen()
		{
			var cell = CellTestHelper.MakeDeadRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(4, 4, 2);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.RED || cell.CurrentState == State.GREEN);
		}

		[TestMethod]
		public void GetNextState___Neighbours_2Red_4Green_4Blue__CellBecomesGreenOrBlue()
		{
			var cell = CellTestHelper.MakeDeadRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(2, 4, 4);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.GREEN || cell.CurrentState == State.BLUE);
		}

		[TestMethod]
		public void GetNextState__Neighbours_4Red_2Green_4Blue__CellBecomesRedOrBlue()
		{
			var cell = CellTestHelper.MakeDeadRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(4, 2, 4);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.RED || cell.CurrentState == State.BLUE);
		}
	}
}
