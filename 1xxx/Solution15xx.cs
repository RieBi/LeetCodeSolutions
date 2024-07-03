using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeetCode.Set1XXX;
internal class Solution15XX
{
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
}
