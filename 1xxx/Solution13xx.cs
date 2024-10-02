namespace LeetCode.Set1XXX;
internal class Solution13XX
{
    [ProblemSolution("1310")]
    public int[] XorQueries(int[] arr, int[][] queries)
    {
        var xors = new int[arr.Length + 1];
        for (int i = 1; i < xors.Length; i++)
            xors[i] = xors[i - 1] ^ arr[i - 1];

        var result = new int[queries.Length];
        for (int i = 0; i < result.Length; i++)
            result[i] = xors[queries[i][1] + 1] ^ xors[queries[i][0]];

        return result;
    }

    [ProblemSolution("1331")]
    public int[] ArrayRankTransform(int[] arr)
    {
        var rank = 0;
        _ = Enumerable.Range(0, arr.Length)
            .OrderBy(f => arr[f])
            .Aggregate(int.MinValue, (a, b) =>
            {
                if (a != arr[b])
                    rank++;

                var prev = arr[b];
                arr[b] = rank;

                return prev;
            });

        return arr;
    }

    [ProblemSolution("1334")]
    public int FindTheCity(int n, int[][] edges, int distanceThreshold)
    {
        var resCounts = new int[n];
        var graph = new List<(int other, int dist)>[n];

        for (int i = 0; i < n; i++)
            graph[i] = [];

        for (int i = 0; i < edges.Length; i++)
        {
            var (from, to, dist) = (edges[i][0], edges[i][1], edges[i][2]);
            graph[from].Add((to, dist));
            graph[to].Add((from, dist));
        }

        for (int i = 0; i < n; i++)
        {
            var visited = new bool[n];
            var distances = new int[n];
            var valid = 0;
            var queue = new SortedSet<(int dist, int node)>();

            for (int j = 0; j < n; j++)
                distances[j] = int.MaxValue;

            queue.Add((0, i));
            visited[i] = true;
            distances[i] = 0;

            while (queue.Count > 0)
            {
                var (curDist, last) = queue.Min;
                queue.Remove((curDist, last));

                visited[last] = true;
                if (curDist > distanceThreshold)
                    break;
                else
                    valid++;

                foreach (var other in graph[last])
                {
                    var newDist = curDist + other.dist;
                    if (!visited[other.other] && newDist < distances[other.other])
                    {
                        queue.Remove((distances[other.other], other.other));
                        queue.Add((newDist, other.other));
                        distances[other.other] = newDist;
                    }
                }
            }

            resCounts[i] = valid;
        }

        var min = resCounts.Min();
        return Array.LastIndexOf(resCounts, min);
    }

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

    [ProblemSolution("1367")]
    public bool IsSubPath(ListNode head, TreeNode root)
    {
        return traverse(head, root);

        bool traverse(ListNode node, TreeNode? treeNode)
        {
            if (treeNode is null)
                return false;

            if (node.val == treeNode.val)
            {
                if (node.next is null)
                    return true;

                if (traverse(node.next, treeNode.left) || traverse(node.next, treeNode.right))
                    return true;
            }

            return node == head && (traverse(head, treeNode.left) || traverse(head, treeNode.right));
        }
    }

    [ProblemSolution("1380")]
    public IList<int> LuckyNumbers(int[][] matrix)
    {
        var minRows = new int[matrix.Length];
        var maxCols = new int[matrix[0].Length];

        for (int i = 0; i < matrix.Length; i++)
        {
            var minVal = matrix[i][0];
            for (int j = 1; j < matrix[i].Length; j++)
                minVal = Math.Min(minVal, matrix[i][j]);

            minRows[i] = minVal;
        }

        for (int j = 0; j < matrix[0].Length; j++)
        {
            var maxVal = matrix[0][j];
            for (int i = 1; i < matrix.Length; i++)
                maxVal = Math.Max(maxVal, matrix[i][j]);

            maxCols[j] = maxVal;
        }

        IList<int> result = [];

        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                var val = matrix[i][j];
                if (val == minRows[i] && val == maxCols[j])
                    result.Add(val);
            }
        }

        return result;
    }

    [ProblemSolution("1381")]
    public class CustomStack(int maxSize)
    {
        private readonly List<(int val, int cumulative)> _list = new(maxSize);

        public void Push(int x)
        {
            if (_list.Count == _list.Capacity)
                return;

            _list.Add((x, 0));
        }

        public int Pop()
        {
            if (_list.Count == 0)
                return -1;

            var top = _list[^1];
            _list.RemoveAt(_list.Count - 1);

            if (_list.Count > 0)
                _list[^1] = (_list[^1].val, _list[^1].cumulative + top.cumulative);

            return top.val + top.cumulative;
        }

        public void Increment(int k, int val)
        {
            if (_list.Count == 0)
                return;

            k = Math.Min(k, _list.Count);
            _list[k - 1] = (_list[k - 1].val, _list[k - 1].cumulative + val);
        }
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

    [ProblemSolution("1395")]
    public int NumTeams(int[] rating)
    {
        var less = new int[rating.Length];
        var more = new int[rating.Length];

        for (int i = 0; i < rating.Length; i++)
        {
            var num = rating[i];
            for (int j = i + 1; j < rating.Length; j++)
            {
                if (rating[j] < num)
                    less[i]++;
                else if (rating[j] > num)
                    more[i]++;
            }
        }

        var result = 0;
        for (int i = 0; i < rating.Length; i++)
        {
            for (int j = i + 1; j < rating.Length; j++)
            {
                if (rating[j] < rating[i])
                    result += less[j];

                if (rating[j] > rating[i])
                    result += more[j];
            }
        }

        return result;
    }
}
