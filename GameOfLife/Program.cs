using System;
using System.Collections.Generic;
using System.Drawing;
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

			Point[] initialCells = {
				new Point(4, 4),
				new Point(4, 5),
				new Point(4, 6),
				new Point(5, 3),
				new Point(5, 4),
				new Point(5, 5),
			};
			firstGrid.SetInitialCells(initialCells);
			firstGrid.Display();
			firstGrid.Cycle();
			firstGrid.Cycle();
			firstGrid.Cycle();
		}
	}
}
