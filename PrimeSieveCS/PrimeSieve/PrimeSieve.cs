// ---------------------------------------------------------------------------
// PrimeCS.cs : Dave's Garage Prime Sieve in C#
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace PrimeSieveCS {
    /// <summary> Optimized for multiple runs. </summary>
    public class PrimeSieve {
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

        readonly int sieveSize = 0;
        readonly bool[] NoPrimes;

        public PrimeSieve(int size) {
            sieveSize = size;
            // It is initialized to false
            // initializing it to true will have
            // a very high extra cost,
            // since the false initialization happens anyway,
            // it seems unfair against c++.
            NoPrimes = new bool[size];
        }

        public int PrimesCount() {
            int count = 1; // The 2
            for (int i = 3; i < NoPrimes.Length; i+=2)
                if (!NoPrimes[i])
                    count++;
            return count;
        }

        public ValidationResult IsValid() => HistoricalData.TryGetValue(sieveSize, out int ival) 
            ? (ival == PrimesCount() ? ValidationResult.Valid : ValidationResult.Invalid)
            : ValidationResult.Unkown;

        /// Calculate the primes up to the specified limit
        public void RunSieve() {
            int factor = 3;
            int sqrt = (int)Math.Sqrt(sieveSize);

            // Search until the sqrt of the maximum number
            while (factor <= sqrt) {
                // Search for the next potential prime number skipping the even.
                for (int n = factor; n <= sieveSize; n+=2) {
                    if (!NoPrimes[n]) {
                        factor = n;
                        break;
                    }
                }
                // Mark all the multiples of the number as not primes
                for (int m = factor * factor; m < sieveSize; m += factor*2)
                    NoPrimes[m] = true;


                factor += 2;
            }
        }

        public void WriteResults(string fsPath) {
            using var fs = File.Create(fsPath);
            WriteResults(fs);
        }
        public void WriteResults(Stream stream) {
            var sw = new StreamWriter(stream);
            sw.Write("2, 3, ");
            for (int i = 5; i < sieveSize; i+=2) {
                if (!NoPrimes[i]) {
                    sw.Write(i);
                    sw.Write(", ");
                }
            }
            sw.WriteLine();
        }

        public void WriteResults(double elapsedSeconds, int passes) {
            Utils.WriteLineSeperator();
            Utils.WriteLine("Passes             : ", passes);
            Utils.WriteLine("Elapsed            : ", elapsedSeconds.ToString("n"), " s");
            Utils.WriteLine("Rate               : ", (passes/elapsedSeconds/1000).ToString("n"), " kops/s");
            Utils.WriteLineSeperator();
            Utils.WriteLine("Sieve-Size         : ", sieveSize);
            Utils.WriteLine("Primes Counted     : ", PrimesCount());
            Utils.WriteLine("Validation-Results : ", IsValid(), IsValid() switch {
                ValidationResult.Unkown => ConsoleColor.Gray,
                ValidationResult.Valid => ConsoleColor.DarkGreen,
                ValidationResult.Invalid => ConsoleColor.Red,
                _ => throw new Exception("Invalid validation results.")
            });
            Utils.WriteLineSeperator();
        }
    }
}
