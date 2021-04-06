// ---------------------------------------------------------------------------
// PrimeCS.cs : Dave's Garage Prime Sieve in C#
// ---------------------------------------------------------------------------

using System;
using System.IO;

using static PrimeSieveCS.Common;

namespace PrimeSieveCS {
    /// <summary> Optimized for multiple runs. </summary>
    public class PrimeSieve {
        readonly long sieveSize = 0;
        readonly bool[] nonPrimes;

        public PrimeSieve(int size) {
            sieveSize = size;
            // It is initialized to false anyway changing it to true will have a very high extra cost,
            // so I'm keeping track of non-primes instead of primes.
            nonPrimes = new bool[size];
        }

        int _count = -1;
        public int PrimesCount{
            get {
                if (_count > -1) return _count;

                int length = nonPrimes.Length;
                int count = 1; // The 2
                for (int i = 3; i < length; i+=2)
                    if (!nonPrimes[i])
                        count++;

                return _count = count;
            }
        }

        public ValidationResult IsValid() => HistoricalData.TryGetValue(sieveSize, out int ival) 
            ? (ival == PrimesCount ? ValidationResult.Valid : ValidationResult.Invalid)
            : ValidationResult.Unkown;

        /// Calculate the primes up to the specified limit
        public void RunSieve() {
            long factor = 3;
            int sqrt = (int)Math.Sqrt(sieveSize);

            // Search until the sqrt of the maximum number
            while (factor <= sqrt) {
                // Search for the next potential prime number skipping the even.
                for (long n = factor; n <= sieveSize; n+=2) {
                    if (!nonPrimes[n]) {
                        factor = n;
                        break;
                    }
                }
                // Mark all the multiples of the number as not primes
                for (long m = factor * factor; m < sieveSize; m += factor*2)
                    nonPrimes[m] = true;

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
                if (!nonPrimes[i]) {
                    sw.Write(i);
                    sw.Write(", ");
                }
            }
            sw.WriteLine();
        }

        public void WriteResults(double elapsedSeconds, int passes) {
            WriteLineSeperator();
            WriteLine("Passes             : ", passes);
            WriteLine("Elapsed            : ", $"{elapsedSeconds:n}", " s");
            WriteLine("Rate               : ", $"{passes/elapsedSeconds/1000:n}", " kops/s");
            WriteLineSeperator();
            WriteLine("Sieve-Size         : ", $"{sieveSize:N0}");
            WriteLine("Primes Counted     : ", PrimesCount);
            WriteLine("Validation-Results : ", IsValid(), IsValid() switch {
                ValidationResult.Unkown => ConsoleColor.Gray,
                ValidationResult.Valid => ConsoleColor.DarkGreen,
                ValidationResult.Invalid => ConsoleColor.Red,
            });
            WriteLineSeperator();
        }
    }
}
