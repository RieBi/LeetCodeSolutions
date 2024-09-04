using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Set0XXX;
internal class Solution08XX
{
    [ProblemSolution("826")]
    public int MaxProfitAssignment(int[] difficulty, int[] profit, int[] worker)
    {
        List<(int difficulty, int profit)> jobs = difficulty.Zip(profit)
            .OrderBy(f => f.Second)
            .ToList();

        var workers = worker.Order().ToList();

        var total = 0;
        while (jobs.Count > 0 && workers.Count > 0)
        {
            var last = jobs[^1];
            jobs.RemoveAt(jobs.Count - 1);

            while (workers.Count > 0 && workers[^1] >= last.difficulty)
            {
                workers.RemoveAt(workers.Count - 1);
                total += last.profit;
            }
        }

        return total;
    }

    [ProblemSolution("834")]
    public int[] SumOfDistancesInTree(int n, int[][] edges)
    {
        if (n == 1)
            return [0];

        var nodes = new List<int>[n];
        var parents = new int[n];
        var total = new int[n];
        var childCounts = new int[n];

        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = [];
            parents[i] = -1;
            total[i] = -1;
            childCounts[i] = -1;
        }

        for (int i = 0; i < edges.Length; i++)
        {
            var nodea = edges[i][0];
            var nodeb = edges[i][1];
            nodes[nodea].Add(nodeb);
            nodes[nodeb].Add(nodea);
        }

        var queue = new Queue<int>();
        queue.Enqueue(0);
        while (queue.Count > 0)
        {
            var last = queue.Dequeue();
            foreach (int other in nodes[last])
            {
                if (other == parents[last])
                    continue;

                parents[other] = last;
                queue.Enqueue(other);
            }
        }


        var stack = new Stack<int>();
        stack.Push(0);
        while (stack.Count > 0)
        {
            var top = stack.Peek();

            if (nodes[top].Count == 1 && parents[top] != -1)
                childCounts[top] = 0;
            if (childCounts[top] == -1)
            {
                foreach (var other in nodes[top])
                {
                    if (other == parents[top])
                        continue;

                    stack.Push(other);
                }
            }
            else
            {
                if (parents[top] != -1)
                {
                    if (childCounts[parents[top]] == -1)
                        childCounts[parents[top]] = 0;
                    childCounts[parents[top]] += 1 + childCounts[top];
                }

                stack.Pop();
            }
        }

        var startTotal = 0;
        var level = 0;
        queue.Enqueue(0);
        while (queue.Count > 0)
        {
            var c = queue.Count;
            for (int i = 0; i < c; i++)
            {
                var last = queue.Dequeue();
                startTotal += level;
                foreach (var other in nodes[last])
                {
                    if (other == parents[last])
                        continue;

                    queue.Enqueue(other);
                }
            }

            level++;
        }

        total[0] = startTotal;
        queue.Enqueue(0);
        while (queue.Count > 0)
        {
            var c = queue.Count;
            for (int i = 0; i < c; i++)
            {
                var last = queue.Dequeue();
                foreach (var other in nodes[last])
                {
                    if (other == parents[last])
                        continue;

                    queue.Enqueue(other);
                }

                if (parents[last] != -1)
                {
                    total[last] = total[parents[last]] - childCounts[last] * 2 + n - 2;
                }
            }
        }

