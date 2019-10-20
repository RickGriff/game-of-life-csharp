using System;
using System.Collections.Generic;

namespace GameOfLifeLibrary
{
	public enum State { DEAD, ALIVE, RED, GREEN, BLUE };
	public class Cell
	{
		public State CurrentState { get; set; }
		public int Row { get; private set; }
		public int Col { get; private set; }
		public char StateChar { get; private set; }
		private State nextState;

		public Cell(int row, int col)
		{
			Row = row;
			Col = col;
			CurrentState = State.DEAD;
			StateChar = StateToChar(CurrentState);
		}
		public virtual char StateToChar(State state)
		{
			var charMap = new Dictionary<State, char>
			{
				{ State.ALIVE, 'X'},
				{ State.DEAD, 'o'}
			};
			return charMap[state];
		}

		internal virtual State ComputeState(Dictionary<State, int> neighbourCounts)
		{  // Conway 'Game of Life' Rules
			int liveNeighbourCells = neighbourCounts.ContainsKey(State.ALIVE) ? neighbourCounts[State.ALIVE] : 0;

			if (liveNeighbourCells == 2)
			{
				return CurrentState;
			}
			else if (liveNeighbourCells == 3)
			{
				return State.ALIVE;
			}
			else
			{
				return State.DEAD;
			}
		}

		public void GetNextState(List<State> neighbourStates)
		{
			var neighbourCounts = new Dictionary<State, int>();

			foreach (var state in neighbourStates)
			{
				neighbourCounts[state] = neighbourCounts.ContainsKey(state) ? neighbourCounts[state] += 1 : 1;
			}

			nextState = ComputeState(neighbourCounts);
		}
		public void UpdateState()
		{
			CurrentState = nextState;
			StateChar = StateToChar(CurrentState);
		}
	}

	public class ViralCell : Cell
	{
		public ViralCell(int row, int col) : base(row, col) { }

		internal override State ComputeState(Dictionary<State, int> neighbourCounts)
		{
			// 'Viral spread' rules
			int liveNeighbourCells = neighbourCounts.ContainsKey(State.ALIVE) ? neighbourCounts[State.ALIVE] : 0;

			if (liveNeighbourCells >= 7) { return State.ALIVE; }

			return CurrentState;
		}
	}

	public class RGBCell : Cell
	{
		public RGBCell(int row, int col) : base(row, col) { }

		private State RandomChoice(List<State> maxStates)
		{
			var rand = new Random();
			int len = maxStates.Count;
			int choice = rand.Next(0, len);
			return maxStates[choice];
		}
		internal State GetLargestNeighbour(Dictionary<State, int> neighbourCounts)
		{
			/* 'Largest Neighbour' rules - cell takes the state of the largest neighbouring population.
			Tiebreak by random selection. */
			int max = 0;
			var maxStates = new List<State>();

			foreach (var pair in neighbourCounts)
			{
				if (pair.Key == State.DEAD)
				{
					continue;  // Dead neighbours don't influence the cell
				}
				if (pair.Value > max)
				{
					maxStates.Clear();
					max = pair.Value;
					maxStates.Add(pair.Key);
				}
				else if (pair.Value == max)
				{
					maxStates.Add(pair.Key);
				}
			}

			State outputState;
			outputState = maxStates.Count >= 1 ? RandomChoice(maxStates) : CurrentState;
			return outputState;
		}

		internal override State ComputeState(Dictionary<State, int> neighbourCounts)
		{
			return GetLargestNeighbour(neighbourCounts);
		}

		public override char StateToChar(State state)
		{
			var charMap = new Dictionary<State, char>
			{
				{ State.ALIVE, 'X' },
				{ State.RED, 'R' },
				{ State.GREEN, 'G' },
				{ State.BLUE, 'B' },
				{ State.DEAD, 'o' },
			};
			return charMap[state];
		}
	}

	public class CyclicRGBCell : RGBCell
	{
		public CyclicRGBCell(int row, int col) : base(row, col) { }
		internal override State ComputeState(Dictionary<State, int> neighbourCounts)
		{
			/* Cyclical eating rules: Red eats Green eats Blue eats Red.
			If cell has colour and >=3 neighbours with it's predator colour, it takes the predator's colour.
			If cell is dead, it just takes largest neighbour's colour */

			var preyToPredatorMap = new Dictionary<State, State>
			{
				{ State.GREEN, State.RED},
				{ State.RED, State.BLUE},
				{ State.BLUE, State.GREEN }
			};

			if (CurrentState == State.DEAD) { return GetLargestNeighbour(neighbourCounts); }

			State predator = preyToPredatorMap[CurrentState];

			if (neighbourCounts.ContainsKey(predator) && (neighbourCounts[predator] >= 3))
			{
				return predator;
			}
			else
			{
				return CurrentState;
			}
		}
	}
}


