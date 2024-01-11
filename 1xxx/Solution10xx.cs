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
}
