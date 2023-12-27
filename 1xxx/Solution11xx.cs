using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace LeetCode.Set1xxx;
internal class Solution11xx
{
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
