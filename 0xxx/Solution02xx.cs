using System.Diagnostics.Tracing;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace LeetCode.Set0xxx;
internal class Solution02xx
{
    [ProblemSolution("200")]
    public int NumIslands(char[][] grid)
    {
        const char explored = '.';
        var count = 0;
        (int, int)[] dirs = [(1, 0), (-1, 0), (0, 1), (0, -1)];
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == '1')
                    count++;

                processTile(i, j);
            }
        }

        return count;

        void processTile(int i, int j)
        {
            if ((i < 0 || i >= grid.Length) || (j < 0 || j >= grid[0].Length) || grid[i][j] != '1')
                return;

            grid[i][j] = explored;
            foreach (var dir in dirs)
                processTile(i + dir.Item1, j + dir.Item2);
        }
    }

    [ProblemSolution("202")]
    public bool IsHappy(int n)
    {
        var set = new HashSet<int>();
        while (n != 1)
        {
            set.Add(n);
            n = (int) n.ToString()
                .Select(f => Math.Pow(int.Parse(f.ToString()), 2))
                .Sum();
            if (set.Contains(n))
                return false;
        }

        return true;
    }

    [ProblemSolution("204")]
    public int CountPrimes(int n)
    {
        var nums = new bool[Math.Max(2, n)];

        nums[0] = true;
        nums[1] = true;
        var top = Math.Sqrt(n);
        for (int i = 2; i < top; i++)
        {
            if (nums[i])
                continue;
            for (int j = 2 * i; j < n; j += i)
                nums[j] = true;
        }

        return nums.Count(f => !f);
    }

    [ProblemSolution("205")]
    public bool IsIsomorphic(string s, string t)
    {
        var mappings = new Dictionary<char, char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (mappings.TryGetValue(s[i], out char value))
            {
                if (value != t[i])
                    return false;
            }
            else
            {
                if (mappings.ContainsValue(t[i]))
                    return false;
                mappings[s[i]] = t[i];
            }
        }

        return true;
    }

    [ProblemSolution("206")]
    public ListNode? ReverseList(ListNode? head)
    {
        if (head is null)
            return null;

        ListNode newHead = null!;
        traverse(head).next = null;
        return newHead;

        ListNode traverse(ListNode node)
        {
            if (node.next is null)
            {
                newHead = node;
                return node;
            }

            var last = traverse(node.next);
            last.next = node;
            return node;
        }
    }

    [ProblemSolution("208")]
    public class Trie
    {
        private class TrieNode(TrieNode[] Children)
        {
            public TrieNode[] Children { get; } = Children;
            public bool IsWord { get; set; } = false;
        }
        private readonly TrieNode root = new(new TrieNode[26]);

        public void Insert(string word)
        {
            var node = root;
            for (int i = 0; i < word.Length; i++)
            {
                var index = word[i] - 'a';
                if (node.Children[index] is null)
                    node.Children[index] = new TrieNode(new TrieNode[26]);

                node = node.Children[index];
            }

            node.IsWord = true;
        }

        public bool Search(string word)
        {
            var node = root;
            for (int i = 0; i < word.Length; i++)
            {
                var index = word[i] - 'a';
                if (node.Children[index] is null)
                    return false;

                node = node.Children[index];
            }

            return node.IsWord;
        }

        public bool StartsWith(string prefix)
        {
            var node = root;
            for (int i = 0; i < prefix.Length; i++)
            {
                var index = prefix[i] - 'a';
                if (node.Children[index] is null)
                    return false;

                node = node.Children[index];
            }

            return true;
        }
    }

    [ProblemSolution("211")]
    public class WordDictionary
    {
        public class WordNode()
        {
            public Dictionary<char, WordNode> Children { get; } = new();
            public bool IsWord { get; set; } = default; 
        }

        private WordNode Root = new();

        public void AddWord(string word)
        {
            var node = Root;
            for (int i = 0; i < word.Length; i++)
            {
                if (!node.Children.ContainsKey(word[i]))
                    node.Children[word[i]] = new();

                node = node.Children[word[i]];
            }

            node.IsWord = true;
        }

        public bool Search(string word) => Match(Root, word, 0);

        private bool Match(WordNode node, string match, int index)
        {
            if (index == match.Length)
                return node.IsWord;

            if (match[index] == '.')
            {
                foreach (var child in node.Children.Values)
                    if (Match(child, match, index + 1))
                        return true;

                return false;
            }
            else if (!node.Children.TryGetValue(match[index], out var value))
                return false;
            else
                return Match(value, match, index + 1);
        }
    }

    [ProblemSolution("212")]
    public IList<string> FindWords(char[][] board, string[] words)
    {
        var root = new WordNode();

        foreach (var word in words)
        {
            var node = root;
            for (int i = 0; i < word.Length; i++)
            {
                if (!node.Children.ContainsKey(word[i]))
                    node.Children[word[i]] = new();

                node = node.Children[word[i]];
            }

            node.IsWord = true;
        }

        var list = new List<char>();
        var result = new List<string>();

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                propagate((i, j), root);
            }
        }

        return result;

        void propagate((int i, int j) pos, WordNode node)
        {
            if (pos.i < 0 || pos.j < 0 || pos.i >= board.Length || pos.j >= board[0].Length
                || !node.Children.ContainsKey(board[pos.i][pos.j]) || board[pos.i][pos.j] == '.')
                return;

            var ch = board[pos.i][pos.j];
            list.Add(ch);
            node = node.Children[ch];
            if (node.IsWord)
            {
                var str = new StringBuilder();
                foreach (var c in list)
                    str.Append(c);
                result.Add(str.ToString());

                node.IsWord = false;
            }

            if (list.Count < 10)
            {
                board[pos.i][pos.j] = '.';
                propagate((pos.i + 1, pos.j), node);
                propagate((pos.i - 1, pos.j), node);
                propagate((pos.i, pos.j + 1), node);
                propagate((pos.i, pos.j - 1), node);
                board[pos.i][pos.j] = ch;
            }

            list.RemoveAt(list.Count - 1);
        }
    }

    [ProblemSolution("212")]
    public class WordNode()
    {
        public Dictionary<char, WordNode> Children { get; } = [];
        public bool IsWord { get; set; } = default;
    }

    [ProblemSolution("217")]
    public bool ContainsDuplicate(int[] nums)
    {
        return nums.Distinct().Count() != nums.Length;
    }

    [ProblemSolution("219")]
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        return nums
            .Zip(Enumerable.Range(0, nums.Length))
            .GroupBy(f => f.First, f => f.Second)
            .Select(f => f.OrderBy(f => f))
            .Any(s =>
            {
                var min = int.MaxValue;
                s.Skip(1).Aggregate(s.First(), (a, b) =>
                {
                    var diff = Math.Abs(a - b);
                    min = Math.Min(min, diff);
                    return b;
                });
                return min <= k;
            });
    }

    [ProblemSolution("220")]
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int indexDiff, int valueDiff)
    {
        var sorted = nums[0..(Math.Min(nums.Length, indexDiff + 1))].ToList();
        sorted.Sort();

        for (int i = 1; i < sorted.Count; i++)
            if (sorted[i] - sorted[i - 1] <= valueDiff)
                return true;

        for (int i = indexDiff + 1; i < nums.Length; i++)
        {
            var prevInd = sorted.BinarySearch(nums[i - 1 - indexDiff]);
            sorted.RemoveAt(prevInd);
            var nextInd = sorted.BinarySearch(nums[i]);
            if (nextInd < 0)
                nextInd = ~nextInd;
            sorted.Insert(nextInd, nums[i]);

            if (nextInd > 0 && sorted[nextInd] - sorted[nextInd - 1] <= valueDiff)
                return true;
            if (nextInd < sorted.Count - 1 && sorted[nextInd + 1] - sorted[nextInd] <= valueDiff)
                return true;
        }

        return false;
    }

    [ProblemSolution("225")]
    public class MyStack
    {
        private Queue<int> queue = new();

        public void Push(int x) => queue.Enqueue(x);

        public int Pop()
        {
            for (int i = 0; i < queue.Count - 1; i++)
                queue.Enqueue(queue.Dequeue());
            return queue.Dequeue();
        }

        public int Top()
        {
            for (int i = 0; i < queue.Count - 1; i++)
                queue.Enqueue(queue.Dequeue());
            var top = queue.Peek();
            queue.Enqueue(queue.Dequeue());
            return top;
        }

        public bool Empty() => queue.Count == 0;
    }

    [ProblemSolution("232")]
    public class MyQueue
    {
        private bool pushMode = true;
        private Stack<int> stack1 = new();
        private Stack<int> stack2 = new();

        public void Push(int x) => stack1.Push(x);

        public int Pop()
        {
            if (stack2.Count == 0)
                Transfer();
            return stack2.Pop();
        }

        public int Peek()
        {
            if (stack2.Count == 0)
                Transfer();
            return stack2.Peek();
        }

        public bool Empty() => stack1.Count == 0 && stack2.Count == 0;

        private void Transfer()
        {
            while (stack1.Count > 0)
                stack2.Push(stack1.Pop());
        }
    }

    [ProblemSolution("235")]
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        var min = Math.Min(p.val, q.val);
        var max = Math.Max(p.val, q.val);

        while (root!.val > max || root.val < min)
        {
            if (root.val > max)
                root = root.left!;
            else
                root = root.right!;
        }

        return root;
    }

    [ProblemSolution("236")]
    public TreeNode LowestCommonAncestor2(TreeNode root, TreeNode p, TreeNode q)
    {
        var stack = new Stack<(TreeNode? node, bool? leftVal, bool? rightVal)>();
        stack.Push((root, null, null));

        while (stack.Count > 0)
        {
            var top = stack.Peek();
            if (top.node is null)
            {
                stack.Pop();
                top = stack.Pop();
                stack.Push((top.node, top.leftVal is null ? false : top.leftVal, top.leftVal is not null ? false : null));
                continue;
            }

            if (top.leftVal is null)
                stack.Push((top.node.left, null, null));
            else if (top.rightVal is null)
                stack.Push((top.node.right, null, null));
            else
            {
                if ((top.leftVal == true && top.rightVal == true)
                    || (top.node == p || top.node == q) && (top.leftVal == true || top.rightVal == true))
                    return top.node;

                bool? result = top.node == p || top.node == q || top.leftVal == true || top.rightVal == true;
                stack.Pop();
                top = stack.Pop();
                stack.Push((top.node, top.leftVal is null ? result : top.leftVal, top.leftVal is not null ? result : top.rightVal));
            }
        }

        return root;
    }

    [ProblemSolution("242")]
    public bool IsAnagram(string s, string t)
    {
        var dicks = new Dictionary<char, int>();
        var dickt = new Dictionary<char, int>();

        FillDick(s, dicks);
        FillDick(t, dickt);

        return DicksEqual(dicks, dickt);

        void FillDick(string str, Dictionary<char, int> dick)
        {
            foreach (var v in str)
            {
                if (dick.TryGetValue(v, out int val))
                    dick[v] = val + 1;
                else
                    dick[v] = 1;
            }
        }

        bool DicksEqual(Dictionary<char, int> dick1, Dictionary<char, int> dick2)
        {
            if (dick1.Count != dick2.Count)
                return false;

            foreach (var v in dick1.Keys)
            {
                if (!(dick1.TryGetValue(v, out int val1) && dick2.TryGetValue(v, out int val2)))
                    return false;
                else if (val1 != val2)
                    return false;
            }

            return true;
        }
    }

    [ProblemSolution("279")]
    public int NumSquares(int n)
    {
        var queue = new Queue<(int max, int sum)>();
        queue.Enqueue((0, 0));

        var count = 1;
        Queue<(int max, int sum)> nextQueue;
        while (true)
        {
            nextQueue = new();
            while (queue.Count > 0)
            {
                var prev = queue.Dequeue();
                var powBase = (int)(Math.Sqrt(prev.max));
                for (; powBase * powBase <= (n - prev.sum); powBase++)
                {
                    var powered = powBase * powBase;
                    var next = (powered, prev.sum + powered);
                    if (next.Item2 == n)
                        return count;
                    nextQueue.Enqueue(next);
                }
            }

            queue = nextQueue;
            count++;
        }
    }

    [ProblemSolution("297")]
    public class Codec
    {
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            if (root is null)
                return "";

            var queue = new Queue<(TreeNode? node, int flag)>();
            queue.Enqueue((root, 0));
            var str = new StringBuilder();
            while (queue.Count > 0)
            {
                var top = queue.Dequeue();
                var node = top.node;
                if (top.flag == 0)
                    str.Append(node!.val);
                else if (top.flag == 1)
                    str.Append("l" + node!.val);
                else if (top.flag == 2)
                    str.Append("r" + node!.val);
                else
                    str.Append('n');

                str.Append(',');

                if (node is null)
                    continue;

                if (node.left is null && node.right is null)
                    queue.Enqueue((null, -1));
                else if (node.left is not null && node.right is not null)
                {
                    queue.Enqueue((node.left, 0));
                    queue.Enqueue((node.right, 0));
                }
                else if (node.left is not null)
                    queue.Enqueue((node.left, 1));
                else
                    queue.Enqueue((node.right, 2));
            }

            return str.ToString();
        }

        // Decodes your encoded data to tree.
        public TreeNode? deserialize(string data)
        {
            if (data == "")
                return null;

            var strs = data.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var queue = new Queue<TreeNode>();
            queue.Enqueue(new TreeNode(int.Parse(strs[0])));
            var head = queue.Peek();
            var ind = 1;

            while (queue.Count > 0)
            {
                var top = queue.Dequeue();
                if (strs[ind][0] == 'l')
                    top.left = new TreeNode(int.Parse(strs[ind][1..]));
                else if (strs[ind][0] == 'r')
                    top.right = new TreeNode(int.Parse(strs[ind][1..]));
                else if (strs[ind][0] != 'n')
                {
                    top.left = new TreeNode(int.Parse(strs[ind++]));
                    top.right = new TreeNode(int.Parse(strs[ind]));
                }

                if (top.left is not null)
                    queue.Enqueue(top.left);
                if (top.right is not null)
                    queue.Enqueue(top.right);

                ind++;
            }

            return head;
        }
    }
}
