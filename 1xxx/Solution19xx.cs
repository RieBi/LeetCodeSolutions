namespace LeetCode.Set1XXX;
internal class Solution19XX
{
    [ProblemSolution("1903")]
    public string LargestOddNumber(string num)
    {
        var largestOddDigit = num.LastIndexOfAny(new char[] {'1', '3', '5', '7', '9'});
        return largestOddDigit == -1 ? "" : num.Substring(0, largestOddDigit + 1);
    }

    [ProblemSolution("1913")]
    public int MaxProductDifference(int[] nums)
    {
        var maxSet = new List<int>(nums[..4].OrderByDescending(f => f).Take(2));
        var minSet = new List<int>(nums[..4].OrderBy(f => f).Take(2));
        for (int i = 4; i < nums.Length; i++)
        {
            var num = nums[i];
            if (maxSet.Min() < num)
            {
                maxSet.Remove(maxSet.Min());
                maxSet.Add(num);
            }

            if (minSet.Max() > num)
            {
                minSet.Remove(minSet.Max());
                minSet.Add(num);
            }
        }

        return (maxSet[0] * maxSet[1] - minSet[0] * minSet[1]);
    }

    [ProblemSolution("1915")]
    public long WonderfulSubstrings(string word)
    {
        var N = word.Length;

        var freq = new Dictionary<int, int>();

        freq[0] = 1;

        var mask = 0;
        var res = 0L;
        for (int i = 0; i < N; i++)
        {
            char c = word[i];
            var bit = c - 'a';

            mask ^= (1 << bit);

            res += freq.TryGetValue(mask, out var value) ? value : 0;

            freq[mask] = freq.TryGetValue(mask, out value) ? value + 1 : 1;

            for (int j = 0; j < 10; j++)
            {
                res += freq.TryGetValue(mask ^ (1 << j), out value) ? value : 0;
            }
        }

        return res;
    }

    [ProblemSolution("1921")]
    public int EliminateMaximum(int[] dist, int[] speed)
    {
        var time = new int[dist.Length];
        for (int i = 0; i < dist.Length; i++)
        {
            var dist0 = (double)dist[i];
            var speed0 = (double)speed[i];
            var time0 = Math.Ceiling(dist0 / speed0);
            time[i] = (int)time0;
        }

        var prefs = new int[dist.Length];
        for (int i = 0; i < dist.Length; i++)
        {
            var t = time[i];
            if (t > dist.Length - 1)
                continue;
            prefs[t] += 1;
        }
        for (int i = 1; i < dist.Length; i++)
        {
            prefs[i] = prefs[i] + prefs[i - 1];
        }

        int n = 0;
        while (n < dist.Length)
        {
            if (prefs[n] <= n)
            {
                n++;
            }
            else
            {
                break;
            }
        }

        return n;
    }

    [ProblemSolution("1930")]
    public int CountPalindromicSubsequence(string s)
    {
        var palindromes = new HashSet<string>();
        var startIndices = new int[26];
        var endIndices = new int[26];

        for (int i = 0; i < 26; i++)
        {
            startIndices[i] = -1;
            endIndices[i] = -1;
        }

        for (int i = 0; i < s.Length; i++)
        {
            var charInd = s[i] - 'a';

            if (startIndices[charInd] == -1)
                startIndices[charInd] = i;

            endIndices[charInd] = i;
        }

        for (int i = 0; i < 26; i++)
        {
            if (startIndices[i] == -1)
                continue;

            for (int j = startIndices[i] + 1; j < endIndices[i]; j++)
            {
                var ch = (char)('a' + i);
                var newStr = new String(new char[] { ch, s[j], ch });
                palindromes.Add(newStr);
            }
        }

        return palindromes.Count;
    }

    [ProblemSolution("1980")]
    public string FindDifferentBinaryString(string[] nums)
    {
        var integers = new List<int>();
        var max = (int)Math.Pow(2, nums.Length);
        for (int i = 0; i < nums.Length; i++)
        {
            var num = Convert.ToInt32(nums[i], 2);
            integers.Add(num);
        }

        integers.Sort();

        for (int i = 0; i < max; i++)
        {
            if (i >= integers.Count || i != integers[i])
                return Convert.ToString(i, 2).PadLeft(nums.Length, '0');
        }
        return "";
    }
}