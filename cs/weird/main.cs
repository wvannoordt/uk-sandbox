using System;

public static class Program
{
	public static void Main(string[] args)
	{
		ListEnum1();
	}
	private static void ListEnum1()
	{
		//Loop terminates, and should.
		ConsoleColor current = (ConsoleColor)0;
		bool end = current.ToString() == 0.ToString();
		for (int i = 0; !end; i++)
		{
			current = (ConsoleColor)i;
			end = current.ToString() == i.ToString();
			Console.WriteLine("enum:" + current + ", i:" + i + ", condition:" + !end);
			Console.ReadLine();
		}
	}
	private static void ListEnum2()
	{
		//Loop never terminates, but should!
		ConsoleColor current = (ConsoleColor)0;
		for (int i = 0; (current.ToString() != i.ToString()); i++)
		{
			current = (ConsoleColor)i;
			Console.WriteLine("enum:" + current + ", i:" + i + ", condition:" + (current.ToString() != i.ToString()));
			Console.ReadLine();
		}
	}
}
