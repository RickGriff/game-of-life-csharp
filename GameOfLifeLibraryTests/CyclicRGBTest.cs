using System;
using System.Collections.Generic;
using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeLibraryTests
{
	[TestClass]
	public class CyclicRGBCellTest
	{
		/* Cells with only dead neighbours.
		Expected behaviour: cells do not change their state. */
		[TestMethod]
		public void GetNextState_0LiveNeighbours_CellStateDoesNotChange()
		{
			var deadCell = CellTestHelper.MakeDeadCyclicRGBCell();
			var redCell = CellTestHelper.MakeRedCyclicRGBCell();
			var greenCell = CellTestHelper.MakeGreenCyclicRGBCell();
			var blueCell = CellTestHelper.MakeBlueCyclicRGBCell();

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

		/* Dead cell with some live neighbours.
		 Expected behaviour: cell takes the state of it's largest neighbour, or remains dead if it has only dead neighbours. */
		[TestMethod]
		public void GetNextState__DeadCell_Neighbours3Red2Green2Blue__CellBecomesRed()
		{
			var cell = CellTestHelper.MakeDeadCyclicRGBCell();
			
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(3, 2, 2);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			
			Assert.IsTrue(cell.CurrentState == State.RED);
		}

		[TestMethod]
		public void GetNextState__DeadCell_Neighbours2Red3Green2Blue__CellBecomesGreen()
		{
			var cell = CellTestHelper.MakeDeadCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(2, 3, 2);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.GREEN);
		}

		[TestMethod]
		public void GetNextState__DeadCell_Neighbours2Red2Green3Blue__CellBecomesBlue()
		{
			var cell = CellTestHelper.MakeDeadCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(2, 2, 3);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.BLUE);
		}

		/* Dead cell with equal neighbours - tiebreak situation.
		Expected behaviour: cell takes one of it's neighbour's states at random. */
		[TestMethod]
		public void GetNextState__DeadCell_Neighbours3Red3Green3Blue__CellBecomesRedOrGreenOrBlue()
		{
			var cell = CellTestHelper.MakeDeadCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(3, 3, 3);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.RED || cell.CurrentState == State.GREEN || cell.CurrentState == State.BLUE );
		}

		/* Live cell.
		Expected behaviour: Cyclical eating. [R]ed eats [G]reen eats [B]lue eats [R]ed. 
		If cell has >= 3 'predator' neighbours, it takes predator colour - otherwise, no state change. */

		//Red cell

		[TestMethod]
		public void GetNextState__RedCell_Neighbours0Red0Green3Blue__CellBecomesBlue()
		{
			var cell = CellTestHelper.MakeRedCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(0, 0, 3);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue( cell.CurrentState == State.BLUE);
		}
		[TestMethod]
		public void GetNextState__RedCell_Neighbours0Red0Green2Blue__CellStaysRed()
		{
			var cell = CellTestHelper.MakeRedCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(0, 0, 2);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue( cell.CurrentState == State.RED);
		}

		[TestMethod]
		public void GetNextState__RedCell_Neighbours0Red3Green2Blue__CellStaysRed()
		{
			var cell = CellTestHelper.MakeRedCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(0, 3, 2);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue( cell.CurrentState == State.RED);
		}

		//Green cell

		[TestMethod]
		public void GetNextState__GreenCell_Neighbours3Red0Green0Blue__CellBecomesRed()
		{
			var cell = CellTestHelper.MakeGreenCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(3, 0, 0);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.RED);
		}

		[TestMethod]
		public void GetNextState__GreenCell_Neighbours2Red0Green0Blue__CellStaysGreen()
		{
			var cell = CellTestHelper.MakeGreenCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(2, 0, 0);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue( cell.CurrentState == State.GREEN );
		}

		[TestMethod]
		public void GetNextState__GreenCell_Neighbours2Red0Green3Blue__CellStaysGreen()
		{
			var cell = CellTestHelper.MakeGreenCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(2, 0, 3);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.GREEN);
		}

		//Blue cell

		[TestMethod]
		public void GetNextState__BlueCell_Neighbours0Red3Green0Blue__CellBecomesGreen()
		{
			var cell = CellTestHelper.MakeBlueCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(0, 3, 0);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.GREEN);
		}

		[TestMethod]
		public void GetNextState__BlueCell_Neighbours0Red2Green0Blue__CellStaysBlue()
		{
			var cell = CellTestHelper.MakeBlueCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(0, 2, 0);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue(cell.CurrentState == State.BLUE);
		}

		[TestMethod]
		public void GetNextState__BlueCell_Neighbours3Red2Green0Blue__CellStaysBlue()
		{
			var cell = CellTestHelper.MakeBlueCyclicRGBCell();
			List<State> deadNeighbours = CellTestHelper.CreateRGBNeighboursList(3, 2, 0);

			CellTestHelper.EvolveCell(cell, deadNeighbours);
			Assert.IsTrue( cell.CurrentState == State.BLUE);
		}
	}
}
