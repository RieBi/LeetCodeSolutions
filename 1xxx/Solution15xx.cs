namespace LeetCode.Set1xxx;
internal class Soltuion15xx
{
    #region Solution for 1535
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
    #endregion

    #region Solution for 1561
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
    #endregion
}