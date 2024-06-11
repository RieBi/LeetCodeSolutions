using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace LeetCode.Set1xxx;
internal class Solution11xx
{
    [ProblemSolution("1122")]
    public int[] RelativeSortArray(int[] arr1, int[] arr2)
    {
        var groups = arr1.GroupBy(f => f).ToDictionary(
            f => f.Key,
            f => f.Count());

        var result = new int[arr1.Length];
        var ind = 0;
        for (int i = 0; i < arr2.Length; i++)
        {
            if (groups.TryGetValue(arr2[i], out var value))
            {
                for (int j = 0; j < value; j++)
                {
                    result[ind++] = arr2[i];
                }

                groups.Remove(arr2[i]);
            }
        }

        var endInd = ind;
        while (ind < result.Length)
        {
            var pair = groups.First();
            for (int i = 0; i < pair.Value; i++)
            {
                result[ind++] = pair.Key;
            }

            groups.Remove(pair.Key);
        }

        if (endInd < result.Length)
            Array.Sort(result, endInd, result.Length - endInd);

        return result;
    }

    [ProblemSolution("1143")]
    public int LongestCommonSubsequence(string text1, string text2)
    {
        var dp = new int[text1.Length + 1][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new int[text2.Length + 1];

        for (int i = 1; i < dp.Length; i++)
        {
            for (int j = 1; j < dp[0].Length; j++)
            {
                if (text1[i - 1] == text2[j - 1])
                    dp[i][j] = dp[i - 1][j - 1] + 1;
                else
                    dp[i][j] = Math.Max(dp[i][j - 1], dp[i - 1][j]);
            }
        }

        return dp[^1][^1];
    }

    [ProblemSolution("1155")]
    public int NumRollsToTarget(int n, int k, int target)
    {
        if (target < n || target > (n * k))
            return 0;

        var max = n * k + 1;

        var dp = new int[n + 1, max];
        dp[n, 0] = 1;

        var modulo = 1_000_000_007;

        for (int dice = n; dice > 0; dice--)
        {
            for (int sum = 0; sum < max; sum++)
            {
                if (dp[dice, sum] == 0)
                    continue;
                for (int i = 1; i <= k; i++)
                {
                    dp[dice - 1, sum + i] += dp[dice, sum];
                    dp[dice - 1, sum + i] %= modulo;
                }
            }
        }

        return dp[0, target];
    }

    [ProblemSolution("1160")]
    public int CountCharacters(string[] words, string chars)
    {
        var charmap = mapArray(chars);
        var result = 0;
        for (int i = 0; i < words.Length; i++)
        {
            if (isWordGood(words[i]))
                result += words[i].Length;
        }

        return result;

        int[] mapArray(string str)
        {
            var map = new int[26];
            for (int i = 0; i < str.Length; i++)
            {
                map[str[i] - 'a']++;
            }

            return map;
        }

        bool isWordGood(string word)
        {
            var wordmap = mapArray(word);

            for (int i = 0; i < wordmap.Length; i++)
            {
                if (wordmap[i] > charmap[i])
                    return false;
            }

            return true;
        }
    }
}
