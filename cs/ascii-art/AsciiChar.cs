using System;

namespace asciiart
{
	public static class AsciiChar
	{
		private static readonly int SPACE_COUNT = 2;
		public static void WriteChar(char subject, char to_print)
		{
			switch (subject)
			{
				case 'A':
				{
					Console.WriteLine("    ###     ");
					Console.WriteLine("   #####    ");
					Console.WriteLine("  ##   ##   ");
					Console.WriteLine(" #########  ");
					Console.WriteLine(" ##     ##  ");
					Console.WriteLine("##       ## ");
					break;
				}
				default:
				{
					ConsoleColor pre = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine();
					Console.WriteLine("Error: char " + subject + " not implemented.");
					Console.WriteLine("THIS IS STUPID. USE MATLAB");
					Console.ForegroundColor = pre;
					break;
				}
			}
		}
	}
}