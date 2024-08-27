namespace Algorithms.Graph.DataStructures
{
    public class GraphByAdjacencyList
    {
        public Dictionary<int, Vertex> Vertices { get; set; } = new();

        public void AddVertex(int vertexId, int[] neighborVertices)
        {
            var vertex = new Vertex { Id = vertexId, State = VertexState.Unvisited };

            foreach (var neighbor in neighborVertices)
            {
                vertex.Adjacency.Add(neighbor);
            }

            Vertices[vertexId] = vertex;
        }

        public Vertex GetRoot()
        {
            return Vertices[0];
        }
    }
}
