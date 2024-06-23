using System.ComponentModel.DataAnnotations;

namespace LeetCode.Set2xxx;
internal class Solution14xx
{
    [ProblemSolution("1422")]
    public int MaxScore(string s)
    {
        var max = 0;
        var zeroes = s[0] == '0' ? 1 : 0;
        var ones = s.Skip(1).Count(f => f == '1');
        for (int i = 1; i < s.Length; i++)
        {
            max = Math.Max(max, zeroes + ones);
            if (s[i] == '0')
                zeroes++;
            else
                ones--;
        }

        return max;
    }

    [ProblemSolution("1424")]
    public int[] FindDiagonalOrder(IList<IList<int>> nums)
    {
        var totalCount = nums.Sum(f => f.Count);
        var result = new int[totalCount];
        var resultInd = 0;

        var queue = new Queue<(int i, int j)>();
        queue.Enqueue((0, 0));

        while (queue.Count > 0)
        {
            var indices = queue.Dequeue();
            result[resultInd++] = nums[indices.i][indices.j];

            if (indices.j == 0 && indices.i + 1 < nums.Count && indices.j < nums[indices.i + 1].Count)
                queue.Enqueue((indices.i + 1, indices.j));
            if (indices.j + 1 < nums[indices.i].Count)
                queue.Enqueue((indices.i, indices.j + 1));
        }

        return result;
    }

    [ProblemSolution("1436")]
    public string DestCity(IList<IList<string>> paths)
    {
        var dick = new Dictionary<string, (bool, bool)>();
        for (int i = 0; i < paths.Count; i++)
        {
            var source = paths[i][0];
            var dest = paths[i][1];

            if (dick.TryGetValue(source, out (bool, bool) val))
                dick[source] = (true, val.Item2);
            else
                dick[source] = (true, false);

            if (dick.TryGetValue(dest, out val))
                dick[dest] = (val.Item1, true);
            else
                dick[dest] = (false, true);
        }

        foreach (var v in dick)
        {
            if (v.Value == (false, true))
                return v.Key;
        }

        return "";
    }

    [ProblemSolution("1438")]
    public int LongestSubarray(int[] nums, int limit)
    {
        var result = 0;
        var maxList = new LinkedList<int>();
        var minList = new LinkedList<int>();

        var left = 0;
        var right = 0;
        while (right < nums.Length)
        {
            addMax(right);
            addMin(right);
            right++;

            while (get(maxList) - get(minList) > limit)
                left++;

            result = Math.Max(result, right - left);
        }

        return result;

        int get(LinkedList<int> list)
        {
            while (list.Count > 0 && list.First.Value < left)
                list.RemoveFirst();

            return nums[list.First.Value];
        }

        void addMax(int ind)
        {
            while (maxList.Count > 0 && nums[maxList.Last!.Value] < nums[ind])
                maxList.RemoveLast();

            maxList.AddLast(ind);
        }

        void addMin(int ind)
        {
            while (minList.Count > 0 && nums[minList.Last!.Value] > nums[ind])
                minList.RemoveLast();

            minList.AddLast(ind);
        }
    }

    [ProblemSolution("1457")]
    public int PseudoPalindromicPaths(TreeNode root)
    {
        return dfs(root, 0);

        static int dfs(TreeNode? node, int state)
        {
            if (node is null)
                return 0;

            var mask = 1 << (node.val - 1);
            state ^= mask;

            if (node.left is null && node.right is null && isLow(state))
                return 1;

            return dfs(node.left, state) + dfs(node.right, state);
        }

        static bool isLow(int num)
        {
            var isSet = false;
            var mask = 1;
            for (int i = 0; i < 9; i++)
            {
                if ((num & mask) != 0)
                {
                    if (isSet)
                        return false;
                    else
                        isSet = true;
                }

                mask <<= 1;
            }

            return true;
        }
    }

    [ProblemSolution("1463")]
    public int CherryPickup(int[][] grid)
    {
        var row = new int[grid[0].Length][];
        for (int i = 0; i < row.Length; i++)
            row[i] = new int[grid[0].Length];
        row[0][^1] = grid[0][0] + grid[0][^1];

        for (int n = 1; n < grid.Length; n++)
        {
            var newRow = new int[grid[0].Length][];
            for (int m = 0; m < newRow.Length; m++)
                newRow[m] = new int[grid[0].Length];

            for (int i = 0; i < Math.Min(n + 1, newRow.Length); i++)
            {
                for (int j = newRow.Length - 1; j >= Math.Max(newRow.Length - 1 - n, 0); j--)
                {
                    var val = i == j ? grid[n][i] : grid[n][i] + grid[n][j];
                    for (int previ = Math.Max(0, i - 1); previ <= Math.Min(newRow.Length - 1, i + 1); previ++)
                    {
                        for (int prevj = Math.Max(0, j - 1); prevj <= Math.Min(newRow.Length - 1, j + 1); prevj++)
                        {
                            newRow[i][j] = Math.Max(newRow[i][j], row[previ][prevj] + val);
                        }
                    }
                }
            }

            row = newRow;
        }

        return row.Select(f => f.Max()).Max();
    }

    [ProblemSolution("1464")]
    public int MaxProduct(int[] nums)
    {
        return nums
            .Select(f => Math.Abs(f - 1))
            .OrderByDescending(f => f)
            .Take(2)
            .Aggregate(1, (a, b) => a * b);
    }

    [ProblemSolution("1481")]
    public int FindLeastNumOfUniqueInts(int[] arr, int k)
    {
        var groups = arr.GroupBy(f => f).Select(f => f.Count()).Order().ToList();
        var total = groups.Count;
        for (int i = 0; i < groups.Count; i++)
        {
            if (groups[i] > k)
                break;
            total--;
            k -= groups[i];
        }

        return total;
    }

    [ProblemSolution("1482")]
    public int MinDays(int[] bloomDay, int m, int k)
    {
        if (bloomDay.Length < (long)m * k)
            return -1;

        var low = bloomDay.Min();
        var high = bloomDay.Max();
        var ans = 0;

        while (low <= high)
        {
            var mid = (low + high) / 2;

            var bouqets = 0;
            var row = 0;
            for (int i = 0; i < bloomDay.Length; i++)
            {
                if (bloomDay[i] <= mid)
                {
                    row++;
                    if (row == k)
                    {
                        bouqets++;
                        row = 0;
                        if (bouqets == m)
                            break;
                    }
                }
                else
                    row = 0;
            }

            if (bouqets == m)
            {
                high = mid - 1;
                ans = mid;
            }
            else
                low = mid + 1;
        }

        return ans;
    }

    [ProblemSolution("1496")]
    public bool IsPathCrossing(string path)
    {
        var set = new HashSet<(int, int)>();
        set.Add((0, 0));
        var x = 0;
        var y = 0;

        for (int i = 0; i < path.Length; i++)
        {
            if (path[i] == 'N')
                y++;
            else if (path[i] == 'S')
                y--;
            else if (path[i] == 'E')
                x++;
            else
                x--;

            if (set.Contains((x, y)))
                return true;
            set.Add((x, y));
        }

        return false;
    }
}