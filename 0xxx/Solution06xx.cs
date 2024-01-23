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
}
