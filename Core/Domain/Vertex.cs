namespace PathFinder.Core.Domain 
{
    using System.Collections.Generic;

    public class Vertex<T> 
    {   
        public T Position { get; set; }
        public IList<Edge<T>> Edges { get; set; } = new List<Edge<T>>();
    }
}
