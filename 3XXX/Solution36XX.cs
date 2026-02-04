namespace LeetCode.Set3XXX;

public class Solution36XX
{
    [ProblemSolution("3637")]
    public bool IsTrionic(int[] nums)
    {
        if (nums[1] <= nums[0])
            return false;

        var changes = 0;
        var dir = 1;
        
        for (var i = 2; i < nums.Length; i++)
        {
            var cur = nums[i] - nums[i - 1];
            if (cur == 0)
                return false;

            if (Math.Sign(cur) != dir)
            {
                (changes, dir) = (changes + 1, -dir);
                if (changes > 2)
                    return false;
            }
        }

        return changes == 2;
    }
}