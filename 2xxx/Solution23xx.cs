namespace LeetCode.Set2xxx;
internal class Solution23xx
{
    [ProblemSolution("2391")]
    public int GarbageCollection(string[] garbage, int[] travel)
    {
        var totalAmount = garbage.Select(f => f.Length).Sum();
        var lastM = 0;
        var lastP = 0;
        var lastG = 0;
        for (int i = garbage.Length - 1; i > 0; i--)
        {
            if (lastM == 0 && garbage[i].Contains('M'))
                lastM = i;
            if (lastP == 0 && garbage[i].Contains('P'))
                lastP = i;
            if (lastG == 0 && garbage[i].Contains('G'))
                lastG = i;
        }

        var travelSums = new int[travel.Length + 1];
        for (int i = 1; i < travelSums.Length; i++)
            travelSums[i] = travelSums[i - 1] + travel[i - 1];

        var timeM = travelSums[lastM];
        var timeP = travelSums[lastP];
        var timeG = travelSums[lastG];

        return totalAmount + timeM + timeP + timeG;
    }
}