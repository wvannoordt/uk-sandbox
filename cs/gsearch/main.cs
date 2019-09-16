using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace gsearch
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				bool add_lucky = args[0] == "-l";
				if (add_lucky) args = shift(args);
				if (args.Length > 0)
				{
					string searchquery = connect(args);
					string googlestuff = formatsearch(searchquery, add_lucky);
					Process.Start("google-chrome", googlestuff);
				}
				else
				{
					Console.WriteLine("No search query passed.");
				}
			}
			else
			{
				Console.WriteLine("No search query passed.");
			}
		}
		private static string[] shift(string[] stuff)
		{
			if (stuff.Length == 0) return stuff;
			string[] output = new string[stuff.Length - 1];
			for (int i = 0; i < output.Length; i++)
			{
				output[i] = stuff[i+1];
			}
			return output;
		}
		private static string connect(string[] stuff)
		{
			string output = stuff.Length > 0 ? stuff[0] : string.Empty;
			for (int i = 1; i < stuff.Length; i++)
			{
				output += " " + stuff[i]; 
			}
			return output;
		}
		private static string formatsearch(string a, bool add_lucky)
		{
			string output = string.Empty;
			foreach(char i in a)
			{
				output += i == ' ' ? '+' : i;
			}
			return "https://www.google.com/search?q=" + output + (add_lucky ? "%s&btnI=Im+Feeling+Lucky" : string.Empty);
		}
	}
}
