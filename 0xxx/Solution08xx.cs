using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode._0xxx;
internal class Solution08xx
{
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
}
