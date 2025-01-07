using Algorithms.Graph.DataStructures;

namespace Algorithms.Graph.SegmentedSearch;

public class DepthFirstSearchAlgorithm<T> where T : notnull, IEquatable<T>
{
    /// <summary>
    /// Purpose:
    /// Determine whether a path exists between two vertices.
    ///
    /// Complexity:
    /// Time: O(|V|+|E|)
    /// Space: O(|V|)
    /// </summary>
    /// <param name="graph">Graph to search through.</param>
    /// <param name="vertexId">ID of starting vertex.</param>
    /// <param name="searchedVertexId">ID of searched vertex.</param>
    /// <returns>True if path exists, false otherwise.</returns>
    public bool DepthFirstSearch(GraphByAdjacencyList<T> graph, T vertexId, T searchedVertexId)
    {
        var currentVertexInfo = graph.Vertices[vertexId];

        if (currentVertexInfo.State == VertexState.Visited)
            return false;

        currentVertexInfo.State = VertexState.Visited;

        var comparator = EqualityComparer<T>.Default;
        if (comparator.Equals(vertexId, searchedVertexId))
            return true;

        var result = false;
        foreach (var edge in currentVertexInfo.Adjacency)
        {
            result |= DepthFirstSearch(graph, edge.TargetVertexId, searchedVertexId);
        }

        return result;
    }
}
