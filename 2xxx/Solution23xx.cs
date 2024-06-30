using System.ComponentModel.DataAnnotations;

namespace LeetCode.Set2XXX;
internal class Solution23XX
{
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
}