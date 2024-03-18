using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace DataStructures.DynamicArray
{
    [SimpleJob(RuntimeMoniker.Net60)]
    [MemoryDiagnoser]
    [CsvMeasurementsExporter]
    [RPlotExporter]
    public class CustomDynamicArrayBenchmarks
    {
        [Params(1_000, 2_000_000)]
        public uint N;

        [Benchmark]
        public List<int> BuiltInList_ResizeBehaviour()
        {
            var list = new List<int>();

            var generator = new Random();
            for (var i = 0; i < N; i++)
            {
                list.Add(generator.Next());
            }

            return list;
        }

        [Benchmark]
        public List<int> BuiltInList_PreallocationBehaviour()
        {
            var list = new List<int>((int)N);

            var generator = new Random();
            for (var i = 0; i < N; i++)
            {
                list.Add(generator.Next());
            }

            return list;
        }

        [Benchmark]
        public CustomDynamicArray<int> CustomList_ResizeBehaviour()
        {
            var list = new CustomDynamicArray<int>();

            var generator = new Random();
            for (var i = 0; i < N; i++)
            {
                list.Add(generator.Next());
            }

            return list;
        }

        [Benchmark]
        public CustomDynamicArray<int> CustomList_PreallocationBehaviour()
        {
            var list = new CustomDynamicArray<int>(N);

            var generator = new Random();
            for (var i = 0; i < N; i++)
            {
                list.Add(generator.Next());
            }

            return list;
        }
    }

    [SimpleJob(RuntimeMoniker.Net60)]
    [MemoryDiagnoser]
    [CsvMeasurementsExporter]
    [RPlotExporter]
    public class CustomDynamicArrayGrowthOffsetBenchmarks
    {

        [Params(
            new[] { 1.0, 1.0 },
            new[] { 1.25, 0.0 },
            new[] { 1.5, 0.0 },
            new[] { 2.0, 0.0 }
        )]
        public double[] GrowthFactorAndOffset = null!;

        [Benchmark]
        public CustomDynamicArray<int> CustomList_ResizeBehaviour()
        {
            var list = new CustomDynamicArray<int>(GrowthFactorAndOffset);

            var generator = new Random();
            for (var i = 0; i < 200_000; i++)
            {
                list.Add(generator.Next());
            }

            return list;
        }
    }

    [SimpleJob(RuntimeMoniker.Net60)]
    [MemoryDiagnoser]
    [CsvMeasurementsExporter]
    [RPlotExporter]
    public class CustomDynamicArrayGrowthFactorBenchmarks
    {
        [Params(1.25, 1.5, 2.0)]
        public double GrowthFactor;

        [Benchmark]
        public CustomDynamicArray<int> CustomList_ResizeBehaviour()
        {
            var list = new CustomDynamicArray<int>(new[] { GrowthFactor, 0 });

            var generator = new Random();
            for (var i = 0; i < 2_000_000; i++)
            {
                list.Add(generator.Next());
            }

            return list;
        }
    }
}
