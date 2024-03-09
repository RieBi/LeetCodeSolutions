namespace LeetCode.Set2xxx;
internal class Solution25xx
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
}
