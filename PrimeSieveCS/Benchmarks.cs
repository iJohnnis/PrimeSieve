using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace PrimeSieveCS {
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class Benchmarks {
        public PrimeSieve Sieve { get; set; } = new PrimeSieve((int)1e7);

        //[Benchmark]
        //public void RunSieveBA() => new PrimeSieveBA(SieveSize).RunSieve();
        [Benchmark]
        public void BuildAndRunSieve() {
            new PrimeSieve((int)1e7).RunSieve();
            new PrimeSieve((int)1e6).RunSieve();
        }
        [Benchmark]
        public void RunSieve() => Sieve.RunSieve();
    }
}
