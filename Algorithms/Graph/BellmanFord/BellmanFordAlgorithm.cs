using Algorithms.Graph.DataStructures;

namespace Algorithms.Graph.BellmanFord;

internal class BellmanFordAlgorithm
{
    /// <summary>
    /// Purpose:
    /// Find the shortest path from source vertex to all other vertices in a directed graph with negative weights.
    /// Detect negative cycle.
    ///
    /// Complexity:
    /// Time: O(|E|*|V|)
    /// Space: O(|V|)
    /// </summary>
    /// <param name="graph">Directed graph with positive or negative weights.</param>
    /// <param name="sourceVertex">Source vertex.</param>
    /// <returns>Map of all target vertices with the shortest path and a predecessor vertex on that shortest path.</returns>
    public BellmanFordResult FindShortestPath(GraphByAdjacencyList graph, int sourceVertex)
    {
        var result = new BellmanFordResult();

        result.PathWeight[sourceVertex] = 0;

        // 1. initialize distance to all other vertices to infinity and their predecessor as unknown
        foreach (var vertex in graph.Vertices.Keys)
        {
            if (vertex == sourceVertex)
                continue;
            
            result.PathWeight[vertex] = BellmanFordResult.Infinity;
            result.PreviousVertex[vertex] = BellmanFordResult.UnknownParent;
        }

        // 2. relax edges |V|-1 times
        for (var i = 0; i < graph.Vertices.Count - 1; i++)
        {
            var hasRelaxedAnyEdges = false;

            foreach (var vertex in graph.Vertices.Values)
            {
                foreach (var edge in vertex.Adjacency)
                {
                    var newWeight = edge.Weight + result.PathWeight[vertex.Id];
                    var oldWeight = result.PathWeight[edge.TargetVertexId];

                    if (result.PathWeight[vertex.Id] != BellmanFordResult.Infinity && newWeight < oldWeight)
                    {
                        result.PathWeight[edge.TargetVertexId] = newWeight;
                        result.PreviousVertex[edge.TargetVertexId] = vertex.Id;
                        hasRelaxedAnyEdges = true;
                    }
                }
            }

            // optimization: if nothing happened in this pass, path has already been found
            if (!hasRelaxedAnyEdges)
            {
                return result;
            }
        }

        // 3. check for negative-weight cycles
        foreach (var vertex in graph.Vertices.Values)
        {
            foreach (var edge in vertex.Adjacency)
            {
                var newWeight = edge.Weight + result.PathWeight[vertex.Id];
                var oldWeight = result.PathWeight[edge.TargetVertexId];

                // edge reachable from a negative cycle but is not necessarily part of the cycle itself
                if (result.PathWeight[vertex.Id] != BellmanFordResult.Infinity && newWeight < oldWeight)
                {
                    result.PreviousVertex[edge.TargetVertexId] = vertex.Id;

                    // find vertex on the cycle
                    var visited = new HashSet<int> { edge.TargetVertexId };
                    var currentVertex = vertex.Id;
                    
                    while (!visited.Contains(currentVertex))
                    {
                        visited.Add(currentVertex);
                        currentVertex = result.PreviousVertex[currentVertex];
                    }

                    // current vertex is guaranteed to be part of a cycle
                    result.NegativeCycle.Add(currentVertex);
                    var predecessor = result.PreviousVertex[currentVertex];
                    
                    // go backwards until full cycle is found
                    while (predecessor != currentVertex)
                    {
                        result.NegativeCycle.Add(predecessor);
                        predecessor = result.PreviousVertex[predecessor];
                    }

                    return result;
                }
            }
        }

        return result;
    }
}
