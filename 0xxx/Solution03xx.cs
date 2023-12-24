namespace LeetCode.Set0xxx;
internal class Solution03xx
{
    [ProblemSolution("349")]
    public int[] Intersection(int[] nums1, int[] nums2) => nums1.Intersect(nums2).ToArray();

    [ProblemSolution("350")]
    public int[] Intersect(int[] nums1, int[] nums2)
    {
        var group1 = nums1.GroupBy(f => f).Select(f => (f.Key, f.Count())).ToDictionary();
        var group2 = nums2.GroupBy(f => f).Select(f => (f.Key, f.Count())).ToDictionary();

        var result = new List<int>();
        foreach (var group in group1)
        {
            if (!group2.TryGetValue(group.Key, out int value))
                continue;
            var count = Math.Min(group.Value, value);
            for (int i = 0; i < count; i++)
                result.Add(group.Key);
        }

        return result.ToArray();
    }

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
