namespace PathFinder.Core
{
    public interface IPathFinder 
    {
        PathFinderResult FindPath(Vertex source, Vertex destination);    
    }
}
