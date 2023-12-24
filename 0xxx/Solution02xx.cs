namespace LeetCode.Set0xxx;
internal class Solution02xx
{
    [ProblemSolution("202")]
    public bool IsHappy(int n)
    {
        var set = new HashSet<int>();
        while (n != 1)
        {
            set.Add(n);
            n = (int) n.ToString()
                .Select(f => Math.Pow(int.Parse(f.ToString()), 2))
                .Sum();
            if (set.Contains(n))
                return false;
        }

        return true;
    }

    [ProblemSolution("217")]
    public bool ContainsDuplicate(int[] nums)
    {
        return nums.Distinct().Count() != nums.Length;
    }

    [ProblemSolution("219")]
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        return nums
            .Zip(Enumerable.Range(0, nums.Length))
            .GroupBy(f => f.First, f => f.Second)
            .Select(f => f.OrderBy(f => f))
            .Any(s =>
            {
                var min = int.MaxValue;
                s.Skip(1).Aggregate(s.First(), (a, b) =>
                {
                    var diff = Math.Abs(a - b);
                    min = Math.Min(min, diff);
                    return b;
                });
                return min <= k;
            });
    }

    [ProblemSolution("242")]
    public bool IsAnagram(string s, string t)
    {
        var dicks = new Dictionary<char, int>();
        var dickt = new Dictionary<char, int>();

        FillDick(s, dicks);
        FillDick(t, dickt);

        return DicksEqual(dicks, dickt);

        void FillDick(string str, Dictionary<char, int> dick)
        {
            foreach (var v in str)
            {
                if (dick.TryGetValue(v, out int val))
                    dick[v] = val + 1;
                else
                    dick[v] = 1;
            }
        }

        bool DicksEqual(Dictionary<char, int> dick1, Dictionary<char, int> dick2)
        {
            if (dick1.Count != dick2.Count)
                return false;

            foreach (var v in dick1.Keys)
            {
                if (!(dick1.TryGetValue(v, out int val1) && dick2.TryGetValue(v, out int val2)))
                    return false;
                else if (val1 != val2)
                    return false;
            }

            return true;
        }
    }
}
