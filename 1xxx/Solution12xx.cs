namespace LeetCode.Set1xxx;
internal class Solution12xx
{
    [ProblemSolution("1235")]
    public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
    {
        (int startTime, int endTime, int profit)[] jobs = startTime.Zip(endTime, profit)
            .OrderBy(f => f.Second)
            .ToArray();
        var dp = new List<(int, int)> { (0, 0) };

        for (int i = 0; i < jobs.Length; i++)
        {
            var job = jobs[i];
            var comparer = Comparer<(int, int)>.Create((a, b) => a.Item1.CompareTo(b.Item1));
            var startInd = dp.BinarySearch((job.startTime, 0), comparer);
            if (startInd < 0)
                startInd = ~startInd - 1;
            var endInd = dp.BinarySearch((job.endTime, 0), comparer);
            if (endInd < 0)
            {
                dp.Add(dp[^1]);
                endInd = ~endInd;
            }

            dp[endInd] = (job.endTime, Math.Max(dp[^1].Item2, dp[startInd].Item2 + job.profit));
        }

        return dp[^1].Item2;
    }

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
