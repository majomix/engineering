namespace Algorithms.Graph.DataStructures
{
    public class Edge<T>
    {
        public int Weight { get; set; } = 1;

        public T TargetVertexId { get; set; }
    }
}
