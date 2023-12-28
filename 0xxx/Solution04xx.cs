namespace LeetCode.Set0xxx;
internal class Solution04xx
{
    public int Compress(char[] chars)
    {
        if (chars.Length == 1)
            return 1;

        var count = 1;
        var pos = 0;
        for (int i = 1; i < chars.Length; i++)
        {
            if (chars[i] != chars[i - 1])
            {
                var countStr = count.ToString();
                chars[pos++] = chars[i - 1];
                if (count > 1)
                {
                    foreach (var ch in countStr)
                        chars[pos++] = ch;
                    count = 1;
                }
            }
            else
            {
                count++;
            }
        }

        var countStr2 = count.ToString();
        chars[pos++] = chars[chars.Length - 1];
        if (count > 1)
        {
            foreach (var ch in countStr2)
                chars[pos++] = ch;
        }

        return pos;
    }
}
