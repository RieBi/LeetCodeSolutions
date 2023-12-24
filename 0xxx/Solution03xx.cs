namespace LeetCode.Set0xxx;
internal class Solution03xx
{
    [ProblemSolution("349")]
    public int[] Intersection(int[] nums1, int[] nums2) => nums1.Intersect(nums2).ToArray();

    [ProblemSolution("387")]
    public int FirstUniqChar(string s)
    {
        return s.Zip(Enumerable.Range(0, s.Length))
            .GroupBy(f => f.First, s => s.Second)
            .Where(f => f.Count() == 1)
            .Select(f => f.Single())
            .FirstOrDefault(-1);
    }
}
