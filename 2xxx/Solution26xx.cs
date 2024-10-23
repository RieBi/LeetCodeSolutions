namespace LeetCode.Set2XXX;
internal class Solution26XX
{
    [ProblemSolution("2610")]
    public IList<IList<int>> FindMatrix(int[] nums)
    {
        var dick = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (dick.TryGetValue(num, out int count))
                dick[num] = count + 1;
            else
                dick[num] = 1;
        }

        var result = new List<IList<int>>();
        foreach (var v in dick)
        {
            for (int i = 0; i < v.Value; i++)
            {
                if (result.Count == i)
                    result.Add([]);

                result[i].Add(v.Key);
            }
        }

        return result;
    }

    [ProblemSolution("2641")]
    public TreeNode ReplaceValueInTree(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        var parents = new Queue<TreeNode>();
        parents.Enqueue(new(0, root));
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var c = queue.Count;
            var sum = queue.Sum(f => f.val);

            for (int i = 0; i < c; i++)
            {
                var last = queue.Dequeue();
                var parent = parents.Dequeue();

                var val = sum;
                if (parent.left is not null)
                    val -= parent.left.val;
                if (parent.right is not null)
                    val -= parent.right.val;

                last.val = val;

                if (i != c - 1 && parents.Peek() == parent)
                {
                    i++;
                    parents.Dequeue();
                    var sibling = queue.Dequeue();
                    sibling.val = val;

                    if (sibling.left is not null)
                    {
                        queue.Enqueue(sibling.left);
                        parents.Enqueue(sibling);
                    }

                    if (sibling.right is not null)
                    {
                        queue.Enqueue(sibling.right);
                        parents.Enqueue(sibling);
                    }
                }

                if (last.left is not null)
                {
                    queue.Enqueue(last.left);
                    parents.Enqueue(last);
                }

                if (last.right is not null)
                {
                    queue.Enqueue(last.right);
                    parents.Enqueue(last);
                }
            }
        }

        return root;
    }

    [ProblemSolution("2678")]
    public int CountSeniors(string[] details)
    {
        return details.Count(f => f[^4] > '6' || f[^4] == '6' && f[^3] != '0');
    }

    [ProblemSolution("2696")]
    public int MinLength(string s)
    {
        var reduced = 0;
        var stack = new Stack<char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] is 'A' or 'C')
                stack.Push(s[i]);
            else if (stack.Count > 0 && (s[i] == 'B' && stack.Peek() == 'A' || s[i] == 'D' && stack.Peek() == 'C'))
            {
                stack.Pop();
                reduced += 2;
            }
            else
                stack.Clear();
        }

        return s.Length - reduced;
    }

    [ProblemSolution("2699")]
    public int[][] ModifiedGraphEdges(int n, int[][] edges, int source, int destination, int target)
    {
        var graph = new List<(int other, int weight, int otherInd)>[n];
        for (int i = 0; i < n; i++)
            graph[i] = [];

        var negativeEdges = new Dictionary<(int a, int b), int>();

        for (int i = 0; i < edges.Length; i++)
        {
            var (a, b, w) = (edges[i][0], edges[i][1], edges[i][2]);
            if (a > b)
                (a, b) = (b, a);

            if (w == -1)
            {
                negativeEdges[(a, b)] = i;
                continue;
            }

            graph[a].Add((b, w, graph[b].Count));
            graph[b].Add((a, w, graph[a].Count - 1));
        }

        var dists = new int[n];
        var dists2 = new int[n];

        var set = new SortedSet<(int dist, int node)>();

        foreach (var (a, b) in negativeEdges.Keys)
        {
            graph[a].Add((b, -1, graph[b].Count));
            graph[b].Add((a, -1, graph[a].Count - 1));
        }

        dijkstraGraph(1);

        var minDist = dists[destination];
        if (minDist > target)
            return [];
        else if (minDist == target)
        {
            foreach (var ind in negativeEdges.Values)
                edges[ind][2] = 1;

            return edges;
        }

        dijkstraGraph(2);

        if (dists2[destination] < target)
            return [];

        return edges;

        void dijkstraGraph(int pass)
        {
            var curDists = pass == 1 ? dists : dists2;

            for (int i = 0; i < n; i++)
                curDists[i] = int.MaxValue;
            curDists[source] = 0;

            set.Add((0, source));

            while (set.Count > 0)
            {
                var last = set.Min;
                set.Remove(last);

                var others = graph[last.node];
                for (int i = 0; i < others.Count; i++)
                {
                    var other = others[i].other;

                    if (pass == 2)
                    {
                        var a = last.node;
                        var b = other;
                        if (a > b)
                            (a, b) = (b, a);

                        if (others[i].weight == -1 && negativeEdges.TryGetValue((a, b), out var value))
                        {
                            var newW = target - (dists[destination] - dists[other]) - curDists[last.node];

                            if (newW < 1)
                            {
                                edges[value][2] = target;
                            }
                            else
                            {
                                var uVal = graph[last.node][i];
                                var vVal = graph[uVal.other][uVal.otherInd];

                                uVal.weight = newW;
                                vVal.weight = newW;

                                graph[last.node][i] = uVal;
                                graph[uVal.other][uVal.otherInd] = vVal;
                                edges[value][2] = newW;
                            }
                        }
                    }

                    var newDist = last.dist + (others[i].weight == -1 ? 1 : others[i].weight);
                    if (newDist < curDists[other])
                    {
                        set.Remove((curDists[other], other));
                        set.Add((newDist, other));
                        curDists[other] = newDist;
                    }
                }
            }
        }
    }
}
