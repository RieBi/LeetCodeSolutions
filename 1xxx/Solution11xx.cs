namespace LeetCode.Set1xxx;
internal class Solution11xx
{
    [ProblemSolution("1160")]
    public int CountCharacters(string[] words, string chars)
    {
        var charmap = mapArray(chars);
        var result = 0;
        for (int i = 0; i < words.Length; i++)
        {
            if (isWordGood(words[i]))
                result += words[i].Length;
        }

        return result;

        int[] mapArray(string str)
        {
            var map = new int[26];
            for (int i = 0; i < str.Length; i++)
            {
                map[str[i] - 'a']++;
            }

            return map;
        }

        bool isWordGood(string word)
        {
            var wordmap = mapArray(word);

            for (int i = 0; i < wordmap.Length; i++)
            {
                if (wordmap[i] > charmap[i])
                    return false;
            }

            return true;
        }
    }
}