        return total;
    }

    [ProblemSolution("840")]
    public int NumMagicSquaresInside(int[][] grid)
    {
        var result = 0;
        for (int i = 2; i < grid.Length; i++)
        {
            for (int j = 2; j < grid[0].Length; j++)
            {
                result += calculate(i, j);
            }
        }

        return result;

        int calculate(int i, int j)
        {
            var sum = grid[i][j] + grid[i - 1][j - 1] + grid[i - 2][j - 2];

            if (grid[i][j - 2] + grid[i - 1][j - 1] + grid[i - 2][j] != sum)
                return 0;

            for (int r = i - 2; r <= i; r++)
            {
                if (grid[r][j] + grid[r][j - 1] + grid[r][j - 2] != sum)
                    return 0;
            }

            for (int c = j - 2; c <= j; c++)
            {
                if (grid[i][c] + grid[i - 1][c] + grid[i - 2][c] != sum)
                    return 0;
            }

            var ar = new BitArray(9);
            for (int r = i - 2; r <= i; r++)
            {
                for (int c = j - 2; c <= j; c++)
                {
                    var n = grid[r][c];
                    if (n < 1 || n > 9)
                        return 0;
                    ar[n - 1] = true;
                }
            }

            return ar.HasAllSet() ? 1 : 0;
        }
    }

    [ProblemSolution("841")]
    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        var keys = new HashSet<int>() { 0 };
        var queue = new Queue<int>();
        queue.Enqueue(0);

        while (queue.Count > 0)
        {
            var room = queue.Dequeue();
            foreach (var key in rooms[room])
            {
                keys.Add(key);
                if (!keys.Contains(key))
                {
                    queue.Enqueue(key);
                }
            }
        }

        return keys.Count == rooms.Count;
    }

    [ProblemSolution("860")]
    public bool LemonadeChange(int[] bills)
    {
        var bill5 = 0;
        var bill10 = 0;

        for (int i = 0; i < bills.Length; i++)
        {
            var bill = bills[i];
            if (bill == 5)
                bill5++;
            else if (bill == 10)
            {
                bill10++;
                bill5--;
            }
            else
            {
                if (bill10 > 0 && bill5 > 0)
                {
                    bill10--;
                    bill5--;
                }
                else
                    bill5 -= 3;
            }

            if (bill5 < 0 || bill10 < 0)
                return false;
        }

        return true;
    }

    [ProblemSolution("867")]
    public int[][] Transpose(int[][] matrix)
    {
        var rows = matrix.Length;
        var cols = matrix[0].Length;

        var resMatrix = new int[cols][];
        for (int i = 0; i < cols; i++)
            resMatrix[i] = new int[rows];

        for (int i = 0; i < rows; i++) 
        {
            for (int j = 0; j < cols; j++)
            {
                resMatrix[j][i] = matrix[i][j];
            }
        }

        return resMatrix;
    }

    [ProblemSolution("872")]
    public bool LeafSimilar(TreeNode root1, TreeNode root2)
    {
        var leaves1 = new List<int>();
        var leaves2 = new List<int>();

        fillLeaves(leaves1, root1);
        fillLeaves(leaves2, root2);

        return leaves1.SequenceEqual(leaves2);

        static void fillLeaves(List<int> list, TreeNode node)
        {
            if (node.left is null && node.right is null)
            {
                list.Add(node.val);
                return;
            }

            if (node.left is not null)
                fillLeaves(list, node.left);
            if (node.right is not null)
                fillLeaves(list, node.right);
        }
    }

    [ProblemSolution("874")]
    public int RobotSim(int[] commands, int[][] obstacles)
    {
        var ground = new HashSet<(int x, int y)>();
        for (int i = 0; i < obstacles.Length; i++)
            ground.Add((obstacles[i][0], obstacles[i][1]));

        (int x, int y)[] dirs = [(0, 1), (1, 0), (0, -1), (-1, 0)];
        var maxSquaredEuclidean = 0;

        var dir = 0;
        int x = 0, y = 0;
        for (int ind = 0; ind < commands.Length; ind++)
        {
            var command = commands[ind];
            if (command == -2)
                dir = dir == 0 ? 3 : dir - 1;
            else if (command == -1)
                dir = dir == 3 ? 0 : dir + 1;
            else
            {
                for (int i = 0; i < command; i++)
                {
                    (int x, int y) destination = (x + dirs[dir].x, y + dirs[dir].y);
                    if (ground.Contains(destination))
                        break;
                    else
                        (x, y) = (destination.x, destination.y);
                }
            }

            maxSquaredEuclidean = Math.Max(maxSquaredEuclidean, x * x + y * y);
        }

        return maxSquaredEuclidean;
    }

    [ProblemSolution("881")]
    public int NumRescueBoats(int[] people, int limit)
    {
        var sorted = people.Order().ToList();
        var l = 0;
        var r = sorted.Count - 1;

        var c = 0;
        while (l < r)
        {
            if (sorted[l] + sorted[r] <= limit)
            {
                l++;
                r--;
            }
            else
                r--;

            c++;
        }

        if (l == r)
            c++;

        return c;
    }

    [ProblemSolution("885")]
    public int[][] SpiralMatrixIII(int rows, int cols, int rStart, int cStart)
    {
        var lines = 2;
        var total = rows * cols;

        var iPos = rStart;
        var jPos = cStart;

        var result = new int[total][];
        result[0] = [iPos, jPos];
        var resultInd = 1;

        while (resultInd < total)
        {
            var length = lines / 2;
            (int i, int j) transform = (lines % 4) switch
            {
                2 => (0, 1),
                3 => (1, 0),
                0 => (0, -1),
                _ => (-1, 0)
            };

            for (int i = 0; i < length; i++)
            {
                iPos += transform.i;
                jPos += transform.j;

                if (iPos >= 0 && jPos >= 0 && iPos < rows && jPos < cols)
                    result[resultInd++] = [iPos, jPos];
            }

            lines++;
        }

        return result;
    }
}
