namespace LeetCode.Set1xxx;
internal class Solution13xx
{
    [ProblemSolution("1335")]
    public int MinDifficulty(int[] jobDifficulty, int d)
    {
        if (d > jobDifficulty.Length)
            return -1;

        var big = int.MaxValue / 4;
        var mem = new Dictionary<(int, int, int), int>();

        return getMax(0, 1, -1);

        int getMax(int index, int day, int curMax)
        {
            if (index >= jobDifficulty.Length && day > d)
                return 0;
            else if (index >= jobDifficulty.Length || day > d)
                return big;

            if (mem.TryGetValue((index, day, curMax), out int value))
                return value;

            var max = Math.Max(jobDifficulty[index], curMax);

            // scenario end
            var scenarioEnd = max + getMax(index + 1, day + 1, -1);

            // scenario continue
            var scenarioContinue = getMax(index + 1, day, max);

            var result = Math.Min(scenarioEnd, scenarioContinue);
            mem[(index, day, curMax)] = result;
            return result;
        }
    }
}
