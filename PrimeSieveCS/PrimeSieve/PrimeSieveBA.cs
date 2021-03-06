// ---------------------------------------------------------------------------
// PrimeCS.cs : Dave's Garage Prime Sieve in C#
// ---------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using static PrimeSieveCS.Common;

namespace PrimeSieveCS {
    /// <summary> Optimized for memory usage. </summary>
    public class PrimeSieveBA {
        readonly int sieveSize = 0;
        readonly BitArray Primes;

        public PrimeSieveBA(int size) {
            sieveSize = size;
            Primes = new BitArray(size/2, true);
        }


        int primesCount = -1;
        public int PrimesCount() {
            if (primesCount > -1) return primesCount;
            int count = 0;
            for (int i = 0; i < Primes.Length; i++)
                if (Primes[i])
                    count++;
            return primesCount = count;
        }

        public ValidationResult ValidationResult() => HistoricalData.TryGetValue(sieveSize, out int ival) 
            ? (ival == PrimesCount() ? PrimeSieveCS.ValidationResult.Valid : PrimeSieveCS.ValidationResult.Invalid)
            : PrimeSieveCS.ValidationResult.Unkown;

        /// Calculate the primes up to the specified limit
        public void RunSieve() {
            int factor = 3;
            int sqrt = (int)Math.Sqrt(sieveSize);

            // Search until the sqrt of the maximum number
            while (factor <= sqrt) {
                // Search for the next potential prime number skipping the even.
                for (int n = factor; n <= sieveSize; n+=2) {
                    if (Primes[n/2]) {
                        factor = n;
                        break;
                    }
                }
                // Mark all the multiples of the number as not primes
                for (int m = factor * factor; m < sieveSize; m += factor*2)
                    Primes[m/2] = false;


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
                if (Primes[i]) {
                    sw.Write(i);
                    sw.Write(", ");
                }
            }
            sw.WriteLine();
        }

        public void PrintResults(double elapsedSeconds, int passes) => Console.WriteLine(
            $"Average: {passes/elapsedSeconds:n} p/s, " +
            $"Passes: {passes}, Elapsed: {elapsedSeconds:n}s, " +
            $"Sieve-Size: {sieveSize}, Primes Found: {PrimesCount()}, " +
            $"Validation-Results: {ValidationResult()}");
    }
}
