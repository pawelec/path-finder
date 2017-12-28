namespace PathFinder.Core 
{
    public struct Edge 
    {
        public Vertex A { get; set; }
        public Vertex B { get; set; }
        public float Weight { get; set; }
    }
}
