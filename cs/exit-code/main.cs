using System;

public static class Program
{
	public static int Main(string[] args)
	{
		int a = 0;
		int b = 1;
		bool condition = a > b;
		if (!condition)
		{
			return 15;
		}
		else
		{
			return 13;
		} 
	}
}
