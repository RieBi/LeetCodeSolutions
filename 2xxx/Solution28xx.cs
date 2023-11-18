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
}