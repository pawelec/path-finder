namespace PathFinder.AStar.Tests 
{
    using System;

    using Core;
    using AStar;
    using Xunit;
    using Shouldly;
    using System.Collections.Generic;

    using PathFinder.Core.Domain;

    public class PathFinderAStarTests 
    {
        [Theory, MemberData(nameof(GetVerticesLineMapScenarios))]
        public void TestVerticesLineScenarios(
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

        [Theory, MemberData(nameof(GetVerticesCartesianMapScenarios))]
        public void TestVerticesCartesianPointScenarios(
            Vertex<Point> source, Vertex<Point> destination, PathFinderResult<Point> exceptedResult)
        {
            // Arrange
            float HeuristicEstimateOfDistanceToGoalAlgorithm(Vertex<Point> current, Vertex<Point> dest)
            {
                double distance = Math.Sqrt(Math.Pow(dest.Position.X - current.Position.X, 2) + Math.Pow(dest.Position.Y - current.Position.Y, 2));
                return (float)distance;
            }

            var pathFinder = new PathFinder<Point>(HeuristicEstimateOfDistanceToGoalAlgorithm);

            // Act
            var result = pathFinder.FindPath(source, destination);

            // Assert
            result.Success.ShouldBe(exceptedResult.Success);
        }

        public static IEnumerable<object[]> GetVerticesLineMapScenarios
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
            }
        }

        public static IEnumerable<object[]> GetVerticesCartesianMapScenarios
        {
            get
            {
                #region #1
                yield return new object[] { null, null, new PathFinderResult<Point>(), };
                #endregion
                #region #2 Destination is null 
                yield return new object[] { new Vertex<Point>(), null, new PathFinderResult<Point>(), };
                #endregion
                #region #3 Source is null 
                yield return new object[] { null, new Vertex<Point>(), new PathFinderResult<Point>(), };
                #endregion
                #region #4 Source and Destination does not have edges
                yield return new object[] { new Vertex<Point>(), new Vertex<Point>(), new PathFinderResult<Point>(), };
                #endregion
            }
        }
    }
}
