using System.Net.Security;

namespace LeetCode.Set0xxx;
internal class Solution01xx
{
    [ProblemSolution("102")]
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        var result = new List<IList<int>>();

        if (root is not null)
            queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var levelCount = queue.Count;
            result.Add([]);

            for (int i = 0; i < levelCount; i++)
            {
                var node = queue.Dequeue();
                result[^1].Add(node.val);

                if (node.left is not null)
                    queue.Enqueue(node.left);
                if (node.right is not null)
                    queue.Enqueue(node.right);
            }
        }

        return result;
    }

    [ProblemSolution("133")]
    public Node? CloneGraph(Node? node)
    {
        if (node is null)
            return null;

        var visited = new HashSet<Node>();
        proliferate(node);

        var newNodes = new Node[visited.Count + 1];
        foreach (var n in visited)
            newNodes[n.val] = new Node(n.val);

        foreach (var n in visited)
        {
            var newNode = newNodes[n.val];
            foreach (var neighbor in n.neighbors)
                newNode.neighbors.Add(newNodes[neighbor.val]);
        }

        return newNodes[1];

        void proliferate(Node n)
        {
            if (visited.Contains(n))
                return;

            visited.Add(n);
            foreach (var other in n.neighbors)
                proliferate(other);
        }
    }

    [ProblemSolution("136")]
    public int SingleNumber(int[] nums)
    {
        return nums.Aggregate((a, b) => a^b);
    }

    [ProblemSolution("144")]
    public IList<int> PreorderTraversal(TreeNode root)
    {
        if (root is null)
            return [];

        var list = new List<int>();
        var stack = new Stack<TreeNode>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var top = stack.Pop();
            list.Add(top.val);

            if (top.right is not null)
                stack.Push(top.right);

            if (top.left is not null)
                stack.Push(top.left);
        }

        return list;
    }

    [ProblemSolution("145")]
    public IList<int> PostorderTraversal(TreeNode root)
    {
        if (root is null)
            return [];

        var result = new List<int>();
        var stack = new Stack<TreeNode>();
        TreeNode? node = root;

        while (stack.Count > 0 || node is not null)
        {
            while (node is not null && (node.left is not null || node.right is not null))
            {
                stack.Push(node);
                node = node.left;
            }

            if (node is not null)
                result.Add(node.val);

            while (stack.Count > 0 && stack.Peek().right == node)
            {
                node = stack.Pop();
                result.Add(node.val);
            }

            if (stack.Count == 0)
                node = null;
            else
                node = stack.Peek().right;
        }

        return result;
    }

    [ProblemSolution("150")]
    public int EvalRPN(string[] tokens)
    {
        var stack = new Stack<int>();
        foreach (var token in tokens)
        {
            if (int.TryParse(token, out int result))
            {
                stack.Push(result);
                continue;
            }

            var op2 = stack.Pop();
            var op1 = stack.Pop();
            if (token == "+")
                stack.Push(op1 + op2);
            else if (token == "-")
                stack.Push(op1 - op2);
            else if (token == "*")
                stack.Push(op1 * op2);
            else
                stack.Push(op1 / op2);
        }

        return stack.Pop();
    }

    [ProblemSolution("155")]
    public class MinStack
    {
        private readonly List<(int val, int min)> list = [];

        public void Push(int val) => list.Add((val, list.Count == 0 ? val : Math.Min(val, list[^1].min)));

        public void Pop() => list.RemoveAt(list.Count - 1);

        public int Top() => list[^1].val;

        public int GetMin() => list[^1].min;
    }

    [ProblemSolution("191")]
    public int HammingWeight(uint n)
    {
        var result = 0;
        uint mask = 1;
        for (int i = 0; i < 32; i++)
        {
            if ((n & mask) == mask)
                result++;
            mask <<= 1;
        }

        return result;
    }
}