using Algorithms.Graph.DataStructures;
using Algorithms.Graph.TopologicalSort;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.TreesAndGraphs
{
    /// <summary>
    /// You are given a list of projects and a list of dependencies.
    /// The dependencies are a list of pairs of projects, where the second project is dependent on the first project.
    /// All of a project's dependencies must be built before the project is. Find the build order that will allow the projects to be built.
    /// If there is no valid build order, return an error.
    ///
    /// Discussion:
    /// There is usually more than one topological sort.
    /// 
    /// Solutions:
    /// * Kahn's Topological Sort
    /// * Depth-First Topological Sort
    /// </summary>
    internal class Task4_7BuildOrder
    {
        public string[] FindBuildOrder(string[] projects, string[][] dependencies)
        {
            var graph = new GraphByAdjacencyList();
            var nameToVertexIdMap = new Dictionary<string, int>();

            // build empty graph
            for (var i = 0; i < projects.Length; i++)
            {
                nameToVertexIdMap[projects[i]] = i;
                graph.AddVertex(i, Array.Empty<int>());
            }

            // fill in adjacent vertices
            foreach (var dependency in dependencies)
            {
                var dependingProject = nameToVertexIdMap[dependency[0]];
                var dependsOn = nameToVertexIdMap[dependency[1]];

                var edge = new Edge { TargetVertexId = dependsOn };

                graph.Vertices[dependingProject].Adjacency.Add(edge);
            }

            // perform topological sort
            var topologicalSortAlgorithm = new KahnTopologicalSortAlgorithm();
            var topologicalSort = topologicalSortAlgorithm.TopologicalSort(graph);

            // re-build the project names in topological sort order
            var buildOrder = new List<string>();
            foreach (var vertexId in topologicalSort)
            {
                buildOrder.Add(projects[vertexId]);
            }

            return buildOrder.ToArray();
        }
    }

    [TestFixture]
    internal class Task4_7BuildOrderTests
    {
        [Test]
        public void FindBuildOrderTest_McDowellPg250Task()
        {
            // arrange
            var sut = new Task4_7BuildOrder();

            // act
            var result = sut.FindBuildOrder(new[] { "a", "b", "c", "d", "e", "f" }, new[]
            {
                new[] { "a", "d" },
                new[] { "f", "b" },
                new[] { "b", "d" },
                new[] { "f", "a" },
                new[] { "d", "c" }
            });

            // assert
            result.Should().BeEquivalentTo(new [] { "e", "f", "b", "a", "d", "c" }, options => options.WithStrictOrdering());
        }

        [Test]
        public void FindBuildOrderTest_McDowellPg250Description()
        {
            // arrange
            var sut = new Task4_7BuildOrder();

            // act
            var result = sut.FindBuildOrder(new[] { "a", "b", "c", "d", "e", "f", "g" }, new[]
            {
                new[] { "f", "c" },
                new[] { "f", "b" },
                new[] { "c", "a" },
                new[] { "b", "a" },
                new[] { "f", "a" },
                new[] { "a", "e" },
                new[] { "b", "e" },
                new[] { "d", "g" },

            });

            // assert
            result.Should().BeEquivalentTo(new[] { "d", "f", "g", "c", "b", "a", "e" }, options => options.WithStrictOrdering());
        }
    }
}
