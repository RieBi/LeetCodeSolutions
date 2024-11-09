using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Set3XXX;
internal class Solution31XX
{
    [ProblemSolution("3133")]
    public long MinEnd(int n, int x)
    {
        n--;
        long result = x;

        var mask = 1L;
        while (n > 0)
        {
            while ((result & mask) != 0)
                mask <<= 1;

            if (n % 2 == 1)
                result |= mask;

            n >>= 1;
            mask <<= 1;
        }

        return result;
    }
}
