using Algorithms.Graph.DataStructures;
using DataStructures.Queue;

namespace Algorithms.Graph.SegmentedSearch;

public class BreadthFirstSearchAlgorithm<T> where T : notnull
{
    /// <summary>
    /// Purpose:
    /// Determine whether a path exists between two vertices.
    /// Find the least number of edges (shortest path in an unweighted graph) between two vertices.
    ///
    /// Complexity:
    /// Time: O(|V|+|E|)
    /// Space: O(|V|)
    /// </summary>
    /// <param name="graph">Graph to search through.</param>
    /// <param name="vertexId">ID of starting vertex.</param>
    /// <param name="searchedVertexId">ID of searched vertex.</param>
    /// <returns>True if path exists, false otherwise.</returns>
    public bool BreadthFirstSearch(GraphByAdjacencyList<T> graph, T vertexId, T searchedVertexId)
    {
        var queue = new CustomQueueByCircularArray<Vertex<T>>();

        queue.Enqueue(graph.Vertices[vertexId]);

        while (queue.Count != 0)
        {
            var currentVertex = queue.Dequeue();

            if (currentVertex.State == VertexState.Visited)
                continue;

            var currentVertexInfo = graph.Vertices[currentVertex.Id];
            currentVertexInfo.State = VertexState.Visited;

            var comparator = EqualityComparer<T>.Default;
            if (comparator.Equals(currentVertex.Id, searchedVertexId))
            {
                return true;
            }

            foreach (var adjacentVertexId in currentVertexInfo.Adjacency)
            {
                var adjacentVertexInfo = graph.Vertices[adjacentVertexId.TargetVertexId];

                if (adjacentVertexInfo.State == VertexState.Visited)
                    continue;

                queue.Enqueue(graph.Vertices[adjacentVertexId.TargetVertexId]);
            }
        }

        return false;
    }
}
