using System.ComponentModel.DataAnnotations;

namespace LeetCode.Set0XXX;
internal class Solution05XX
{
    [ProblemSolution("502")]
    public int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
    {
        List<(int profit, int capital)> sorted = profits
            .Zip(capital)
            .OrderByDescending(f => f.Second)
            .ToList();

        var profitQueue = new PriorityQueue<int, int>();

        for (int i = 0; i < k; i++)
        {
            while (sorted.Count > 0 && sorted[^1].capital <= w)
            {
                var item = sorted[^1];
                sorted.RemoveAt(sorted.Count - 1);
                profitQueue.Enqueue(item.profit, -item.profit);
            }

            if (profitQueue.TryDequeue(out var profit, out _))
                w += profit;
            else
                break;
        }

        return w;
    }

    [ProblemSolution("506")]
    public string[] FindRelativeRanks(int[] score)
    {
        var ranks = score
            .Zip(Enumerable.Range(0, score.Length))
            .OrderByDescending(f => f.First)
            .ToList();

        var result = new string[score.Length];
        for (int i = 0; i < score.Length; i++)
        {
            var ind = ranks[i].Second;
            result[ind] = i == 0 ? "Gold Medal" :
                i == 1 ? "Silver Medal" :
                i == 2 ? "Bronze Medal" :
                (i + 1).ToString();
        }

        return result;
    }

    [ProblemSolution("509")]
    public int Fib(int n)
    {
        var cache = new Dictionary<int, int>();
        return calculate(n);
        
        int calculate(int num)
        {
            if (num < 2)
                return num;
            else if (cache.TryGetValue(num, out int value))
                return value;
            else
                return calculate(num - 1) + calculate(num - 2);
        }
    }

    [ProblemSolution("523")]
    public bool CheckSubarraySum(int[] nums, int k)
    {
        var hash = new Dictionary<int, int>();
        var sum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            sum = (sum + nums[i]) % k;
            if (sum == 0 && i > 0)
                return true;

            if (hash.TryGetValue(sum, out var value))
            {
                if (i - value > 1)
                    return true;
            }
            else
            {
                hash[sum] = i;
            }
        }

        return false;
    }

    [ProblemSolution("542")]
    public int[][] UpdateMatrix(int[][] mat)
    {
        var transforms = new List<(int, int)>
        {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1)
        };

        var queue = new Queue<int>();
        var lookup = new bool[mat.Length][];
        for (int i = 0; i < lookup.Length; i++)
            lookup[i] = new bool[mat[i].Length];

        for (int i = 0; i < mat.Length; i++)
        {
            for (int j = 0; j < mat[0].Length; j++)
            {
                if (mat[i][j] == 0)
                {
                    queue.Enqueue(i * mat[0].Length + j);
                    lookup[i][j] = true;
                }
            }
        }

        while (queue.Count > 0)
        {
            var elem = queue.Dequeue();
            var i = elem / mat[0].Length;
            var j = elem % mat[0].Length;

            foreach (var trans in transforms)
            {
                var newi = i + trans.Item1;
                var newj = j + trans.Item2;
                if (newi < 0 || newj < 0 || newi >= mat.Length || newj >= mat[0].Length || lookup[newi][newj])
                    continue;

                lookup[newi][newj] = true;
                mat[newi][newj] = mat[i][j] + 1;
                queue.Enqueue(newi * mat[0].Length + newj);
            }
        }

        return mat;
    }

    [ProblemSolution("559")]
    public int MaxDepth(Node root)
    {
        if (root is null)
            return 0;

        var max = 0;
        foreach (var child in root.children)
            max = Math.Max(max, MaxDepth(child));

        return max + 1;
    }

    [ProblemSolution("576")]
    public int FindPaths(int m, int n, int maxMove, int startRow, int startColumn)
    {
        List<(int i, int j)> transitions = [(0, -1), (0, 1), (1, 0), (-1, 0)];

        var prev = createMatrix(m, n);
        prev[startRow][startColumn] = 1;
        var result = 0;
        var modulo = 1_000_000_007;
        for (int count = 0; count < maxMove; count++)
        {
            var cur = createMatrix(m, n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    foreach (var transition in transitions)
                    {
                        var newI = i + transition.i;
                        var newJ = j + transition.j;

                        if (newI < 0 || newJ < 0 || newI >= m || newJ >= n)
                            result = (result + prev[i][j]) % modulo;
                        else
                            cur[newI][newJ] = (prev[i][j] + cur[newI][newJ]) % modulo;
                    }
                }
            }

            prev = cur;
        }

        return result;

        static int[][] createMatrix(int m, int n)
        {
            var ar = new int[m][];
            for (int i = 0; i < ar.Length; i++)
                ar[i] = new int[n];
            return ar;
        }
    }

    [ProblemSolution("589")]
    public IList<int> Preorder(Node root)
    {
        var stack = new Stack<(Node node, int cur)>();
        if (root is not null)
            stack.Push((root, 0));
        var list = new List<int>();

        while (stack.Count > 0)
        {
            var top = stack.Pop();
            if (top.cur == 0)
                list.Add(top.node.val);
            if (top.cur >= top.node.children.Count)
                continue;

            stack.Push((top.node, top.cur + 1));
            stack.Push((top.node.children[top.cur], 0));
        }

        return list;
    }

    [ProblemSolution("590")]
    public IList<int> Postorder(Node root)
    {
        var stack = new Stack<(Node node, int cur)>();
        if (root is not null)
            stack.Push((root, 0));
        var list = new List<int>();

        while (stack.Count > 0)
        {
            var top = stack.Pop();
            if (top.cur >= top.node.children.Count)
            {
                list.Add(top.node.val);
                continue;
            }

            stack.Push((top.node, top.cur + 1));
            stack.Push((top.node.children[top.cur], 0));
        }

        return list;
    }

    [ProblemSolution("599")]
    public string[] FindRestaurant(string[] list1, string[] list2)
    {
        var commons = list1.Zip(Enumerable.Range(0, list1.Length))
            .Concat(list2.Zip(Enumerable.Range(0, list2.Length)))
            .GroupBy(f => f.First)
            .Where(f => f.Count() == 2);

        var min = commons.Select(f => f.Sum(el => el.Second)).Min();
        return commons.Where(f => f.Sum(el => el.Second) == min)
            .Select(f => f.Key)
            .ToArray();
    }
}
