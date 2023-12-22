using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode._0xxx;
internal class Solution07xx
{
    [ProblemSolution("704")]
    public int Search(int[] nums, int target)
    {
        var low = 0;
        var high = nums.Length - 1;
        while (low <= high)
        {
            var mid = (high + low) / 2;
            var num = nums[mid];
            if (num == target)
                return mid;
            else if (num > target)
                high = mid - 1;
            else
                low = mid + 1;
        }

        return -1;
    }
}
