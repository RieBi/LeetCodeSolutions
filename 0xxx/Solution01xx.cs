namespace LeetCode.Set0xxx;
internal class Solution01xx
{
    [ProblemSolution("136")]
    public int SingleNumber(int[] nums)
    {
        return nums.Aggregate((a, b) => a^b);
    }

    [ProblemSolution("155")]
    public class MinStack
    {
        private readonly List<(int val, int min)> list = [];

        public void Push(int val) => list.Add((val, list.Count == 0 ? val : Math.Min(val, list[^1].min)));

        public void Pop() => list.RemoveAt(list.Count - 1);

        public int Top() => list[^1].val;

        public int GetMin() => list[^1].min;
    }

    [ProblemSolution("191")]
    public int HammingWeight(uint n)
    {
        var result = 0;
        uint mask = 1;
        for (int i = 0; i < 32; i++)
        {
            if ((n & mask) == mask)
                result++;
            mask <<= 1;
        }

        return result;
    }
}