namespace LeetCode.Set3XXX;

public class Solution35XX
{
    [ProblemSolution("3507")]
    public int MinimumPairRemoval(int[] nums)
    {
        var list = nums.ToList();
        var ops = 0;
        
        while (!isNonDecreasing())
        {
            ops++;
            var min = 1;
            var minValue = list[0] + list[1];

            for (var i = 2; i < list.Count; i++)
            {
                if (list[i] + list[i - 1] < minValue)
                {
                    min = i;
                    minValue = list[i] + list[i - 1];
                }
            }

            list[min - 1] = minValue;
            list.RemoveAt(min);
        }

        return ops;

        bool isNonDecreasing()
        {
            for (var i = 1; i < list.Count; i++)
            {
                if (list[i] < list[i - 1])
                    return false;
            }

            return true;
        }
    }
}