namespace LeetCode.Set2xxx;
internal class Solution24xx
{
    [ProblemSolution("2402")]
    public int MostBooked(int n, int[][] meetings)
    {
        Array.Sort(meetings, (a, b) => a[0].CompareTo(b[0]));
        var vacantRooms = new PriorityQueue<int, int>();
        for (int i = 0; i < n; i++)
            vacantRooms.Enqueue(i, i);

        var rooms = new int[n];
        var meets = new PriorityQueue<(long end, int room), (long end, int room)>();
        long t = 0;

        for (int i = 0; i < meetings.Length; i++)
        {
            var meeting = meetings[i];
            var start = meeting[0];
            var end = meeting[1];
            t = Math.Max(start, t);

            while (meets.Count > 0 && t >= meets.Peek().end || vacantRooms.Count == 0)
            {
                var last = meets.Dequeue();
                vacantRooms.Enqueue(last.room, last.room);
                t = Math.Max(t, last.end);
            }

            var room = vacantRooms.Dequeue();
            rooms[room]++;
            var realEnd = t + (end - start);
            meets.Enqueue((realEnd, room), (realEnd, room));
        }

        var max = rooms.Max();
        return rooms.ToList().IndexOf(max);
    }

    [ProblemSolution("2441")]
    public int FindMaxK(int[] nums)
    {
        var negatives = nums.Where(f => f < 0).ToHashSet();
        return nums.Where(f => f > 0).Aggregate(-1, (a, b) => negatives.Contains(-b) ? Math.Max(a, b) : a);
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
}
