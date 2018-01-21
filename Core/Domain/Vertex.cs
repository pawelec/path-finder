namespace PathFinder.Core 
{
    using System.Collections.Generic;
    
    public class Vertex 
    {   
        public int Id { get; set; }
        public IList<Edge> Edges { get; set; } = new List<Edge>();
    }
}
