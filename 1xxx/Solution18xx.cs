namespace LeetCode.Set1XXX;
internal class Solution18XX
{
    [ProblemSolution("1813")]
    public bool AreSentencesSimilar(string sentence1, string sentence2)
    {
        var words1 = sentence1.Split(' ');
        var words2 = sentence2.Split(' ');

        if (words1.Length < words2.Length)
            (words1, words2) = (words2, words1);

        var diff = 0;
        while (diff < words2.Length && words1[diff] == words2[diff])
            diff++;

        var missing = words1.Length - words2.Length;

        for (int j = diff; j < words2.Length; j++)
        {
            if (words1[j + missing] != words2[j])
                return false;
        }

        return true;
    }

    [ProblemSolution("1814")]
    public int CountNicePairs(int[] nums)
    {
        long findRev(long num)
        {
            var rev = 0L;

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

        var total = 0L;
        foreach (var v in dick)
        {
            var elemTotal = (v.Value * (v.Value - 1)) / 2;
            total += elemTotal;
        }

        var modulo = 1000000007L;
        return (int)(total % modulo);
    }

    [ProblemSolution("1823")]
    public int FindTheWinner(int n, int k)
    {
        var ans = 0;
        for (int i = 2; i <= n; i++)
        {
            ans += k;
            ans %= i;
        }

        return ans + 1;
    }

    [ProblemSolution("1838")]
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

    [ProblemSolution("1845")]
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

    [ProblemSolution("1877")]
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

    [ProblemSolution("1887")]
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

    [ProblemSolution("1894")]
    public int ChalkReplacer(int[] chalk, int k)
    {
        var sum = 0;
        for (int i = 0; i < chalk.Length; i++)
        {
            sum += chalk[i];
            if (sum > k)
                return i;
        }

        if (sum <= k)
            k %= sum;

        var ind = 0;
        while (k >= chalk[ind])
            k -= chalk[ind++];

        return ind;
    }

    [ProblemSolution("1897")]
    public bool MakeEqual(string[] words)
    {
        var counts = new int[26];
        foreach (var word in words)
        {
            foreach (var ch in word)
                counts[ch - 'a']++;
        }

        return counts.All(f => f % words.Length == 0);
    }
}