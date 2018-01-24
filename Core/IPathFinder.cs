namespace PathFinder.Core
{
    using PathFinder.Core.Domain;

    public interface IPathFinder<T> 
    {
        PathFinderResult<T> FindPath(Vertex<T> source, Vertex<T> destination);    
    }
}
