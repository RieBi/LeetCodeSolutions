namespace LeetCode.Set0xxx;
internal class Solution01xx
{
    [ProblemSolution("136")]
    public int SingleNumber(int[] nums)
    {
        return nums.Aggregate((a, b) => a^b);
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