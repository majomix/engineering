namespace Algorithms.Graph.DataStructures
{
    public class GraphByAdjacencyList<T>
    {
        public Dictionary<T, Vertex<T>> Vertices { get; set; } = new();

        public void AddVertex(T vertexId, T[] neighborVertices)
        {
            var vertex = new Vertex<T> { Id = vertexId, State = VertexState.Unvisited };

            foreach (var neighbor in neighborVertices)
            {
                vertex.Adjacency.Add(new Edge<T> { TargetVertexId = neighbor });
            }

            Vertices[vertexId] = vertex;
        }

        public void AddVertex(T vertexId, (T VertexId, int Weight)[] neighborVertices)
        {
            var vertex = new Vertex<T> { Id = vertexId, State = VertexState.Unvisited };

            foreach (var neighbor in neighborVertices)
            {
                vertex.Adjacency.Add(new Edge<T> { TargetVertexId = neighbor.VertexId, Weight = neighbor.Weight });
            }

            Vertices[vertexId] = vertex;
        }

        public void AddEdgeToVertex(T vertexId, T targetVertexId)
        {
            if (!Vertices.ContainsKey(vertexId))
            {
                AddVertex(vertexId, Array.Empty<T>());
            }

            Vertices[vertexId].Adjacency.Add(new Edge<T> { TargetVertexId = targetVertexId });
        }
    }
}
