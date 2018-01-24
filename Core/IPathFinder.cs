namespace PathFinder.Core
{
    using PathFinder.Core.Domain;

    public interface IPathFinder 
    {
        PathFinderResult FindPath(Vertex source, Vertex destination);    
    }
}
