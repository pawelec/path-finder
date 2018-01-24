namespace PathFinder.Core.Domain 
{
    public struct Edge<T> 
    {
        public Vertex<T> A { get; set; }
        public Vertex<T> B { get; set; }
        public float Weight { get; set; }
    }
}
