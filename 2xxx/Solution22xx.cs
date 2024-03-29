﻿using System.Text.RegularExpressions;

namespace LeetCode.Set2xxx;
internal class Solution22xx
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
}
