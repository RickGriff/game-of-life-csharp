using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameOfLifeLibrary
{
    public class Cell
    {
		public string State { get; private set; }
		public string NextState { get; private set; }
		public int Row { get; private set; }
		public int Col { get; private set; }

		public Cell( int row, int col )
		{
			Row = row;
			Col = col;
			State = "o";
		}
		string viralSpreadRule(int population)
		{
			if (population >= 3) { return "X"; }
			return State;
		}
		string ComputeState(int population)
		{
			// returns state based on on game-of-life rules
			return viralSpreadRule(population);
		}

		public void GetNextState(List<string> states)
		{
			int population = 0;
			foreach(var state in states)
			{

				if (state == "X")
				{
					population += 1;
				}
			}
			NextState = ComputeState(population);
		}


		void UpdateState()
		{
			State = NextState;
		}

	
    }
}
