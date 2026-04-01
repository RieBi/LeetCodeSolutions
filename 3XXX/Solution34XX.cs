namespace LeetCode.Set3XXX;
internal class Solution34XX
{
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
