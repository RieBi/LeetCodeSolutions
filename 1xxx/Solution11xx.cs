using System.Text;

namespace LeetCode.Set1XXX;
internal class Solution11XX
{
    [ProblemSolution("1105")]
    public int MinHeightShelves(int[][] books, int shelfWidth)
    {
        var dp = new int[books.Length + 1];
        dp[0] = 0;
        dp[1] = books[0][1];

        for (int i = 2; i <= books.Length; i++)
        {
            var remaining = shelfWidth -  books[i - 1][0];
            var maxHeight = books[i - 1][1];

            dp[i] = dp[i - 1] + maxHeight;

            for (int j = i - 1; j > 0 && remaining - books[j - 1][0] >= 0; j--)
            {
                remaining -= books[j - 1][0];
                maxHeight = Math.Max(maxHeight, books[j - 1][1]);

                dp[i] = Math.Min(dp[i], dp[j - 1] + maxHeight);
            }
        }

        return dp[^1];
    }

    [ProblemSolution("1110")]
    public IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
    {
        var tbd = to_delete.ToHashSet();
        var result = new List<TreeNode>();

        if (!tbd.Contains(root.val))
            result.Add(root);

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var c = queue.Count;
            for (int i = 0; i < c; i++)
            {
                var node = queue.Dequeue();
                if (node.left is not null)
                    queue.Enqueue(node.left);
                if (node.right is not null)
                    queue.Enqueue(node.right);

                if (tbd.Contains(node.val))
                {
                    if (!tbd.Contains(node.left?.val ?? to_delete[0]))
                        result.Add(node.left!);

                    if (!tbd.Contains(node.right?.val ?? to_delete[0]))
                        result.Add(node.right!);
                }
                else
                {
                    if (tbd.Contains(node.left?.val ?? 0))
                        node.left = null;

                    if (tbd.Contains(node.right?.val ?? 0))
                        node.right = null;
                }
            }
        }

        return result;
    }

    [ProblemSolution("1122")]
    public int[] RelativeSortArray(int[] arr1, int[] arr2)
    {
        var groups = arr1.GroupBy(f => f).ToDictionary(
            f => f.Key,
            f => f.Count());

        var result = new int[arr1.Length];
        var ind = 0;
        for (int i = 0; i < arr2.Length; i++)
        {
            if (groups.TryGetValue(arr2[i], out var value))
            {
                for (int j = 0; j < value; j++)
                {
                    result[ind++] = arr2[i];
                }

                groups.Remove(arr2[i]);
            }
        }

        var endInd = ind;
        while (ind < result.Length)
        {
            var pair = groups.First();
            for (int i = 0; i < pair.Value; i++)
            {
                result[ind++] = pair.Key;
            }

            groups.Remove(pair.Key);
        }

        if (endInd < result.Length)
            Array.Sort(result, endInd, result.Length - endInd);

        return result;
    }

    [ProblemSolution("1143")]
    public int LongestCommonSubsequence(string text1, string text2)
    {
        var dp = new int[text1.Length + 1][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new int[text2.Length + 1];

        for (int i = 1; i < dp.Length; i++)
        {
            for (int j = 1; j < dp[0].Length; j++)
            {
                if (text1[i - 1] == text2[j - 1])
                    dp[i][j] = dp[i - 1][j - 1] + 1;
                else
                    dp[i][j] = Math.Max(dp[i][j - 1], dp[i - 1][j]);
            }
        }

        return dp[^1][^1];
    }

    [ProblemSolution("1155")]
    public int NumRollsToTarget(int n, int k, int target)
    {
        if (target < n || target > (n * k))
            return 0;

        var max = n * k + 1;

        var dp = new int[n + 1, max];
        dp[n, 0] = 1;

        var modulo = 1_000_000_007;

        for (int dice = n; dice > 0; dice--)
        {
            for (int sum = 0; sum < max; sum++)
            {
                if (dp[dice, sum] == 0)
                    continue;
                for (int i = 1; i <= k; i++)
                {
                    dp[dice - 1, sum + i] += dp[dice, sum];
                    dp[dice - 1, sum + i] %= modulo;
                }
            }
        }

        return dp[0, target];
    }

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

    [ProblemSolution("1190")]
    public string ReverseParentheses(string s)
    {
        var dictionary = new Dictionary<int, int>();
        var stack = new Stack<int>();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
                stack.Push(i);
            else if (s[i] == ')')
            {
                var ind = stack.Pop();
                dictionary[ind] = i;
                dictionary[i] = ind;
            }
        }

        var str = new StringBuilder();

        traverse(1, 0);
        return str.ToString();

        void traverse(int dir, int ind)
        {
            var otherSymbol = dir == 1 ? '(' : ')';
            var endSymbol = dir == 1 ? ')' : '(';

            if (s[ind] == endSymbol)
                ind = dictionary[ind] + dir;

            while (ind >= 0 && ind < s.Length && s[ind] != endSymbol)
            {
                if (s[ind] == otherSymbol)
                {
                    traverse(-dir, ind);
                    ind = dictionary[ind];
                }
                else
                    str.Append(s[ind]);

                ind += dir;
            }
        }
    }
}
