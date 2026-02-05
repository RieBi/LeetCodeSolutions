namespace LeetCode.Set3XXX;

public class Solution33XX
{
    [ProblemSolution("3314")]
    public int[] MinBitwiseArray(IList<int> nums)
    {
        var results = new int[nums.Count];
        
        for (var i = 0; i < nums.Count; i++)
        {
            var num = nums[i];

            if (num == 2)
            {
                results[i] = -1;
                continue;
            }

            var cur = 1;

            while ((cur & num) > 0)
                cur <<= 1;

            var result = num ^ (cur >> 1);
            results[i] = result;
        }

        return results;
    }
    
    [ProblemSolution("3379")]
    public int[] ConstructTransformedArray(int[] nums)
    {
        var results = new int[nums.Length];

        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0)
                continue;
            
            var ind = ((nums[i] + i) % nums.Length + nums.Length) % nums.Length;
            results[i] = nums[ind];
        }

        return results;
    }
}