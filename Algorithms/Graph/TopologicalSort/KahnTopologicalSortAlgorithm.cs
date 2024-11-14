using Algorithms.Graph.DataStructures;

namespace Algorithms.Graph.TopologicalSort;

public class KahnTopologicalSortAlgorithm
{
    public List<int> TopologicalSort(GraphByAdjacencyList graph)
    {
        var topologicallySortedVertices = new List<int>();
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
            return new List<int> { -1 };
        }

        return topologicallySortedVertices;
    }

    private Dictionary<int, int> CreateMapOfIncomingEdges(GraphByAdjacencyList graph)
    {
        var vertexToCountOfIncomingEdgesMap = new Dictionary<int, int>();

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

    private Queue<int> CreateQueueOfVerticesWithNoIncomingEdges(GraphByAdjacencyList graph, Dictionary<int, int> vertexToCountOfIncomingEdgesMap)
    {
        var verticesWithNoIncomingEdges = new Queue<int>();
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
