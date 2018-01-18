namespace PathFinder.AStar
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    // Based on https://pl.wikipedia.org/wiki/Algorytm_A*
    public class PathFinder : IPathFinder
    {    
        private Dictionary<Vertex, float> gScore = new Dictionary<Vertex, float>();
        private Dictionary<Vertex, float> hScore = new Dictionary<Vertex, float>();
        private Dictionary<Vertex, float> fScore = new Dictionary<Vertex, float>();
        private Dictionary<Vertex, Vertex> cameFrom = new Dictionary<Vertex, Vertex>();

        public void FindPath(Vertex source, Vertex destination)
        {
            var closedset = new List<Vertex>();
            var openset = source.Edges.Select(edge => edge.B).ToList();
            this.gScore[source] = 0;

            do
            {
                var vertexWithLowestFScore = this.GetVertexWithLowestFScore(openset);
                if (vertexWithLowestFScore == destination)
                {
                    this.ReconstructPath(vertexWithLowestFScore, destination);
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
        }

        private Vertex GetVertexWithLowestFScore(IEnumerable<Vertex> vertices)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<Vertex> ReconstructPath(Vertex cameFrom, Vertex destination)
        {
            throw new System.NotImplementedException();
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
