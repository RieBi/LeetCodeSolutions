using System.Collections.Specialized;
using System.Text;

namespace LeetCode.Set0xxx;
internal class Solution03XX
{
    [ProblemSolution("300")]
    public int LengthOfLIS(int[] nums)
    {
        var dp = new List<int>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            var ind = dp.BinarySearch(nums[i]);
            if (ind < 0)
            {
                ind = ~ind;
            }

            if (ind == dp.Count)
                dp.Add(nums[i]);
            else
                dp[ind] = nums[i];
        }

        return dp.Count;
    }

    [ProblemSolution("330")]
    public int MinPatches(int[] nums, int n)
    {
        var sum = 1L;
        var max = 1L;
        var i = nums[0] == 1 ? 1 : 0;
        var count = i == 0 ? 1 : 0;

        while (i < nums.Length && sum < n)
        {
            if (nums[i] <= sum + 1)
            {
                sum += nums[i];
                max = nums[i];
                i++;
            }
            else
            {
                max = sum + 1;
                sum += max;
                count++;
            }
        }

        while (sum < n)
        {
            max = sum + 1;
            sum += max;
            count++;
        }

        return count;
    }

    [ProblemSolution("336")]
    public IList<IList<int>> PalindromePairs(string[] words)
    {
        var root = new PalindromNode();
        for (int i1 = 0; i1 < words.Length; i1++)
        {
            string? word = words[i1];
            var node = root;
            for (int i = 0; i < word.Length; i++)
            {
                if (!node.Children.ContainsKey(word[i]))
                    node.Children[word[i]] = new();

                node = node.Children[word[i]];
            }

            node.IsWord = true;
            node.Index = i1;
        }

        var result = new List<IList<int>>();
        var list = new List<char>();
        for (int i1 = 0; i1 < words.Length; i1++)
        {
            string word = words[i1];
            if (word == "")
            {
                for (int c = 0; c < words.Length; c++)
                    if (c != i1 && IsPalindrome(words[c], 0, words[c].Length - 1))
                        result.Add([i1, c]);
            }

            var node = root;
            var i = word.Length - 1;
            for (; i >= 0; i--)
            {
                if (node.Children.TryGetValue(word[i], out var value))
                    node = value;
                else
                    break;

                if (node.IsWord && IsPalindrome(word, 0, i - 1) && node.Index != i1)
                    result.Add([node.Index, i1]);
            }

            if (i >= 0)
                continue;

            propagate(node, i1);
        }

        return result;

        void propagate(PalindromNode node, int ind)
        {
            foreach (var child in node.Children)
            {
                list.Add(child.Key);
                if (child.Value.IsWord && IsPalindromeList(list, 0, list.Count - 1) && child.Value.Index != ind)
                {
                    result.Add([child.Value.Index, ind]);
                }

                propagate(child.Value, ind);
                list.RemoveAt(list.Count - 1);
            }
        }

        bool IsPalindrome(string str, int left, int right)
        {
            while (left < right)
            {
                if (str[left] != str[right])
                    return false;

                left++;
                right--;
            }

            return true;
        }

        bool IsPalindromeList(List<char> str, int left, int right)
        {
            while (left < right)
            {
                if (str[left] != str[right])
                    return false;

                left++;
                right--;
            }

            return true;
        }
    }

    [ProblemSolution("336")]
    public class PalindromNode
    {
        public Dictionary<char, PalindromNode> Children { get; } = [];
        public bool IsWord { get; set; } = default;
        public int Index { get; set; } = -1;
    }

    [ProblemSolution("344")]
    public void ReverseString(char[] s)
    {
        substitute(0);
        void substitute(int index)
        {
            (s[index], s[s.Length - index - 1]) = (s[s.Length - index - 1], s[index]);
            if (index < s.Length / 2 - 1)
                substitute(++index);
        }
    }

    [ProblemSolution("347")]
    public int[] TopKFrequent(int[] nums, int k)
    {
        return nums.GroupBy(f => f)
            .OrderByDescending(f => f.Count())
            .Select(f => f.Key)
            .Take(k)
            .ToArray();
    }

    [ProblemSolution("349")]
    public int[] Intersection(int[] nums1, int[] nums2) => nums1.Intersect(nums2).ToArray();

    [ProblemSolution("350")]
    public int[] Intersect(int[] nums1, int[] nums2)
    {
        var group1 = nums1.GroupBy(f => f).Select(f => (f.Key, f.Count())).ToDictionary();
        var group2 = nums2.GroupBy(f => f).Select(f => (f.Key, f.Count())).ToDictionary();

        var result = new List<int>();
        foreach (var group in group1)
        {
            if (!group2.TryGetValue(group.Key, out int value))
                continue;
            var count = Math.Min(group.Value, value);
            for (int i = 0; i < count; i++)
                result.Add(group.Key);
        }

        return result.ToArray();
    }

    [ProblemSolution("368")]
    public IList<int> LargestDivisibleSubset(int[] nums)
    {
        Array.Sort(nums);
        var dp = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            dp[i] = 1;
            for (int j = i - 1; j >= 0; j--)
            {
                if (nums[i] % nums[j] == 0)
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
            }
        }

        var list = new List<int>();
        var max = dp.Max();
        var ind = dp.Length - 1;
        while (dp[ind] != max)
            ind--;

        var num = nums[ind];
        while (max >= 1)
        {
            if (num % nums[ind] == 0 && dp[ind] == max)
            {
                list.Add(nums[ind]);
                max--;
                num = nums[ind];
            }

            ind--;
        }

        return list;
    }

    [ProblemSolution("380")]
    public class RandomizedSet
    {
        private readonly OrderedDictionary dick = [];

        public bool Insert(int val)
        {
            if (dick.Contains(val))
                return false;
            dick.Add(val, val);
            return true;
        }

        public bool Remove(int val)
        {
            if (!dick.Contains(val))
                return false;
            dick.Remove(val);
            return true;
        }

        public int GetRandom()
        {
            return (int)dick[Random.Shared.Next(dick.Count)]!;
        }
    }

    [ProblemSolution("387")]
    public int FirstUniqChar(string s)
    {
        return s.Zip(Enumerable.Range(0, s.Length))
            .GroupBy(f => f.First, s => s.Second)
            .Where(f => f.Count() == 1)
            .Select(f => f.Single())
            .FirstOrDefault(-1);
    }

    [ProblemSolution("394")]
    public string DecodeString(string s)
    {
        s = "1[" + s + "]";
        var sb = new StringBuilder();
        decodePortion(0);
        return sb.ToString();

        int decodePortion(int start)
        {
            var num = new StringBuilder();

            var ind = start;
            while (char.IsDigit(s[ind]))
            {
                num.Append(s[ind]);
                ind++;
            }

            var repeat = int.Parse(num.ToString());
            var repeatStart = ind + 1;

            for (int i = 0; i < repeat; i++)
            {
                var count = 1;
                ind = repeatStart;
                for (; count > 0; ind++)
                {
                    if (char.IsDigit(s[ind]))
                        ind = decodePortion(ind) - 1;
                    else if (s[ind] == ']')
                        count--;
                    else
                        sb.Append(s[ind]);
                }
            }

            return ind;
        }
    }
}
