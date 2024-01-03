namespace LeetCode.Set2xxx;
internal class Solution21xx
{
    [ProblemSolution("2125")]
    public int NumberOfBeams(string[] bank)
    {
        var precCount = bank[0].Count(f => f == '1');
        var sum = 0;
        for (int i = 1; i < bank.Length; i++)
        {
            var count = bank[i].Count(f => f == '1');
            if (count != 0)
            {
                sum += precCount * count;
                precCount = count;
            }
        }

        return sum;
    }

    [ProblemSolution("2147")]
    public int NumberOfWays(string corridor)
    {
        var result = 1L;
        var curStreak = 0;
        var plantStreak = 0;
        var modulo = 1_000_000_007L;
        for (int i = 0; i < corridor.Length; i++)
        {
            var curObject = corridor[i];
            if (curStreak != 2)
            {
                if (curObject == 'S')
                    curStreak++;
            }
            else
            {
                if (curObject == 'S')
                {
                    result *= (long)(plantStreak + 1);
                    result %= modulo;
                    curStreak = 1;
                    plantStreak = 0;
                }
                else if (curObject == 'P')
                {
                    plantStreak++;
                }
            }
        }

        if (curStreak != 2)
            result = 0;

        return (int)result;
    }
}
