namespace LeetCode.Set1xxx;
internal class Solution10xx
{
    [ProblemSolution("1020")]
    public int NumEnclaves(int[][] grid)
    {
        for (int i = 0; i < grid[0].Length; i++)
        {
            processCell(0, i);
            processCell(grid.Length - 1, i);
        }

        for (int i = 0; i < grid.Length; i++)
        {
            processCell(i, 0);
            processCell(i, grid[0].Length - 1);
        }

        return grid.Sum(f => f.Count(x => x == 1));

        void processCell(int i, int j)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length)
                return;
            if (grid[i][j] != 1)
                return;
            
            grid[i][j] = -1;

            processCell(i - 1, j);
            processCell(i + 1, j);
            processCell(i, j - 1);
            processCell(i, j + 1);
        }
    }
}
