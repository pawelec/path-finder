using System.Collections.Generic;

namespace PathFinder.Core
{
    public class PathFinderResult 
    {
        public bool Success { get; set; }

        public IEnumerable<Vertex> Path { get; set; } = new List<Vertex>(0);
    }
}
