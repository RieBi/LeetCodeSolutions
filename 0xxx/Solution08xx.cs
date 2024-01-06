using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode._0xxx;
internal class Solution08xx
{
    [ProblemSolution("841")]
    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        var keys = new HashSet<int>();
        keys.Add(0);
        var queue = new Queue<int>();
        queue.Enqueue(0);

        while (queue.Count > 0)
        {
            var room = queue.Dequeue();
            foreach (var key in rooms[room])
            {
                if (!keys.Contains(key))
                {
                    keys.Add(key);
                    queue.Enqueue(key);
                }
            }
        }

        return keys.Count == rooms.Count;
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
}
