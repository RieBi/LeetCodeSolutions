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

    [ProblemSolution("872")]
    public bool LeafSimilar(TreeNode root1, TreeNode root2)
    {
        var leaves1 = new List<int>();
        var leaves2 = new List<int>();

        fillLeaves(leaves1, root1);
        fillLeaves(leaves2, root2);

        return leaves1.SequenceEqual(leaves2);

        void fillLeaves(List<int> list, TreeNode node)
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
}
