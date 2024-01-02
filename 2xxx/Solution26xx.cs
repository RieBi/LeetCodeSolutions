namespace LeetCode.Set2xxx;
internal class Solution26xx
{
    [ProblemSolution("2610")]
    public IList<IList<int>> FindMatrix(int[] nums)
    {
        var dick = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (dick.TryGetValue(num, out int count))
                dick[num] = count + 1;
            else
                dick[num] = 1;
        }

        var result = new List<IList<int>>();
        foreach (var v in dick)
        {
            for (int i = 0; i < v.Value; i++)
            {
                if (result.Count == i)
                    result.Add([]);

                result[i].Add(v.Key);
            }
        }

        return result;
    }
}
