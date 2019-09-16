using System;
namespace p2dreader
{
	public static class Error
	{
		public static void Kill(object sender, string message)
		{
			throw new Exception("[" + (sender.GetType().Name.ToLower() == "string" ? sender.ToString() : sender.GetType().Name) + "] Error: " + message);
		}
		public static void Warning(object sender, string message)
		{
			ConsoleColor pre = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("[" + (sender.GetType().Name.ToLower() == "string" ? sender.ToString() : sender.GetType().Name) + "] Warning: " + message);
			Console.ForegroundColor = pre;
		}
	}
}
