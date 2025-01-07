using Algorithms.Graph.DataStructures;
using DataStructures.PriorityQueue;

namespace Algorithms.Graph.Dijkstra;

public class DijkstraAlgorithm<T> where T : IComparable<T>
{
    /// <summary>
    /// Purpose:
    /// Find the shortest path from source vertex to all other vertices in a directed acyclic graph with non-negative weights.
    ///
    /// Complexity:
    /// Time: O(|E|+|V|*log(|V|))
    /// Space: O(|V|)
    /// </summary>
    /// <param name="graph">DAG with non-negative weights.</param>
    /// <param name="sourceVertex">Source vertex.</param>
    /// <returns>Map of all target vertices with the shortest path and a predecessor vertex on that shortest path.</returns>
    public DijkstraResult<T> FindShortestPath(GraphByAdjacencyList<T> graph, T sourceVertex)
    {
        var result = new DijkstraResult<T>();
        var priorityQueue = new CustomMinPriorityQueueByMinHeap<int, T>();

        // start search from the source vertex which has a zero distance to itself
        priorityQueue.Insert(0, sourceVertex);
        result.PathWeight[sourceVertex] = 0;

        // initialize distance to all other vertices to infinity and their predecessor as unknown
        var comparator = EqualityComparer<T>.Default;
        foreach (var vertex in graph.Vertices.Keys)
        {
            if (comparator.Equals(vertex, sourceVertex))
                continue;

            result.PathWeight[vertex] = DijkstraResult<T>.Infinity;
            result.PreviousVertex[vertex] = DijkstraResult<T>.UnknownParent;
            priorityQueue.Insert(DijkstraResult<T>.Infinity, vertex);
        }

        while (priorityQueue.Count > 0)
        {
            var minimum = priorityQueue.ExtractMinimum();

            foreach (var neighbor in graph.Vertices[minimum].Adjacency)
            {
                var newWeight = neighbor.Weight + result.PathWeight[minimum];
                var oldWeight = result.PathWeight[neighbor.TargetVertexId];

                // if there is a shorter path through currently processed minimum vertex to this neighbor, relax the distance and set new predecessor
                // important: filter out unreachable vertices
                if (result.PathWeight[minimum] != DijkstraResult<T>.Infinity && newWeight < oldWeight)
                {
                    result.PathWeight[neighbor.TargetVertexId] = newWeight;
                    result.PreviousVertex[neighbor.TargetVertexId] = minimum;

                    priorityQueue.DecreasePriority(neighbor.TargetVertexId, result.PathWeight[neighbor.TargetVertexId]);
                }
            }
        }

        return result;
    }
}
