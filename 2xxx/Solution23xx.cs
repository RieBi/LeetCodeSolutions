using System.ComponentModel.DataAnnotations;

namespace LeetCode.Set2XXX;
internal class Solution23XX
{
    [ProblemSolution("2326")]
    public int[][] SpiralMatrix(int m, int n, ListNode head)
    {
        var result = new int[m][];
        for (int i = 0; i < m; i++)
            result[i] = new int[n];

        var cur = head;
        foreach (var (i, j) in getSpiral())
        {
            result[i][j] = cur?.val ?? -1;
            cur = cur?.next;
        }

        return result;

        IEnumerable<(int i, int j)> getSpiral()
        {
            var top = 0;
            var bot = m - 1;
            var left = 0;
            var right = n - 1;

            while (true)
            {
                for (int i = left; i <= right; i++)
                    yield return (top, i);

                top++;
                if (left > right || top > bot)
                    yield break;

                for (int i = top; i <= bot; i++)
                    yield return (i, right);

                right--;
                if (left > right || top > bot)
                    yield break;

                for (int i = right; i >= left; i--)
                    yield return (bot, i);

                bot--;
                if (left > right || top > bot)
                    yield break;

                for (int i = bot; i >= top; i--)
                    yield return (i, left);

                left++;
                if (left > right || top > bot)
                    yield break;
            }
        }
    }

    [ProblemSolution("2353")]
    public class FoodRatings
    {
        private Dictionary<string, (int rating, string cuisine)> FoodToRating;
        private Dictionary<string, SortedSet<(string food, int rating)>> RatingsByCuisine;

        public FoodRatings(string[] foods, string[] cuisines, int[] ratings)
        {
            FoodToRating = new Dictionary<string, (int, string)>(foods.Length);
            RatingsByCuisine = new Dictionary<string, SortedSet<(string food, int rating)>>();

            for (int i = 0; i < foods.Length; i++)
            {
                var food = foods[i];
                var cuisine = cuisines[i];
                var rating = ratings[i];

                FoodToRating[food] = (rating, cuisine);
                if (RatingsByCuisine.TryGetValue(cuisine, out SortedSet<(string food, int rating)> set))
                {
                    set.Add((food, rating));
                }
                else
                {
                    RatingsByCuisine[cuisine] = new SortedSet<(string food, int rating)>(new FoodComparer());
                    RatingsByCuisine[cuisine].Add((food, rating));
                }
            }
        }

        public void ChangeRating(string food, int newRating)
        {
            var oldRat = FoodToRating[food].rating;
            var cuisine = FoodToRating[food].cuisine;
            FoodToRating[food] = (newRating, cuisine);
            var list = RatingsByCuisine[cuisine];

            var oldVal = (food, oldRat);
            var newVal = (food, newRating);

            list.Remove(oldVal);
            list.Add(newVal);
        }

        public string HighestRated(string cuisine)
        {
            return RatingsByCuisine[cuisine].Max.food;
        }

        private class FoodComparer : IComparer<(string, int)>
        {
            public int Compare((string, int) x, (string, int) y)
            {
                // First, compare by int value
                int compareByInt = x.Item2.CompareTo(y.Item2);

                if (compareByInt != 0)
                {
                    // If int values are not equal, return the comparison result
                    return compareByInt;
                }
                else
                {
                    // If int values are equal, compare by string value
                    return y.Item1.CompareTo(x.Item1);
                }
            }
        }
    }

