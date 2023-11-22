namespace LeetCode.Set2xxx;
internal class Solution14xx
{
    #region Solution for 1424
    public int[] FindDiagonalOrder(IList<IList<int>> nums)
    {
        var totalCount = nums.Sum(f => f.Count);
        var result = new int[totalCount];
        var resultInd = 0;

        var queue = new Queue<(int i, int j)>();
        queue.Enqueue((0, 0));

        while (queue.Count > 0)
        {
            var indices = queue.Dequeue();
            result[resultInd++] = nums[indices.i][indices.j];

            if (indices.j == 0 && indices.i + 1 < nums.Count && indices.j < nums[indices.i + 1].Count)
                queue.Enqueue((indices.i + 1, indices.j));
            if (indices.j + 1 < nums[indices.i].Count)
                queue.Enqueue((indices.i, indices.j + 1));
        }

        return result;
    }
    #endregion
}