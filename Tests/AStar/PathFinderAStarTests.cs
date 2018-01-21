namespace PathFinder.AStar.Tests 
{
    using Core;
    using AStar;
    using Xunit;
    using Shouldly;
    using System.Collections.Generic;
    using System.Linq;

    public class PathFinderAStarTests 
    {
        [Theory, MemberData(nameof(GetVerticesMapScenarios))]
        public void TestVerticesScenarios(
            Core.Vertex source, Core.Vertex destination, PathFinderResult exceptedResult)
        {
            // Arrange
            var pathFinder = new PathFinder();

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
                yield return new object[] { null, null, new PathFinderResult(), };
                #endregion
                #region #2 Destination is null 
                yield return new object[] { new Vertex(), null, new PathFinderResult(), };
                #endregion
                #region #3 Source is null 
                yield return new object[] { null, new Vertex(), new PathFinderResult(), };
                #endregion
                #region #4 Source and Destination does not have edges
                yield return new object[] { new Vertex(), new Vertex(), new PathFinderResult(), };
                #endregion

                #region #6 Source and Destination are connected only by one edge
                var source = new Vertex { Id = 5 };
                var destination = new Vertex { Id = 10 };
                var sourceEdge = new Edge {  Weight = 3, A = source, B = destination };
                var destinationEdge = new Edge {  Weight = 3, A = destination, B = source };
                source.Edges.Add(sourceEdge);
                destination.Edges.Add(destinationEdge);

                yield return new object[] { source, destination, new PathFinderResult { Success = true } };
                #endregion
            }
        }
    }
}
