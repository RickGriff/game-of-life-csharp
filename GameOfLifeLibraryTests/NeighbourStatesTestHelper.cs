using GameOfLifeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLifeLibraryTests
{
	public static class NeighbourStatesTestHelper
	{
		public static Grid MakeFilled4x4RGBGrid()
		{
			var grid = new RGBGrid(4, false);
			SetAlternatingStates(grid);
			return grid;
		}

		private static void SetAlternatingStates(Grid grid)
		{
			grid.Data[0, 0].CurrentState = State.DEAD;
			grid.Data[0, 1].CurrentState = State.RED;
			grid.Data[0, 2].CurrentState = State.GREEN;
			grid.Data[0, 3].CurrentState = State.BLUE;

			grid.Data[1, 0].CurrentState = State.DEAD;
			grid.Data[1, 1].CurrentState = State.RED;
			grid.Data[1, 2].CurrentState = State.GREEN;
			grid.Data[1, 3].CurrentState = State.BLUE;

			grid.Data[2, 0].CurrentState = State.DEAD;
			grid.Data[2, 1].CurrentState = State.RED;
			grid.Data[2, 2].CurrentState = State.GREEN;
			grid.Data[2, 3].CurrentState = State.BLUE;

			grid.Data[3, 0].CurrentState = State.DEAD;
			grid.Data[3, 1].CurrentState = State.RED;
			grid.Data[3, 2].CurrentState = State.GREEN;
			grid.Data[3, 3].CurrentState = State.BLUE;
		}

		public static Dictionary<State, int> GetStateCountsDict(List<State> statesList)
		{
			var groupedStates = statesList.GroupBy(state => state);

			Dictionary<State, int> stateCounts = groupedStates.ToDictionary(group => group.Key, group => group.Count());

			return stateCounts;
		}

		public static bool DictsAreEqual(Dictionary<State, int> dict1, Dictionary<State, int> dict2)
		{
			// Dicts are 'equal' if same length, and there is no set difference between the kv-pair sequences
			bool areEqual = (dict1.Count == dict2.Count) && (dict1.Except(dict2).Any() == false);
			return areEqual;
		}

		public static void PrintDict<T, U>(Dictionary<T, U> dict)
		{
			Console.WriteLine("Dictionary keys and values:");
			foreach (var pair in dict)
			{
				Console.WriteLine($"Key: {pair.Key.ToString()} Value: {pair.Value.ToString()}");
			}
		}
	}
}
