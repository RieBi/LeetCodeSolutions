using System.Linq.Expressions;

namespace LeetCode.Set1XXX;
internal class Solution17XX
{
    [ProblemSolution("1701")]
    public double AverageWaitingTime(int[][] customers)
    {
        var time = 0L;
        var totalWaitTime = .0;
        for (int i = 0; i < customers.Length; i++)
        {
            var (start, prepare) = (customers[i][0], customers[i][1]);

            time = Math.Max(time, start);
            time += prepare;

            totalWaitTime += time - start;
        }

        return totalWaitTime / customers.Length;
    }

    [ProblemSolution("1704")]
    public bool HalvesAreAlike(string s)
    {
        HashSet<char> vowels = ['a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'];
        var (part1, part2) = (0, 0);
        for (int i = 0; i < s.Length; i++)
        {
            if (!vowels.Contains(s[i]))
                continue;
            if (i < s.Length / 2)
                part1++;
            else
                part2++;
        }

        return part1 == part2;
    }

    [ProblemSolution("1716")]
    public int TotalMoney(int n)
    {
        var total = 0;
        for (int i = 1; i <= n; i++)
            total += ((int)Math.Ceiling((double)i / 7) + (i - 1) % 7);

        return total;
    }

    [ProblemSolution("1717")]
    public int MaximumGain(string s, int x, int y)
    {
        var aCount = 0;
        var bCount = 0;

        var points = 0;
        var high = x > y ? 'b' : 'a';
        var highPoints = Math.Max(x, y);
        var minPoints = Math.Min(x, y);

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == 'a')
                aCount++;
            else if (s[i] == 'b')
                bCount++;
            else
                updateStop();

            if (aCount > 0 && bCount > 0 && s[i] == high)
            {
                points += highPoints;
                aCount--;
                bCount--;
            }
        }

        updateStop();

        return points;

        void updateStop()
        {
            var min = Math.Min(aCount, bCount);
            points += minPoints * min;
            (aCount, bCount) = (0, 0);
        }
    }

    [ProblemSolution("1727")]
    public int LargestSubmatrix(int[][] matrix)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;

        for (int i = 1; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] != 0)
                    matrix[i][j] = matrix[i - 1][j] + 1;
            }
        }

        var max = 0;
        for (int i = 0; i < m; i++)
        {
            var row = matrix[i];
            Array.Sort(row);
            for (int j = n; j > 0; j--)
            {
                var localMax = j * row[n - j];
                max = Math.Max(max, localMax);
            }
        }

        return max;
    }

    [ProblemSolution("1758")]
    public int MinOperations(string s)
    {
        var count = s
            .Select(f => int.Parse(f.ToString()))
            .Zip(Enumerable.Range(0, s.Length))
            .Count(f => f.First % 2 == f.Second % 2);

        return Math.Min(count, s.Length - count);
    }

    [ProblemSolution("1759")]
    public int CountHomogenous(string s)
    {
        var dp = new long[s.Length];
        dp[0] = 1;

        for (int i = 1; i < dp.Length; i++)
        {
            if (s[i] == s[i - 1])
            {
                dp[i] = dp[i - 1] + 1;
            }
            else
            {
                dp[i] = 1;
            }
        }
        long modulo = 1000000000 + 7;
        long total = 0;
        long n = 1;
        for (int i = 1; i < dp.Length; i++)
        {
            if (dp[i] <= n)
            {
                var sum = n * (n + 1) / 2;
                total += sum;
                total %= modulo;
                n = 1;
            }
            else
            {
                n = dp[i];
            }
        }
        total += (n * (n + 1) / 2);

        return (int)(total %= modulo);
    }

    [ProblemSolution("1765")]
    public int[][] HighestPeak(int[][] isWater)
    {
        var result = new int[isWater.Length][];
        var queue = new Queue<(int i, int j)>();

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new int[isWater[0].Length];
            for (int j = 0; j < result[i].Length; j++)
            {
                if (isWater[i][j] == 1)
                {
                    queue.Enqueue((i, j));
                    result[i][j] = 0;
                }
                else
                    result[i][j] = -1;
            }
        }

        (int i, int j)[] transforms = [
            (0, 1),
            (0, -1),
            (1, 0),
            (-1, 0)
        ];

        var level = 1;
        while (queue.Count > 0)
        {
            var c = queue.Count;
            for (int i = 0; i < c; i++)
            {
                var (curI, curJ) = queue.Dequeue();

                for (int j = 0; j < 4; j++)
                {
                    var (otherI, otherJ) = (curI + transforms[j].i, curJ + transforms[j].j);
                    if (otherI < 0 || otherJ < 0 || otherI >= result.Length || otherJ >= result[0].Length || result[otherI][otherJ] != -1)
                        continue;

                    result[otherI][otherJ] = level;
                    queue.Enqueue((otherI, otherJ));
                }
            }

            level++;
        }

        return result;
    }

    [ProblemSolution("1769")]
    public int[] MinOperations2(string boxes)
    {
        var rightSum = 0;
        var rightOnes = 0;
        var leftSum = 0;
        var leftOnes = 0;

        for (int i = 1; i < boxes.Length; i++)
        {
            var num = boxes[i] - '0';
            rightOnes += num;
            rightSum += num * i;
        }

        var result = new int[boxes.Length];
        for (int i = 0; i < boxes.Length; i++)
        {
            result[i] = leftSum + rightSum;

            var num = boxes[i] - '0';
            leftOnes += num;
            leftSum += leftOnes;
            
            if (i != boxes.Length - 1)
            {
                var nextNum = boxes[i + 1] - '0';
                rightSum -= rightOnes;
                rightOnes -= nextNum;
            }
        }

        return result;
    }

    [ProblemSolution("1791")]
    public int FindCenter(int[][] edges)
    {
        List<int> nums = [edges[0][0], edges[0][1], edges[1][0], edges[1][1]];
        return nums.GroupBy(f => f)
            .OrderByDescending(f => f.Count())
            .First().Key;
    }

    [ProblemSolution("1792")]
    public double MaxAverageRatio(int[][] classes, int extraStudents)
    {
        var totalPass = 0D;
        var queue = new PriorityQueue<(double pass, double total), double>();

        for (int i = 0; i < classes.Length; i++)
        {
            (double cur, double curTotal) = (classes[i][0], classes[i][1]);
            var curRate = cur / curTotal;

            var diff = ((cur + 1) / (curTotal + 1)) - curRate;
            totalPass += curRate;
            queue.Enqueue((cur, curTotal), -diff);
        }

        while (extraStudents > 0)
        {
            queue.TryDequeue(out var elem, out var diff);

            totalPass -= diff;
            var nextDiff = ((elem.pass + 2) / (elem.total + 2)) - ((elem.pass + 1) / (elem.total + 1));
            queue.Enqueue((elem.pass + 1, elem.total + 1), -nextDiff);

            extraStudents--;
        }

        return totalPass / classes.Length;
    }
}
