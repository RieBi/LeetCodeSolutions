namespace LeetCode.Set2xxx;
internal class Solution28xx
{
    #region Solution for 2849
    public bool IsReachableAtTime(int sx, int sy, int fx, int fy, int t)
    {
        var x = Math.Abs(fx - sx);
        var y = Math.Abs(fy - sy);
        var time = Math.Max(x, y);

        if (sx == fx && sy == fy)
        {
            return t != 1;
        }

        return t >= time;
    }
    #endregion

    #region Solution for 2869
    public int MinOperations(IList<int> nums, int k)
    {
        var doneInts = new bool[k + 1];
        var doneIntsCount = 0;
        var ops = 0;

        for (int i = nums.Count - 1; i >= 0; i--)
        {
            if (nums[i] <= k && doneInts[nums[i]] == false)
            {
                doneIntsCount++;
                doneInts[nums[i]] = true;
            }

            ops++;
            if (doneIntsCount == k)
                return ops;
        }

        return -1;
    }
    #endregion
}