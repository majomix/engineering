using Algorithms.Graph.DataStructures;
using DataStructures.Queue;

namespace Algorithms.Graph
{
    public class BreadthFirstSearchAlgorithm
    {
        public bool BreadthFirstSearch(GraphByAdjacencyList graph, int vertexId, int searchedVertexId)
        {
            var queue = new CustomQueueByCircularArray<Vertex>();

            queue.Enqueue(graph.Vertices[vertexId]);

            while (queue.Count != 0)
            {
                var currentVertex = queue.Dequeue();

                if (currentVertex.State == VertexState.Visited)
                    continue;

                var currentVertexInfo = graph.Vertices[currentVertex.Id];
                currentVertexInfo.State = VertexState.Visited;

                if (currentVertex.Id == searchedVertexId)
                {
                    return true;
                }

                foreach (var adjacentVertexId in currentVertexInfo.Adjacency)
                {
                    var adjacentVertexInfo = graph.Vertices[adjacentVertexId];

                    if (adjacentVertexInfo.State == VertexState.Visited)
                        continue;

                    queue.Enqueue(graph.Vertices[adjacentVertexId]);
                }
            }

            return false;
        }
    }
}
