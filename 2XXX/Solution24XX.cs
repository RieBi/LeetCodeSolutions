using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace LeetCode.Set2XXX;
internal class Solution24XX
{
    [ProblemSolution("2402")]
    public int MostBooked(int n, int[][] meetings)
    {
        Array.Sort(meetings, (a, b) => a[0].CompareTo(b[0]));

        var queue = new PriorityQueue<(long freeStart, int room), (long freeStart, int room)>();
        
        for (var i = 0; i < n; i++)
            queue.Enqueue((0, i), (0, i));

        var rooms = new int[n];

        foreach (var meeting in meetings)
        {
            var (start, end) = (meeting[0], meeting[1]);

            var vacantRoom = queue.Dequeue();

            while (vacantRoom.freeStart < start)
            {
                var el = (start, vacantRoom.room);
                queue.Enqueue(el, el);
                vacantRoom = queue.Dequeue();
            }
            
            rooms[vacantRoom.room]++;
            var newEl = (vacantRoom.freeStart + end - start, vacantRoom.room);
            
            queue.Enqueue(newEl, newEl);
        }

        var index = 0;
        var max = rooms[0];

        for (var i = 1; i < n; i++)
        {
            if (rooms[i] > max)
            {
                max = rooms[i];
                index = i;
            }
        }

        return index;
    }

    [ProblemSolution("2416")]
    public int[] SumPrefixScores(string[] words)
    {
        var root = new PrefixNode();
        for (int i = 0; i < words.Length; i++)
        {
            var word = words[i];
            var node = root;
            for (int j = 0; j < word.Length; j++)
                node = node.Move(word[j]);

            node.Score++;
        }

        root.CalculateScore();
        var result = new int[words.Length];
        for (int i = 0; i < words.Length; i++)
        {
            var sum = 0;
            var node = root;
            var word = words[i];
            for (int j = 0; j < word.Length; j++)
            {
                node = node.Move(word[j]);
                sum += node.Score;
            }

            result[i] = sum;
        }

        return result;
    }

    [ProblemSolution("2416")]
    private sealed class PrefixNode()
    {
        public Dictionary<char, PrefixNode> Children { get; set; } = [];
        public int Score { get; set; }

        public PrefixNode Move(char ch)
        {
            if (Children.TryGetValue(ch, out var value))
                return value;
            else
            {
                var node = new PrefixNode();
                Children[ch] = node;
                return node;
            }
        }

        public int CalculateScore()
        {
            foreach (var child in Children.Values)
                Score += child.CalculateScore();

            return Score;
        }
    }

    [ProblemSolution("2418")]
    public string[] SortPeople(string[] names, int[] heights)
    {
        Array.Sort(heights, names);
        Array.Reverse(names);

        return names;
    }

    [ProblemSolution("2419")]
    public int LongestSubarray(int[] nums)
    {
        var row = 0;
        var maxRow = 0;
        var max = nums[0];
        
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == max)
                row++;
            else if (nums[i] > max)
                (row, max, maxRow) = (1, nums[i], 1);
            else
                row = 0;

