using Algorithms.Graph;
using Algorithms.Graph.DataStructures;
using NUnit.Framework;
using FluentAssertions;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// Given a directed graph and two nodes (S and E), design an algorithm to find whether there is a route from S to E.
    ///
    /// Solutions:
    /// * depth-first
    /// * breadth-first
    /// </summary>
    internal class Task4_1RouteBetweenNodes
    {
        public bool HasRouteByDepthFirst(GraphByAdjacencyList graph, int sourceVertex, int targetVertex)
        {
            return new DepthFirstSearchAlgorithm().DepthFirstSearch(graph, sourceVertex, targetVertex);
        }

        public bool HasRouteByBreadthFirst(GraphByAdjacencyList graph, int sourceVertex, int targetVertex)
        {
            return new BreadthFirstSearchAlgorithm().BreadthFirstSearch(graph, sourceVertex, targetVertex);
        }
    }

    [TestFixture]
    internal class Task4_1RouteBetweenNodesTests
    {
        private static object[] testCases =
        {
            new object[] { 1, 5, true },
            new object[] { 2, 5, true },
            new object[] { 5, 4, false }
        };

        [TestCaseSource(nameof(testCases))]
        public void HasRouteByDepthFirstTest(int sourceVertex, int targetVertex, bool expectedResult)
        {
            // arrange
            var sut = new Task4_1RouteBetweenNodes();
            var graph = new GraphByAdjacencyList();
            graph.AddVertex(1, new [] { 2, 3, 4, 5 });
            graph.AddVertex(2, new[] { 3 });
            graph.AddVertex(3, new[] { 4, 5 });
            graph.AddVertex(4, new[] { 1 });
            graph.AddVertex(5, new[] { 5 });

            // act
            var result = sut.HasRouteByDepthFirst(graph, sourceVertex, targetVertex);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void HasRouteByBreadthFirstTest(int sourceVertex, int targetVertex, bool expectedResult)
        {
            // arrange
            var sut = new Task4_1RouteBetweenNodes();
            var graph = new GraphByAdjacencyList();
            graph.AddVertex(1, new[] { 2, 3, 4, 5 });
            graph.AddVertex(2, new[] { 3 });
            graph.AddVertex(3, new[] { 4, 5 });
            graph.AddVertex(4, new[] { 1 });
            graph.AddVertex(5, new[] { 5 });

            // act
            var result = sut.HasRouteByBreadthFirst(graph, sourceVertex, targetVertex);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
