namespace LeetCode.Set1xxx;
internal class Solution17xx
{
    [ProblemSolution("1716")]
    public int TotalMoney(int n)
    {
        var total = 0;
        for (int i = 1; i <= n; i++)
            total += ((int)Math.Ceiling((double)i / 7) + (i - 1) % 7);

        return total;
    }

    [ProblemSolution("1727")]
    public int LargestSubmatrix(int[][] matrix)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;

        for (int i = 1; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] != 0)
                    matrix[i][j] = matrix[i - 1][j] + 1;
            }
        }

        var max = 0;
        for (int i = 0; i < m; i++)
        {
            var row = matrix[i];
            Array.Sort(row);
            for (int j = n; j > 0; j--)
            {
                var localMax = j * row[n - j];
                max = Math.Max(max, localMax);
            }
        }

        return max;
    }

    [ProblemSolution("1759")]
    public int CountHomogenous(string s)
    {
        var dp = new long[s.Length];
        dp[0] = 1;

        for (int i = 1; i < dp.Length; i++)
        {
            if (s[i] == s[i - 1])
            {
                dp[i] = dp[i - 1] + 1;
            }
            else
            {
                dp[i] = 1;
            }
        }
        long modulo = 1000000000 + 7;
        long total = 0;
        long n = 1;
        for (int i = 1; i < dp.Length; i++)
        {
            if (dp[i] <= n)
            {
                var sum = n * (n + 1) / 2;
                total += sum;
                total %= modulo;
                n = 1;
            }
            else
            {
                n = dp[i];
            }
        }
        total += (n * (n + 1) / 2);

        return (int)(total %= modulo);
    }
}