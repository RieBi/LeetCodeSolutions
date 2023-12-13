namespace LeetCode.Set1xxx;
internal class Soltuion15xx
{
    [ProblemSolution("1535")]
    public int GetWinner(int[] arr, int k)
    {
        var max = arr.Max();
        if (arr[0] == max)
            return max;

        var ind = 0;
        var count = 0;

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] == max)
                return max;

            if (arr[ind] > arr[i])
            {
                count++;
            }
            else
            {
                ind = i;
                count = 1;
            }

            if (count >= k)
            {
                return arr[ind];
            }
        }

        return -1;
    }

    [ProblemSolution("1561")]
    public int MaxCoins(int[] piles)
    {
        Array.Sort(piles);
        var triples = piles.Length / 3;
        var result = 0;
        for (int i = 1; i <= triples; i++)
        {
            result += piles[piles.Length - 2 * i];
        }

        return result;
    }

    public int NumSpecial(int[][] mat)
    {
        var rows = mat
            .Select(f => f.Count(el => el == 1))
            .ToList();

        var cols = new List<int>(mat[0].Length);
        for (int i = 0; i < mat[0].Length; i++)
        {
            var count = 0;
            for (int j = 0; j < mat.Length; j++)
            {
                if (mat[j][i] == 1)
                    count++;
            }

            cols.Add(count);
        }

        var total = 0;
        for (int i = 0; i < mat.Length; i++)
        {
            for (int j = 0; j < mat[0].Length; j++)
            {
                if (mat[i][j] == 1 && rows[i] == 1 && cols[j] == 1)
                    total++;
            }
        }

        return total;
    }
}