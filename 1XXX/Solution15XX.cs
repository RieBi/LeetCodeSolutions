namespace LeetCode.Set1XXX;
internal class Solution15XX
{
    [ProblemSolution("1508")]
    public int RangeSum(int[] nums, int n, int left, int right)
    {
        var pref = new int[nums.Length + 1];
        for (int i = 1; i < pref.Length; i++)
            pref[i] = pref[i - 1] + nums[i - 1];

        var sums = new List<int>(n);
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i; j < nums.Length; j++)
            {
                sums.Add(pref[j + 1] - pref[i]);
            }
        }

        sums.Sort();

        var result = 0;
        left--;

        for (; left < right; left++)
            result = (result + sums[left]) % 1_000_000_007;

        return result;
    }

    [ProblemSolution("1509")]
    public int MinDifference(int[] nums)
    {
        if (nums.Length < 5)
            return 0;

        var lowSet = new SortedSet<(int, int)>();
        var highSet = new SortedSet<(int, int)>();

        for (int i = 0; i < nums.Length; i++)
        {
            lowSet.Add((nums[i], i));
            highSet.Add((nums[i], i));

            if (i > 3)
            {
                lowSet.Remove(lowSet.Max);
                highSet.Remove(highSet.Min);
            }
        }

        return lowSet
            .Select(f => f.Item1)
            .Zip(highSet.Select(f => f.Item1))
            .Min(f => f.Second - f.First);
    }

    [ProblemSolution("1514")]
    public double MaxProbability(int n, int[][] edges, double[] succProb, int start_node, int end_node)
    {
        var nodes = new List<(int other, double prob)>[n];
        for (int i = 0; i < n; i++)
            nodes[i] = [];

        var prob = new double[n];
        prob[start_node] = 1;

        for (int i = 0; i < edges.Length; i++)
        {
            var a = edges[i][0];
            var b = edges[i][1];

            nodes[a].Add((b, succProb[i]));
            nodes[b].Add((a, succProb[i]));
        }

        var queue = new SortedSet<(double, int)>
        {
            (1, start_node)
        };

        while (queue.Count > 0)
        {
            var last = queue.Max;
            queue.Remove(last);

            foreach ((var other, var otherProb) in nodes[last.Item2])
            {
                var newProb = last.Item1 * otherProb;
                if (newProb > prob[other])
                {
                    queue.Remove((prob[other], other));
                    queue.Add((newProb, other));
                    prob[other] = newProb;
                }
            }
        }

        return prob[end_node];
    }

    [ProblemSolution("1518")]
    public int NumWaterBottles(int numBottles, int numExchange)
    {
        var empty = 0;
        var drank = 0;
        while (numBottles > 0)
        {
            drank += numBottles;
            empty += numBottles;

            numBottles = empty / numExchange;
            empty %= numExchange;
        }

        return drank;
    }

    [ProblemSolution("1530")]
    public int CountPairs(TreeNode root, int distance)
    {
        var result = 0;

        traverse(root, 0);

        return result;

        List<int> traverse(TreeNode? node, int height)
        {
            if (node is null)
                return [];

            if (node.left is null && node.right is null)
                return [height];

            var lList = traverse(node.left, height + 1);
            var rList = traverse(node.right, height + 1);

            count(lList, rList, height * 2 + distance);

            return merge(lList, rList);
        }

        void count(List<int> lList, List<int> rList, int plausibleDistance)
        {
            if (lList.Count == 0 || rList.Count == 0)
                return;

            var left = 0;
            var right = rList.Count - 1;

            for (; left < lList.Count; left++)
            {
                while (right >= 0 && lList[left] + rList[right] > plausibleDistance)
                    right--;

                if (right == -1)
                    return;

                result += right + 1;
            }
        }

        List<int> merge(List<int> lList, List<int> rList)
        {
            var merged = new List<int>();

            var left = 0;
            var right = 0;

            while (left < lList.Count && right < rList.Count)
            {
                if (lList[left] <= rList[right])
                    merged.Add(lList[left++]);
                else
                    merged.Add(rList[right++]);
            }

            if (left >= lList.Count)
                merged.AddRange(rList[right..]);
            else
                merged.AddRange(lList[left..]);

            return merged;
        }
    }

    [ProblemSolution("1531")]
    public int GetLengthOfOptimalCompression(string s, int k)
    {
        var n = s.Length;
        var m = k;

        var dp = new int[110][];
        for (int i = 0; i < dp.Length; i++)
            dp[i] = new int[110];

        for (int i = 1; i <= n; i++)
        {
            for (int j = 0; j <= i && j <= m; j++)
            {
                var needRemove = 0;
                var groupCount = 0;
                dp[i][j] = int.MaxValue;
                if (j != 0)
                    dp[i][j] = dp[i - 1][j - 1];
                for (k = i; k >= 1; k--)
                {
                    if (s[k - 1] != s[i - 1])
                        needRemove += 1;
                    else
                        groupCount += 1;

                    if (needRemove > j)
                        break;

                    var len = groupCount switch
                    {
                        1 => 1,
                        < 10 => 2,
                        < 100 => 3,
                        _ => 4
                    };

                    dp[i][j] = Math.Min(dp[i][j], dp[k - 1][j - needRemove] + len);
                }
            }
        }

        return dp[n][m];
    }

    [ProblemSolution("1535")]
    public int GetWinner(int[] arr, int k)
    {
        var max = arr.Max();
        if (arr[0] == max)
            return max;

        var ind = 0;
        var count = 0;

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] == max)
                return max;

            if (arr[ind] > arr[i])
            {
                count++;
            }
            else
            {
                ind = i;
                count = 1;
            }

            if (count >= k)
            {
                return arr[ind];
            }
        }

        return -1;
    }

    [ProblemSolution("1550")]
    public bool ThreeConsecutiveOdds(int[] arr)
    {
        var row = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] % 2 == 1)
            {
                row++;
                if (row == 3)
                    return true;
            }
            else
                row = 0;
        }

        return false;
    }

    [ProblemSolution("1551")]
    public int MinOperations(int n)
    {
        var l = n / 2;
        var m = n % 2;
        var a1 = 1 + m;
        var an = 2 * l - 1 + m;
        return l * (a1 + an) / 2;
    }

    [ProblemSolution("1552")]
    public int MaxDistance(int[] position, int m)
    {
        Array.Sort(position);
        var min = 1;
        var max = position[^1] / (m - 1);

        var low = min;
        var high = max;
        var ans = 0;

        while (low <= high)
        {
            var mid = (low + high) / 2;
            var balls = 1;
            var lastBall = position[0];
            for (int i = 1; i < position.Length; i++)
            {
                if (position[i] - lastBall >= mid)
                {
                    balls++;
                    lastBall = position[i];
                    if (balls == m)
                        break;
                }
            }

            if (balls == m)
            {
                low = mid + 1;
                ans = mid;
            }
            else
                high = mid - 1;
        }

        return ans;
    }

    [ProblemSolution("1561")]
    public int MaxCoins(int[] piles)
    {
        Array.Sort(piles);
        var triples = piles.Length / 3;
        var result = 0;
        for (int i = 1; i <= triples; i++)
        {
            result += piles[piles.Length - 2 * i];
        }

        return result;
    }

    [ProblemSolution("1568")]
    public int MinDays(int[][] grid)
    {
        var total1 = 0;

        for (int i = 0; i < grid.Length; i++)
            for (int j = 0; j < grid[0].Length; j++)
                if (grid[i][j] == 1)
                    total1++;

        if (total1 <= 1)
            return total1;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    var count = propagate(i, j, createState());
                    if (count != total1)
                        return 0;

                    i = grid.Length;
                    break;
                }
            }
        }

        List<(int i, int j)> transform = [(0, 1), (0, -1), (1, 0), (-1, 0)];

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    grid[i][j] = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        var num = propagate(i + transform[k].i, j + transform[k].j, createState());
                        if (num > 0 && num < total1 - 1)
                            return 1;
                    }

                    grid[i][j] = 1;
                }
            }
        }

        return 2;

        bool[][] createState()
        {
            var result = new bool[grid.Length][];
            for (int i = 0; i < result.Length; i++)
                result[i] = new bool[grid[0].Length];

            return result;
        }

        int propagate(int i, int j, bool[][] state)
        {
            if (i < 0 || j < 0 || i >= state.Length || j >= state[0].Length)
                return 0;

            if (grid[i][j] == 0 || state[i][j])
                return 0;

            var total = 1;
            state[i][j] = true;

            total += propagate(i + 1, j, state);
            total += propagate(i - 1, j, state);
            total += propagate(i, j + 1, state);
            total += propagate(i, j - 1, state);

            return total;
        }
    }

    [ProblemSolution("1579")]
    public int MaxNumEdgesToRemove(int n, int[][] edges)
    {
        var red = new HashSet<(int a, int b)>();
        var green = new HashSet<(int a, int b)>();
        var blue = new HashSet<(int a, int b)>();

        for (int i = 0; i < edges.Length; i++)
        {
            var el = edges[i];
            var pair = (el[1] - 1, el[2] - 1);
            if (el[0] == 3)
                blue.Add(pair);
            else if (el[0] == 1)
                red.Add(pair);
            else if (el[0] == 2)
                green.Add(pair);
        }

        var blueList = blue.ToList();
        var ind = new int[n];
        fill(ind);

        for (int i = 0; i < blueList.Count; i++)
        {
            var edge = blueList[i];
            merge(edge.a, edge.b, ind, ab => blue.Remove(ab));
        }

        var redList = red.ToList();
        var greenInd = ind.ToArray();

        for (int i = 0; i < redList.Count; i++)
        {
            var edge = redList[i];
            if (blue.Contains(edge))
            {
                red.Remove(edge);
                continue;
            }

            merge(edge.a, edge.b, ind, ab => red.Remove(ab));
        }

        if (isNoPath(ind))
            return -1;

        var greenList = green.ToList();
        ind = greenInd;

        for (int i = 0; i < greenList.Count; i++)
        {
            var edge = greenList[i];
            if (blue.Contains(edge))
            {
                green.Remove(edge);
                continue;
            }

            merge(edge.a, edge.b, ind, ab => green.Remove(ab));
        }

        if (isNoPath(ind))
            return -1;

        var afterCount = red.Count + green.Count + blue.Count;
        return edges.Length - afterCount;


        int find(int el, int[] nodes)
        {
            var start = el;

            while (nodes[el] != el)
                el = nodes[el];

            while (nodes[start] != el)
            {
                var temp = nodes[start];
                nodes[start] = el;
                start = temp;
            }

            return el;
        }

        void merge(int el1, int el2, int[] nodes, Action<(int a, int b)> action)
        {
            var root1 = find(el1, nodes);
            var root2 = find(el2, nodes);

            if (root1 == root2)
            {
                action((el1, el2));
                return;
            }

            nodes[root1] = root2;
        }

        void fill(int[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = i;
        }

        bool isNoPath(int[] nodes)
        {
            var startRoot = find(0, nodes);
            for (int i = 1; i < nodes.Length; i++)
            {
                if (startRoot != find(i, nodes))
                    return true;
            }

            return false;
        }
    }

    [ProblemSolution("1578")]
    public int MinCost(string colors, int[] neededTime)
    {
        var sum = neededTime[0];
        var max = neededTime[0];
        var count = 1;
        var total = 0;
        for (int i = 1; i < colors.Length; i++)
        {
            if (colors[i] == colors[i - 1])
            {
                sum += neededTime[i];
                max = Math.Max(max, neededTime[i]);
                count++;
            }
            else
            {
                if (count > 1)
                    total += (sum - max);
                sum = neededTime[i];
                max = neededTime[i];
                count = 1;
            }
        }

        if (count > 1)
            total += (sum - max);

        return total;
    }

    [ProblemSolution("1582")]
    public int NumSpecial(int[][] mat)
    {
        var rows = mat
            .Select(f => f.Count(el => el == 1))
            .ToList();

        var cols = new List<int>(mat[0].Length);
        for (int i = 0; i < mat[0].Length; i++)
        {
            var count = 0;
            for (int j = 0; j < mat.Length; j++)
            {
                if (mat[j][i] == 1)
                    count++;
            }

            cols.Add(count);
        }

        var total = 0;
        for (int i = 0; i < mat.Length; i++)
        {
            for (int j = 0; j < mat[0].Length; j++)
            {
                if (mat[i][j] == 1 && rows[i] == 1 && cols[j] == 1)
                    total++;
            }
        }

        return total;
    }

    [ProblemSolution("1590")]
    public int MinSubarray(int[] nums, int p)
    {
        var last = new Dictionary<int, int>();
        var min = nums.Length;
        var required = nums.Aggregate(0, (acc, num) => (acc + num) % p);

        if (required == 0)
            return 0;

        var sum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            sum = (sum + nums[i]) % p;
            var other = (sum + p - required) % p;

            if (last.TryGetValue(other, out var value))
                min = Math.Min(min, i - value);
            else if (sum == required)
                min = Math.Min(min, i + 1);

            last[sum] = i;
        }

        return min == nums.Length ? -1 : min;
    }

    [ProblemSolution("1598")]
    public int MinOperations(string[] logs)
    {
        var folder = 0;
        for (int i = 0; i < logs.Length; i++)
        {
            if (logs[i].StartsWith("..") && folder > 0)
                folder--;
            else if (!logs[i].StartsWith('.'))
                folder++;
        }

        return folder;
    }
}
