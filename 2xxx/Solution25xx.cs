namespace LeetCode.Set2XXX;
internal class Solution25XX
{
    [ProblemSolution("2540")]
    public int GetCommon(int[] nums1, int[] nums2)
    {
        var p1 = 0;
        var p2 = 0;
        while (p1 < nums1.Length && p2 < nums2.Length)
        {
            if (nums1[p1] == nums2[p2])
                return nums1[p1];
            else if (nums1[p1] < nums2[p2])
                p1++;
            else
                p2++;
        }

        return -1;
    }

    [ProblemSolution("2582")]
    public int PassThePillow(int n, int time)
    {
        time %= (n - 1) * 2;
        if (time < n)
            return time + 1;
        else
            return n - (time % n + 1);
    }

    [ProblemSolution("2598")]
    public int FindSmallestInteger(int[] numbers, int value)
    {
        var mods = new int[Math.Min(numbers.Length, value)];
        for (int i = 0; i < numbers.Length; i++)
        {
            var mod = numbers[i] % value;
            if (mod < 0)
                mod = value - -mod;

            if (mod < mods.Length)
                mods[mod]++;
        }

        var min = int.MaxValue;
        int minInd = 0;
        for (; minInd < mods.Length && mods[minInd] > 0; minInd++)
            min = Math.Min(min, mods[minInd]);

        if (minInd < value)
            return minInd;

        var secondInd = Array.FindIndex(mods, f => f == min);
        return value * min + secondInd;
    }
}
