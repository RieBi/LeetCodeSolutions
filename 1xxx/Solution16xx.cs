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

    #region Solution for 1685
    public int[] GetSumAbsoluteDifferences(int[] nums)
    {
        var prefSums = new int[nums.Length];
        prefSums[0] = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            prefSums[i] = prefSums[i - 1] + nums[i];
        }

        int getSum(int l, int r)
        {
            if (l == 0)
                return prefSums[r];

            return prefSums[r] - prefSums[l - 1];
        }

        var resultArr = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            var sum = 0;
            if (i != nums.Length - 1)
                sum += getSum(i + 1, nums.Length - 1) - nums[i] * (nums.Length - 1 - i);
            if (i != 0)
                sum += nums[i] * i - getSum(0, i - 1);
            resultArr[i] = sum;
        }

        return resultArr;
    }
    #endregion
}
