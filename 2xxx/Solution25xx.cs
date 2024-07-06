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
}
