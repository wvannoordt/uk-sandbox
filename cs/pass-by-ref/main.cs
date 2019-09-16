using System;

public class Program
{
	public static void Main(string[] args)
	{
		int i = 0;
		Console.WriteLine(i);
		increment_a(i);
		Console.WriteLine(i);
	}
	private unsafe static void increment_a(int *a)
	{
		a += 1;
	}
}
