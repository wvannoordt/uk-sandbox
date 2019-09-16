using System;
using System.IO;
using System.Collections.Generic;

namespace p2dreader
{
	public class P2D
	{
		private const long MAX_SIZE = 1000000000;
		private string filename, headerstr;
		private double[,] x,y;
		private int nx, ny;
		
		public P2D(string _filename, P2DReadMode mode = P2DReadMode.LOAD)
		{
			if (!File.Exists(_filename)) Error.Kill(this, "could not find file " + _filename + ".");
			filename = _filename;
			read_init_default(mode);
		}
		
		public P2D(string _filename, string _headerstr, double[,] _x, double[,] _y, int _nx, int _ny)
		{
			filename = _filename;
			headerstr = _headerstr;
			x = _x;
			y = _y;
			nx = _nx;
			ny = _ny;
		}
		
		public static P2D FromCsv(string _filename, int _nx, int _ny, P2DReadMode mode = P2DReadMode.LOAD)
		{
			switch (mode)
			{
				case P2DReadMode.LOAD:
				{
					return read_csv(_filename, _nx, _ny);
				}
				case P2DReadMode.STREAM:
				{
					Error.Kill("P2D", "stream loading not implemented yet.");
					break;
				}
			}
			return null;
		}
		private static P2D read_csv(string _filename, int _nx, int _ny)
		{
			string header = "1       ";
			string[] file_lines = File.ReadAllLines(_filename);
			if (_nx * _ny > file_lines.Length) Error.Kill("P2D", "not enough data.");
			if (_nx * _ny < file_lines.Length) Error.Warning("P2D", "too much data provided, some data my be ignored.");
			double[,] _x = new double[_nx,_ny];
			double[,] _y = new double[_nx,_ny];
			int k = 0;
			int too_many_count = 0;
			for (int iy = 0; iy < _ny; iy++)
			{
				for (int ix = 0; ix < _nx; ix++)
				{
					string line = file_lines[k++];
					string[] linespt = line.Split(',');
					
					if (linespt.Length < 2) Error.Kill("P2D", "not enough data at line " + k + ".");
					if (linespt.Length > 2 && too_many_count < 10) 
					{
						if (too_many_count == 9)
						{
							Error.Warning("P2D", "too many entries provided at line " + k + ". Ignoring further instances.");
						}
						else
						{
							Error.Warning("P2D", "too many entries provided at line " + k + ".");
						}
						too_many_count++;
					}
					if (!double.TryParse(linespt[0], out _x[ix, iy])) Error.Kill("P2D", "cannot parse input at line " + k + ".");
					if (!double.TryParse(linespt[1], out _y[ix, iy])) Error.Kill("P2D", "cannot parse input at line " + k + ".");
				}
			}
			return new P2D(_filename, header, _x, _y, _nx, _ny);
		}
		private void read_init_default(P2DReadMode mode)
		{
			switch (mode)
			{
				case P2DReadMode.LOAD:
				{
					init_all_default_load();
					break;
				}
				case P2DReadMode.STREAM:
				{
					Error.Kill(this, "stream loading not implemented yet.");
					break;
				}
			}
		}
		
