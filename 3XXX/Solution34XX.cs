namespace LeetCode.Set3XXX;
internal class Solution34XX
{
    [ProblemSolution("3418")]
    public int MaximumAmount(int[][] coins)
    {
        var dp = new int[coins.Length][][];

        for (var i = 0; i < coins.Length; i++)
        {
            dp[i] = new int[coins[i].Length][];

            for (var j = 0; j < coins[i].Length; j++)
                dp[i][j] = [-int.MaxValue, -int.MaxValue, -int.MaxValue];
        }

        var start = dp[0][0];

        start[0] = coins[0][0];
        start[1] = coins[0][0] < 0 ? 0 : start[0];
        start[2] = start[1];

        for (var i = 0; i < coins.Length; i++)
        {
            for (var j = 0; j < coins[i].Length; j++)
            {
                var arr = dp[i][j];
                var (cur0, cur1, cur2) = (arr[0], arr[1], arr[2]);

                if (i < coins.Length - 1)
                {
                    var val = coins[i + 1][j];
                    var (d0, d1, d2) = (cur0 + val, cur1 + val, cur2 + val);

                    if (val < 0)
                    {
                        d1 = Math.Max(d1, cur0);
                        d2 = Math.Max(d2, cur1);
                    }

                    var newArr = dp[i + 1][j];

                    newArr[0] = Math.Max(newArr[0], d0);
                    newArr[1] = Math.Max(newArr[1], d1);
                    newArr[2] = Math.Max(newArr[2], d2);
                }

                if (j < coins[i].Length - 1)
                {
                    var val = coins[i][j + 1];
                    var (d0, d1, d2) = (cur0 + val, cur1 + val, cur2 + val);

                    if (val < 0)
                    {
                        d1 = Math.Max(d1, cur0);
                        d2 = Math.Max(d2, cur1);
                    }

                    var newArr = dp[i][j + 1];

                    newArr[0] = Math.Max(newArr[0], d0);
                    newArr[1] = Math.Max(newArr[1], d1);
                    newArr[2] = Math.Max(newArr[2], d2);
                }
            }
        }

        var finalArr = dp[^1][^1];

        return finalArr.Max();
    }
    
    [ProblemSolution("3474")]
    public string GenerateString(string str1, string str2)
    {
        var str = new char[str1.Length + str2.Length - 1];
        var marked = new bool[str1.Length + str2.Length - 1];

        for (var i = 0; i < str1.Length; i++)
        {
            if (str1[i] == 'F')
                continue;

            for (var j = i; j < i + str2.Length; j++)
            {
                if (str[j] != '\0' && str[j] != str2[j - i])
                    return string.Empty;

                str[j] = str2[j - i];
                marked[j] = true;
            }
        }

        for (var i = 0; i < str1.Length; i++)
        {
            if (str1[i] == 'T')
                continue;

            if (str[i] == '\0')
                str[i] = 'a';

            var lastUnmarked = -1;
            var match = true;

            for (var j = i; j < i + str2.Length; j++)
            {
                var ch = str[j] == '\0' ? 'a' : str[j];

                if (ch != str2[j - i])
                {
                    match = false;
                    break;
                }

                if (!marked[j])
                    lastUnmarked = j;
            }

            if (match)
            {
                if (lastUnmarked != -1)
                    str[lastUnmarked] = 'b';
                else
                    return string.Empty;
            }
        }

        for (var i = str1.Length; i < str.Length; i++)
        {
            if (str[i] == '\0')
                str[i] = 'a';
        }
        
        return new string(str);
    }
}
