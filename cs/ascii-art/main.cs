using System;
using System.IO;

namespace asciiart
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			string output = connect(args);
			AsciiArtist artist = new AsciiArtist(output);
			artist.WriteOut();
		}
		private static string connect(string[] stuff)
		{
			string output = string.Empty;
			if (stuff.Length == 0) return output;
			output = stuff[0];
			for (int i = 1; i < stuff.Length; i++)
			{
				output += " " + stuff[i];
			}
			return output;
		}
	}
	public class AsciiArtist
	{
		private string message;
		public AsciiArtist(string _message)
		{
			message = _message;
		}
		public void WriteOut()
		{
			for (int i = 0; i < message.Length; i++)
			{
				AsciiChar.WriteChar(message[i], '#');
			}
		}
	}
}
