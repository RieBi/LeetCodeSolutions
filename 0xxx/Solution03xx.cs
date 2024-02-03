using System.Collections.Specialized;
using System.Text;

namespace LeetCode.Set0xxx;
internal class Solution03xx
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
