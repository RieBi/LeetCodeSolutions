namespace LeetCode.Set3XXX;
internal class Solution30XX
{
    [ProblemSolution("3010")]
    public int MinimumCost(int[] nums)
    {
        var min = int.MaxValue;
        for (int i = 1; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                min = Math.Min(min, nums[0] + nums[i] + nums[j]);
            }
        }

        return min;
    }

    [ProblemSolution("3011")]
    public bool CanSortArray(int[] nums)
    {
        for (int i = 1; i < nums.Length; i++)
        {
            var j = i;
            while (j > 0 && nums[j] < nums[j - 1] && bitsSet(nums[j]) == bitsSet(nums[j - 1]))
            {
                (nums[j], nums[j - 1]) = (nums[j - 1], nums[j]);
                j--;
            }
        }
        
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] < nums[i - 1])
                return false;
        }

        return true;

        static int bitsSet(int num)
        {
            var amount = 0;
            var mask = 1;
            for (int i = 0; i < 32; i++)
            {
                if ((num & mask) > 0)
                    amount++;

                mask <<= 1;
            }

            return amount;
        }
    }

    [ProblemSolution("3012")]
    public int MinimumArrayLength(int[] nums)
    {
        var minVal = nums.Min();
        var minCount = nums.Count(f => f == minVal);

        if (minCount == 1)
            return 1;

        for (int i = 0; i < nums.Length; i++)
        {
            var rem = nums[i] % minVal;
            if (rem > 0)
                return 1;
        }

        if (minCount % 2 == 1)
            minCount++;
        return minCount / 2;
    }

    [ProblemSolution("3013")]
    public long MinimumCost(int[] nums, int k, int dist)
    {
        k -= 1;

        var used = new SortedSet<(long val, int ind)>();
        var unused = new SortedSet<(long val, int ind)>();

        for (int i = 1; i <= dist + 1; i++)
            used.Add((nums[i], i));

        while (used.Count > k)
        {
            var lmax = used.Max;
            unused.Add(lmax);
            used.Remove(lmax);
        }

        var minSum = long.MaxValue;
        var sum = nums[0] + used.Sum(f => f.val);

        for (int i = 1, j = Math.Min(nums.Length - 1, dist + 1); j < nums.Length - 1; i++, j++)
        {
            minSum = Math.Min(sum, minSum);

            (long val, int ind) toRemove = (nums[i], i);
            (long val, int ind) toAdd = (nums[j + 1], j + 1);

            if (used.Contains(toRemove))
            {
                sum -= toRemove.val;
                used.Remove(toRemove);
            }
            else
                unused.Remove(toRemove);

            unused.Add(toAdd);
            var rmin = unused.Min;
            if (used.Count < k)
            {
                sum += rmin.val;
                used.Add(rmin);
                unused.Remove(rmin);
                rmin = unused.Min;
            }

            var lmax = used.Max;
            if (lmax.val > rmin.val && unused.Count > 0)
            {
                sum = sum - lmax.val + rmin.val;
                used.Remove(lmax);
                unused.Remove(rmin);
                used.Add(rmin);
                unused.Add(lmax);
            }
        }

        minSum = Math.Min(sum, minSum);
        return minSum;
    }

    [ProblemSolution("3014")]
    public int MinimumPushes(string word)
    {
        var frequencies = new byte[26];
        for (int i = 0; i < word.Length; i++)
            frequencies[word[i] - 'a']++;

        Array.Sort(frequencies, (a, b) => b - a);

        var sum = 0;
        for (int i = 0; i < frequencies.Length && frequencies[i] > 0; i++)
            sum += i / 8 + 1;

        return sum;
    }

    [ProblemSolution("3016")]
    public int MinimumPushes2(string word)
    {
        var c = 0;
        return word
            .GroupBy(f => f)
            .OrderByDescending(f => f.Count())
            .Aggregate(0, (a, b) =>
            {
                var mult = c++ / 8 + 1;
                return a + mult * b.Count();
            });
    }

    [ProblemSolution("3075")]
    public long MaximumHappinessSum(int[] happiness, int k)
    {
        var suitable = happiness.OrderDescending().Take(k).ToList();
        var sum = 0L;
        for (int turn = 0; turn < suitable.Count; turn++)
        {
            var val = suitable[turn] - turn;
            if (val <= 0)
                break;
            sum += val;
        }

        return sum;
    }
}
