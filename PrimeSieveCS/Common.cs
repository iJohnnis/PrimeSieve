using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeSieveCS
{
    public static class Common {
        public static readonly string Seperator = new('-', 80);

		// Historical data for validating our results - the number of primes to be found under some limit, such as 168 primes under 1000
		public static readonly Dictionary<long, int> HistoricalData = new() {
			[(long)1e01] = 4,
			[(long)1e02] = 25,
			[(long)1e03] = 168,
			[(long)1e04] = 1229,
			[(long)1e05] = 9592,
			[(long)1e06] = 78498,
			[(long)1e07] = 664579,
			[(long)1e08] = 5761455,
			[(long)1e09] = 50847534
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
