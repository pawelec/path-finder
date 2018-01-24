using System.Collections.Generic;

namespace PathFinder.Core
{
    using PathFinder.Core.Domain;

    public class PathFinderResult<T> 
    {
        public bool Success { get; set; }

        public IEnumerable<Vertex<T>> Path { get; set; } = new List<Vertex<T>>(0);
    }
}
