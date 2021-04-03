using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeSieveCS
{
    public static class Utils {
        public static readonly string Seperator = new string('-', 80);

		public static void WriteLineSeperator() => Console.WriteLine(Seperator);

		public static ConsoleColor EmpaticColor { get; set; } = ConsoleColor.DarkCyan;
		public static void WriteLine(params object[] p) {
			int length = p.Length;
			if (length == 0) return;
			if (p[length-1] is not ConsoleColor color)
				color = EmpaticColor;
			else length--;

			var oColor = Console.ForegroundColor;

			Console.Write(p[0]);
			for (int i = 1; i < length; i++) {
				Console.ForegroundColor = (i%2 == 0) ? oColor : color;
				Console.Write(p[i].ToString());
			}

			Console.ForegroundColor = oColor;
			Console.WriteLine();
		}
	}
}
