namespace PathFinder.Core 
{
    using System.Collections.Generic;
    
    public class Vertex 
    {
        public Vertex()
        {
            this.Edges = new List<Edge>();
        }
        
        public int Id { get; set; }
        public IEnumerable<Edge> Edges { get; set; }    
    }
}
