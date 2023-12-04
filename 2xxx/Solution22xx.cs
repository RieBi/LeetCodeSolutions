using System.Text.RegularExpressions;

namespace LeetCode.Set2xxx;
internal class Solution22xx
{
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
