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

            do
            {
                var x = this.GetVetexWithLowestFScore(openset);
                // g_score.Add(x, )
                if(x == destination) {
                    this.ReconstructPath(x, destination);
                }
                openset.Remove(x);
                closedset.Add(x);

                foreach (var neightbor in x.Edges.Select(edge => edge.B).ToList())
                {
                    if(closedset.Contains(neightbor)) 
                    {
                        continue;
                    }
                    var tentative_g_score = g_score[x] + DistBetween(x, neightbor);
                    var tentative_is_better = false;

                    if(!openset.Contains(neightbor))
                    {

                    }
                    else if(tentative_g_score < g_score[neightbor])
                    {

                    }
                    if(tentative_is_better)
                    {
                        //  came_from[y] := x
                        //  g_score[y] := tentative_g_score
                        //  f_score[y] := g_score[y] + h_score[y]
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

        private float DistBetween(Vertex current, Vertex destination) 
        {
            throw new System.NotImplementedException();
        }
    }
}
