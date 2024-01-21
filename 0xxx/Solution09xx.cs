using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode._0xxx;
internal class Solution09xx
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
}
