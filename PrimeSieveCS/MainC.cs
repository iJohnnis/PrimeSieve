// ---------------------------------------------------------------------------
// PrimeCS.cs : Dave's Garage Prime Sieve in C#
// ---------------------------------------------------------------------------

using System;
using System.Diagnostics;

using BenchmarkDotNet.Running;

using static PrimeSieveCS.Common;

namespace PrimeSieveCS {
    public class MainC {
		static void Main() {
            BenchmarkRunner.Run<Benchmarks>();
            //RunForSeconds(10, 1e6);
            //RunTimes((int)1e8);
            //DavesTest();
        }

		public static void RunForSeconds(double duration=5, double sieve_size=1e6) {
            var primaryStopwatch = Stopwatch.StartNew();
			int sieveSize = (int)sieve_size;

			PrimeSieve sieve = null;
			int passesCount = 0;
			while(primaryStopwatch.Elapsed.TotalSeconds < duration) {
				sieve = new PrimeSieve(sieveSize);
				sieve.RunSieve();
				passesCount++;
            }
			sieve.WriteResults(primaryStopwatch.Elapsed.TotalSeconds, passesCount);
        }

		public static void RunTimes(double len, int times = 1) {
			var primaryStopwatch = Stopwatch.StartNew();

			PrimeSieveBA sieve = null;
			int primesCount = -1;
            while (times-- > 0) {
				sieve = new((int)len);
				sieve.RunSieve();
				primesCount = sieve.PrimesCount();
			}
			var isValid = sieve.ValidationResult();

			primaryStopwatch.Stop();

			WriteLine("Expected primes   : ", HistoricalData.TryGetValue((int)len, out int e) 
				? e.ToString() : "unknown");
            WriteLine("Counted primes    : ", primesCount);
			WriteLineSeperator();
			WriteLine("Validation-Result : ", isValid, isValid switch {
				ValidationResult.Unkown => ConsoleColor.DarkGray,
				ValidationResult.Valid => ConsoleColor.DarkGreen,
				ValidationResult.Invalid => ConsoleColor.Red,
				_ => throw new Exception("Invalid validation result!")
			});
			WriteLineSeperator();
			WriteLine("Elapsed           : ", primaryStopwatch.ElapsedMilliseconds, " ms.", ConsoleColor.DarkRed);
			WriteLineSeperator();
		}

		public static void DavesTest() {
			var tStart = DateTime.UtcNow;
			var passes = 0;
			PrimeSieveDaves sieve = null;

			while ((DateTime.UtcNow - tStart).TotalSeconds < 5) {
				sieve = new PrimeSieveDaves(1000000);
				sieve.runSieve();
				passes++;
			}

			var tD = DateTime.UtcNow - tStart;
			if (sieve != null)
				sieve.printResults(false, tD.TotalSeconds, passes);
		}
	}
}
