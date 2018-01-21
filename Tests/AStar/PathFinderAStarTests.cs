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
        public static IEnumerable<object[]> GetVerticesMapScenarios
        {
            get 
            {
                //// #1 Source and Destination are null
                //yield return new object[] { null, null, new PathFinderResult() };
                //// #2 Source and Destination does not have edges
                //yield return new object[] { new Vertex(), new Vertex(), new PathFinderResult() };
                // #3 Source and Destination are not connected
                #region
                var source = new Vertex {  Id = 5 };
                var destination = new Vertex {  Id = 10 };
                destination.Edges.Add(new Edge());
                var b = new Vertex {  Id = 6 };
                var sourceToBEdge = new Edge { A = source, B = b, Weight = 2 };
                source.Edges.Add(sourceToBEdge);
                b.Edges.Add(new Edge { A = b, B = source, Weight = 2 });

                yield return new object[]
                             {
                                 source,
                                 destination,
                                 new PathFinderResult() 
                             };
                #endregion
            }
        }

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
    }
}
