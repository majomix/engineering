using BenchmarkDotNet.Running;
using DataStructures.DynamicArray;

BenchmarkRunner.Run<CustomDynamicArrayBenchmarks>();
BenchmarkRunner.Run<CustomDynamicArrayGrowthOffsetBenchmarks>();
BenchmarkRunner.Run<CustomDynamicArrayGrowthFactorBenchmarks>();
