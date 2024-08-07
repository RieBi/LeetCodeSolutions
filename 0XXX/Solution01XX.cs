﻿using System.Data;
using System.Net.Security;

namespace LeetCode.Set0XXX;
internal class Solution01XX
{
    [ProblemSolution("101")]
    public bool IsSymmetric(TreeNode root)
    {
        return getVals(root.left).SequenceEqual(getVals(root.right, false));

        IEnumerable<int?> getVals(TreeNode? node, bool isLeft = true)
        {
            var stack = new Stack<TreeNode>();

            TreeNode? cur = node;

            while (stack.Count > 0 || cur is not null)
            {
                while (cur is not null)
                {
                    yield return cur.val;
                    stack.Push(cur);
                    cur = isLeft ? cur.left : cur.right;
                }

                yield return cur?.val;

                cur = isLeft ? stack.Pop().right : stack.Pop().left;
                if (cur is null)
                    yield return cur?.val;
            }
        }
    }

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

    [ProblemSolution("104")]
    public int MaxDepth(TreeNode root)
    {
        return traverse(root);

        int traverse(TreeNode? node)
        {
            if (node is null)
                return 0;

            return Math.Max(traverse(node.left), traverse(node.right)) + 1;
        }
    }

    [ProblemSolution("105")]
    public TreeNode? BuildTree(int[] preorder, int[] inorder)
    {
        return getRoot(0, preorder.Length - 1, 0, inorder.Length - 1);

        TreeNode? getRoot(int preStart, int preEnd, int inStart, int inEnd)
        {
            if (preEnd - preStart < 0 || inEnd - inStart < 0)
                return null;

            var root = preorder[preStart];
            if (preStart == preEnd)
                return new TreeNode(root);

            var inInd = 0;
            for (int i = inStart; i <= inEnd; i++)
            {
                if (inorder[i] == root)
                {
                    inInd = i;
                    break;
                }
            }

            var rightStart = inInd - inStart + preStart + 1;
            var leftChild = getRoot(preStart + 1, rightStart - 1, inStart, inInd - 1);
            var rightChild = getRoot(rightStart, preEnd, inInd + 1, inEnd);

            return new TreeNode(root, leftChild, rightChild);
        }
    }

    [ProblemSolution("106")]
    public TreeNode? BuildTree2(int[] inorder, int[] postorder)
    {
        return getRoot((0, inorder.Length - 1), (0, postorder.Length - 1));

        TreeNode? getRoot((int start, int end) inorderInterval, (int start, int end) postorderInterval)
        {
            if (inorderInterval.end - inorderInterval.start < 0 
                || postorderInterval.end - postorderInterval.start < 0)
                return null;

            var root = postorder[postorderInterval.end];
            if (postorderInterval.start == postorderInterval.end)
                return new TreeNode(root);

            var inorderInd = 0;
            for (int i = inorderInterval.start; i <= inorderInterval.end; i++)
            {
                if (inorder[i] == root)
                {
                    inorderInd = i;
                    break;
                }
            }

            var rightStart = inorderInd - inorderInterval.start + postorderInterval.start;

            var leftChild = getRoot((inorderInterval.start, inorderInd - 1),
                (postorderInterval.start, rightStart - 1));

            var rightChild = getRoot((inorderInd + 1, inorderInterval.end),
                (rightStart, postorderInterval.end - 1));

            var node = new TreeNode(root, leftChild, rightChild);

            return node;
        }
    }

    [ProblemSolution("108")]
    public TreeNode? SortedArrayToBST(int[] nums)
    {
        return convert(0, nums.Length - 1);
        TreeNode? convert(int left, int right)
        {
            if (left > right)
                return null;

            var mid = (left + right) / 2;
            var node = new TreeNode(nums[mid]);
            node.left = convert(left, mid - 1);
            node.right = convert(mid + 1, right);
            return node;
        }
    }

    [ProblemSolution("110")]
    public bool IsBalanced(TreeNode root)
    {
        var balanced = true;
        propagate(root);
        return balanced;

        int propagate(TreeNode? node)
        {
            if (node is null)
                return 0;

            var left = propagate(node.left);
            var right = propagate(node.right);
            if (Math.Abs(right - left) > 1)
                balanced = false;

            return Math.Max(left, right) + 1;
        }
    }

    [ProblemSolution("112")]
    public bool HasPathSum(TreeNode root, int targetSum)
    {
        var stack = new Stack<(TreeNode node, int sum)>();

        if (root is not null)
            stack.Push((root, 0));

        while (stack.Count > 0)
        {
            var top = stack.Pop();
            var value = top.sum + top.node.val;

            if (top.node.left is null && top.node.right is null && value == targetSum)
                return true;

            if (top.node.left is not null)
                stack.Push((top.node.left, value));
            if (top.node.right is not null)
                stack.Push((top.node.right, value));
        }

        return false;
    }