		private void init_all_default_load()
		{
			FileInfo info = new FileInfo(filename);
			if (info.Length > MAX_SIZE) Error.Kill(this, "file too big for this read mode. Max size: " + int_commas(MAX_SIZE) + " bytes. Try specifying P2DReadMode.STREAM");
			string[] file_lines = File.ReadAllLines(filename);
			if (file_lines.Length < 3) Error.Kill(this, "not enough data.");
			headerstr = file_lines[0];
			char[] white_space = new char[] {' ', '\t'};
			string[] dims_strings = reduce_whitespace(file_lines[1], white_space, '!').Split('!');
			if (dims_strings.Length < 2) Error.Kill(this, "could not read dimensions.");
			if (!int.TryParse(dims_strings[0], out nx) || !int.TryParse(dims_strings[1], out ny)) Error.Kill(this, "could not read dimensions.");
			List<double> xs = new List<double>();
			List<double> ys = new List<double>();
			int k = 0;
			double current_entry = -1;
			for (int i = 2; i < file_lines.Length; i++)
			{
				string[] entry_strings = reduce_whitespace(file_lines[i], white_space, '!').Split('!');
				for (int j = 0; j < entry_strings.Length; j++)
				{
					if (!double.TryParse(entry_strings[j], out current_entry)) Error.Kill(this, "could not parse entry " + j + " on line " + i + ".");
					if (k >= nx*ny)
					{
						ys.Add(current_entry);
					}
					else
					{
						xs.Add(current_entry);
					}
					k++;
				}
			}
			if (xs.Count != nx*ny || ys.Count != nx*ny) Error.Kill(this, "not enough points in the input file.");
			x = new double[nx, ny];
			y = new double[nx, ny];
			int X_IDX = 0;
			int Y_IDX = 0;
			for (int t = 0; t < xs.Count; t++)
			{
				if (X_IDX == nx)
				{
					X_IDX = 0;
					Y_IDX++;
				}
				x[X_IDX,Y_IDX] = xs[t];
				y[X_IDX,Y_IDX] = ys[t];
				X_IDX++;
			}
		}
		
		public void ExtendRight(int num_cells, double inflation_factor = 1)
		{
			double delta = x[nx - 1, 0] - x[nx - 2, 0];
			double[,] newx = new double[num_cells, ny];
			double[,] newy = new double[num_cells, ny];
			double current_inflation = 1;
			for (int ix = 0; ix < num_cells; ix++)
			{
				for (int iy = 0; iy < ny; iy++)
				{
					newx[ix,iy] = x[nx - 1, 0] + (ix + 1) *current_inflation*delta;
					newy[ix,iy] = y[nx - 1, iy];
				}
				current_inflation*= inflation_factor;
			}
			double[,] totx = new double[nx + num_cells, ny];
			double[,] toty = new double[nx + num_cells, ny];
			for (int ix = 0; ix < nx; ix++)
			{
				for (int iy = 0; iy < ny; iy++)
				{
					totx[ix,iy] = x[ix,iy];
					toty[ix,iy] = y[ix,iy];
				}
			}
			for (int ix = 0; ix < num_cells; ix++)
			{
				for (int iy = 0; iy < ny; iy++)
				{
					totx[ix + nx,iy] = newx[ix,iy];
					toty[ix + nx,iy] = newy[ix,iy];
				}
			}
			x = totx;
			y = toty;
			nx = nx + num_cells;
		}
		public void ExtendLeft(int num_cells, double inflation_factor = 1)
		{
			double delta = x[1, 0] - x[0, 0];
			double[,] newx = new double[num_cells, ny];
			double[,] newy = new double[num_cells, ny];
			double current_inflation = 1;
			for (int ix = 0; ix < num_cells; ix++)
			{
				for (int iy = 0; iy < ny; iy++)
				{
					newx[num_cells - ix - 1,iy] = x[0, 0] - (ix + 1) *current_inflation*delta;
					newy[num_cells - ix - 1,iy] = y[0, iy];
				}
				current_inflation*= inflation_factor;
			}
			double[,] totx = new double[nx + num_cells, ny];
			double[,] toty = new double[nx + num_cells, ny];
			for (int ix = 0; ix < num_cells; ix++)
			{
				for (int iy = 0; iy < ny; iy++)
				{
					totx[ix,iy] = newx[ix,iy];
					toty[ix,iy] = newy[ix,iy];
				}
			}
			for (int ix = 0; ix < nx; ix++)
			{
				for (int iy = 0; iy < ny; iy++)
				{
					totx[ix + num_cells,iy] = x[ix,iy];
					toty[ix + num_cells,iy] = y[ix,iy];
				}
			}
			x = totx;
			y = toty;
			nx = nx + num_cells;
		}
		public void ExtendUp(int num_cells, double inflation_factor = 1)
		{
			double delta = y[0, ny - 1] - y[0, ny - 2];
			double[,] newx = new double[nx, num_cells];
			double[,] newy = new double[nx, num_cells];
			double current_inflation = 1;
			for (int ix = 0; ix < nx; ix++)
			{
				for (int iy = 0; iy < num_cells; iy++)
				{
					newx[ix,iy] = x[ix, ny - 1];
					newy[ix,iy] = y[ix, ny - 1] + (iy + 1) * current_inflation * delta;
				}
				current_inflation*= inflation_factor;
			}
			double[,] totx = new double[nx, ny + num_cells];
			double[,] toty = new double[nx, ny + num_cells];
			for (int ix = 0; ix < nx; ix++)
			{
				for (int iy = 0; iy < num_cells; iy++)
				{
					totx[ix,ny + iy] = newx[ix,iy];
					toty[ix,ny + iy] = newy[ix,iy];
				}
			}
			for (int ix = 0; ix < nx; ix++)
			{
				for (int iy = 0; iy < ny; iy++)
				{
					totx[ix,iy] = x[ix,iy];
					toty[ix,iy] = y[ix,iy];
				}
			}
			x = totx;
			y = toty;
			ny = ny + num_cells;
		}
		
