using Algorithms.Graph.DataStructures;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Graph.Dijkstra;

[TestFixture]
public class DijkstraAlgorithmTests
{
    [Test]
    public void FindShortestPathTest()
    {
        // arrange
        var sut = new DijkstraAlgorithm();

        var graph = new GraphByAdjacencyList();
        graph.AddVertex(1, new[] { (2, 5), (3, 3), (5, 2) });
        graph.AddVertex(2, new[] { (3, 2) });
        graph.AddVertex(3, new[] { (2, 1), (4, 1) });
        graph.AddVertex(4, new[] { (1, 1), (7, 2), (8, 1) });
        graph.AddVertex(5, new[] { (1, 1), (7, 4), (9, 7) });
        graph.AddVertex(6, new[] { (2, 3), (8, 1) });
        graph.AddVertex(7, new[] { (3, 3), (9, 2) });
        graph.AddVertex(8, new[] { (3, 2), (7, 2) });
        graph.AddVertex(9, Array.Empty<(int, int)>());

        // act
        var result = sut.FindShortestPath(graph, 1);

        // assert
        result.PathWeight[9].Should().Be(8);
    }
}
