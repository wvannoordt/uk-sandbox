using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace qkdat
{
	public class Program
	{
		public static int Main(string[] args)
		{
			Console.WriteLine("Calling Python...");
			Process pyth = Process.Start("python", "junk.py");
			pyth.WaitForExit();
			Console.WriteLine("Process exited.");
			return 0;
		}
	}
}
