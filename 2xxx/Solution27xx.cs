namespace LeetCode.Set2xxx;
internal class Solution27xx
{
    [ProblemSolution("2706")]
    public int BuyChoco(int[] prices, int money)
    {
        var min1 = prices[0];
        var min2 = prices[1];
        if (min1 > min2)
            (min1, min2) = (min2, min1);

        for (int i = 2; i < prices.Length; i++)
        {
            if (prices[i] < min2)
                min2 = prices[i];

            if (min1 > min2)
                (min1, min2) = (min2, min1);
        }

        var leftover = money - min1 - min2;
        return leftover >= 0 ? leftover : money;
    }
}
