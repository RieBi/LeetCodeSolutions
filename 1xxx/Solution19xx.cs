using System.Text;

namespace LeetCode.Set1XXX;
internal class Solution19XX
{
    [ProblemSolution("1903")]
    public string LargestOddNumber(string num)
    {
        var largestOddDigit = num.LastIndexOfAny(new char[] {'1', '3', '5', '7', '9'});
        return largestOddDigit == -1 ? "" : num.Substring(0, largestOddDigit + 1);
    }

    [ProblemSolution("1905")]
    public int CountSubIslands(int[][] grid1, int[][] grid2)
    {
        var result = 0;

        List<(int i, int j)> dirs = [(1, 0), (-1, 0), (0, 1), (0, -1)];

        for (int i = 0; i < grid2.Length; i++)
        {
            for (int j = 0; j < grid2[0].Length; j++)
            {
                if (grid2[i][j] == 1)
                    traverse(i, j);
            }
        }

        return result;

        void traverse(int i, int j)
        {
            var queue = new Queue<(int i, int j)>();
            queue.Enqueue((i, j));
            grid2[i][j] = 0;

            var matched = true;

            while (queue.Count > 0)
            {
                var last = queue.Dequeue();
                if (grid1[last.i][last.j] == 0)
                    matched = false;

                for (int d = 0; d < 4; d++)
                {
                    (int i, int j) other = (last.i + dirs[d].i, last.j + dirs[d].j);
                    if (other.i >= 0 && other.j >= 0 && other.i < grid2.Length && other.j < grid2[0].Length
                        && grid2[other.i][other.j] == 1)
                    {
                        grid2[other.i][other.j] = 0;
                        queue.Enqueue(other);
                    }
                }
            }

            if (matched)
                result++;
        }
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

    [ProblemSolution("1937")]
    public long MaxPoints(int[][] points)
    {
        var discussed = points[0].Select(f => (long)f).ToArray();
        var discussing = new long[points[0].Length];

        for (int i = 1; i < points.Length; i++)
        {
            var actual = points[i];

            var currentVal = 0L;
            for (int j = 0; j < discussing.Length; j++)
            {
                currentVal = Math.Max(currentVal, discussed[j]);
                discussing[j] = Math.Max(discussing[j], currentVal-- + actual[j]);
            }

            currentVal = 0L;
            for (int j = discussing.Length - 1; j >= 0; j--)
            {
                currentVal = Math.Max(currentVal, discussed[j]);
                discussing[j] = Math.Max(discussing[j], currentVal-- + actual[j]);
            }

            (discussed, discussing) = (discussing, discussed);
            for (int j = 0; j < discussing.Length; j++)
                discussing[j] = 0;
        }

        return discussed.Max();
    }

    [ProblemSolution("1945")]
    public int GetLucky(string s, int k)
    {
        var str = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
            str.Append((s[i] - 'a' + 1).ToString());

        var num = str.ToString();
        var result = 0;
        for (int i = 0; i < num.Length; i++)
            result += num[i] - '0';

        for (int i = 1; i < k; i++)
        {
            var temp = 0;
            while (result > 0)
            {
                temp += result % 10;
                result /= 10;
            }

            result = temp;
        }

        return result;
    }

    [ProblemSolution("1963")]
    public int MinSwaps(string s)
    {
        var misses = 0;
        var cur = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '[')
                cur++;
            else if (cur > 0)
                cur--;
            else
                misses++;
        }

        return misses - misses / 2;
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