namespace LeetCode.Set1xxx;
internal class Solution15xx
{
    public int GetLengthOfOptimalCompression(string s, int k)
    {
        var n = s.Length;
        var m = k;

        var dp = new int[110][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new int[110];

        for (int i = 1; i <= n; i++)
        {
            for (int j = 0; j <= i && j <= m; j++)
            {
                var needRemove = 0;
                var groupCount = 0;
                dp[i][j] = int.MaxValue;
                if (j != 0)
                    dp[i][j] = dp[i - 1][j - 1];
                for (k = i; k >= 1; k--)
                {
                    if (s[k - 1] != s[i - 1])
                        needRemove += 1;
                    else
                        groupCount += 1;

                    if (needRemove > j)
                        break;

                    var len = groupCount switch
                    {
                        1 => 1,
                        < 10 => 2,
                        < 100 => 3,
                        _ => 4
                    };

                    dp[i][j] = Math.Min(dp[i][j], dp[k - 1][j - needRemove] + len);
                }
            }
        }

        return dp[n][m];
    }

    [ProblemSolution("1535")]
    public int GetWinner(int[] arr, int k)
    {
        var max = arr.Max();
        if (arr[0] == max)
            return max;

        var ind = 0;
        var count = 0;

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] == max)
                return max;

            if (arr[ind] > arr[i])
            {
                count++;
            }
            else
            {
                ind = i;
                count = 1;
            }

            if (count >= k)
            {
                return arr[ind];
            }
        }

        return -1;
    }

    [ProblemSolution("1552")]
    public int MaxDistance(int[] position, int m)
    {
        Array.Sort(position);
        var min = 1;
        var max = position[^1] / (m - 1);

        var low = min;
        var high = max;
        var ans = 0;

        while (low <= high)
        {
            var mid = (low + high) / 2;
            var balls = 1;
            var lastBall = position[0];
            for (int i = 1; i < position.Length; i++)
            {
                if (position[i] - lastBall >= mid)
                {
                    balls++;
                    lastBall = position[i];
                    if (balls == m)
                        break;
                }
            }

            if (balls == m)
            {
                low = mid + 1;
                ans = mid;
            }
            else
                high = mid - 1;
        }

        return ans;
    }

    [ProblemSolution("1561")]
    public int MaxCoins(int[] piles)
    {
        Array.Sort(piles);
        var triples = piles.Length / 3;
        var result = 0;
        for (int i = 1; i <= triples; i++)
        {
            result += piles[piles.Length - 2 * i];
        }

        return result;
    }

    [ProblemSolution("1578")]
    public int MinCost(string colors, int[] neededTime)
    {
        var sum = neededTime[0];
        var max = neededTime[0];
        var count = 1;
        var total = 0;
        for (int i = 1; i < colors.Length; i++)
        {
            if (colors[i] == colors[i - 1])
            {
                sum += neededTime[i];
                max = Math.Max(max, neededTime[i]);
                count++;
            }
            else
            {
                if (count > 1)
                    total += (sum - max);
                sum = neededTime[i];
                max = neededTime[i];
                count = 1;
            }
        }

        if (count > 1)
            total += (sum - max);

        return total;
    }

    [ProblemSolution("1582")]
    public int NumSpecial(int[][] mat)
    {
        var rows = mat
            .Select(f => f.Count(el => el == 1))
            .ToList();

        var cols = new List<int>(mat[0].Length);
        for (int i = 0; i < mat[0].Length; i++)
        {
            var count = 0;
            for (int j = 0; j < mat.Length; j++)
            {
                if (mat[j][i] == 1)
                    count++;
            }

            cols.Add(count);
        }

        var total = 0;
        for (int i = 0; i < mat.Length; i++)
        {
            for (int j = 0; j < mat[0].Length; j++)
            {
                if (mat[i][j] == 1 && rows[i] == 1 && cols[j] == 1)
                    total++;
            }
        }

        return total;
    }
}
