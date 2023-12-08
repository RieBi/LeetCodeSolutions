using System.Text;

namespace LeetCode._0xxx;
internal class Solution06xx
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
}