            maxRow = Math.Max(maxRow, row);
        }

        return maxRow;
    }

    [ProblemSolution("2425")]
    public int XorAllNums(int[] nums1, int[] nums2)
    {
        var result = 0;

        if (nums2.Length % 2 == 1)
        {
            for (int i = 0; i < nums1.Length; i++)
                result ^= nums1[i];
        }

        if (nums1.Length % 2 == 1)
        {
            for (int i = 0; i < nums2.Length; i++)
                result ^= nums2[i];
        }

        return result;
    }

    [ProblemSolution("2429")]
    public int MinimizeXor(int num1, int num2)
    {
        var goal = BitOperations.PopCount((uint)num2);
        var cur = BitOperations.PopCount((uint)num1);

        if (cur > goal)
        {
            while (cur > goal)
            {
                num1 ^= num1 & (-num1);
                cur--;
            }
        }
        else if (cur < goal)
        {
            var mask = 1;

            while (cur < goal)
            {
                if ((mask & num1) == 0)
                {
                    num1 ^= mask;
                    cur++;
                }

                mask <<= 1;
            }
        }

        return num1;
    }

    [ProblemSolution("2441")]
    public int FindMaxK(int[] nums)
    {
        var negatives = nums.Where(f => f < 0).ToHashSet();
        return nums.Where(f => f > 0).Aggregate(-1, (a, b) => negatives.Contains(-b) ? Math.Max(a, b) : a);
    }

    [ProblemSolution("2458")]
    public int[] TreeQueries(TreeNode root, int[] queries)
    {
        var maxHeights = new List<int>();
        propagate(root, 0);

        var results = new int[queries.Length];

        var existing = new List<(int prev, int actual)>();
        var dict = new Dictionary<int, int>();
        for (int i = 0; i < queries.Length; i++)
        {
            if (dict.TryGetValue(queries[i], out var value))
                existing.Add((value, i));
            else
                dict[queries[i]] = i;
        }

        calculateResults(root, 0, 0);

        for (int i = 0; i < existing.Count; i++)
            results[existing[i].actual] = results[existing[i].prev];

        return results;

        void calculateResults(TreeNode? node, int height, int max)
        {
            if (node is null)
                return;

            if (dict.TryGetValue(node.val, out var index))
                results[index] = Math.Max(max, height - 1);

            calculateResults(node.left, height + 1, node.right is null ? max : Math.Max(maxHeights[node.right.val], max));
            calculateResults(node.right, height + 1, node.left is null ? max : Math.Max(maxHeights[node.left.val], max));
        }

        int propagate(TreeNode? node, int height)
        {
            if (node is null)
                return height - 1;

            var val = Math.Max(propagate(node.left, height + 1), propagate(node.right, height + 1));
            while (maxHeights.Count <= node.val)
                maxHeights.Add(0);

            maxHeights[node.val] = val;
            return val;
        }
    }

    [ProblemSolution("2463")]
    public long MinimumTotalDistance(IList<int> robot, int[][] factory)
    {
        if (robot is List<int> robotList)
            robotList.Sort();

        Array.Sort(factory, (a, b) => a[0].CompareTo(b[0]));

        var prev = new long[robot.Count];
        var cur = new long[robot.Count];
        for (int i = 0; i < robot.Count; i++)
            (prev[i], cur[i]) = (long.MaxValue / 2, long.MaxValue / 2);

        var factorySum = 0;
        for (int i = 0; i < factory.Length; i++)
        {
            factorySum += factory[i][1];
            var limit = Math.Min(robot.Count, factorySum);

            for (int j = 0; j < limit; j++)
            {
                var minDist = prev[j];
                var curDist = 0L;

                var min = Math.Max(-1, j - factory[i][1]);
                for (int k = j; k > min; k--)
                {
                    curDist += Math.Abs((long)robot[k] - factory[i][0]);

                    if (k == 0 || prev[k - 1] != long.MaxValue / 2)
                        minDist = Math.Min(minDist, (k == 0 ? 0 : prev[k - 1]) + curDist);
                }

                cur[j] = minDist;
            }

            for (int j = limit; j < robot.Count; j++)
                cur[j] = long.MaxValue / 2;

            (prev, cur) = (cur, prev);
            for (int ind = 0; ind < robot.Count; ind++)
                cur[ind] = long.MaxValue / 2;
        }

        return prev[^1];
    }

    [ProblemSolution("2482")]
    public int[][] OnesMinusZeros(int[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        var onesRow = new int[n];
        var zeroesRow = new int[n];
        var onesCol = new int[m];
        var zeroesCol = new int[m];

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == 0)
                    zeroesRow[j]++;
                else
                    onesRow[j]++;
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (grid[j][i] == 0)
                    zeroesCol[j]++;
                else
                    onesCol[j]++;
            }
        }

        var diff = new int[m][];
        for (int i = 0; i < m; i++)
        {
            diff[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                diff[i][j] = calculateDiffVal(i, j);
            }
        }

        return diff;

        int calculateDiffVal(int j, int i) => onesRow[i] + onesCol[j] - zeroesRow[i] - zeroesCol[j];
    }
    
    [ProblemSolution("2483")]
    public int BestClosingTime(string customers)
    {
        var penalty = customers.Length - customers.Count(ch => ch == 'Y');

        var close = 0;
        var best = penalty;

        for (var i = 0; i < customers.Length; i++)
        {
            if (customers[i] == 'Y')
                penalty--;
            else
                penalty++;

            if (penalty < best)
            {
                close = i + 1;
                best = penalty;
            }
        }

        return close;
    }

    [ProblemSolution("2485")]
    public int PivotInteger(int n)
    {
        var total = (n * (n + 1)) / 2;
        var cur = 0;
        for (int i = 1; i <= n; i++)
        {
            cur += i;
            total -= i - 1;
            if (cur == total)
                return i;
        }

        return -1;
    }

    [ProblemSolution("2487")]
    public ListNode RemoveNodes(ListNode head)
    {
        var stack = new Stack<ListNode>();
        var cur = head;
        
        while (cur != null)
        {
            while (stack.Count > 0 && stack.Peek().val < cur.val)
                stack.Pop();

            stack.Push(cur);
            cur = cur.next;
        }

        var list = stack.Reverse().ToList();
        head = list[0];
        cur = head;
        for (int i = 1; i < list.Count; i++)
        {
            cur.next = list[i];
            cur = cur.next;
        }

        return head;
    }

    [ProblemSolution("2490")]
    public bool IsCircularSentence(string sentence)
    {
        if (sentence[0] != sentence[^1])
            return false;

        for (int i = 0; i < sentence.Length; i++)
        {
            if (sentence[i] == ' ' && sentence[i - 1] != sentence[i + 1])
                return false;
        }

        return true;
    }

    [ProblemSolution("2491")]
    public long DividePlayers(int[] skill)
    {
        Array.Sort(skill);
        long chemistry = skill[0] * skill[^1];
        var score = skill[0] + skill[^1];

        for (int i = 1; i < skill.Length / 2; i++)
        {
            if (skill[i] + skill[^(i + 1)] != score)
                return -1;

            chemistry += skill[i] * skill[^(i + 1)];
        }

        return chemistry;
    }

    [ProblemSolution("2493")]
    public int MagnificentSets(int n, int[][] edges)
    {
        var graph = new List<int>[n];

        for (int i = 0; i < graph.Length; i++)
            graph[i] = [];

        for (int i = 0; i < edges.Length; i++)
        {
            var (a, b) = (edges[i][0] - 1, edges[i][1] - 1);

            graph[a].Add(b);
            graph[b].Add(a);
        }

        var marked = new BitArray(n);
        var parity = new BitArray(n);
        var queue = new Queue<int>();
        var maxes = new int[n];

        for (int i = 0; i < marked.Count; i++)
        {
            var groups = 0;

            queue.Clear();
            queue.Enqueue(i);

            marked.SetAll(false);
            marked[i] = true;

            var minNode = int.MaxValue;
            var curParity = false;
            parity.SetAll(false);

            while (queue.Count > 0)
            {
                curParity = !curParity;
                groups++;
                var c = queue.Count;
                for (int j = 0; j < c; j++)
                {
                    var last = queue.Dequeue();
                    minNode = Math.Min(minNode, last);

                    var lastList = graph[last];
                    
                    for (int k = 0; k < lastList.Count; k++)
                    {
                        var other = lastList[k];

                        if (marked[other])
                        {
                            if (parity[other] != curParity)
                                return -1;

                            continue;
                        }

                        queue.Enqueue(other);
                        marked[other] = true;
                        parity[other] = curParity;
                    }

                }

            }

            maxes[minNode] = Math.Max(maxes[minNode], groups);
        }

        return maxes.Sum();
    }
}
