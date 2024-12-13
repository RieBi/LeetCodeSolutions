using System.Collections;
using System.Runtime.Serialization;

namespace LeetCode.Set2XXX;
internal class Solution25XX
{
    [ProblemSolution("2501")]
    public int LongestSquareStreak(int[] nums)
    {
        Array.Sort(nums);
        var dict = new Dictionary<int, int>();

        var longest = -1;

        for (int i = nums.Length - 1; i >= 0; i--)
        {
            var count = 1;
            if (nums[i] < 317 && dict.TryGetValue(nums[i] * nums[i], out var value))
                count = value + 1;

            dict[nums[i]] = count;
            if (count > 1)
                longest = Math.Max(longest, count);
        }

        return longest;
    }

    [ProblemSolution("2530")]
    public long MaxKelements(int[] nums, int k)
    {
        var queue = new PriorityQueue<int, int>();
        for (int i = 0; i < nums.Length; i++)
            queue.Enqueue(nums[i], -nums[i]);

        var score = 0L;
        for (int i = 0; i < k; i++)
        {
            var max = queue.Dequeue();
            score += max;
            var newVal = (int)Math.Ceiling(max / 3.0);
            queue.Enqueue(newVal, -newVal);
        }

        return score;
    }

    [ProblemSolution("2540")]
    public int GetCommon(int[] nums1, int[] nums2)
    {
        var p1 = 0;
        var p2 = 0;
        while (p1 < nums1.Length && p2 < nums2.Length)
        {
            if (nums1[p1] == nums2[p2])
                return nums1[p1];
            else if (nums1[p1] < nums2[p2])
                p1++;
            else
                p2++;
        }

        return -1;
    }

    [ProblemSolution("2563")]
    public long CountFairPairs(int[] nums, int lower, int upper)
    {
        var leftComparer = Comparer<int>.Create((a, b) =>
        {
            var comp = a.CompareTo(b);
            return comp == 0 ? 1 : comp;
        });

        var rightComparer = Comparer<int>.Create((a, b) =>
        {
            var comp = a.CompareTo(b);
            return comp == 0 ? -1 : comp;
        });

        Array.Sort(nums);

        var result = 0L;
        for (int i = 1; i < nums.Length; i++)
        {
            var left = Array.BinarySearch(nums, 0, i, lower - nums[i], leftComparer);

            if (left < 0)
                left = ~left;

            var right = Array.BinarySearch(nums, 0, i, upper - nums[i], rightComparer);
            if (right < 0)
                right = ~right - 1;

            var length = right - left + 1;
            if (length > 0)
                result += length;
        }

        return result;
    }

    [ProblemSolution("2582")]
    public int PassThePillow(int n, int time)
    {
        time %= (n - 1) * 2;
        if (time < n)
            return time + 1;
        else
            return n - (time % n + 1);
    }

    [ProblemSolution("2583")]
    public long KthLargestLevelSum(TreeNode root, int k)
    {
        var node = root;
        var level = 1;
        var stack = new Stack<(TreeNode node, int level)>();

        var sums = new List<long>();
        while (stack.Count > 0 || node is not null)
        {
            while (node is not null)
            {
                stack.Push((node, level));
                node = node.left;
                level++;
            }

            var last = stack.Pop();

            while (sums.Count < last.level)
                sums.Add(0);

            sums[last.level - 1] += last.node.val;

            (node, level) = (last.node.right, last.level + 1);
        }

        if (k > sums.Count)
            return -1;

        return sums.OrderDescending().Skip(k - 1).First();
    }

    [ProblemSolution("2593")]
    public long FindScore(int[] nums)
    {
        var indices = Enumerable.Range(0, nums.Length)
            .OrderBy(f => nums[f])
            .ToList();

        var bits = new BitArray(indices.Count);

        var sum = 0L;
        for (int i = 0; i < indices.Count; i++)
        {
            if (bits[indices[i]])
                continue;

            sum += nums[indices[i]];

            bits[indices[i]] = true;
            if (indices[i] > 0)
                bits[indices[i] - 1] = true;
            if (indices[i] < indices.Count - 1)
                bits[indices[i] + 1] = true;
        }

        return sum;
    }

    [ProblemSolution("2598")]
    public int FindSmallestInteger(int[] numbers, int value)
    {
        var mods = new int[Math.Min(numbers.Length, value)];
        for (int i = 0; i < numbers.Length; i++)
        {
            var mod = numbers[i] % value;
            if (mod < 0)
                mod = value - -mod;

            if (mod < mods.Length)
                mods[mod]++;
        }

        var min = int.MaxValue;
        int minInd = 0;
        for (; minInd < mods.Length && mods[minInd] > 0; minInd++)
            min = Math.Min(min, mods[minInd]);

        if (minInd < value)
            return minInd;

        var secondInd = Array.FindIndex(mods, f => f == min);
        return value * min + secondInd;
    }
}
