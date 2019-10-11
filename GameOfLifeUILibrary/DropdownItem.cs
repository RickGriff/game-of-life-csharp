using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeUILibrary
{
	public enum Game { CONWAY, LARGEST_NEIGHBOUR, CYCLIC_EATING };

	public class DropdownItem
		{ 
			public Game Game { get; set; }
			public string Text { get; set; }
		}
  
}
