using Algorithms.Graph.DataStructures;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Graph.TopologicalSort;

[TestFixture]
internal class DepthFirstSearchTopologicalSortAlgorithmTests
{
    [Test]
    public void DepthFirstSearchTopologicalSortAlgorithm_McDowell_Success()
    {
        // arrange
        var graph = CreateGraphByMcDowellPg250();
        var depthFirstTopologicalSortAlgorithm = new DepthFirstSearchTopologicalSortAlgorithm<int>();
        
        // act
        var topologicalSort = depthFirstTopologicalSortAlgorithm.TopologicalSort(graph);

        // assert
        topologicalSort.Should().BeEquivalentTo(new[] { 6, 7, 1, 3, 2, 4, 5 }, options => options.WithStrictOrdering());
    }

    [Test]
    public void DepthFirstSearchTopologicalSortAlgorithm_GraphWithCycle_CycleIsDetected()
    {
        // arrange
        var graph = CreateGraphWithCycle();
        var depthFirstTopologicalSortAlgorithm = new DepthFirstSearchTopologicalSortAlgorithm<int>();

        // act
        var topologicalSort = depthFirstTopologicalSortAlgorithm.TopologicalSort(graph);

        // assert
        topologicalSort.Should().BeEmpty();
    }

    private GraphByAdjacencyList<int> CreateGraphByMcDowellPg250()
    {
        var graph = new GraphByAdjacencyList<int>();
        
        graph.AddVertex(1, new[] { 2, 3 });
        graph.AddVertex(2, new[] { 4 });
        graph.AddVertex(3, new[] { 4, 5 });
        graph.AddVertex(4, new[] { 5 });
        graph.AddVertex(5, Array.Empty<int>());
        graph.AddVertex(6, new[] { 7 });
        graph.AddVertex(7, Array.Empty<int>());

        return graph;
    }

    private GraphByAdjacencyList<int> CreateGraphWithCycle()
    {
        var graph = new GraphByAdjacencyList<int>();

        graph.AddVertex(1, new[] { 2, 3 });
        graph.AddVertex(2, new[] { 4 });
        graph.AddVertex(3, new[] { 4 });
        graph.AddVertex(4, new[] { 1 });

        return graph;
    }
}
