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
    
    [ProblemSolution("3661")]
    public int MaxWalls(int[] robots, int[] distance, int[] walls)
    {
        var sorted = new List<(bool isRobot, int pos, int distance, bool isMarked)>(capacity: robots.Length + walls.Length);

        var same = 0;
        var positions = new HashSet<int>(capacity: robots.Length);

        for (var i = 0; i < robots.Length; i++)
        {
            sorted.Add((isRobot: true, pos: robots[i], distance: distance[i], isMarked: false));
            positions.Add(robots[i]);
        }

        for (var i = 0; i < walls.Length; i++)
        {
            if (positions.Contains(walls[i]))
                same++;
            else
                sorted.Add((isRobot: false, pos: walls[i], distance: 0, isMarked: false));
        }

        sorted.Sort((a, b) => a.pos.CompareTo(b.pos));

        var ind = 0;

        while (ind < sorted.Count)
        {
            while (ind < sorted.Count && !sorted[ind].isRobot)
                ind++;

            if (ind == sorted.Count)
                break;

            var cover = sorted[ind].pos + sorted[ind].distance;

            ind++;

            while (ind < sorted.Count && !sorted[ind].isRobot && sorted[ind].pos <= cover)
            {
                var cur = sorted[ind];

                sorted[ind] = (false, cur.pos, 0, true);
                ind++;
            }
        }

        ind = 0;

        var prevLeft = 0;
        var prevRight = 0;

        while (ind < sorted.Count)
        {
            while (ind < sorted.Count && !sorted[ind].isRobot)
                ind++;

            if (ind == sorted.Count)
                break;
            
            var coverLeft =  sorted[ind].pos - sorted[ind].distance;

            var leftCount = 0;
            var leftFull = 0;
            var rightCount = 0;

            int j;
            for (j = ind - 1; j >= 0 && !sorted[j].isRobot && !sorted[j].isMarked &&
                                  sorted[j].pos >= coverLeft; j--)
                leftCount++;

            for (; j >= 0 && !sorted[j].isRobot && sorted[j].pos >= coverLeft; j--)
                leftFull++;

            for (j = ind + 1; j < sorted.Count && !sorted[j].isRobot && sorted[j].isMarked; j++)
                rightCount++;

            var curLeft = Math.Max(prevLeft + leftCount + leftFull, prevRight + leftCount);
            var curRight = Math.Max(prevLeft, prevRight) + rightCount;

            (prevLeft, prevRight) = (curLeft, curRight);

            ind++;
        }
        
        return Math.Max(prevLeft, prevRight) + same;
    }
}