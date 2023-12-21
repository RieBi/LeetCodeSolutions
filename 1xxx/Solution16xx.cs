namespace LeetCode.Set1xxx;
internal class Solution16xx
{
    [ProblemSolution("1611")]
    public int MinimumOneBitOperations(int n)
    {
        var sign = 1;
        var mask = 1 << 29;
        var ops = 0;
        for (int i = 0; i < 32; i++)
        {
            if ((n & mask) == mask)
            {
                ops += ((mask << 1) - 1) * sign;
                sign *= -1;
            }

            mask >>= 1;
        }

        return ops;
    }

    [ProblemSolution("1630")]
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

    [ProblemSolution("1637")]
    public int MaxWidthOfVerticalArea(int[][] points)
    {
        var sorted = points
            .Select(f => f[0])
            .OrderBy(f => f)
            .ToList();

        var max = 0;
        for (int i = 1; i < sorted.Count(); i++)
        {
            max = Math.Max(max, sorted[i] - sorted[i - 1]);
        }

        return max;
    }

    [ProblemSolution("1662")]
    public bool ArrayStringsAreEqual(string[] word1, string[] word2)
    {
        var chars = new int[1000];
        
        IEnumerable<char> getChars(string[] word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < word[i].Length; j++)
                {
                    yield return word[i][j];
                }
            }
        }

        var enum1 = getChars(word1).GetEnumerator();
        var enum2 = getChars(word2).GetEnumerator();

        while (true)
        {
            var is1 = enum1.MoveNext();
            var is2 = enum2.MoveNext();

            if (!is1 && !is2)
                break;
            else if (is1 != is2)
                return false;

            if (enum1.Current != enum2.Current)
                return false;
        }

        return true;
    }

    [ProblemSolution("1685")]
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

    [ProblemSolution("1688")]
    public int NumberOfMatches(int n)
    {
        return n - 1;
    }
}
