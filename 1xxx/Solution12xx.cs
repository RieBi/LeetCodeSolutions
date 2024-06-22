using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace LeetCode.Set1xxx;
internal class Solution12xx
{
    [ProblemSolution("1207")]
    public bool UniqueOccurrences(int[] arr)
    {
        var groups = arr.GroupBy(f => f).Select(f => f.Count());
        return groups.Count() == groups.Distinct().Count();
    }

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

    [ProblemSolution("1239")]
    public int MaxLength(IList<string> arr)
    {
        var ints = new List<int>();
        foreach (var word in arr)
        {
            ints.Add(0);
            foreach (var ch in word)
            {
                var mask = 1 << (ch - 'a');
                if ((ints[^1] & mask) != 0)
                {
                    ints.RemoveAt(ints.Count - 1);
                    break;
                }

                ints[^1] |= mask;
            }
        }

        var max = 0;
        if (ints.Count == 0)
            return max;

        for (int i = 0; i < ints.Count; i++)
            dfs(i, 0);
        return max;

        void dfs(int ind, int state)
        {
            if ((state & ints[ind]) != 0)
                return;

            state |= ints[ind];
            max = Math.Max(max, weight(state));
            for (int i = ind + 1; i < ints.Count; i++)
                dfs(i, state);
        }

        static int weight(int num)
        {
            var mask = 1;
            var count = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((mask & num) != 0)
                    count++;
                mask <<= 1;
            }

            return count;
        }
    }

    [ProblemSolution("1248")]
    public int NumberOfSubarrays(int[] nums, int k)
    {
        var indices = new List<int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] % 2 == 1)
                indices.Add(i);
        }

        var niceCount = 0;

        for (int i = 0; i + k - 1 < indices.Count; i++)
        {
            var ind = indices[i];
            var ind2 = indices[i + k - 1];
            var prev = i == 0 ? -1 : indices[i - 1];
            var next = i + k - 1 == indices.Count - 1 ? nums.Length : indices[i + k];
            var sum1 = ind - prev;
            var sum2 = next - ind2;
            niceCount += sum1 * sum2;
        }

        return niceCount;
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

    [ProblemSolution("1289")]
    public int MinFallingPathSum(int[][] grid)
    {
        for (int i = 1; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                var min = int.MaxValue;
                for (int p = 0; p < grid[0].Length; p++)
                {
                    if (p == j)
                        continue;
                    min = Math.Min(min, grid[i][j] + grid[i - 1][p]);
                }

                grid[i][j] = min;
            }
        }

        return grid[^1].Min();
    }

    [ProblemSolution("1291")]
    public IList<int> SequentialDigits(int low, int high)
    {
        var starter = 1;
        var adder = 1;

        var list = new List<int>();
        for (int i = 1; i <= 9; i++)
        {
            var num = starter;
            starter *= 10;
            starter += i + 1;

            for (int j = 0; j < (10 - i); j++)
            {
                if (num >= low && num <= high)
                    list.Add(num);
                num += adder;
            }

            adder *= 10;
            adder += 1;
        }

        return list;
    }
}
