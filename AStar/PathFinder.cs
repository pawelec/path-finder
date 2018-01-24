namespace PathFinder.AStar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    using global::PathFinder.Core.Domain;

    // Based on https://pl.wikipedia.org/wiki/Algorytm_A*
    public class PathFinder<T> : IPathFinder<T>
    {    
        private Vertex<T> destination;
        private Dictionary<Vertex<T>, float> gScore = new Dictionary<Vertex<T>, float>();
        private Dictionary<Vertex<T>, float> hScore = new Dictionary<Vertex<T>, float>();
        private Dictionary<Vertex<T>, float> fScore = new Dictionary<Vertex<T>, float>();
        private Dictionary<Vertex<T>, Vertex<T>> cameFrom = new Dictionary<Vertex<T>, Vertex<T>>();

        public PathFinderResult<T> FindPath(Vertex<T> source, Vertex<T> destination)
        {
            this.destination = destination;
            var result = new PathFinderResult<T>();

            if(source == null || source.Edges == null || !source.Edges.Any() ||
               destination == null || destination.Edges == null || !destination.Edges.Any())
                {
                    result.Success = false;
                    return result;
                } 
            var closedset = new List<Vertex<T>>();
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

        private Vertex<T> GetVertexWithLowestFScore(IEnumerable<Vertex<T>> vertices)
        {
            float lowestFScore = int.MaxValue;
            Vertex<T> vertexWithLowestScore = vertices.FirstOrDefault();

            foreach (var vertex in vertices)
            {
                float vertexGScore = 0;
                this.cameFrom.TryGetValue(vertex, out Vertex<T> vertexParent);
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

        private IEnumerable<Vertex<T>> ReconstructPath(Vertex<T> cameFrom, Vertex<T> destination)
        {
            var path = new List<Vertex<T>>();
            if(this.cameFrom[cameFrom] != null) 
            {
                path.AddRange(this.ReconstructPath(cameFrom, this.cameFrom[cameFrom]));
                return path;
            }
            return new List<Vertex<T>>(0);
        }

        private float DistanceBetween(Vertex<T> parent, Vertex<T> current)
            => parent.Edges.First(edge => edge.B == current).Weight;

        private float GetHeuristicEstimateOfDistanceToGoalFrom(Vertex<T> current, Vertex<T> destination) 
        {
            throw new NotImplementedException();
        }
    }
}
