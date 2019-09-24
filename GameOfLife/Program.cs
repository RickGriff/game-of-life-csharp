using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameOfLifeLibrary;

namespace GameOfLife
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());

			var firstGrid = new Grid(10);
			firstGrid.Display();

			//int[,] initialCells = new int[,] { { 4, 4 }, { 4, 5 }, { 5, 4 },  { 5, 5 } };
			//Grid.SetInitialCells(initialCells)
			//Grid.PrintGrid();
		}
	}
}
