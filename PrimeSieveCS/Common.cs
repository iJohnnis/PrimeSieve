using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeSieveCS
{
    public static class Common {
        public static readonly string Seperator = new string('-', 80);

		// Historical data for validating our results - the number of primes to be found under some limit, such as 168 primes under 1000
		public static readonly Dictionary<int, int> HistoricalData = new() {
			[10] = 4,
			[25] = 9,
			[100] = 25,
			[1_000] = 168,
			[10_000] = 1229,
			[100_000] = 9592,
			[1_000_000] = 78498,
			[10_000_000] = 664579,
			[100_000_000] = 5761455,
			[1000_000_000] = 50847534
		};

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
