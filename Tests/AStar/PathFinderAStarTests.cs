namespace PathFinder.AStar.Tests 
{
    using Core;
    using AStar;
    using Xunit;
    using Shouldly;
    using System.Collections.Generic;

    using PathFinder.Core.Domain;

    public class PathFinderAStarTests 
    {
        [Theory, MemberData(nameof(GetVerticesMapScenarios))]
        public void TestVerticesScenarios(
            Vertex<int> source, Vertex<int> destination, PathFinderResult<int> exceptedResult)
        {
            // Arrange
            float HeuristicEstimateOfDistanceToGoalAlgorithm(Vertex<int> current, Vertex<int> dest)
            {
                float distance = current.Position > dest.Position ? current.Position - dest.Position : dest.Position - dest.Position;
                return distance < 0 ? -distance : distance;
            }

            var pathFinder = new PathFinder<int>(HeuristicEstimateOfDistanceToGoalAlgorithm);

            // Act
            var result = pathFinder.FindPath(source, destination);

            // Assert
            result.Success.ShouldBe(exceptedResult.Success);
        }

        public static IEnumerable<object[]> GetVerticesMapScenarios
        {
            get
            {
                #region #1 Source and Destination are null 
                yield return new object[] { null, null, new PathFinderResult<int>(), };
                #endregion
                #region #2 Destination is null 
                yield return new object[] { new Vertex<int>(), null, new PathFinderResult<int>(), };
                #endregion
                #region #3 Source is null 
                yield return new object[] { null, new Vertex<int>(), new PathFinderResult<int>(), };
                #endregion
                #region #4 Source and Destination does not have edges
                yield return new object[] { new Vertex<int>(), new Vertex<int>(), new PathFinderResult<int>(), };
                #endregion

                #region #6 Source and Destination are connected only by one edge
                var source = new Vertex<int> { Position = 5 };
                var destination = new Vertex<int> { Position = 10 };
                var sourceEdge = new Edge<int> {  Weight = 3, A = source, B = destination };
                var destinationEdge = new Edge<int> {  Weight = 3, A = destination, B = source };
                source.Edges.Add(sourceEdge);
                destination.Edges.Add(destinationEdge);

                yield return new object[] { source, destination, new PathFinderResult<int> { Success = true } };
                #endregion
            }
        }
    }
}