    [ProblemSolution("116")]
    public Node3? Connect(Node3? root)
    {
        if (root is null)
            return null;

        var cur = root;
        var next = root.left;

        while (next is not null)
        {
            cur.left.next = cur.right;
            if (cur.next is not null)
                cur.right.next = cur.next.left;

            cur = cur.next;
            if (cur is null)
                (cur, next) = (next, next.left);
        }

        return root;
    }

    [ProblemSolution("117")]
    public Node3? Connect2(Node3? root)
    {
        if (root is null)
            return null;

        var start = root;
        var cur = root;
        var nextcur = root.next;

        while (start is not null)
        {
            Node3? child = null;
            if (cur!.left is not null && cur.right is not null)
            {
                cur.left.next = cur.right;
                child = cur.right;
            }
            else if (cur.left is not null)
                child = cur.left;
            else if (cur.right is not null)
                child = cur.right;

            if (nextcur == cur)
                nextcur = nextcur.next;
            while (nextcur is not null && child is not null && child.next is null)
            {
                if (nextcur.left is not null)
                    child.next = nextcur.left;
                else if (nextcur.right is not null)
                    child.next = nextcur.right;
                else
                    nextcur = nextcur.next;
            }

            cur = cur.next;
            if (cur is null)
            {
                while (start is not null)
                {
                    if (start.left is not null || start.right is not null)
                    {
                        start = start.left ?? start.right;
                        break;
                    }
                    else
                        start = start.next;
                }

                cur = start;
                nextcur = start;
            }
        }

        return root;
    }

    [ProblemSolution("119")]
    public IList<int> GetRow(int rowIndex)
    {
        if (rowIndex == 0)
            return [1];

        var prev = GetRow(rowIndex - 1);
        List<int> list = [1];
        for (int i = 1; i < rowIndex; i++)
        {
            list.Add(prev[i - 1] + prev[i]);
        }

        list.Add(1);
        return list;
    }

    [ProblemSolution("129")]
    public int SumNumbers(TreeNode root)
    {
        var sum = 0;
        propagate(root, 0);
        return sum;

        void propagate(TreeNode? node, int prev)
        {
            if (node is null)
                return;

            prev *= 10;
            prev += node.val;

            if (node.left is null && node.right is null)
            {
                sum += prev;
            }
            else
            {
                propagate(node.left, prev);
                propagate(node.right, prev);
            }
        }
    }

    [ProblemSolution("133")]
    public Node2? CloneGraph(Node2? node)
    {
        if (node is null)
            return null;

        var visited = new HashSet<Node2>();
        proliferate(node);

        var newNodes = new Node2[visited.Count + 1];
        foreach (var n in visited)
            newNodes[n.val] = new Node2(n.val);

        foreach (var n in visited)
        {
            var newNode = newNodes[n.val];
            foreach (var neighbor in n.neighbors)
                newNode.neighbors.Add(newNodes[neighbor.val]);
        }

        return newNodes[1];

        void proliferate(Node2 n)
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
            while (node is not null)
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

    [ProblemSolution("165")]
    public int CompareVersion(string version1, string version2)
    {
        var revs1 = version1.Split('.').Select(f => int.Parse(f)).ToList();
        var revs2 = version2.Split('.').Select(f => int.Parse(f)).ToList();

        var maxCount = Math.Max(revs1.Count, revs2.Count);
        while (revs1.Count < maxCount)
            revs1.Add(0);
        while (revs2.Count < maxCount)
            revs2.Add(0);

        for (int i = 0; i < maxCount; i++)
        {
            if (revs1[i] < revs2[i])
                return -1;
            else if (revs1[i] > revs2[i])
                return 1;
        }

        return 0;
    }

    [ProblemSolution("169")]
    public int MajorityElement(int[] nums) => nums.GroupBy(f => f).OrderByDescending(f => f.Count()).First().Key;

    [ProblemSolution("173")]
    public class BSTIterator(TreeNode? root)
    {
        private readonly Stack<TreeNode> stack = new();

        public int Next()
        {
            while (root is not null)
            {
                stack.Push(root);
                root = root.left;
            }

            var top = stack.Pop();
            root = top.right;
            return top.val;
        }

        public bool HasNext() => stack.Count > 0 || root is not null;
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

    [ProblemSolution("198")]
    public int Rob(int[] nums)
    {
        var dp = new int[2];
        for (int i = 0; i < nums.Length; i++)
        {
            var newSum = Math.Max(dp[1], dp[0] + nums[i]);
            (dp[0], dp[1]) = (dp[1], newSum);
        }

        return dp[1];
    }
}
