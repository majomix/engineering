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
                vertex.Adjacency.Add(new Edge { TargetVertexId = neighbor });
            }

            Vertices[vertexId] = vertex;
        }

        public void AddVertex(int vertexId, (int VertexId, int Weight)[] neighborVertices)
        {
            var vertex = new Vertex { Id = vertexId, State = VertexState.Unvisited };

            foreach (var neighbor in neighborVertices)
            {
                vertex.Adjacency.Add(new Edge { TargetVertexId = neighbor.VertexId, Weight = neighbor.Weight });
            }

            Vertices[vertexId] = vertex;
        }
    }
}
