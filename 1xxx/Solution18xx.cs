namespace LeetCode.Set1xxx;
internal class Solution18xx
{
    #region Solution for 1845
    public class SeatManager
    {
        SortedList<int, int> dick;
        public SeatManager(int n)
        {
            dick = new SortedList<int, int>();
            for (int i = 1; i <= n; i++)
            {
                dick[i] = i;
            }
        }

        public int Reserve()
        {
            var min = dick.Keys.First();
            dick.Remove(min);
            return min;
        }

        public void Unreserve(int seatNumber)
        {
            dick[seatNumber] = seatNumber;
        }
    }
    #endregion

    #region Solution for 1877
    public int MinPairSum(int[] nums)
    {
        Array.Sort(nums);
        var max = int.MinValue;
        for (int i = 0; i < nums.Length / 2; i++)
        {
            max = Math.Max(max, nums[i] + nums[nums.Length - 1 - i]);
        }
        return max;
    }
    #endregion
}