namespace LeetCode.Set2xxx;
internal class Solution21xx
{
    [ProblemSolution("2108")]
    public string FirstPalindrome(string[] words)
    {
        foreach (var word in words)
        {
            var left = 0;
            var right = word.Length - 1;
            for (; left < right; left++, right--)
            {
                if (word[left] != word[right])
                    break;
            }

            if (word[left] == word[right])
                return word;
        }

        return "";
    }

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

    [ProblemSolution("2149")]
    public int[] RearrangeArray(int[] nums)
    {
        var stash = new Queue<int>();
        var stash2 = new Queue<int>();
        var l = 0;
        var r = 0;

        while (l < nums.Length)
        {
            var s = l % 2 == 0 ? stash : stash2;
            if (s.Count == 0)
            {
                if (Math.Sign(nums[r]) == 1)
                    stash.Enqueue(nums[r++]);
                else
                    stash2.Enqueue(nums[r++]);
            }
            else
                nums[l++] = s.Dequeue();
        }

        return nums;
    }
}
