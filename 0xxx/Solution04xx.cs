namespace LeetCode.Set0xxx;
internal class Solution04xx
{
    [ProblemSolution("413")]
    public int NumberOfArithmeticSlices0(int[] nums)
    {
        var count = 1;
        var diff = 0;
        var result = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] - nums[i - 1] == diff)
                count++;
            else
            {
                if (count >= 3)
                {
                    count -= 2;
                    result += (count * (count + 1)) / 2;
                }

                count = 2;
                diff = nums[i] - nums[i - 1];
            }
        }

        if (count >= 3)
        {
            count -= 2;
            result += (count * (count + 1)) / 2;
        }

        return result;
    }

    [ProblemSolution("443")]
    public int Compress(char[] chars)
    {
        if (chars.Length == 1)
            return 1;

        var count = 1;
        var pos = 0;
        for (int i = 1; i < chars.Length; i++)
        {
            if (chars[i] != chars[i - 1])
            {
                var countStr = count.ToString();
                chars[pos++] = chars[i - 1];
                if (count > 1)
                {
                    foreach (var ch in countStr)
                        chars[pos++] = ch;
                    count = 1;
                }
            }
            else
            {
                count++;
            }
        }

        var countStr2 = count.ToString();
        chars[pos++] = chars[chars.Length - 1];
        if (count > 1)
        {
            foreach (var ch in countStr2)
                chars[pos++] = ch;
        }

        return pos;
    }

    [ProblemSolution("446")]
    public int NumberOfArithmeticSlices(int[] nums)
    {
        var dp = new Dictionary<long, int>[nums.Length]; // diff : count
        dp[0] = [];

        var result = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            dp[i] = [];
            for (int j = i - 1; j >= 0; j--)
            {
                var diff = (long)nums[i] - nums[j];
                var value = 0;
                dp[i].TryGetValue(diff, out value);
                value++;

                if (dp[j].TryGetValue(diff, out int value2))
                {
                    value += value2;
                    result += value2;
                }

                dp[i][diff] = value;
            }
        }

        return result;
    }

    [ProblemSolution("454")]
    public int FourSumCount(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
    {
        var n = nums1.Length;
        var dick1 = new Dictionary<int, int>();
        var dick2 = new Dictionary<int, int>();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var sum = nums1[i] + nums2[j];
                if (dick1.TryGetValue(sum, out int value))
                    dick1[sum] = value + 1;
                else
                    dick1[sum] = 1;
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var sum = nums3[i] + nums4[j];
                if (dick2.TryGetValue(sum, out int value))
                    dick2[sum] = value + 1;
                else
                    dick2[sum] = 1;
            }
        }

        var result = 0;
        foreach (var v in dick1)
        {
            if (dick2.TryGetValue(-v.Key, out int value))
                result += v.Value * value;
        }

        return result;
    }

    [ProblemSolution("455")]
    public int FindContentChildren(int[] g, int[] s)
    {
        var gsorted = g.Order().ToList();
        var ssorted = s.Order().ToList();

        var count = 0;
        var gInd = 0;
        var sInd = 0;
        while (gInd < g.Length && sInd < s.Length)
        {
            if (gsorted[gInd] <= ssorted[sInd++])
            {
                count++;
                gInd++;
            }
        }

        return count;
    }

    [ProblemSolution("494")]
    public int FindTargetSumWays(int[] nums, int target)
    {
        var sum = nums.Sum();
        if (Math.Abs(target) > sum)
            return 0;

        var dp = new int[2][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new int[sum * 2 + 1];

        if (nums[0] == 0)
            dp[0][0] = 2;
        else
        {
            dp[0][nums[0]] += 1;
            dp[0][nums[0] + sum] += 1;
        }

        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = 0; j < dp[0].Length; j++)
            {
                if (dp[0][j] == 0)
                    continue;

                var curSum = j > sum ? -(j - sum) : j;
                var plusSum = curSum + nums[i + 1];
                var minusSum = curSum - nums[i + 1];
                var plusInd = plusSum < 0 ? -plusSum + sum : plusSum;
                var minusInd = minusSum < 0 ? -minusSum + sum : minusSum;
                dp[1][plusInd] += dp[0][j];
                dp[1][minusInd] += dp[0][j];
            }

            (dp[0], dp[1]) = (dp[1], dp[0]);
            for (int k = 0; k < dp[0].Length; k++)
                dp[1][k] = 0;
        }

        return dp[0][target < 0 ? -target + sum : target];
    }
}
