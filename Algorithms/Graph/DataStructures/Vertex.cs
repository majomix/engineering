namespace Algorithms.Graph.DataStructures
{
    public class Vertex
    {
        public int Id { get; set; }

        public VertexState State { get; set; }

        public List<Edge> Adjacency { get; set; } = new();
    }
}
