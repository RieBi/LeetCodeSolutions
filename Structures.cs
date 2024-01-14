using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode;

public class Node
{
    public int val;
    public Node left;
    public Node right;
    public Node next;

    public Node() { }

    public Node(int _val)
    {
        val = _val;
    }

    public Node(int _val, Node _left, Node _right, Node _next)
    {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}

public class Node2
{
    public int val;
    public IList<Node2> neighbors;

    public Node2()
    {
        val = 0;
        neighbors = new List<Node2>();
    }

    public Node2(int _val)
    {
        val = _val;
        neighbors = new List<Node2>();
    }

    public Node2(int _val, List<Node2> _neighbors)
    {
        val = _val;
        neighbors = _neighbors;
    }
}

public class ListNode
{
    public int val;
    public ListNode? next;
    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }
}

public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;
    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}
