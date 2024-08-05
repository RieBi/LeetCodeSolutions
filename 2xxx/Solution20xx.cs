using System.Text;

namespace LeetCode.Set2XXX;
internal class Solution20XX
{
    [ProblemSolution("2000")]
    public string ReversePrefix(string word, char ch)
    {
        var ind = word.IndexOf(ch);
        if (ind == -1)
            return word;

        return new string(word[..(ind + 1)].Reverse().ToArray()) + word[(ind + 1)..];
    }

    [ProblemSolution("2037")]
    public int MinMovesToSeat(int[] seats, int[] students)
    {
        Array.Sort(seats);
        Array.Sort(students);
        return Enumerable.Range(0, seats.Length)
            .Sum(f => Math.Abs(seats[f] - students[f]));
    }

    [ProblemSolution("2045")]
    public int SecondMinimum(int n, int[][] edges, int time, int change)
    {
        var nodes = new (List<int> others, List<int> dist)[n];
        for (int i = 0; i < n; i++)
            nodes[i] = ([], []);

        for (int i = 0; i < edges.Length; i++)
        {
            var from = edges[i][0] - 1;
            var to = edges[i][1] - 1;

            nodes[from].others.Add(to);
            nodes[to].others.Add(from);
        }

        nodes[0].dist.Add(0);
        var queue = new Queue<int>();
        queue.Enqueue(0);

        var newQueue = new Queue<int>();
        var set = new HashSet<int>();

        while (queue.Count > 0)
        {
            var c = queue.Count;
            for (int i = 0; i < c; i++)
            {
                var last = queue.Dequeue();
                foreach (var other in nodes[last].others)
                {
                    if (nodes[other].dist.Count == 0)
                    {
                        nodes[other].dist.Add(nodes[last].dist[0] + 1);
                        queue.Enqueue(other);
                    }
                    else
                    {
                        var otherDist = nodes[other].dist[0];
                        var newDist = nodes[last].dist[0] + 1;
                        var diff = newDist - otherDist;
                        if (diff % 2 == 1)
                        {
                            if (nodes[other].dist.Count == 1)
                                nodes[other].dist.Add(newDist);
                            else
                                nodes[other].dist[1] = Math.Min(nodes[other].dist[1], newDist);

                            if (!set.Contains(other))
                            {
                                newQueue.Enqueue(other);
                                set.Add(other);
                            }
                        }
                    }
                }
            }
        }

        while (newQueue.Count > 0)
        {
            var c = newQueue.Count;
            for (int i = 0; i < c; i++)
            {
                var last = newQueue.Dequeue();
                set.Remove(last);

                foreach (var other in nodes[last].others)
                {
                    var otherDistList = nodes[other].dist;
                    var otherDist = otherDistList.Count == 1 ? otherDistList[0] : otherDistList[1];
                    var newDist = nodes[last].dist[1] + 1;

                    var diff = newDist - otherDist;
                    var improved = otherDistList.Count == 1 || newDist < otherDist;
                    if (improved && diff % 2 == 1)
                    {
                        if (otherDistList.Count == 1)
                            otherDistList.Add(newDist);
                        else
                            otherDistList[1] = newDist;

                        if (!set.Contains(other))
                        {
                            newQueue.Enqueue(other);
                            set.Add(other);
                        }
                    }
                }
            }
        }

        var lastDist = nodes[n - 1].dist;
        var guaranteed = calculateTime(lastDist[0]);

        var secondDist = lastDist.Count == 1 ? int.MaxValue : lastDist[1];
        var next = Math.Min(lastDist[0] + 2, secondDist);
        var nextTime = calculateTime(next);

        return nextTime;

        int calculateTime(int steps)
        {
            var cur = 0;
            var curTime = 0;

            while (cur < steps)
            {
                var cycle = curTime % (change * 2);
                if (cycle >= change)
                {
                    var diff = change * 2 - cycle;
                    curTime += diff;
                }

                cur++;
                curTime += time;
            }

            return curTime;
        }
    }

