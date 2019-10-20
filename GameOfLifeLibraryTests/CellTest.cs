using System;
using System.Collections.Generic;
using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeLibraryTests
{
	[TestClass]
	public class CellTest

	{
		// Constructor tests
		[TestMethod]
		public void Cell_NewCell_InitializesRowAndCol()
		{
			var cell = new Cell(5, 5);
			Assert.IsTrue(cell.Row == 5);
			Assert.IsTrue(cell.Col == 5);
		}

		[TestMethod]
		public void Cell_NewCell_InitializesCurrentState()
		{
			var cell = new Cell(5, 5);
			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void Cell_NewCell_InitializesStateCharacter()
		{
			var cell = new Cell(5, 5);
			Assert.IsTrue(cell.StateChar == 'o');
		}

		// GetNextState tests

		[TestMethod]
		public void GetNextState_DeadCell_0LiveNeighbours_Dies()
		{
			var cell = CellTestHelper.MakeDeadCell();

			List<State> zeroNeighbours = CellTestHelper.CreateConwayNeighboursList(0);
			CellTestHelper.EvolveCell(cell, zeroNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_0LiveNeighbours_Dies()
		{
			var cell = CellTestHelper.MakeLiveCell();

			List<State> zeroNeighbours = CellTestHelper.CreateConwayNeighboursList(0);
			CellTestHelper.EvolveCell(cell, zeroNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_DeadCell_1LiveNeighbours_StaysDead()
		{
			var cell = CellTestHelper.MakeDeadCell();

			List<State> oneNeighbour = CellTestHelper.CreateConwayNeighboursList(1);
			CellTestHelper.EvolveCell(cell, oneNeighbour);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_1LiveNeighbours_Dies()
		{
			var cell = CellTestHelper.MakeLiveCell();

			List<State> oneNeighbour = CellTestHelper.CreateConwayNeighboursList(1);
			CellTestHelper.EvolveCell(cell, oneNeighbour);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_DeadCell_2LiveNeighbours_StaysDead()
		{
			var cell = CellTestHelper.MakeDeadCell();

			List<State> twoNeighbours = CellTestHelper.CreateConwayNeighboursList(2);

			CellTestHelper.EvolveCell(cell, twoNeighbours); 
			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_2LiveNeighbours_StaysAlive()
		{
			var cell = CellTestHelper.MakeLiveCell();

			List<State> twoNeighbours = CellTestHelper.CreateConwayNeighboursList(2);
			CellTestHelper.EvolveCell(cell, twoNeighbours);

			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_DeadCell_3LiveNeighbours_BecomesAlive()
		{
			var cell = CellTestHelper.MakeDeadCell();

			List<State> threeNeighbours = CellTestHelper.CreateConwayNeighboursList(3);
			CellTestHelper.EvolveCell(cell, threeNeighbours);

			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_LiveCell_3LiveNeighbours_StaysAlive()
		{
			var cell = CellTestHelper.MakeLiveCell();

			List<State> threeNeighbours = CellTestHelper.CreateConwayNeighboursList(3);
			CellTestHelper.EvolveCell(cell, threeNeighbours);

			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_DeadCell_4LiveNeighbours_Dies()
		{
			var cell = CellTestHelper.MakeDeadCell();

			List<State> fourNeighbours = CellTestHelper.CreateConwayNeighboursList(4);
			CellTestHelper.EvolveCell(cell, fourNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_4LiveNeighbours_Dies()
		{
			var cell = CellTestHelper.MakeLiveCell();

			List<State> fourNeighbours = CellTestHelper.CreateConwayNeighboursList(4);
			CellTestHelper.EvolveCell(cell, fourNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_DeadCell_9LiveNeighbours_Dies()
		{
			var cell = CellTestHelper.MakeDeadCell();

			List<State> fourNeighbours = CellTestHelper.CreateConwayNeighboursList(9);
			CellTestHelper.EvolveCell(cell, fourNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_9LiveNeighbours_Dies()
		{
			var cell = CellTestHelper.MakeLiveCell();

			List<State> nineNeighbours = CellTestHelper.CreateConwayNeighboursList(9);
			CellTestHelper.EvolveCell(cell, nineNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}
	}
}
