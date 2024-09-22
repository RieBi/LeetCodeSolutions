using System.Diagnostics;
using System.Text;

namespace LeetCode.Set0XXX;
internal class Solution04XX
{
    [ProblemSolution("404")]
    public int SumOfLeftLeaves(TreeNode root)
    {
        var queue = new Queue<(TreeNode node, bool isLeft)>();
        queue.Enqueue((root, false));

        var sum = 0;
        while (queue.Count > 0)
        {
            var (node, left) = queue.Dequeue();
            if (left && node.left is null && node.right is null)
                sum += node.val;

            if (node.left is not null)
                queue.Enqueue((node.left, true));
            if (node.right is not null)
                queue.Enqueue((node.right, false));
        }

        return sum;
    }

    [ProblemSolution("413")]
    public int NumberOfArithmeticSlices0(int[] nums)
    {
        var count = 1;
        var diff = 0;
        var result = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] - nums[i - 1] == diff)
                count++;
            else
            {
                if (count >= 3)
                {
                    count -= 2;
                    result += (count * (count + 1)) / 2;
                }

                count = 2;
                diff = nums[i] - nums[i - 1];
            }
        }

        if (count >= 3)
        {
            count -= 2;
            result += (count * (count + 1)) / 2;
        }

        return result;
    }

    [ProblemSolution("421")]
    public int FindMaximumXOR(int[] nums)
    {
        var root = new XorNode();
        foreach (var v in nums)
            insert(v);

        var max = 0;
        foreach (var v in nums)
            max = Math.Max(max, xored(v));

        return max;

        int xored(int cur)
        {
            var node = root;
            var num = 0;
            for (int i = 31; i >= 0; i--)
            {
                var map = 1 << i;
                var bit = (cur & map) == map ? 1 : 0;
                if (bit == 1 && node.Children[0] is not null || bit == 0 && node.Children[1] is null)
                    node = node.Children[0];
                else
                {
                    node = node.Children[1];
                    num |= map;
                }
            }

            return num ^ cur;
        }

        void insert(int number)
        {
            var node = root;
            for (int i = 31; i >= 0; i--)
            {
                var map = 1 << i;
                var ind = (number & map) == map ? 1 : 0;
                if (node.Children[ind] is null)
                    node.Children[ind] = new();

                node = node.Children[ind];
            }
        }
    }

    [ProblemSolution("421")]
    public class XorNode()
    {
        public XorNode[] Children { get; } = new XorNode[2];
    }

    [ProblemSolution("429")]
    public IList<IList<int>> LevelOrder(Node root)
    {
        var queue = new Queue<Node>();
        List<IList<int>> result = [];
        if (root is not null)
            queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var levelCount = queue.Count;
            var list = new List<int>(levelCount);
            for (int i = 0; i < levelCount; i++)
            {
                var node = queue.Dequeue();
                list.Add(node.val);
                foreach (var child in node.children)
                    queue.Enqueue(child);
            }

            result.Add(list);
        }

        return result;
    }

    [ProblemSolution("440")]
    public int FindKthNumber(int n, int k)
    {
        var result = 1;
        k--;

        while (k > 0)
        {
            var steps = countSteps(result, result + 1);

            if (k >= steps)
            {
                result++;
                k -= (int)steps;
            }
            else
            {
                result *= 10;
                k--;
            }
        }

        return result;

        long countSteps(long from, long to)
        {
            var steps = 0L;
            while (from <= n)
            {
                steps += Math.Min(to, n + 1) - from;

                from *= 10;
                to *= 10;
            }

            return steps;
        }
    }

    [ProblemSolution("443")]
    public int Compress(char[] chars)
    {
        if (chars.Length == 1)
            return 1;

        var count = 1;
        var pos = 0;
        for (int i = 1; i < chars.Length; i++)
        {
            if (chars[i] != chars[i - 1])
            {
                var countStr = count.ToString();
                chars[pos++] = chars[i - 1];
                if (count > 1)
                {
                    foreach (var ch in countStr)
                        chars[pos++] = ch;
                    count = 1;
                }
            }
            else
            {
                count++;
            }
        }

        var countStr2 = count.ToString();
        chars[pos++] = chars[^1];
        if (count > 1)
        {
            foreach (var ch in countStr2)
                chars[pos++] = ch;
        }

        return pos;
    }

    [ProblemSolution("446")]
    public int NumberOfArithmeticSlices(int[] nums)
    {
        var dp = new Dictionary<long, int>[nums.Length]; // diff : count
        dp[0] = [];

        var result = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            dp[i] = [];
            for (int j = i - 1; j >= 0; j--)
            {
                var diff = (long)nums[i] - nums[j];
                dp[i].TryGetValue(diff, out int value);
                value++;

                if (dp[j].TryGetValue(diff, out int value2))
                {
                    value += value2;
                    result += value2;
                }

                dp[i][diff] = value;
            }
        }

        return result;
    }

    [ProblemSolution("450")]
    public TreeNode? DeleteNode(TreeNode? root, int key)
    {
        var node = find(root, key);
        if (node is null)
            return root;

        if (node.left is null && node.right is null)
        {
            var parent = findParent(root, node);
            if (parent is null)
                return null;

            if (parent.left == node)
                parent.left = null;
            else
                parent.right = null;
        }
        else if (node.left is null || node.right is null)
        {
            var parent = findParent(root, node);
            if (parent is null)
            {
                if (node.left is not null)
                    root = node.left;
                else
                    root = node.right;
            }
            else
            {
                var child = node.left is not null ? node.left : node.right;
                if (parent.left == node)
                    parent.left = child;
                else
                    parent.right = child;
            }
        }
        else
        {
            var success = node.right;
            var prev = node;
            while (success.left is not null)
            {
                prev = success;
                success = success.left;
            }

            (node.val, success.val) = (success.val, node.val);

            if (prev != node)
                prev.left = success.right;
            else
                prev.right = success.right;
        }

        return root;

        TreeNode? find(TreeNode? node, int val)
        {
            while (node is not null && node.val != val)
            {
                if (val < node.val)
                    node = node.left;
                else
                    node = node.right;
            }

            return node;
        }

        TreeNode? findParent(TreeNode? root, TreeNode node)
        {
            if (root == node || root is null)
                return null;

            while (root!.left != node && root.right != node)
            {
                if (node.val < root.val)
                    root = root.left;
                else
                    root = root.right;
            }

            return root;
        }
    }

    [ProblemSolution("451")]
    public string FrequencySort(string s)
    {
        var frequencies = s.GroupBy(f => f).Select(f => (f.Key, f.Count())).OrderByDescending(f => f.Item2);
        var str = new StringBuilder();
        foreach (var ch in frequencies)
            str.Append(new string(ch.Key, ch.Item2));
        return str.ToString();
    }

    [ProblemSolution("454")]
    public int FourSumCount(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
    {
        var n = nums1.Length;
        var dick1 = new Dictionary<int, int>();
        var dick2 = new Dictionary<int, int>();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var sum = nums1[i] + nums2[j];
                if (dick1.TryGetValue(sum, out int value))
                    dick1[sum] = value + 1;
                else
                    dick1[sum] = 1;
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var sum = nums3[i] + nums4[j];
                if (dick2.TryGetValue(sum, out int value))
                    dick2[sum] = value + 1;
                else
                    dick2[sum] = 1;
            }
        }

        var result = 0;
        foreach (var v in dick1)
        {
            if (dick2.TryGetValue(-v.Key, out int value))
                result += v.Value * value;
        }

        return result;
    }

    [ProblemSolution("455")]
    public int FindContentChildren(int[] g, int[] s)
    {
        var gsorted = g.Order().ToList();
        var ssorted = s.Order().ToList();

        var count = 0;
        var gInd = 0;
        var sInd = 0;
        while (gInd < g.Length && sInd < s.Length)
        {
            if (gsorted[gInd] <= ssorted[sInd++])
            {
                count++;
                gInd++;
            }
        }

        return count;
    }

    [ProblemSolution("463")]
    public int IslandPerimeter(int[][] grid)
    {
        var total = 0;
        var adjacent = new (int i, int j)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    foreach (var other in adjacent)
                    {
                        var oi = other.i + i;
                        var oj = other.j + j;

                        if (oi < 0 || oj < 0 || oi >= grid.Length || oj >= grid[0].Length || grid[oi][oj] == 0)
                            total++;
                    }
                }
            }
        }

        return total;
    }

    [ProblemSolution("475")]
    public int FindRadius(int[] houses, int[] heaters)
    {
        Array.Sort(houses);
        Array.Sort(heaters);

        var radius = 0;
        for (int i = 0; i < houses.Length; i++)
        {
            var house = houses[i];
            var greaterHeater = Array.BinarySearch(heaters, house);
            if (greaterHeater < 0)
                greaterHeater = ~greaterHeater;
            var smallerHeater = greaterHeater - 1;

            var closer = Math.Min(
                greaterHeater < heaters.Length ? heaters[greaterHeater] - house : int.MaxValue,
                smallerHeater >= 0 ? house - heaters[smallerHeater] : int.MaxValue
                );

            radius = Math.Max(radius, closer);
        }

        return radius;
    }

    [ProblemSolution("476")]
    public int FindComplement(int num)
    {
        var mask = 1 << 30;
        while ((mask & num) == 0)
            mask >>= 1;

        mask = (mask - 1) * 2 + 1;
        return ~num & mask;
    }

    [ProblemSolution("494")]
    public int FindTargetSumWays(int[] nums, int target)
    {
        var sum = nums.Sum();
        if (Math.Abs(target) > sum)
            return 0;

        var dp = new int[2][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new int[sum * 2 + 1];

        if (nums[0] == 0)
            dp[0][0] = 2;
        else
        {
            dp[0][nums[0]] += 1;
            dp[0][nums[0] + sum] += 1;
        }

        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = 0; j < dp[0].Length; j++)
            {
                if (dp[0][j] == 0)
                    continue;

                var curSum = j > sum ? -(j - sum) : j;
                var plusSum = curSum + nums[i + 1];
                var minusSum = curSum - nums[i + 1];
                var plusInd = plusSum < 0 ? -plusSum + sum : plusSum;
                var minusInd = minusSum < 0 ? -minusSum + sum : minusSum;
                dp[1][plusInd] += dp[0][j];
                dp[1][minusInd] += dp[0][j];
            }

            (dp[0], dp[1]) = (dp[1], dp[0]);
            for (int k = 0; k < dp[0].Length; k++)
                dp[1][k] = 0;
        }

        return dp[0][target < 0 ? -target + sum : target];
    }
}
