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

    [ProblemSolution("1287")]
    public int FindSpecialInteger(int[] arr)
    {
        if (arr.Length == 1)
            return arr[0];

        var count = 1;
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] != arr[i - 1])
                count = 1;
            else
                count++;

            if (count > 25 * arr.Length / 100)
                return arr[i];
        }

        throw new ArgumentException("Array contains no elements occuring >25% of the time");
    }
}
