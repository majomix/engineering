namespace Algorithms.Graph.DataStructures
{
    public class Vertex<T>
    {
        public T Id { get; set; }

        public VertexState State { get; set; }

        public List<Edge<T>> Adjacency { get; set; } = new();
    }
}
