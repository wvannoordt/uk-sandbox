using System;
using System.IO;

namespace p2dreader
{
	public class Program
	{
		public static int Main(string[] args)
		{
			try
			{
				P2D grid = P2D.FromCsv("input_grids/mesh197x99.csv", 197, 99, P2DReadMode.LOAD);
				//grid.ExtendRight(4);
				//grid.ExtendLeft(4);
				//grid.ExtendUp(4);
				grid.SaveAs("./output_grids/bump-grid-modified-197x99.x");
				return 0;
			}
			catch (Exception e)
			{
				PrintError(e.Message);
				return 1;
			}
		}
		public static void PrintError(string message)
		{
			ConsoleColor pre = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			Console.ForegroundColor = pre;
		}
	}
}
