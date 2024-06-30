namespace LeetCode.Set1XXX;
internal class Solution13XX
{
    [ProblemSolution("1335")]
    public int MinDifficulty(int[] jobDifficulty, int d)
    {
        if (d > jobDifficulty.Length)
            return -1;

        var big = int.MaxValue / 4;
        var mem = new Dictionary<(int, int, int), int>();

        return getMax(0, 1, -1);

        int getMax(int index, int day, int curMax)
        {
            if (index >= jobDifficulty.Length && day > d)
                return 0;
            else if (index >= jobDifficulty.Length || day > d)
                return big;

            if (mem.TryGetValue((index, day, curMax), out int value))
                return value;

            var max = Math.Max(jobDifficulty[index], curMax);

            // scenario end
            var scenarioEnd = max + getMax(index + 1, day + 1, -1);

            // scenario continue
            var scenarioContinue = getMax(index + 1, day, max);

            var result = Math.Min(scenarioEnd, scenarioContinue);
            mem[(index, day, curMax)] = result;
            return result;
        }
    }

    [ProblemSolution("1347")]
    public int MinSteps(string s, string t)
    {
        var dickS = s.GroupBy(f => f).ToDictionary(f => f.Key, f => f.Count());
        var dickT = t.GroupBy(f => f).ToDictionary(f => f.Key, f => f.Count());

        var steps = 0;
        foreach (var v in dickS)
        {
            if (!dickT.TryGetValue(v.Key, out int value))
                steps += v.Value;
            else if (v.Value > value)
                steps += v.Value - value;
        }

        return steps;
    }

    [ProblemSolution("1382")]
    public TreeNode BalanceBST(TreeNode root)
    {
        var list = new List<int>();

        var initial = root;
        var node = root;
        var stack = new Stack<TreeNode>();
        while (stack.Count > 0 || node != null)
        {
            while (node is not null)
            {
                stack.Push(node);
                node = node.left;
            }

            var last = stack.Pop();
            list.Add(last.val);
            node = last.right;
        }

        return getNode(0, list.Count - 1);

        TreeNode? getNode(int l, int r)
        {
            if (l > r || l < 0 || r >= list.Count)
                return null;

            var mid = (l + r) / 2;
            var resultRoot = new TreeNode(list[mid]);
            resultRoot.left = getNode(l, mid - 1);
            resultRoot.right = getNode(mid + 1, r);
            return resultRoot;
        }
    }
}
