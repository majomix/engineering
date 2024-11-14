using Algorithms.Graph.DataStructures;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Graph.TopologicalSort;

[TestFixture]
internal class KahnTopologicalSortAlgorithmTests
{
    [Test]
    public void KahnTopologicalSortAlgorithm_McDowell_Success()
    {
        // arrange
        var graph = CreateGraphByMcDowellPg250();
        var kahnAlgorithm = new KahnTopologicalSortAlgorithm();
        
        // act
        var topologicalSort = kahnAlgorithm.TopologicalSort(graph);

        // assert
        topologicalSort.Should().BeEquivalentTo(new[] { 1, 6, 2, 3, 7, 4, 5 }, options => options.WithStrictOrdering());
    }

    [Test]
    public void KahnTopologicalSortAlgorithm_GraphWithCycle_CycleIsDetected()
    {
        // arrange
        var graph = CreateGraphWithCycle();
        var kahnAlgorithm = new KahnTopologicalSortAlgorithm();

        // act
        var topologicalSort = kahnAlgorithm.TopologicalSort(graph);

        // assert
        topologicalSort.Should().BeEquivalentTo(new[] { -1 });
    }

    private GraphByAdjacencyList CreateGraphByMcDowellPg250()
    {
        var graph = new GraphByAdjacencyList();
        
        graph.AddVertex(1, new[] { 2, 3 });
        graph.AddVertex(2, new[] { 4 });
        graph.AddVertex(3, new[] { 4, 5 });
        graph.AddVertex(4, new[] { 5 });
        graph.AddVertex(5, Array.Empty<int>());
        graph.AddVertex(6, new[] { 7 });
        graph.AddVertex(7, Array.Empty<int>());

        return graph;
    }

    private GraphByAdjacencyList CreateGraphWithCycle()
    {
        var graph = new GraphByAdjacencyList();

        graph.AddVertex(1, new[] { 2, 3 });
        graph.AddVertex(2, new[] { 4 });
        graph.AddVertex(3, new[] { 4 });
        graph.AddVertex(4, new[] { 1 });

        return graph;
    }
}
