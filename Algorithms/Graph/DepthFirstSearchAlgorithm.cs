using Algorithms.Graph.DataStructures;

namespace Algorithms.Graph
{
    public class DepthFirstSearchAlgorithm
    {
        public bool DepthFirstSearch(GraphByAdjacencyList graph, int vertexId, int searchedVertex)
        {
            var currentVertexInfo = graph.Vertices[vertexId];

            if (currentVertexInfo.State == VertexState.Visited)
                return false;

            currentVertexInfo.State = VertexState.Visited;

            if (vertexId == searchedVertex)
                return true;

            var result = false;
            foreach (var vertex in currentVertexInfo.Adjacency)
            {
                result |= DepthFirstSearch(graph, vertex, searchedVertex);
            }

            return result;
        }
    }
}
