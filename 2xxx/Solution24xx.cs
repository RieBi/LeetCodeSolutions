namespace LeetCode.Set2xxx;
internal class Solution24xx
{
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
}
