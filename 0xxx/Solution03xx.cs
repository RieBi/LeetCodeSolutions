namespace LeetCode.Set0xxx;
internal class Solution03xx
{
    [ProblemSolution("349")]
    public int[] Intersection(int[] nums1, int[] nums2) => nums1.Intersect(nums2).ToArray();
}