    [ProblemSolution("2385")]
    public int AmountOfTime(TreeNode root, int start)
    {
        var heads = new Dictionary<TreeNode, TreeNode?>();
        heads[root] = null;

        var headQueue = new Queue<TreeNode>();
        headQueue.Enqueue(root);
        TreeNode infected = default!;
        while (headQueue.Count > 0)
        {
            var node = headQueue.Dequeue();
            if (node.val == start)
                infected = node;

            if (node.left is not null)
            {
                heads[node.left] = node;
                headQueue.Enqueue(node.left);
            }

            if (node.right is not null)
            {
                heads[node.right] = node;
                headQueue.Enqueue(node.right);
            }
        }

        var queue = new Queue<(TreeNode node, TreeNode? past, int distance)>();
        var max = 0;
        queue.Enqueue((infected, null, 0));
        while (queue.Count > 0)
        {
            var (node, past, distance) = queue.Dequeue();
            max = Math.Max(max, distance);
            foreach (var child in (TreeNode?[])[node.left, node.right, heads[node]])
            {
                if (child == past || child is null)
                    continue;
                queue.Enqueue((child, node, distance + 1));
            }
        }

        return max;
    }

    [ProblemSolution("2391")]
    public int GarbageCollection(string[] garbage, int[] travel)
    {
        var totalAmount = garbage.Select(f => f.Length).Sum();
        var lastM = 0;
        var lastP = 0;
        var lastG = 0;
        for (int i = garbage.Length - 1; i > 0; i--)
        {
            if (lastM == 0 && garbage[i].Contains('M'))
                lastM = i;
            if (lastP == 0 && garbage[i].Contains('P'))
                lastP = i;
            if (lastG == 0 && garbage[i].Contains('G'))
                lastG = i;
        }

        var travelSums = new int[travel.Length + 1];
        for (int i = 1; i < travelSums.Length; i++)
            travelSums[i] = travelSums[i - 1] + travel[i - 1];

        var timeM = travelSums[lastM];
        var timeP = travelSums[lastP];
        var timeG = travelSums[lastG];

        return totalAmount + timeM + timeP + timeG;
    }

    [ProblemSolution("2392")]
    public int[][] BuildMatrix(int k, int[][] rowConditions, int[][] colConditions)
    {
        var rowGraph = new (int top, List<int> bot)[k];
        for (int i = 0; i < k; i++)
            rowGraph[i] = (0, []);

        for (int i = 0; i < rowConditions.Length; i++)
        {
            var topCond = rowConditions[i][0] - 1;
            var botCond = rowConditions[i][1] - 1;

            rowGraph[topCond].bot.Add(botCond);
            rowGraph[botCond].top++;
        }

        var empty = new Stack<int>();
        for (int i = 0; i < rowGraph.Length; i++)
        {
            if (rowGraph[i].top == 0)
                empty.Push(i);
        }

        var rowOrder = new int[k];
        var rowInd = 0;

        while (empty.Count > 0)
        {
            var node = empty.Pop();
            rowOrder[rowInd++] = node;
            
            foreach (var other in rowGraph[node].bot)
            {
                rowGraph[other].top--;
                if (rowGraph[other].top == 0)
                    empty.Push(other);
            }
        }

        if (rowInd != k)
            return [];

        var colGraph = new (int left, List<int> right)[k];
        for (int i = 0; i < k; i++)
            colGraph[i] = (0, []);

        for (int i = 0; i < colConditions.Length; i++)
        {
            var leftCond = colConditions[i][0] - 1;
            var rightCond = colConditions[i][1] - 1;

            colGraph[leftCond].right.Add(rightCond);
            colGraph[rightCond].left++;
        }

        for (int i = 0; i < colGraph.Length; i++)
        {
            if (colGraph[i].left == 0)
                empty.Push(i);
        }

        var colOrder = new int[k];
        var colInd = 0;

        while (empty.Count > 0)
        {
            var node = empty.Pop();
            colOrder[node] = colInd++;

            foreach (var other in colGraph[node].right)
            {
                colGraph[other].left--;
                if (colGraph[other].left == 0)
                    empty.Push(other);
            }
        }

        if (colInd != k)
            return [];

        var result = new int[k][];
        for (int i = 0; i < k; i++)
            result[i] = new int[k];

        for (int i = 0; i < k; i++)
        {
            var ni = i;
            var nj = colOrder[rowOrder[i]];

            result[ni][nj] = rowOrder[i] + 1;
        }

        return result;
    }
}