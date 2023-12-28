namespace LeetCode.Set0xxx;
internal class Solution04xx
{
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
}
