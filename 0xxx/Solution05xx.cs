using System.ComponentModel.DataAnnotations;

namespace LeetCode.Set0xxx;
internal class Solution05xx
{
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
