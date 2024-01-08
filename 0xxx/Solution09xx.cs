using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode._0xxx;
internal class Solution09xx
{
    [ProblemSolution("938")]
    public int RangeSumBST(TreeNode root, int low, int high)
    {
        var sum = 0;
        propagate(root);

        void propagate(TreeNode? node)
        {
            if (node == null)
                return;

            if (node.val >= low && node.val <= high)
                sum += node.val;

            propagate(node.left);
            propagate(node.right);
        }

        return sum;
    }
}
