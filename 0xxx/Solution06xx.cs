using System.Text;

namespace LeetCode.Set0xxx;
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
}
