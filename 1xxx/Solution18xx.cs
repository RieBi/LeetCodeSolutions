namespace LeetCode.Set1xxx;
internal class Solution18xx
{
    #region Solution for 1814
    public int CountNicePairs(int[] nums)
    {
        long findRev(long num)
        {
            var rev = 0l;

            while (num > 0)
            {
                var lastDigit = num % 10;
                num /= 10;
                rev *= 10;
                rev += lastDigit;
            }

            return rev;
        }

        // Key: a - rev(a)
        // Value: number of occurences of key
        var dick = new Dictionary<long, long>();
        for (int i = 0; i < nums.Length; i++)
        {
            var key = nums[i] - findRev(nums[i]);
            if (dick.TryGetValue(key, out long value))
                dick[key] = value + 1;
            else
                dick[key] = 1;
        }

        var total = 0l;
        foreach (var v in dick)
        {
            var elemTotal = (v.Value * (v.Value - 1)) / 2;
            total += elemTotal;
        }

        var modulo = 1000000007l;
        return (int)(total % modulo);
    }
    #endregion

    #region Solution for 1838
    public int MaxFrequency(int[] nums, int k)
    {
        Array.Sort(nums);
        var dp = new int[nums.Length];
        for (int i = 1; i < nums.Length; i++)
        {
            var value = dp[i - 1] + i * (nums[i] - nums[i - 1]);
            dp[i] = value;
        }

        int regionCost(int left, int right)
        {
            var cost = dp[right];
            if (left == 0)
            {
                return cost;
            }
            else
            {
                return cost - (dp[left - 1] + left * (nums[right] - nums[left - 1]));
            }
        }

        bool isFrequencyOK(int frequency)
        {
            for (int i = 0; i + frequency <= nums.Length; i++)
            {
                if (regionCost(i, i + frequency - 1) <= k)
                    return true;
            }
            return false;
        }

        var left = 1;
        var right = nums.Length;
        var maxFrequency = 1;

        while (left != right)
        {
            var diff = right - left;
            if (diff % 2 != 0)
                diff++;

            var mid = left + (diff) / 2;
            if (isFrequencyOK(mid))
            {
                left = mid;
                maxFrequency = mid;
            }
            else
            {
                right = mid - 1;
            }
        }
        return maxFrequency;
    }
    #endregion

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

    #region Solution for 1887
    public int ReductionOperations(int[] nums)
    {
        Array.Sort(nums);
        var sum = 0;
        var cur = 0;
        var dp = new int[nums.Length];

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[i - 1])
                cur++;

            sum += cur;
        }

        return sum;
    }
    #endregion
}