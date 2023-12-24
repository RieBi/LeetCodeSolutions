namespace LeetCode.Set0xxx;
internal class Solution05xx
{
    [ProblemSolution("599")]
    public string[] FindRestaurant(string[] list1, string[] list2)
    {
        var commons = list1.Zip(Enumerable.Range(0, list1.Length))
            .Concat(list2.Zip(Enumerable.Range(0, list2.Length)))
            .GroupBy(f => f.First)
            .Where(f => f.Count() == 2);

        var min = commons.Select(f => f.First().Second + f.Last().Second).Min();
        return commons.Where(f => f.First().Second + f.Last().Second == min)
            .Select(f => f.Key)
            .ToArray();
    }
}
