﻿namespace LeetCode.Set2XXX;
internal class Solution26XX
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

    [ProblemSolution("2678")]
    public int CountSeniors(string[] details)
    {
        return details.Count(f => f[^4] > '6' || f[^4] == '6' && f[^3] != '0');
    }
}
