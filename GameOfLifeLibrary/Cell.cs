using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameOfLifeLibrary
{
    public class Cell
    {
		public static void PrintGrid()
		{
			int[] a = { 1, 2, 3, 4, 5 };
			int[] b = { 1, 2, 3, 4, 5 };
			int[] c = { 1, 2, 3, 4, 5 };
			int[] d = { 1, 2, 3, 4, 5 };
			int[] e = { 1, 2, 3, 4, 5 };

			var arrays = new int[][] { a, b, c, d, e };
			int rowNum = 1;
			foreach (var arr in arrays)
			{
				Console.WriteLine($" {rowNum}. [ {string.Join(" ", arr)} ]");
				rowNum += 1;
			}
			
		}
    }
}
