using System;
using System.Collections.Generic;

namespace blocks
{
	public class Program
	{
		public static int Main(string[] args)
		{
			int numx, numy;
			if (check_inputs(args, out numx, out numy))
			{
				int cx = numx - 1;
				int cy = numy - 1;
				Console.WriteLine("nx = " + cx + ", ny = " + cy);
				int cells = gcd(cx, cy);
				Console.WriteLine("What about blocks of " + cells + "x" + cells + " cells with " + (cx / cells) + " x-blocks and " + (cy / cells) + " y-blocks?");
				int[] divisors = slowfactor(cells);
				if (divisors.Length > 0)
				{
					Console.WriteLine();
					Console.WriteLine("Alternatively:");
					foreach (int d in divisors)
					{
						Console.WriteLine("Blocks of " + cells / d + "x" + cells / d + " cells with " + ((cx * d) / cells) + " x-blocks and " + ((cy * d) / cells) + " y-blocks");
					}
				}
				return 0;
			}
			else
			{
				Console.WriteLine("Cannot parse input {" + connect(args) + "}.");
				return 1;
			}
		}
		private static int[] slowfactor(int i)
		{
			int to_factor = i > 0 ? i : -i;
			List<int> output_list = new List<int>();
			for (int d = 2; d < to_factor; d++)
			{
				if (to_factor % d == 0) output_list.Add(d);
			}
			return output_list.ToArray();
		}
		private static string connect(string[] stuff)
		{
			if (stuff.Length == 0) return string.Empty;
			string output = stuff[0];
			for (int i = 1; i < stuff.Length; i++)
			{
				output += "," + stuff[i];
			}
			return output;
		}
		private static bool check_inputs(string[] args, out int x_out, out int y_out)
		{
			x_out = 0;
			y_out = 0;
			if (args.Length < 2) return false;
			return int.TryParse(args[0], out x_out) && int.TryParse(args[1], out y_out);
		}
		private static int gcd(int a, int b)
		{
			if((a == 0) || (b == 0)) return 0;
			else if((a < 0) || (b < 0)) return -1;
			if (a < b) swap<int>(ref a, ref b);
			int r;
			while (true)
			{
				r = a % b;
				if(r == 0) return b;
				a = b;
				b = r;
			}
		}
		private static void swap<T>(ref T a, ref T b)
		{
			T med = a;
			a = b;
			b = med;
		}
	}
}
