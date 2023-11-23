namespace LeetCode.Set1xxx;
internal class Solution16xx
{
    #region Solution for 1630
    public IList<bool> CheckArithmeticSubarrays(int[] nums, int[] l, int[] r)
    {
        bool isRangeArithmeticBruteforce(int lPoint, int rPoint)
        {
            var range = nums[lPoint..(rPoint + 1)].OrderBy(f => f).ToList();
            if (range.Count <= 2)
                return true;

            var constante = range[1] - range[0];
            for (int i = 2; i < range.Count; i++)
            {
                if (range[i] - range[i - 1] != constante)
                    return false;
            }

            return true;
        }

        var result = new List<bool>();
        for (int i = 0; i < l.Count(); i++)
        {
            result.Add(isRangeArithmeticBruteforce(l[i], r[i]));
        }

        return result;
    }
    #endregion
}
