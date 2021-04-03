using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrimeSieveCS;

using Xunit;

namespace PrimeSieveUT {
    public class PrimeSieveUT {
        [Theory]
        [MemberData(nameof(GetHistoricalData), (int)1e7)]
        public void CompareToHistoricalData(int sieveSize, int expected) {
            var sieve = new PrimeSieve(sieveSize);
            sieve.RunSieve();
            Assert.Equal(expected, sieve.PrimesCount());
        }

        public static IEnumerable<object[]> GetHistoricalData(int max) {
            foreach(var (sieveSize,primesCount) in PrimeSieve.HistoricalData) {
                if (sieveSize < max)
                    yield return new object[] { sieveSize, primesCount };
            }
        }
    }
}
