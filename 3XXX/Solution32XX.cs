namespace LeetCode.Set3XXX;
internal class Solution32XX
{
    [ProblemSolution("3203")]
    public int MinimumDiameterAfterMerge(int[][] edges1, int[][] edges2)
    {
        var (length1, diameter1) = getDist(edges1);
        var (length2, diameter2) = getDist(edges2);

        return Math.Max(length1 + length2 + 1, Math.Max(diameter1, diameter2));

        (int length, int diameter) getDist(int[][] edges)
        {
            if (edges.Length <= 1)
                return (edges.Length, edges.Length);

            var graph = new List<int>[edges.Length + 1];

            for (int i = 0; i < graph.Length; i++)
                graph[i] = [];

            for (int i = 0; i < edges.Length; i++)
            {
                var (a, b) = (edges[i][0], edges[i][1]);

                graph[a].Add(b);
                graph[b].Add(a);
            }

            var dist = new List<int>[edges.Length + 1];
            for (int i = 0; i < dist.Length; i++)
                dist[i] = new(capacity: graph[i].Count);

            var stack = new Stack<DiameterStack>();
            stack.Push(new(0, -1, 0));

            while (stack.Count > 0)
            {
                var item = stack.Peek();

                if (item.NextIndex == graph[item.Node].Count || item.Node != 0 && graph[item.Node].Count == 1)
                {
                    stack.Pop();
                    if (item.Parent != -1)
                    {
                        var distList_ = dist[item.Parent];
                        var list = graph[item.Parent];

                        if (list[distList_.Count] != item.Node)
                            distList_.Add(-1);

                        var maxDist = dist[item.Node].Count == 0 ? 0 : dist[item.Node].Max();
                        distList_.Add(maxDist + 1);
                    }
                }
                else if (graph[item.Node][item.NextIndex] == item.Parent)
                    item.NextIndex++;
                else
                {
                    stack.Push(new(graph[item.Node][item.NextIndex], item.Node, 0));
                    item.NextIndex++;
                }
            }

            var cur = 0;
            var prevMax = 0;

            while (true)
            {
                var distList = dist[cur];

                var maxInd = 0;
                for (int i = 1; i < distList.Count; i++)
                {
                    if (distList[i] > distList[maxInd])
                        maxInd = i;
                }

                var maxVal = distList[maxInd];

                for (int i = 0; i < distList.Count; i++)
                {
                    if (i != maxInd && distList[i] > prevMax)
                        prevMax = distList[i];
                }

                if (maxVal - prevMax < 2)
                    return (Math.Max(maxVal, prevMax), maxVal + prevMax);

                cur = graph[cur][maxInd];
                prevMax++;
            }
        }
    }

    private sealed class DiameterStack(int node, int parent, int nextIndex)
    {
        public int Node = node;
        public int Parent = parent;
        public int NextIndex = nextIndex;
    }

    [ProblemSolution("3217")]
    public ListNode ModifiedList(int[] nums, ListNode head)
    {
        var set = nums.ToHashSet();

        while (set.Contains(head.val))
            head = head.next!;

        var cur = head;
        while (cur is not null)
        {
            while (cur.next is not null && set.Contains(cur.next.val))
                cur.next = cur.next.next;

            cur = cur.next;
        }

        return head;
    }

    [ProblemSolution("3223")]
    public int MinimumLength(string s)
    {
        Span<int> counts = stackalloc int[26];

        for (int i = 0; i < s.Length; i++)
            counts[s[i] - 'a']++;

        var result = 0;
        for (int i = 0; i < 26; i++)
        {
            if (counts[i] == 0)
                result += 0;
            else if (counts[i] % 2 == 0)
                result += 2;
            else
                result += 1;
        }

        return result;
    }

    [ProblemSolution("3254")]
    public int[] ResultsArray(int[] nums, int k)
    {
        if (k == 1)
            return nums;

        var result = new int[nums.Length - k + 1];

        var dequeue = new (int ind, int val)[k];
        var start = 0;
        var count = 0;

        var streak = 1;
        pushEnd(0, nums[0]);

        for (int i = 1; i + 1 < k; i++)
        {
            pushEnd(i, nums[i]);

            if (nums[i] == nums[i - 1] + 1)
                streak++;
            else
                streak = 1;
        }

        for (int i = k - 1; i < nums.Length; i++)
        {
            if (peekStart().ind == i - k)
                popStart();

            while (count > 0 && peekEnd().val <= nums[i])
                popEnd();

            pushEnd(i, nums[i]);

            if (nums[i] == nums[i - 1] + 1)
                streak++;
            else
                streak = 1;

            if (streak >= k)
                result[i - k + 1] = peekStart().val;
            else
                result[i - k + 1] = -1;
        }

        return result;

        void pushEnd(int ind, int val)
        {
            dequeue[(start + count) % dequeue.Length] = (ind, val);
            count++;
        }

        (int ind, int val) peekEnd() => dequeue[(start + count - 1) % dequeue.Length];

        void popEnd() => count--;

        (int ind, int val) peekStart() => dequeue[start];

        void popStart() => (start, count) = ((start + 1) % dequeue.Length, count - 1);
    }
}