		public void SaveAs(string output_filename)
		{
			string head = headerstr;
			string dims = bufferspace(nx.ToString(), 10) + bufferspace(ny.ToString(), 10);
			List<string> stuff = new List<string>();
			stuff.Add(head);
			stuff.Add(dims);
			//24
			string curstring = "";
			int linecount = 0;
			int q = 0;
			for (int iy = 0; iy < ny; iy++)
			{
				for (int ix = 0; ix < nx; ix++)
				{
					curstring += bufferspace(x[ix,iy].ToString("N15"), 24);
					linecount++;
					q++;
					if (linecount > 2)
					{
						linecount = 0;
						stuff.Add(curstring);
						curstring = "";
					}
				}
			}
			for (int iy = 0; iy < ny; iy++)
			{
				for (int ix = 0; ix < nx; ix++)
				{
					curstring += bufferspace(y[ix,iy].ToString("N15"), 24);
					linecount++;
					q++;
					if (linecount > 2)
					{
						linecount = 0;
						stuff.Add(curstring);
						curstring = "";
					}
				}
			}
			if (!string.IsNullOrEmpty(curstring))
			{
				stuff.Add(curstring);
			}
			File.WriteAllLines(output_filename, stuff.ToArray());
		}
		
		private string int_commas(long a)
		{
			string output = string.Empty;
			string astr = a.ToString();
			for (int i = 0; i < astr.Length; i++)
			{
				output += astr[i];
				if (i == astr.Length - 1) break;
				if (astr.Length % 3 == (i+1) % 3) output +=  ",";
			}
			return output;
		}
		private string reduce_whitespace(string s_in, char[] bufferchar, char replacechar)
		{
			//x       y     z -> x,y,z
			string s = s_in.Trim();
			string output = "";
			bool onspace = false;
			for (int i = 0; i < s.Length; i++)
			{
				//Lead
				if (!onspace && (search(bufferchar, s[i])))
				{
					output += replacechar;
					onspace = true;
				}
				//Fall
				if (onspace && (!search(bufferchar, s[i]))) onspace = false;
				if (!onspace) output += s[i];
			}
			return output.Trim();
		}
		
		private bool search(char[] xs, char y)
		{	
			foreach(char x in xs)
			{
				if (y == x) return true;
			}
			return false;
		}
		
		private string bufferspace(string s, int width)
		{
			string output = string.Empty;
			while (output.Length + s.Length < width)
			{
				output += ' ';
			}
			return output + s;
			
		}
	}
	public enum P2DReadMode
	{
		LOAD,
		STREAM
	}
}
