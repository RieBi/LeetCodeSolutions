using System.Diagnostics.Tracing;
using System.Text;

namespace LeetCode.Set0XXX;
internal class Solution06XX
{
    [ProblemSolution("606")]
    public string Tree2str(TreeNode root)
    {
        var resultStr = new StringBuilder();

        void updateStr(TreeNode? node)
        {
            if (node == null)
                return;

            resultStr.Append(node.val);

            if (node.left != null || node.right != null)
            {
                resultStr.Append('(');
                updateStr(node.left);
                resultStr.Append(')');
            }
            if (node.right != null)
            {
                resultStr.Append('(');
                updateStr(node.right);
                resultStr.Append(')');
            }
        }

        updateStr(root);

        return resultStr.ToString();
    }

    [ProblemSolution("622")]
    public class MyCircularQueue(int k)
    {
        private readonly int[] ar = new int[k];
        private int count = 0;
        private int head = 0;
        private int tail = -1;

        public bool EnQueue(int value)
        {
            if (count == ar.Length)
                return false;

            tail++;
            tail %= ar.Length;
            ar[tail] = value;
            count++;
            return true;
        }

        public bool DeQueue()
        {
            if (count == 0)
                return false;

            head++;
            head %= ar.Length;
            count--;
            return true;
        }

        public int Front() => count == 0 ? -1 : ar[head];

        public int Rear() => count == 0 ? -1 : ar[tail];

        public bool IsEmpty() => count == 0;

        public bool IsFull() => count == ar.Length;
    }

    [ProblemSolution("623")]
    public TreeNode AddOneRow(TreeNode root, int val, int depth)
    {
        if (depth == 1)
        {
            var newroot = new TreeNode(val, root);
            return newroot;
        }

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        var level = 1;

        while (level < depth - 1)
        {
            var count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                var last = queue.Dequeue();
                if (last.left is not null)
                    queue.Enqueue(last.left);
                if (last.right is not null)
                    queue.Enqueue(last.right);
            }

            level++;
        }

        while (queue.Count > 0)
        {
            var last = queue.Dequeue();
            var newleft = new TreeNode(val, left: last.left);
            last.left = newleft;
            var newright = new TreeNode(val, right: last.right);
            last.right = newright;
        }

