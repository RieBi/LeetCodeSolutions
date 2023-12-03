namespace LeetCode.Set1xxx;
internal class Solution12xx
{
    [ProblemSolution("1266")]
    public int MinTimeToVisitAllPoints(int[][] points)
    {
        var resultTime = 0;
        for (int i = 1; i < points.Length; i++)
        {
            var pointA = new { x = points[i - 1][0], y = points[i - 1][1] };
            var pointB = new { x = points[i][0], y = points[i][1] };
            var distance = Math.Max(Math.Abs(pointB.x - pointA.x), Math.Abs(pointB.y - pointA.y));
            resultTime += distance;
        }

        return resultTime;
    }
}
