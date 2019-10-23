using GameOfLifeLibrary;
using System;
using System.Collections.Generic;

namespace GameOfLifeLibraryTests
{
	public static class CellTestHelper
	{
		public static List<State> CreateConwayNeighboursList(int liveStatesCount)
		{
			var states = new List<State>();
			int deadStatesCount = 9 - liveStatesCount;

			if (deadStatesCount + liveStatesCount != 9)
			{
				throw new ArgumentOutOfRangeException("Length of neighbour states list should be >= 0 and <= 9");
			}

			for (int i = 0; i < liveStatesCount; i++) { states.Add(State.ALIVE); }

			for (int i = 0; i < deadStatesCount; i++) { states.Add(State.DEAD); }

			return states;
		}

		public static List<State> CreateRGBNeighboursList(int redStatesCount, int greenStatesCount, int blueStatesCount)
		{
			var states = new List<State>();
			var deadStatesCount = 9 - (redStatesCount + greenStatesCount + blueStatesCount);

			if (deadStatesCount + redStatesCount + greenStatesCount + blueStatesCount != 9)
			{
				throw new ArgumentOutOfRangeException("Length of neighbour states list should be >= 0 and <= 9");
			}

			for (int i = 0; i < redStatesCount; i++) { states.Add(State.RED); }

			for (int i = 0; i < greenStatesCount; i++) { states.Add(State.GREEN); }

			for (int i = 0; i < blueStatesCount; i++) { states.Add(State.BLUE); }

			for (int i = 0; i < deadStatesCount; i++) { states.Add(State.DEAD); }

			return states;
		}

		public static void EvolveCell(Cell cell, List<State> neighbours)
		{
			cell.GetNextState(neighbours);
			cell.UpdateState();
		}

		// Conway Cell methods
		public static Cell MakeDeadCell()
		{
			var cell = new Cell(1, 1);
			cell.CurrentState = State.DEAD;
			return cell;
		}

		public static Cell MakeLiveCell()
		{
			var cell = new Cell(1, 1);
			cell.CurrentState = State.ALIVE;
			return cell;
		}

		// Viral Cell methods
		public static Cell MakeDeadViralCell()
		{
			var cell = new ViralCell(1, 1);
			cell.CurrentState = State.DEAD;
			return cell;
		}

		public static Cell MakeLiveViralCell()
		{
			var cell = new ViralCell(1, 1);
			cell.CurrentState = State.ALIVE;
			return cell;
		}

		// RGB Cell methods
		public static Cell MakeDeadRGBCell()
		{
			var cell = new RGBCell(1, 1);
			cell.CurrentState = State.DEAD;
			return cell;
		}
		public static Cell MakeRedRGBCell()
		{
			var cell = new RGBCell(1, 1);
			cell.CurrentState = State.RED;
			return cell;
		}

		public static Cell MakeGreenRGBCell()
		{
			var cell = new RGBCell(1, 1);
			cell.CurrentState = State.GREEN;
			return cell;
		}

		public static Cell MakeBlueRGBCell()
		{
			var cell = new RGBCell(1, 1);
			cell.CurrentState = State.BLUE;
			return cell;
		}

		// Cyclic RGB cell methods
		public static Cell MakeDeadCyclicRGBCell()
		{
			var cell = new CyclicRGBCell(1, 1);
			cell.CurrentState = State.DEAD;
			return cell;
		}
		public static Cell MakeRedCyclicRGBCell()
		{
			var cell = new CyclicRGBCell(1, 1);
			cell.CurrentState = State.RED;
			return cell;
		}

		public static Cell MakeGreenCyclicRGBCell()
		{
			var cell = new CyclicRGBCell(1, 1);
			cell.CurrentState = State.GREEN;
			return cell;
		}

		public static Cell MakeBlueCyclicRGBCell()
		{
			var cell = new CyclicRGBCell(1, 1);
			cell.CurrentState = State.BLUE;
			return cell;
		}
	}
}