    [ProblemSolution("2053")]
    public string KthDistinct(string[] arr, int k)
    {
        return arr
            .GroupBy(f => f)
            .Where(f => f.Count() == 1)
            .Select(f => f.Key)
            .Skip(k - 1)
            .FirstOrDefault(string.Empty);
    }

    [ProblemSolution("2058")]
    public int[] NodesBetweenCriticalPoints(ListNode head)
    {
        var firstInd = -1;
        var lastInd = -1;

        var minDist = int.MaxValue;

        var ind = 0;
        var node = head;
        while (node.next is not null && node.next.next is not null)
        {
            var prevVal = node.val;
            node = node.next;
            ind++;
            var curVal = node.val;
            var nextVal = node.next!.val;

            if ((curVal > prevVal && curVal > nextVal) || (curVal < prevVal && curVal < nextVal))
            {
                if (firstInd == -1)
                    firstInd = ind;

                if (lastInd != -1)
                    minDist = Math.Min(minDist, ind - lastInd);
                lastInd = ind;
            }
        }

        if (firstInd == -1 || firstInd == lastInd)
            return [-1, -1];
        else
            return [minDist, lastInd - firstInd];
    }

    [ProblemSolution("2092")]
    public IList<int> FindAllPeople(int n, int[][] meetings, int firstPerson)
    {
        var secrets = new bool[n];
        secrets[0] = true;
        secrets[firstPerson] = true;

        var meets = meetings.GroupBy(f => f[2]).OrderBy(f => f.Key);
        foreach (var meet in meets)
        {
            var dick = new Dictionary<int, List<int>>();
            foreach (var rendezvous in meet)
            {
                if (dick.TryGetValue(rendezvous[0], out var val1))
                    val1.Add(rendezvous[1]);
                else
                    dick[rendezvous[0]] = [rendezvous[1]];

                if (dick.TryGetValue(rendezvous[1], out var val2))
                    val2.Add(rendezvous[0]);
                else
                    dick[rendezvous[1]] = [rendezvous[0]];
            }

            var seen = new HashSet<int>();
            foreach (var v in dick)
            {
                if (seen.Contains(v.Key) || !secrets[v.Key])
                    continue;

                seen.Add(v.Key);
                var queue = new Queue<int>();
                queue.Enqueue(v.Key);
                while (queue.Count > 0)
                {
                    var c = queue.Count;
                    for (int i = 0; i < c; i++)
                    {
                        var top = queue.Dequeue();
                        foreach (var other in dick[top])
                        {
                            if (seen.Contains(other))
                                continue;

                            seen.Add(other);
                            secrets[other] = true;
                            queue.Enqueue(other);
                        }
                    }
                }
            }
        }

        var result = new List<int>();
        for (int i = 0; i < n; i++)
            if (secrets[i])
                result.Add(i);

        return result;
    }

    [ProblemSolution("2096")]
    public string GetDirections(TreeNode root, int startValue, int destValue)
    {
        var node = root;
        var stack = new Stack<TreeNode>();
        var paths = new string[2];

        while (node is not null || stack.Count > 0)
        {
            if (node is not null)
            {
                if (node.val == startValue || node.val == destValue)
                {
                    var result = getPath(stack.ToList(), node.val);
                    if (node.val == startValue)
                        paths[0] = result;
                    else
                        paths[1] = result;
                }

                stack.Push(node);
                node = node.left;
            }
            else
            {
                node = stack.Pop().right;
            }
        }

        var minLen = paths.Min(f => f.Length);
        var ind = 0;
        while (ind < minLen && paths[0][ind] == paths[1][ind])
            ind++;

        return new string('U', paths[0].Length - ind) + paths[1][ind..];

        string getPath(List<TreeNode> list, int target)
        {
            var cur = root;
            var str = new StringBuilder();
            while (cur!.val != target)
            {
                if (list.Count > 0 && list[^1] == cur)
                {
                    cur = cur.left;
                    list.RemoveAt(list.Count - 1);
                    str.Append("L");
                }
                else
                {
                    cur = cur.right;
                    str.Append("R");
                }
            }

            return str.ToString();
        }
    }
}