        return root;
    }

    [ProblemSolution("624")]
    public int MaxDistance(IList<IList<int>> arrays)
    {
        var ars = (List<IList<int>>)arrays;

        ars.Sort((a, b) => a[0] - b[0]);
        var max = Math.Abs(ars[0][^1] - ars[1][0]);

        for (int i = 1; i < ars.Count; i++)
            max = Math.Abs(Math.Max(max, ars[i][^1] - ars[0][0]));

        return max;
    }

    [ProblemSolution("629")]
    public int KInversePairs(int n, int k)
    {
        var dp = new int[n][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new int[k + 1];

        var modulo = 1_000_000_007;
        dp[0][0] = 1;
        for (int i = 1; i < dp.Length; i++)
        {
            var sum = 0;
            for (int j = 0; j < dp[i].Length; j++)
            {
                sum += dp[i - 1][j];
                if (j > i)
                    sum -= dp[i - 1][j - i - 1];
                if (sum < 0)
                    sum += modulo;
                sum %= modulo;
                dp[i][j] = sum;
            }
        }

        return dp[^1][^1];
    }

    [ProblemSolution("633")]
    public bool JudgeSquareSum(int c)
    {
        for (long i = 0; i * i <= c; i++)
        {
            var b2 = c - i * i;
            var bf = Math.Sqrt(b2);
            if (bf == Math.Floor(bf))
                return true;
        }

        return false;
    }

    [ProblemSolution("645")]
    public int[] FindErrorNums(int[] nums)
    {
        var freq = new int[nums.Length];
        foreach (var num in nums)
            freq[num - 1]++;

        var result = new int[2];
        for (int i = 0; i < freq.Length; i++)
        {
            if (freq[i] == 2)
                result[0] = i + 1;
            if (freq[i] == 0)
                result[1] = i + 1;
        }

        return result;
    }

    [ProblemSolution("647")]
    public int CountSubstrings(string s)
    {
        var count = s.Length;
        for (int i = 0; i < s.Length; i++)
        {
            for (int shift = 1; i - shift >= 0 && i + shift < s.Length && s[i - shift] == s[i + shift]; shift++)
                count++;

            for (int shift = 0; i - shift >= 0 && i + shift + 1 < s.Length && s[i - shift] == s[i + shift + 1]; shift++)
                count++;
        }

        return count;
    }

    [ProblemSolution("648")]
    public string ReplaceWords(IList<string> dictionary, string sentence)
    {
        var root = new ReplaceNode();
        foreach (var r in dictionary)
            insert(r);

        var words = sentence.Split(' ');
        for (int i = 0; i < words.Length; i++)
            words[i] = getRoot(words[i]);

        return string.Join(' ', words);

        void insert(string str)
        {
            var node = root!;
            for (int i = 0; i < str.Length; i++)
            {
                if (!node.Children.ContainsKey(str[i]))
                    node.Children[str[i]] = new();

                node = node.Children[str[i]];
            }

            node.IsRoot = true;
        }

        string getRoot(string str)
        {
            var node = root!;
            for (int i = 0; i < str.Length; i++)
            {
                if (!node.Children.TryGetValue(str[i], out ReplaceNode? value))
                    return str;

                node = value;
                if (node.IsRoot)
                    return str[..(i + 1)];
            }

            return str;
        }
    }

    [ProblemSolution("648")]
    public class ReplaceNode
    {
        public Dictionary<char, ReplaceNode> Children { get; } = [];
        public bool IsRoot { get; set; } = default;
    }

    [ProblemSolution("652")]
    public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
    {
        var dick = new Dictionary<int, int>();
        var result = new List<TreeNode>();

        calculateHash(root);
        return result;

        int calculateHash(TreeNode? node)
        {
            if (node == null)
                return 777;
            var hash = HashCode.Combine(calculateHash(node.left), node.val, calculateHash(node.right));
            if (dick.TryGetValue(hash, out int value))
            {
                if (value == 1)
                {
                    result.Add(node);
                    dick[hash] = value + 1;
                }
            }
            else
            {
                dick[hash] = 1;
            }

            return hash;
        }
    }

    [ProblemSolution("661")]
    public int[][] ImageSmoother(int[][] img)
    {
        var dp = new int[img.Length][];
        for (int i = 0; i < img.Length; i++)
        {
            dp[i] = new int[img[0].Length];
            for (int j = 0; j < img[0].Length; j++)
            {
                var left = j == 0 ? 0 : dp[i][j - 1];
                var top = i == 0 ? 0 : dp[i - 1][j];
                var rest = i == 0 || j == 0 ? 0 : dp[i - 1][j - 1];
                dp[i][j] = img[i][j] + left + top - rest;
            }
        }

        var result = new int[img.Length][];
        for (int i = 0; i < img.Length; i++)
        {
            result[i] = new int[img[0].Length];
            for (int j = 0; j < img[0].Length; j++)
            {
                var headI = Math.Min(i + 1, img.Length - 1);
                var headJ = Math.Min(j + 1, img[0].Length - 1);
                var head = dp[headI][headJ];
                var top = i < 2 ? 0 : dp[i - 2][headJ];
                var left = j < 2 ? 0 : dp[headI][j - 2];
                var rest = i < 2 || j < 2 ? 0 : dp[i - 2][j - 2];

                var res = head - left - top + rest;
                var starti = Math.Max(0, i - 1);
                var startj = Math.Max(0, j - 1);
                var area = (headI - starti + 1) * (headJ - startj + 1);
                result[i][j] = res / area;
            }
        }

        return result;
    }

    [ProblemSolution("677")]
    public class MapSum
    {
        private sealed class MapNode()
        {
            public Dictionary<char, MapNode> Children { get; } = [];
            public int Value { get; set; } = 0;
        }

        private readonly MapNode Root = new();


        public void Insert(string key, int val)
        {
            var node = Root;
            for (int i = 0; i < key.Length; i++)
            {
                if (!node.Children.ContainsKey(key[i]))
                    node.Children[key[i]] = new();

                node = node.Children[key[i]];
            }

            node.Value = val;
        }

        public int Sum(string prefix)
        {
            var node = Root;
            for (int i = 0; i < prefix.Length; i++)
            {
                if (!node.Children.TryGetValue(prefix[i], out var value))
                    return 0;
                else
                    node = value;
            }

            var queue = new Queue<MapNode>();
            queue.Enqueue(node);
            var sum = 0;
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                sum += item.Value;
                foreach (var child in item.Children.Values)
                    queue.Enqueue(child);
            }

            return sum;
        }
    }
}
