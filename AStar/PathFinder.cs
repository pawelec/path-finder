namespace PathFinder.AStar
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    // Based on https://pl.wikipedia.org/wiki/Algorytm_A*
    public class PathFinder : IPathFinder
    {    
        private Vertex destination;
        private Dictionary<Vertex, float> gScore = new Dictionary<Vertex, float>();
        private Dictionary<Vertex, float> hScore = new Dictionary<Vertex, float>();
        private Dictionary<Vertex, float> fScore = new Dictionary<Vertex, float>();
        private Dictionary<Vertex, Vertex> cameFrom = new Dictionary<Vertex, Vertex>();

        public PathFinderResult FindPath(Vertex source, Vertex destination)
        {
            this.destination = destination;
            var result = new PathFinderResult();

            if(source == null || source.Edges == null || !source.Edges.Any() ||
               destination == null || destination.Edges == null || !destination.Edges.Any())
                {
                    result.Success = false;
                    return result;
                } 
            var closedset = new List<Vertex>();
            var openset = source.Edges.Select(edge => edge.B).ToList();
            this.gScore[source] = 0;

            do
            {
                var vertexWithLowestFScore = this.GetVertexWithLowestFScore(openset);
                if (vertexWithLowestFScore == destination)
                {
                    result.Path = this.ReconstructPath(vertexWithLowestFScore, destination);
                    result.Success = true;
                    return result;
                }
                openset.Remove(vertexWithLowestFScore);
                closedset.Add(vertexWithLowestFScore);

                foreach (var neightbor in vertexWithLowestFScore.Edges.Select(edge => edge.B).ToList())
                {
                    if (closedset.Contains(neightbor))
                    {
                        continue;
                    }
                    var tentative_g_score = this.gScore[vertexWithLowestFScore] + DistanceBetween(vertexWithLowestFScore, neightbor);
                    var tentative_is_better = false;

                    if (!openset.Contains(neightbor))
                    {
                        openset.Add(neightbor);
                        this.hScore[neightbor] = this.GetHeuristicEstimateOfDistanceToGoalFrom(neightbor, destination);
                        tentative_is_better = true;
                    }
                    else if (tentative_g_score < this.gScore[neightbor])
                    {
                        tentative_is_better = true;
                    }
                    if (tentative_is_better)
                    {
                        this.cameFrom[neightbor] = vertexWithLowestFScore;
                        this.gScore[neightbor] = tentative_g_score;
                        this.fScore[neightbor] = this.gScore[neightbor] + this.hScore[neightbor];
                    }
                }
            } while (openset.Any());
            
            result.Success = false;
            return result;
        }

        private Vertex GetVertexWithLowestFScore(IEnumerable<Vertex> vertices)
        {
            float lowestFScore = int.MaxValue;
            Vertex vertexWithLowestScore = vertices.FirstOrDefault();

            foreach (var vertex in vertices)
            {
                float vertexGScore = 0;
                this.cameFrom.TryGetValue(vertex, out Vertex vertexParent);
                if (vertexParent != null)
                {
                    this.gScore.TryGetValue(vertexParent, out vertexGScore);
                }

                float vertexHScore = this.GetHeuristicEstimateOfDistanceToGoalFrom(vertex, this.destination);
                if (vertexGScore + vertexHScore < lowestFScore)
                {
                    lowestFScore = vertexGScore + vertexHScore;
                    vertexWithLowestScore = vertex;
                }
            }

            return vertexWithLowestScore;
        }

        private IEnumerable<Vertex> ReconstructPath(Vertex cameFrom, Vertex destination)
        {
            var path = new List<Vertex>();
            if(this.cameFrom[cameFrom] != null) 
            {
                path.AddRange(this.ReconstructPath(cameFrom, this.cameFrom[cameFrom]));
                return path;
            }
            return new List<Vertex>(0);
        }

        private float DistanceBetween(Vertex parent, Vertex current)
            => parent.Edges.First(edge => edge.B == current).Weight;

        private float GetHeuristicEstimateOfDistanceToGoalFrom(Vertex current, Vertex destination) 
        {
            float distance = current.Id > destination.Id ? current.Id - destination.Id : destination.Id - current.Id;
            return distance < 0 ? -distance : distance;
        }
    }
}
