using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode._0xxx;
internal class Solution09XX
{
    [ProblemSolution("907")]
    public int SumSubarrayMins(int[] arr)
    {
        var left = new int[arr.Length];
        var right = new int[arr.Length];

        var stack = new Stack<int>();
        for (int i = 0; i < arr.Length; i++)
        {
            left[i] = -1;
            right[i] = arr.Length;
            while (stack.Count > 0 && arr[stack.Peek()] > arr[i])
            {
                var popped = stack.Pop();
                right[popped] = i;
            }

            if (stack.Count > 0)
                left[i] = stack.Peek();
            stack.Push(i);
        }

        var total = 0L;
        var modulo = 1_000_000_007;

        for (int i = 0; i < arr.Length; i++)
            total += (long)arr[i] * (i - left[i]) * (right[i] - i);

        return (int)(total % modulo);
    }

    [ProblemSolution("931")]
    public int MinFallingPathSum(int[][] matrix)
    {
        List<(int i, int j)> dirs = [(-1, -1), (-1, 0), (-1, 1)];

        var vals = new List<int>();
        for (int i = 1; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix.Length; j++)
            {
                foreach (var dir in dirs)
                {
                    var newi = dir.i + i;
                    var newj = dir.j + j;
                    if (newi < 0 || newj < 0 || newi >= matrix.Length || newj >= matrix.Length)
                        continue;
                    vals.Add(matrix[i][j] + matrix[newi][newj]);
                }

                matrix[i][j] = vals.Min();
                vals.Clear();
            }
        }

        return matrix[^1].Min();
    }

    [ProblemSolution("938")]
    public int RangeSumBST(TreeNode root, int low, int high)
    {
        var sum = 0;
        propagate(root);

        void propagate(TreeNode? node)
        {
            if (node == null)
                return;

            if (node.val >= low && node.val <= high)
                sum += node.val;

            propagate(node.left);
            propagate(node.right);
        }

        return sum;
    }

    [ProblemSolution("945")]
    public int MinIncrementForUnique(int[] nums)
    {
        Array.Sort(nums);
        var min = nums[0];
        var steps = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] < min)
            {
                steps += min - nums[i];
                nums[i] = min;
            }

            min = nums[i] + 1;
        }

        return steps;
    }

    [ProblemSolution("950")]
    public int[] DeckRevealedIncreasing(int[] deck)
    {
        Array.Sort(deck);
        var result = new int[deck.Length];

        for (int i = 0; i < deck.Length; i += 2)
            result[i] = deck[i / 2];

        for (int i = 1; i < deck.Length; i += 2)
        {
            var k = i;
            while (k % 2 == 1)
                k += (deck.Length - (k / 2 + 1));

            result[i] = deck[k / 2];
        }

        return result;
    }

    [ProblemSolution("974")]
    public int SubarraysDivByK(int[] nums, int k)
    {
        var hash = new Dictionary<int, int>();
        hash[0] = 1;
        var mod = 0;
        var total = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            mod = ((mod + nums[i] % k) + k) % k;
            if (hash.TryGetValue(mod, out var value))
            {
                total += value;
                hash[mod] = value + 1;
            }
            else
                hash[mod] = 1;
        }

        return total;
    }

    [ProblemSolution("995")]
    public int MinKBitFlips(int[] nums, int k)
    {
        var switches = 0;
        var queue = new Queue<int>();
        for (int i = 0; i + k - 1 < nums.Length; i++)
        {
            while (queue.Count > 0 && queue.Peek() <= i - k)
                queue.Dequeue();

            var num = (nums[i] + queue.Count) % 2;
            if (num == 1)
                continue;

            switches++;
            queue.Enqueue(i);
        }

        for (int i = nums.Length - k + 1; i < nums.Length; i++)
        {
            while (queue.Count > 0 && queue.Peek() <= i - k)
                queue.Dequeue();

            if ((nums[i] + queue.Count) % 2 == 0)
                return -1;
        }

        return switches;
    }

    [ProblemSolution("997")]
    public int FindJudge(int n, int[][] trust)
    {
        var trustee = new int[n + 1];
        var trusted = new int[n + 1];

        foreach (var relation in trust)
        {
            trustee[relation[0]]++;
            trusted[relation[1]]++;
        }

        for (int i = 1; i < trustee.Length; i++)
            if (trustee[i] == 0 && trusted[i] == n - 1)
                return i;

        return -1;
    }
}
