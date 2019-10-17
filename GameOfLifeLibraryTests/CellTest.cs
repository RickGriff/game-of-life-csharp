using System;
using System.Collections.Generic;
using GameOfLifeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeLibraryTests
{
	public static class CellTestHelper
	{
		public static List<State> CreateConwayNeighboursList(int liveStates)
		{
			var states = new List<State>();
			var deadStates = 9 - liveStates;

			if (deadStates + liveStates != 9)
			{
				throw new ArgumentOutOfRangeException( "Length of neighbour states list should be >= 0 and <= 9" );
			}

			for (int i = 0; i < liveStates; i++) { states.Add(State.ALIVE); }

			for (int i = 0; i < deadStates; i++) { states.Add(State.DEAD); }

			return states;
		}

		public static List<State> CreateRGBNeighboursList(int redStates, int greenStates, int blueStates)
		{
			var states = new List<State>();
			var deadStates = 9 - ( redStates + greenStates + blueStates);

			if (deadStates + redStates + greenStates + blueStates != 9 )
			{
				throw new ArgumentOutOfRangeException("Length of neighbour states list should be >= 0 and <= 9");
			}

			for (int i = 0; i < redStates; i++) { states.Add(State.RED); }

			for (int i = 0; i < greenStates; i++) { states.Add(State.GREEN); }

			for (int i = 0; i < blueStates; i++) { states.Add(State.BLUE); }

			for (int i = 0; i < deadStates; i++) { states.Add(State.DEAD); }

			return states;
		}

		public static void EvolveCell(Cell cell, List<State> neighbours)
		{
			cell.GetNextState(neighbours);
			cell.UpdateState();
		}
	}


	[TestClass]
	public class CellTest

	{
		// Helper Methods

		public Cell MakeDeadCell()
		{
			var cell = new Cell(1, 1);
			cell.CurrentState = State.DEAD;
			return cell;
		}

		public Cell MakeLiveCell()
		{
			var cell = new Cell(1, 1);
			cell.CurrentState = State.ALIVE;
			return cell;
		}

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
			var cell = MakeDeadCell();

			List<State> zeroNeighbours = CellTestHelper.CreateConwayNeighboursList(0);
			CellTestHelper.EvolveCell(cell, zeroNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_0LiveNeighbours_Dies()
		{
			var cell = MakeLiveCell();

			List<State> zeroNeighbours = CellTestHelper.CreateConwayNeighboursList(0);
			CellTestHelper.EvolveCell(cell, zeroNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_DeadCell_1LiveNeighbours_StaysDead()
		{
			var cell = MakeDeadCell();

			List<State> oneNeighbour = CellTestHelper.CreateConwayNeighboursList(1);
			CellTestHelper.EvolveCell(cell, oneNeighbour);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_1LiveNeighbours_Dies()
		{
			var cell = MakeLiveCell();

			List<State> oneNeighbour = CellTestHelper.CreateConwayNeighboursList(1);
			CellTestHelper.EvolveCell(cell, oneNeighbour);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_DeadCell_2LiveNeighbours_StaysDead()
		{
			var cell = MakeDeadCell();

			List<State> twoNeighbours = CellTestHelper.CreateConwayNeighboursList(2);

			CellTestHelper.EvolveCell(cell, twoNeighbours); 
			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_2LiveNeighbours_StaysAlive()
		{
			var cell = MakeLiveCell();

			List<State> twoNeighbours = CellTestHelper.CreateConwayNeighboursList(2);
			CellTestHelper.EvolveCell(cell, twoNeighbours);

			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_DeadCell_3LiveNeighbours_BecomesAlive()
		{
			var cell = MakeDeadCell();

			List<State> threeNeighbours = CellTestHelper.CreateConwayNeighboursList(3);
			CellTestHelper.EvolveCell(cell, threeNeighbours);

			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_LiveCell_3LiveNeighbours_StaysAlive()
		{
			var cell = MakeLiveCell();

			List<State> threeNeighbours = CellTestHelper.CreateConwayNeighboursList(3);
			CellTestHelper.EvolveCell(cell, threeNeighbours);

			Assert.IsTrue(cell.CurrentState == State.ALIVE);
		}

		[TestMethod]
		public void GetNextState_DeadCell_4LiveNeighbours_Dies()
		{
			var cell = MakeDeadCell();

			List<State> fourNeighbours = CellTestHelper.CreateConwayNeighboursList(4);
			CellTestHelper.EvolveCell(cell, fourNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_4LiveNeighbours_Dies()
		{
			var cell = MakeLiveCell();

			List<State> fourNeighbours = CellTestHelper.CreateConwayNeighboursList(4);
			CellTestHelper.EvolveCell(cell, fourNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_DeadCell_9LiveNeighbours_Dies()
		{
			var cell = MakeDeadCell();

			List<State> fourNeighbours = CellTestHelper.CreateConwayNeighboursList(9);
			CellTestHelper.EvolveCell(cell, fourNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}

		[TestMethod]
		public void GetNextState_LiveCell_9LiveNeighbours_Dies()
		{
			var cell = MakeLiveCell();

			List<State> nineNeighbours = CellTestHelper.CreateConwayNeighboursList(9);
			CellTestHelper.EvolveCell(cell, nineNeighbours);

			Assert.IsTrue(cell.CurrentState == State.DEAD);
		}
	}
}
