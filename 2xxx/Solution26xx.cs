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

    [ProblemSolution("2678")]
    public int CountSeniors(string[] details)
    {
        return details.Count(f => f[^4] > '6' || f[^4] == '6' && f[^3] != '0');
    }

    [ProblemSolution("2699")]
    //public int[][] ModifiedGraphEdges(int n, int[][] edges, int source, int destination, int target)
    //{
    //    var graph = new List<(int other, int weight)>[n];
    //    for (int i = 0; i < n; i++)
    //        graph[i] = [];

    //    var negativeEdges = new List<int>();

    //    for (int i = 0; i < edges.Length; i++)
    //    {
    //        var (a, b, w) = (edges[i][0], edges[i][1], edges[i][2]);

    //        if (w == -1)
    //        {
    //            negativeEdges.Add(i);
    //            continue;
    //        }

    //        graph[a].Add((b, w));
    //        graph[b].Add((a, w));
    //    }

    //    var dists = new int[n];
    //    var set = new SortedSet<(int dist, int node)>();

    //    dijkstraGraph();

    //    var minDist = dists[destination];
    //    if (minDist < target)
    //        return [];
    //    else if (minDist == target)
    //    {
    //        foreach (var edge in negativeEdges)
    //            edges[edge][2] = 2_000_000_000;

    //        return edges;
    //    }

    //    foreach (var edge in negativeEdges)
    //    {
    //        var (a, b) = (edges[edge][0], edges[edge][1]);
    //        graph[a].Add((b, 0));
    //        graph[b].Add((a, 0));
    //    }

    //    dijkstraGraph();

    //    minDist = dists[destination];
    //    if (minDist >= target)
    //        return [];

    //    var dp = new (int minNegative, int from)[n];
    //    for (int i = 0; i < dp.Length; i++)
    //        dp[i] = (-1, -1);
    //    dp[source] = (0, -1);

    //    var minNegatives = propagate(destination);
    //    var maxNegatives = target - minDist;

    //    if (minNegatives > maxNegatives)
    //        return [];

    //    var mapping = new Dictionary<(int a, int b), int>();
    //    for (int i = 0; i < negativeEdges.Count; i++)
    //    {
    //        var ind = negativeEdges[i];
    //        var (a, b) = (edges[ind][0], edges[ind][1]);
    //        if (a > b)
    //            (a, b) = (b, a);

    //        mapping[(a, b)] = ind;
    //    }

    //    var cur = destination;
    //    while (cur != source)
    //    {
    //        var saved = dp[cur];
    //        var others = graph[cur];

    //        if (others[saved.from].weight == 0)
    //        {
    //            var setVal = 1;
    //            if (saved.minNegative == 1)
    //                setVal = maxNegatives - minNegatives + 1;

    //            var a = cur;
    //            var b = others[saved.from].other;

    //            if (a > b)
    //                (a, b) = (b, a);

    //            edges[mapping[(a, b)]][2] = setVal;
    //        }

    //        cur = graph[cur][saved.from].other;
    //    }

    //    for (int i = 0; i < negativeEdges.Count; i++)
    //    {
    //        var ind = negativeEdges[i];
    //        if (edges[ind][2] == -1)
    //            edges[ind][2] = 2_000_000_000;
    //    }

    //    return edges;

    //    int propagate(int node)
    //    {
    //        if (dp[node].minNegative >= 0)
    //            return dp[node].minNegative;

    //        var others = graph[node];
    //        var minNegative = int.MaxValue;
    //        var from = -1;
    //        var myDist = dists[node];
    //        dists[node] = 1_000_000_000;

    //        for (int i = 0; i < others.Count; i++)
    //        {
    //            if (dists[others[i].other] + others[i].weight == myDist)
    //            {
    //                var val = propagate(others[i].other);
    //                if (others[i].weight == 0)
    //                    val += 1;

    //                if (val < minNegative)
    //                {
    //                    minNegative = val;
    //                    from = i;
    //                }
    //            }
    //        }

    //        dists[node] = myDist;
    //        dp[node] = (minNegative, from);
    //        return minNegative;
    //    }

    //    void dijkstraGraph()
    //    {
    //        for (int i = 0; i < n; i++)
    //            dists[i] = int.MaxValue;
    //        dists[source] = 0;

    //        set.Add((0, source));

    //        while (set.Count > 0)
    //        {
    //            var last = set.Min;
    //            set.Remove(last);

    //            var others = graph[last.node];
    //            for (int i = 0; i < others.Count; i++)
    //            {
    //                var other = others[i].other;
    //                var newDist = last.dist + others[i].weight;
    //                if (newDist < dists[other])
    //                {
    //                    set.Remove((dists[other], other));
    //                    set.Add((newDist, other));
    //                    dists[other] = newDist;
    //                }
    //            }
    //        }
    //    }
    //}
    public int[][] ModifiedGraphEdges(int n, int[][] edges, int source, int destination, int target)
    {
        var adjacencyList = new List<(int, int)>[n];
        for (int i = 0; i < n; i++)
        {
            adjacencyList[i] = new List<(int, int)>();
        }
        for (int i = 0; i < edges.Length; i++)
        {
            int nodeA = edges[i][0], nodeB = edges[i][1];
            adjacencyList[nodeA].Add((nodeB, i));
            adjacencyList[nodeB].Add((nodeA, i));
        }

        var distances = new int[n, 2];
        for (int i = 0; i < n; i++)
        {
            distances[i, 0] = distances[i, 1] = i == source ? 0 : int.MaxValue;
        }

        RunDijkstra(adjacencyList, edges, distances, source, 0, 0);
        int difference = target - distances[destination, 0];
        if (difference < 0) return new int[0][];

        RunDijkstra(adjacencyList, edges, distances, source, difference, 1);
        if (distances[destination, 1] < target) return new int[0][];

        foreach (var edge in edges)
        {
            if (edge[2] == -1) edge[2] = 1;
        }
        return edges;

        static void RunDijkstra(List<(int, int)>[] adjacencyList, int[][] edges, int[,] distances, int source, int difference, int run)
        {
            int n = adjacencyList.Length;
            var priorityQueue = new SortedSet<(int, int)>(Comparer<(int, int)>.Create((a, b) => a.Item2 != b.Item2 ? a.Item2.CompareTo(b.Item2) : a.Item1.CompareTo(b.Item1)));
            priorityQueue.Add((source, 0));
            distances[source, run] = 0;

            while (priorityQueue.Count > 0)
            {
                var (currentNode, currentDistance) = priorityQueue.Min;
                priorityQueue.Remove((currentNode, currentDistance));

                if (currentDistance > distances[currentNode, run]) continue;

                foreach (var (nextNode, edgeIndex) in adjacencyList[currentNode])
                {
                    int weight = edges[edgeIndex][2];
                    if (weight == -1) weight = 1;

                    if (run == 1 && edges[edgeIndex][2] == -1)
                    {
                        int newWeight = difference + distances[nextNode, 0] - distances[currentNode, 1];
                        if (newWeight > weight)
                        {
                            edges[edgeIndex][2] = weight = newWeight;
                        }
                    }

                    if (distances[nextNode, run] > distances[currentNode, run] + weight)
                    {
                        priorityQueue.Remove((nextNode, distances[nextNode, run]));
                        distances[nextNode, run] = distances[currentNode, run] + weight;
                        priorityQueue.Add((nextNode, distances[nextNode, run]));
                    }
                }
            }
        }
    }
}
