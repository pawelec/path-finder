namespace PathFinder.AStar
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    // Based on https://pl.wikipedia.org/wiki/Algorytm_A*
    public class PathFinder : IPathFinder
    {    
        public void FindPath(Vertex source, Vertex destination)
        {
            var closedset = new List<Vertex>();
            var openset = source.Edges.Select(edge => edge.B).ToList();
            var g_score = new Dictionary<Vertex, float>
            {
                [source] = 0
            };
            var h_score = new Dictionary<Vertex, float>();
            var f_score = new Dictionary<Vertex, float>();
            var cameFrom = new Dictionary<Vertex, Vertex>();

            do
            {
                var vertexWithLowestFScore = this.GetVetexWithLowestFScore(openset);
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
                    var tentative_g_score = g_score[vertexWithLowestFScore] + DistBetween(vertexWithLowestFScore, neightbor);
                    var tentative_is_better = false;

                    if (!openset.Contains(neightbor))
                    {
                        openset.Add(neightbor);
                        h_score[neightbor] = this.GetHeuristicEstimateOfDistanceToGoalFrom(neightbor, destination);
                        tentative_is_better = true;
                    }
                    else if (tentative_g_score < g_score[neightbor])
                    {
                        tentative_is_better = true;
                    }
                    if (tentative_is_better)
                    {
                        cameFrom[neightbor] = vertexWithLowestFScore;
                        g_score[neightbor] = tentative_g_score;
                        f_score[neightbor] = g_score[neightbor] + h_score[neightbor];
                    }
                }
            } while (openset.Any());
        }

        private Vertex GetVetexWithLowestFScore(IEnumerable<Vertex> vertices)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<Vertex> ReconstructPath(Vertex cameFrom, Vertex destination)
        {
            throw new System.NotImplementedException();
        }

        private float DistBetween(Vertex parent, Vertex current)
        {
            throw new System.NotImplementedException();
        }

        private float GetHeuristicEstimateOfDistanceToGoalFrom(Vertex current, Vertex destination) 
        {
            float distance = current.Id > destination.Id ? current.Id - destination.Id : destination.Id - current.Id;
            return distance < 0 ? -distance : distance;
        }
    }
}
