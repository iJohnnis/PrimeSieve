// ---------------------------------------------------------------------------
// PrimeCS.cs : Dave's Garage Prime Sieve in C#
// ---------------------------------------------------------------------------

using System;
using System.Diagnostics;

using BenchmarkDotNet.Running;

using static PrimeSieveCS.Utils;

namespace PrimeSieveCS {
    public class MainC {
		static void Main() {
            //BenchmarkRunner.Run<Benchmarks>();
            RunForSeconds(5, 1e6);
            //RunOnce((int)1e8);
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

		public static void RunOnce(double len) {
			var primaryStopwatch = Stopwatch.StartNew();
			PrimeSieveBitArray sieve = new ((int)len);
			sieve.RunSieve();
			int primesCount = sieve.PrimesCount();
			primaryStopwatch.Stop();


			var colors = new[] {
				ConsoleColor.Gray,
				ConsoleColor.DarkGreen,
				ConsoleColor.Red,
			};

			if (PrimeSieveBitArray.Facts.TryGetValue((int)len, out int expected))
				WriteLine("Expected primes   : ", expected);
            WriteLine("Counted primes    : ", primesCount);
			WriteLineSeperator();
			WriteLine("Validation-Result : ", sieve.IsValid(), colors[(int)sieve.IsValid()]);
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
