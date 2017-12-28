namespace PathFinder.Core 
{
    using System.Collections.Generic;
    
    public class Vertex 
    {
        public Vertex()
        {
            this.Edges = new List<Edge>();
        }
        
        public char Character { get; set; }
        public IEnumerable<Edge> Edges { get; set; }    
    }
}
