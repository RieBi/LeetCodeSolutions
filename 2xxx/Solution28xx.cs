using System.Text;
namespace LeetCode.Set2xxx;
internal class Solution28xx
{
    [ProblemSolution("2849")]
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

    [ProblemSolution("2869")]
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

    [ProblemSolution("2870")]
    public int MinOperations(int[] nums)
    {
        var result = 0;
        var groups = nums.GroupBy(f => f);
        foreach (var group in groups)
        {
            var count = group.Count();
            if (count == 1)
                return -1;
            else if (count % 3 == 0)
                result += count / 3;
            else
                result += count / 3 + 1;
        }

        return result;
    }

    [ProblemSolution("2871")]
    public int MaxSubarrays(int[] nums)
    {
        var result = 0;
        var min = nums[0];
        for (int i = 0; i < nums.Length; i++)
            min &= nums[i];

        if (min > 0)
            return 1;

        var cur = nums[0];
        for (int i = 0; i < nums.Length; i++)
        {
            cur &= nums[i];
            if (cur == min)
            {
                result++;
                cur = (1 << 25) - 1;
            }
        }

        return result;
    }

    [ProblemSolution("2872")]
    public int MaxKDivisibleComponents(int n, int[][] edges, int[] values, int k)
    {
        var vertices = new Dictionary<int, (long value, HashSet<int> connections)>();
        for (int i = 0; i < n; i++)
        {
            var value = values[i];
            vertices[i] = (value, new HashSet<int>());
        }

        for (int i = 0; i < n - 1; i++)
        {
            var v1 = edges[i][0];
            var v2 = edges[i][1];

            vertices[v1].connections.Add(v2);
            vertices[v2].connections.Add(v1);
        }

        var regionCount = 0;
        long GetVerticeValue(int ind, int prevInd)
        {
            var current = vertices[ind];
            var sum = current.value;
            foreach (var child in current.connections.Where(f => f != prevInd))
            {
                var value = GetVerticeValue(child, ind);

                if (value % k != 0)
                    sum += value;
            }

            if (sum % k == 0)
                regionCount++;
            return sum;
        }

        GetVerticeValue(0, -1);

        return regionCount;
    }

    [ProblemSolution("2873")]
    public long MaximumTripletValue(int[] nums)
    {
        static long getValue((long i, long j, long k) values) => (values.i - values.j) * values.k;
        var max = 0L;

        for (int i = 0; i < nums.Length - 2; i++)
        {
            for (int j = i + 1; j < nums.Length - 1; j++)
            {
                for (int k = j + 1; k < nums.Length; k++)
                {
                    max = Math.Max(max, getValue((nums[i], nums[j], nums[k])));
                }
            }
        }

        return max;
    }

    [ProblemSolution("2874")]
    public long MaximumTripletValue2(int[] nums)
    {
        var len = nums.Length;
        var prefs = new int[len];
        var suffs = new int[len];
        prefs[0] = nums[0];
        suffs[len - 1] = nums[len - 1];

        for (int i = 1; i < len; i++)
            prefs[i] = Math.Max(nums[i], prefs[i - 1]);
        for (int i = len - 2; i >= 0; i--)
            suffs[i] = Math.Max(nums[i], suffs[i + 1]);

        var maxTriple = 0L;
        for (int j = 1; j < len - 1; j++)
            maxTriple = Math.Max(maxTriple, calculateTripletValue(prefs[j - 1], nums[j], suffs[j + 1]));

        return maxTriple;

        long calculateTripletValue(int val1, int val2, int val3)
        {
            return (long)(val1 - val2) * (long)val3;
        }
    }

    [ProblemSolution("2875")]
    public int MinSizeSubarray(int[] nums, int target)
    {
        var longy = nums.Select(f => (long)f);
        var ltarget = (long)target;
        var infinite = longy.Concat(longy).ToList();

        var numsSum = longy.Sum();
        var totalLength = longy.Count() * (ltarget / numsSum);
        ltarget %= numsSum;
        if (ltarget == 0)
            return (int)totalLength;

        var left = 0;
        var right = 0;
        var sum = infinite[0];
        var minLength = int.MaxValue;
        while (right < infinite.Count - 1)
        {
            if (sum == ltarget && (right - left + 1) < minLength)
                minLength = (right - left + 1);

            if (left == right || sum < ltarget)
                sum += infinite[++right];
            else
                sum -= infinite[left++];
        }

        if (minLength == int.MaxValue)
            return -1;

        return (int)(totalLength + minLength);
    }

    [ProblemSolution("2895")]
    public int MinProcessingTime(IList<int> processorTime, IList<int> tasks)
    {
        var processorSorted = processorTime.OrderBy(f => f);
        var tasksSorted = tasks.OrderByDescending(f => f).Chunk(4).Select(f => f.Max());

        return processorSorted.Zip(tasksSorted).Aggregate(0, (a, b) => Math.Max(a, b.First + b.Second));
    }

    [ProblemSolution("2896")]
    public int MinOperations(string s1, string s2, int x)
    {
        var diffs = new List<int>(s1.Length);
        for (int i = 0; i < s1.Length; i++)
        {
            if (s1[i] != s2[i])
                diffs.Add(i);
        }

        if (diffs.Count % 2 == 1)
            return -1;
        if (diffs.Count == 0)
            return 0;

        var dpPlusTwo = 0;
        var dpPlusOne = x;
        var dp = -1;
        for (int i = diffs.Count - 2; i >= 0; i--)
        {
            dp = Math.Min(
                dpPlusOne + x,
                dpPlusTwo + 2 * (diffs[i + 1] - diffs[i])
            );
            (dpPlusTwo, dpPlusOne) = (dpPlusOne, dp);
        }

        return dp / 2;
    }

    [ProblemSolution("2897")]
    public int MaxSum(IList<int> nums, int k)
    {
        var counts = new long[31];
        for (int i = 0; i < nums.Count; i++)
            populateCounts(nums[i]);

        var results = new List<long>();
        while (tryAddResults()) { }

        var resultSum = 0L;
        for (int i = 0; i < k && i < results.Count; i++)
        {
            resultSum += results[i] * results[i];
            resultSum %= 1_000_000_007;
        }

        return (int)resultSum;

        void populateCounts(int num)
        {
            var mask = 1;
            for (int i = 0; i < 31; i++)
            {
                if ((mask & num) == mask)
                    counts![i]++;
                mask <<= 1;
            }
        }

        bool tryAddResults()
        {
            var num = 0;
            var mask = 1;
            for (int i = 0; i < 31; i++)
            {
                if (counts![i] > 0)
                {
                    counts[i]--;
                    num |= mask;
                }

                mask <<= 1;
            }

            if (num == 0)
                return false;

            results!.Add(num);
            return true;
        }
    }

    [ProblemSolution("2899")]
    public IList<int> LastVisitedIntegers(IList<string> words)
    {
        var nums = new List<int>();
        var result = new List<int>();

        var prevCount = 0;
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i] == "prev")
            {
                prevCount++;
                var ind = nums.Count - prevCount;
                result.Add(ind >= 0 ? nums[ind] : -1);
            }
            else
            {
                prevCount = 0;
                nums.Add(int.Parse(words[i]));
            }
        }

        return result;
    }
}