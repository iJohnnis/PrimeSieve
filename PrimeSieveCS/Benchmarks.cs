using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace PrimeSieveCS {
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class Benchmarks {
        [Params((int)1e7, (int)1e8, (int)1e9)]
        public int SieveSize { get; set; }

        [Benchmark]
        public void RunSieveBitArray() => new PrimeSieveBitArray(SieveSize).RunSieve();
        [Benchmark]
        public void RunSieveBoolArray() => new PrimeSieve(SieveSize).RunSieve();
    }
}
