using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLibrary { 
	public enum States { DEAD, ALIVE };
    public class Cell
    {
		public States State { get; set; }
		public int Row { get; private set; }
		public int Col { get; private set; }

		private States nextState;

		public Cell( int row, int col )
		{
			Row = row;
			Col = col;
			State = States.DEAD;
		}
		States ViralSpreadRules(int population)
		{
			if (population >= 1) { return States.ALIVE; }
			return State;
		}
		States ConwayRules(int population)
		{
			if (population == 2)
			{
				return State;
			}  
			else if (population == 3)
			{
				return States.ALIVE;
			}
			else 
			{
				return States.DEAD;
			}    
		}
		States ComputeState(int population)
		{
			// returns state based on on game-of-life rules
			return ConwayRules(population);
		}

		public void GetNextState(List<States> states)
		{
			int population = 0;
			foreach(var state in states)
			{
				if (state == States.ALIVE)
				{
					population += 1;
				}
			}
			nextState = ComputeState(population);
		}
		public void UpdateState()
		{
			State = nextState;
		}
    }
}
