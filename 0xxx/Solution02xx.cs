using System.Net.WebSockets;

namespace LeetCode.Set0xxx;
internal class Solution02xx
{
    [ProblemSolution("200")]
    public int NumIslands(char[][] grid)
    {
        const char explored = '.';
        var count = 0;
        (int, int)[] dirs = [(1, 0), (-1, 0), (0, 1), (0, -1)];
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == '1')
                    count++;

                processTile(i, j);
            }
        }

        return count;

        void processTile(int i, int j)
        {
            if ((i < 0 || i >= grid.Length) || (j < 0 || j >= grid[0].Length) || grid[i][j] != '1')
                return;

            grid[i][j] = explored;
            foreach (var dir in dirs)
                processTile(i + dir.Item1, j + dir.Item2);
        }
    }

    [ProblemSolution("202")]
    public bool IsHappy(int n)
    {
        var set = new HashSet<int>();
        while (n != 1)
        {
            set.Add(n);
            n = (int) n.ToString()
                .Select(f => Math.Pow(int.Parse(f.ToString()), 2))
                .Sum();
            if (set.Contains(n))
                return false;
        }

        return true;
    }

    [ProblemSolution("205")]
    public bool IsIsomorphic(string s, string t)
    {
        var mappings = new Dictionary<char, char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (mappings.TryGetValue(s[i], out char value))
            {
                if (value != t[i])
                    return false;
            }
            else
            {
                if (mappings.ContainsValue(t[i]))
                    return false;
                mappings[s[i]] = t[i];
            }
        }

        return true;
    }

    [ProblemSolution("217")]
    public bool ContainsDuplicate(int[] nums)
    {
        return nums.Distinct().Count() != nums.Length;
    }

    [ProblemSolution("219")]
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        return nums
            .Zip(Enumerable.Range(0, nums.Length))
            .GroupBy(f => f.First, f => f.Second)
            .Select(f => f.OrderBy(f => f))
            .Any(s =>
            {
                var min = int.MaxValue;
                s.Skip(1).Aggregate(s.First(), (a, b) =>
                {
                    var diff = Math.Abs(a - b);
                    min = Math.Min(min, diff);
                    return b;
                });
                return min <= k;
            });
    }

    [ProblemSolution("242")]
    public bool IsAnagram(string s, string t)
    {
        var dicks = new Dictionary<char, int>();
        var dickt = new Dictionary<char, int>();

        FillDick(s, dicks);
        FillDick(t, dickt);

        return DicksEqual(dicks, dickt);

        void FillDick(string str, Dictionary<char, int> dick)
        {
            foreach (var v in str)
            {
                if (dick.TryGetValue(v, out int val))
                    dick[v] = val + 1;
                else
                    dick[v] = 1;
            }
        }

        bool DicksEqual(Dictionary<char, int> dick1, Dictionary<char, int> dick2)
        {
            if (dick1.Count != dick2.Count)
                return false;

            foreach (var v in dick1.Keys)
            {
                if (!(dick1.TryGetValue(v, out int val1) && dick2.TryGetValue(v, out int val2)))
                    return false;
                else if (val1 != val2)
                    return false;
            }

            return true;
        }
    }

    [ProblemSolution("279")]
    public int NumSquares(int n)
    {
        var sum = 0;
        var lowest = int.MaxValue;
        var stack = new Stack<int>();
        var startNum = (int)Math.Sqrt(n);
        addElem(startNum);

        while (stack.Count > 0)
        {
            if (sum == n)
                lowest = Math.Min(lowest, stack.Count);

            if (sum == n || stack.Count >= lowest)
            {
                var dequeued = 1;
                while (dequeued == 1)
                {
                    if (stack.Count == 0)
                        break;
                    dequeued = removeElem();
                }

                var lowernum = (int)Math.Sqrt(dequeued) - 1;
                if (lowernum == 0)
                    break;

                addElem(lowernum);
            }
            else
            {
                var num = (int)Math.Sqrt(n - sum);
                addElem(num);
            }
        }

        return lowest;
        
        void addElem(int elem)
        {
            elem *= elem;
            stack.Push(elem);
            sum += elem;
        }

        int removeElem()
        {
            var elem = stack.Pop();
            sum -= elem;
            return elem;
        }
    }
}
