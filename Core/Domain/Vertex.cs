namespace PathFinder.Core.Domain 
{
    using System.Collections.Generic;

    public class Vertex 
    {   
        public int Position { get; set; }
        public IList<Edge> Edges { get; set; } = new List<Edge>();
    }
}
