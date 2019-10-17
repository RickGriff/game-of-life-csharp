using System;
using System.Collections.Generic;
using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeLibraryTests
{
	[TestClass]
	public class ViralCellTest
	{ 

		// Helper methods
		public Cell MakeDeadViralCell()
		{
			var cell = new ViralCell(1, 1);
			cell.CurrentState = State.DEAD;
			return cell;
		}

		public Cell MakeLiveViralCell()
		{
			var cell = new ViralCell(1, 1);
			cell.CurrentState = State.ALIVE;
			return cell;
		}

		// GetNextState  - Dead Cell
		[TestMethod]

		public void GetNextState_DeadCell_0LiveNeighbours_StaysDead()
		{
			var cell = MakeDeadViralCell();
			List<State> zeroNeighbours = CellTestHelper.CreateConwayNeighboursList(0);

			CellTestHelper.EvolveCell(cell, zeroNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_DeadCell_6LiveNeighbours_StaysDead()
		{
			var cell = MakeDeadViralCell();
			List<State> sixNeighbours = CellTestHelper.CreateConwayNeighboursList(6);

			CellTestHelper.EvolveCell(cell, sixNeighbours);
			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_DeadCell_7LiveNeighbours_BecomesAlive()
		{
			var cell = MakeDeadViralCell();
			List<State> sevenNeighbours = CellTestHelper.CreateConwayNeighboursList(7);

			CellTestHelper.EvolveCell(cell, sevenNeighbours);
			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_DeadCell_9LiveNeighbours_BecomesAlive()
		{
			var cell = MakeDeadViralCell();
			List<State> nineNeighbours = CellTestHelper.CreateConwayNeighboursList(9);

			CellTestHelper.EvolveCell(cell, nineNeighbours);
			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		//GetNextState - LiveCell

		[TestMethod]
		public void GetNextState_LiveCell_0LiveNeighbours_StaysAlive()
		{
			var cell = MakeLiveViralCell();
			List<State> zeroNeighbours = CellTestHelper.CreateConwayNeighboursList(0);

			CellTestHelper.EvolveCell(cell, zeroNeighbours);
			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_LiveCell_6LiveNeighbours_StaysAlive()
		{
			var cell = MakeLiveViralCell();
			List<State> sixNeighbours = CellTestHelper.CreateConwayNeighboursList(6);

			CellTestHelper.EvolveCell(cell, sixNeighbours);
			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_LiveCell_7_LiveNeighbours_StaysAlive()
		{
			var cell = MakeLiveViralCell();
			List<State> sevenNeighbours = CellTestHelper.CreateConwayNeighboursList(7);

			CellTestHelper.EvolveCell(cell, sevenNeighbours);
			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_LiveCell_8LiveNeighbours_StaysAlive()
		{
			var cell = MakeLiveViralCell(); ;
			List<State> eightNeighbours = CellTestHelper.CreateConwayNeighboursList(8);

			CellTestHelper.EvolveCell(cell, eightNeighbours);
			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}
	}
}
