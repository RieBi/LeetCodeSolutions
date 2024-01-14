using System.Net.WebSockets;
using System.Runtime.CompilerServices;
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

    [ProblemSolution("236")]
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
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
