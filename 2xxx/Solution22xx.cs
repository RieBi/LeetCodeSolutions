using System.Text.RegularExpressions;

namespace LeetCode.Set2XXX;
internal class Solution22XX
{
    [ProblemSolution("2225")]
    public IList<IList<int>> FindWinners(int[][] matches)
    {
        var dick = new SortedDictionary<int, int>();
        foreach (var match in matches)
        {
            if (!dick.ContainsKey(match[0]))
                dick[match[0]] = 0;

            if (dick.TryGetValue(match[1], out int value))
                dick[match[1]] = value + 1;
            else
                dick[match[1]] = 1;
        }

        var result = new List<IList<int>>();
        result.Add([]);
        result.Add([]);

        foreach (var pair in dick)
        {
            if (pair.Value == 0)
                result[0].Add(pair.Key);
            else if (pair.Value == 1)
                result[1].Add(pair.Key);
        }

        return result;
    }

    [ProblemSolution("2264")]
    public string LargestGoodInteger(string num)
    {
        var pattern = @"(\d)\1{2}";
        var matches = Regex.Matches(num, pattern);
        if (matches.Count == 0)
            return "";

        var max = "000";
        foreach (Match match in matches)
        {
            if (match.Value.CompareTo(max) > 0)
                max = match.Value;
        }

        return max;
    }

    [ProblemSolution("2285")]
    public long MaximumImportance(int n, int[][] roads)
    {
        var grouped = roads
            .SelectMany(f => f)
            .GroupBy(f => f)
            .Select(f => f.Count())
            .OrderDescending()
            .ToList();

        var sum = 0L;
        var low = n - grouped.Count;
        var ind = 0;
        while (n > low)
        {
            sum += (long)n-- * grouped[ind++];
        }

        return sum;
    }
}
