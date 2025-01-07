using Algorithms.Graph.DataStructures;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Graph.Dijkstra;

[TestFixture]
public class DijkstraAlgorithmTests
{
    [Test]
    public void Dijkstra_FindShortestPathTest_McDowell()
    {
        // arrange
        var sut = new DijkstraAlgorithm<int>();

        var graph = new GraphByAdjacencyList<int>();
        graph.AddVertex(1, new[] { (2, 5), (3, 3), (5, 2) });
        graph.AddVertex(2, new[] { (4, 2) });
        graph.AddVertex(3, new[] { (2, 1), (4, 1) });
        graph.AddVertex(4, new[] { (1, 1), (7, 2), (8, 1) });
        graph.AddVertex(5, new[] { (1, 1), (8, 4), (9, 7) });
        graph.AddVertex(6, new[] { (2, 3), (8, 1) });
        graph.AddVertex(7, new[] { (3, 3), (9, 2) });
        graph.AddVertex(8, new[] { (3, 2), (6, 2), (7, 2) });
        graph.AddVertex(9, Array.Empty<(int, int)>());

        // act
        var result = sut.FindShortestPath(graph, 1);

        // assert
        result.PathWeight[1].Should().Be(0);
        result.PathWeight[2].Should().Be(4);
        result.PreviousVertex[2].Should().Be(3);
        result.PathWeight[3].Should().Be(3);
        result.PreviousVertex[3].Should().Be(1);
        result.PathWeight[4].Should().Be(4);
        result.PreviousVertex[4].Should().Be(3);
        result.PathWeight[5].Should().Be(2);
        result.PreviousVertex[5].Should().Be(1);
        result.PathWeight[6].Should().Be(7);
        result.PreviousVertex[6].Should().Be(8);
        result.PathWeight[7].Should().Be(6);
        result.PreviousVertex[7].Should().Be(4);
        result.PathWeight[8].Should().Be(5);
        result.PreviousVertex[8].Should().Be(4);
        result.PathWeight[9].Should().Be(8);
        result.PreviousVertex[9].Should().Be(7);
    }

    [Test]
    public void Dijkstra_FindShortestPathTest_Navrat()
    {
        // arrange
        var sut = new DijkstraAlgorithm<int>();

        var graph = new GraphByAdjacencyList<int>();
        graph.AddVertex(1, new[] { (2, 10), (3, 5) });
        graph.AddVertex(2, new[] { (3, 2), (4, 1) });
        graph.AddVertex(3, new[] { (2, 3), (4, 9), (5, 2) });
        graph.AddVertex(4, new[] { (5, 4) });
        graph.AddVertex(5, new[] { (4, 6), (1, 7) });

        // act
        var result = sut.FindShortestPath(graph, 1);

        // assert
        result.PathWeight[1].Should().Be(0);
        result.PathWeight[2].Should().Be(8);
        result.PreviousVertex[2].Should().Be(3);
        result.PathWeight[3].Should().Be(5);
        result.PreviousVertex[3].Should().Be(1);
        result.PathWeight[4].Should().Be(9);
        result.PreviousVertex[4].Should().Be(2);
        result.PathWeight[5].Should().Be(7);
        result.PreviousVertex[5].Should().Be(3);
    }
}
