namespace LeetCode.Set1xxx;
internal class Solution17xx
{
    #region Solution for 1759
    public int CountHomogenous(string s)
    {
        var dp = new long[s.Length];
        dp[0] = 1;

        for (int i = 1; i < dp.Length; i++)
        {
            if (s[i] == s[i - 1])
            {
                dp[i] = dp[i - 1] + 1;
            }
            else
            {
                dp[i] = 1;
            }
        }
        long modulo = 1000000000 + 7;
        long total = 0;
        long n = 1;
        for (int i = 1; i < dp.Length; i++)
        {
            if (dp[i] <= n)
            {
                var sum = n * (n + 1) / 2;
                total += sum;
                total %= modulo;
                n = 1;
            }
            else
            {
                n = dp[i];
            }
        }
        total += (n * (n + 1) / 2);

        return (int)(total %= modulo);
    }
    #endregion
}