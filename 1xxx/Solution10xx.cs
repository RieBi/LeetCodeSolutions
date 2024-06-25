namespace LeetCode.Set1xxx;
internal class Solution10xx
{
    [ProblemSolution("1020")]
    public int NumEnclaves(int[][] grid)
    {
        for (int i = 0; i < grid[0].Length; i++)
        {
            processCell(0, i);
            processCell(grid.Length - 1, i);
        }

        for (int i = 0; i < grid.Length; i++)
        {
            processCell(i, 0);
            processCell(i, grid[0].Length - 1);
        }

        return grid.Sum(f => f.Count(x => x == 1));

        void processCell(int i, int j)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length)
                return;
            if (grid[i][j] != 1)
                return;
            
            grid[i][j] = -1;

            processCell(i - 1, j);
            processCell(i + 1, j);
            processCell(i, j - 1);
            processCell(i, j + 1);
        }
    }

    [ProblemSolution("1026")]
    public int MaxAncestorDiff(TreeNode root)
    {
        var v = 0;
        propagate(root);
        return v;

        (int min, int max) propagate(TreeNode? node)
        {
            if (node is null)
                return (int.MaxValue, int.MinValue);

            var min = node.val;
            var max = node.val;

            var (min1, max1) = propagate(node.left);
            (min, max) = (Math.Min(min, min1), Math.Max(max, max1));

            var (min2, max2) = propagate(node.right);
            (min, max) = (Math.Min(min, min2), Math.Max(max, max2));

            v = Math.Max(v, Math.Max(Math.Abs(node.val - min), Math.Abs(node.val - max)));
            return (min, max);
        }
    }

    [ProblemSolution("1038")]
    public TreeNode BstToGst(TreeNode root)
    {
        var initialRoot = root;
        var sum = 0;
        var stack = new Stack<TreeNode>();
        while (stack.Count > 0 || root != null)
        {
            while (root != null)
            {
                stack.Push(root);
                root = root.right!;
            }

            var node = stack.Pop();
            var preSum = sum;
            sum += node.val;
            node.val += preSum;
            root = node.left!;
        }

        return initialRoot;
    }

    [ProblemSolution("1043")]
    public int MaxSumAfterPartitioning(int[] arr, int k)
    {
        var dp = new int[arr.Length];
        for (int i = 0; i  < arr.Length; i++)
        {
            var max = -1;
            for (int j = i; j < arr.Length && j - i < k; j++)
            {
                max = Math.Max(max, arr[j]);
                var prevMax = i == 0 ? 0 : dp[i - 1];
                dp[j] = Math.Max(dp[j], prevMax + (j - i + 1) * max);
            }
        }

        return dp[^1];
    }

    [ProblemSolution("1051")]
    public int HeightChecker(int[] heights)
    {
        return heights.Zip(heights.Order())
            .Count(f => f.First != f.Second);
    }

    [ProblemSolution("1052")]
    public int MaxSatisfied(int[] customers, int[] grumpy, int minutes)
    {
        var guaranteed = customers.Where((a, i) => grumpy[i] == 0).Sum();
        var extra = customers.Take(minutes).Where((a, i) => grumpy[i] == 1).Sum();

        var max = extra;

        for (int ind = minutes; ind < customers.Length; ind++)
        {
            var before = ind - minutes;
            if (grumpy[before] == 1)
                extra -= customers[before];
            if (grumpy[ind] == 1)
                extra += customers[ind];

            max = Math.Max(max, extra);
        }

        return max + guaranteed;
    }

    [ProblemSolution("1074")]
    public int NumSubmatrixSumTarget(int[][] matrix, int target)
    {
        var dp = new int[matrix.Length][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new int[matrix[0].Length];

        for (int i = 0; i < dp.Length; i++)
        {
            for (int j = 0; j < dp[0].Length; j++)
            {
                var left = j == 0 ? 0 : dp[i][j - 1];
                var top = i == 0 ? 0 : dp[i - 1][j];
                var corner = (i == 0 || j == 0) ? 0 : dp[i - 1][j - 1];
                dp[i][j] = left + top - corner + matrix[i][j];
            }
        }

        var count = 0;
        for (int startI = 0; startI < matrix.Length; startI++)
        {
            for (int startJ = 0; startJ < matrix[0].Length; startJ++)
            {
                for (int endI = startI; endI < matrix.Length; endI++)
                {
                    for (int endJ = startJ; endJ < matrix[0].Length; endJ++)
                    {
                        var left = startJ == 0 ? 0 : dp[endI][startJ - 1];
                        var top = startI == 0 ? 0 : dp[startI - 1][endJ];
                        var corner = (startI == 0 || startJ == 0) ? 0 : dp[startI - 1][startJ - 1];
                        if (dp[endI][endJ] - left - top + corner == target)
                            count++;
                    }
                }
            }
        }

        return count;
    }
}
