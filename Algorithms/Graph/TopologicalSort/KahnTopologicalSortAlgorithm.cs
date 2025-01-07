using Algorithms.Graph.DataStructures;

namespace Algorithms.Graph.TopologicalSort;

public class KahnTopologicalSortAlgorithm<T> where T : notnull
{
    /// <summary>
    /// Purpose:
    /// Sort items in a directed acyclic graph in such way that each vertex appears before all the vertices it points to.
    ///
    /// Complexity:
    /// Time: O(|V|+|E|)
    /// Space: O(|V|)
    /// </summary>
    /// <param name="graph">Graph to search through.</param>
    /// <returns>List of vertex ids in a topologically sorted order or a list with element -1 if no such order exists.</returns>
    public List<T> TopologicalSort(GraphByAdjacencyList<T> graph)
    {
        var topologicallySortedVertices = new List<T>();
        var vertexToCountOfIncomingEdgesMap = CreateMapOfIncomingEdges(graph);
        var verticesWithNoIncomingEdges = CreateQueueOfVerticesWithNoIncomingEdges(graph, vertexToCountOfIncomingEdgesMap);

        while (verticesWithNoIncomingEdges.Count > 0)
        {
            var vertexToProcess = verticesWithNoIncomingEdges.Dequeue();

            topologicallySortedVertices.Add(vertexToProcess);

            foreach (var targetVertex in graph.Vertices[vertexToProcess].Adjacency)
            {
                vertexToCountOfIncomingEdgesMap[targetVertex.TargetVertexId]--;

                if (vertexToCountOfIncomingEdgesMap[targetVertex.TargetVertexId] == 0)
                {
                    verticesWithNoIncomingEdges.Enqueue(targetVertex.TargetVertexId);
                }
            }
        }

        if (topologicallySortedVertices.Count != graph.Vertices.Count)
        {
            return new List<T>();
        }

        return topologicallySortedVertices;
    }

    private Dictionary<T, int> CreateMapOfIncomingEdges(GraphByAdjacencyList<T> graph)
    {
        var vertexToCountOfIncomingEdgesMap = new Dictionary<T, int>();

        foreach (var vertex in graph.Vertices.Values)
        {
            vertexToCountOfIncomingEdgesMap.TryAdd(vertex.Id, 0);

            foreach (var adjacent in vertex.Adjacency)
            {
                if (!vertexToCountOfIncomingEdgesMap.TryAdd(adjacent.TargetVertexId, 1))
                {
                    vertexToCountOfIncomingEdgesMap[adjacent.TargetVertexId]++;
                }
            }
        }

        return vertexToCountOfIncomingEdgesMap;
    }

    private Queue<T> CreateQueueOfVerticesWithNoIncomingEdges(GraphByAdjacencyList<T> graph, Dictionary<T, int> vertexToCountOfIncomingEdgesMap)
    {
        var verticesWithNoIncomingEdges = new Queue<T>();
        foreach (var vertex in graph.Vertices.Values)
        {
            if (vertexToCountOfIncomingEdgesMap[vertex.Id] == 0)
            {
                verticesWithNoIncomingEdges.Enqueue(vertex.Id);
            }
        }

        return verticesWithNoIncomingEdges;
    }
}
