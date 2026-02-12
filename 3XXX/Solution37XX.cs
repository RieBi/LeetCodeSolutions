namespace LeetCode.Set3XXX;

public class Solution37XX
{
    [ProblemSolution("3713")]
    public int LongestBalanced(string s)
    {
        var counts = new int[s.Length][];
        var empty = new int[26];

        for (var i = 0; i < s.Length; i++)
            counts[i] = new int[26];

        counts[0][s[0] - 'a']++;

        for (var i = 1; i < s.Length; i++)
        {
            Array.Copy(counts[i - 1], counts[i], 26);
            counts[i][s[i] - 'a']++;
        }

        var longest = Math.Min(2, s.Length);

        for (var i = 0; i < s.Length; i++)
        {
            for (var j = i + longest; j < s.Length; j++)
            {
                var ar1 = i == 0 ? empty : counts[i - 1];
                var ar2 = counts[j];

                var expected = 0;
                var bad = false;

                for (var ind = 0; ind < 26; ind++)
                {
                    if (ar2[ind] - ar1[ind] == 0)
                        continue;

                    if (expected == 0)
                        expected = ar2[ind] - ar1[ind];

                    if (ar2[ind] - ar1[ind] != expected)
                    {
                        bad = true;
                        break;
                    }
                }

                if (!bad)
                    longest = j - i + 1;
            }
        }

        return longest;
    }
}