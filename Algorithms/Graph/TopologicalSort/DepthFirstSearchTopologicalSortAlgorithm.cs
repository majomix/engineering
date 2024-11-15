using Algorithms.Graph.DataStructures;

namespace Algorithms.Graph.TopologicalSort
{
    public class DepthFirstSearchTopologicalSortAlgorithm
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
        public List<int> TopologicalSort(GraphByAdjacencyList graph)
        {
            var topologicallySortedVertices = new List<int>();

            var topologicalSortExists = true;

            foreach (var vertexToProcess in graph.Vertices.Values)
            {
                topologicalSortExists &= DepthFirstTopologicalSort(graph, vertexToProcess, topologicallySortedVertices);
            }

            if (!topologicalSortExists)
            {
                return new List<int> { -1 };
            }

            topologicallySortedVertices.Reverse();

            return topologicallySortedVertices;
        }

        private bool DepthFirstTopologicalSort(GraphByAdjacencyList graph, Vertex vertexToProcess, List<int> sortedVertices)
        {
            if (vertexToProcess.State == VertexState.Visited)
                return true;

            if (vertexToProcess.State == VertexState.Visiting)
                return false;

            vertexToProcess.State = VertexState.Visiting;

            var topologicalSortExists = true;
            foreach (var edge in vertexToProcess.Adjacency)
            {
                var adjacentVertex = graph.Vertices[edge.TargetVertexId];
                topologicalSortExists &= DepthFirstTopologicalSort(graph, adjacentVertex, sortedVertices);
            }

            vertexToProcess.State = VertexState.Visited;
            sortedVertices.Add(vertexToProcess.Id);

            return topologicalSortExists;
        }
    }
}
