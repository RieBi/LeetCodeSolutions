using System.ComponentModel.DataAnnotations;

namespace LeetCode.Set2xxx;
internal class Solution14xx
{
    [ProblemSolution("1422")]
    public int MaxScore(string s)
    {
        var max = 0;
        var zeroes = s[0] == '0' ? 1 : 0;
        var ones = s.Skip(1).Count(f => f == '1');
        for (int i = 1; i < s.Length; i++)
        {
            max = Math.Max(max, zeroes + ones);
            if (s[i] == '0')
                zeroes++;
            else
                ones--;
        }

        return max;
    }

    [ProblemSolution("1424")]
    public int[] FindDiagonalOrder(IList<IList<int>> nums)
    {
        var totalCount = nums.Sum(f => f.Count);
        var result = new int[totalCount];
        var resultInd = 0;

        var queue = new Queue<(int i, int j)>();
        queue.Enqueue((0, 0));

        while (queue.Count > 0)
        {
            var indices = queue.Dequeue();
            result[resultInd++] = nums[indices.i][indices.j];

            if (indices.j == 0 && indices.i + 1 < nums.Count && indices.j < nums[indices.i + 1].Count)
                queue.Enqueue((indices.i + 1, indices.j));
            if (indices.j + 1 < nums[indices.i].Count)
                queue.Enqueue((indices.i, indices.j + 1));
        }

        return result;
    }

    [ProblemSolution("1436")]
    public string DestCity(IList<IList<string>> paths)
    {
        var dick = new Dictionary<string, (bool, bool)>();
        for (int i = 0; i < paths.Count; i++)
        {
            var source = paths[i][0];
            var dest = paths[i][1];

            if (dick.TryGetValue(source, out (bool, bool) val))
                dick[source] = (true, val.Item2);
            else
                dick[source] = (true, false);

            if (dick.TryGetValue(dest, out val))
                dick[dest] = (val.Item1, true);
            else
                dick[dest] = (false, true);
        }

        foreach (var v in dick)
        {
            if (v.Value == (false, true))
                return v.Key;
        }

        return "";
    }

    [ProblemSolution("1464")]
    public int MaxProduct(int[] nums)
    {
        return nums
            .Select(f => Math.Abs(f - 1))
            .OrderByDescending(f => f)
            .Take(2)
            .Aggregate(1, (a, b) => a * b);
    }
}