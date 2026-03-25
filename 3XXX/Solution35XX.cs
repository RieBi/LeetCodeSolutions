using System.Linq.Expressions;

namespace LeetCode.Set3XXX;

public class Solution35XX
{
    [ProblemSolution("3507")]
    public int MinimumPairRemoval1(int[] nums)
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
    
    [ProblemSolution("3510")]
    public int MinimumPairRemoval2(int[] nums)
    {
        var breaks = 0;
        var set = new PriorityQueue<LinkedListNode<(long value, int position)>, (long sum, int position)>();
        var linked = new LinkedList<(long value, int position)>();

        linked.AddLast((nums[0], 0));

        for (var i = 1; i < nums.Length; i++)
        {
            if (nums[i] < nums[i - 1])
                breaks++;

            var last = linked.AddLast((nums[i], i));

            set.Enqueue(last, ((long)nums[i] + nums[i - 1], i));
        }
        
        var ops = 0;
        
        while (breaks > 0)
        {
            ops++;

            set.TryDequeue(out var element, out var priority);

            while (element?.Previous is null || (element.Value.value + element.Previous.Value.value != priority.sum))
                set.TryDequeue(out element, out priority);

            var prevBreaks = 0;
            var afterBreaks = 0;

            var curNode = element;
            var nextNode = curNode.Next;

            var curNodeValue = curNode.Value.value;

            var firstNode = curNode.Previous!;
            var prevNode = firstNode.Previous;

            var firstNodeValue = firstNode.Value.value;
            if (curNodeValue < firstNodeValue)
                prevBreaks++;
            
            var sum = firstNodeValue + curNodeValue;

            var curNodePosition = curNode.Value.position;

            if (prevNode is not null)
            {
                var prevNodeValue = prevNode.Value.value;
                
                set.Enqueue(curNode, (sum + prevNodeValue, curNodePosition));

                if (firstNodeValue < prevNodeValue)
                    prevBreaks++;

                if (sum < prevNodeValue)
                    afterBreaks++;
            }

            if (nextNode is not null)
            {
                var nextNodeValue = nextNode.Value.value;
                var nextNodePosition = nextNode.Value.position;
                
                set.Enqueue(nextNode, (sum + nextNodeValue, nextNodePosition));

                if (nextNodeValue < curNodeValue)
                    prevBreaks++;

                if (nextNodeValue < sum)
                    afterBreaks++;
            }

            linked.Remove(firstNode);

            curNode.Value = (sum, curNodePosition);

            breaks += (afterBreaks - prevBreaks);
        }

        return ops;
    }
    
    [ProblemSolution("3546")]
    public bool CanPartitionGrid(int[][] grid)
    {
        var total = grid.Sum(row => (long)row.Sum(el => (long)el));

        var cur = 0L;
        for (var i = grid.Length - 1; i >= 0; i--)
        {
            cur += grid[i].Sum(el => (long)el);

            if (cur * 2 == total)
                return true;
        }

        cur = 0L;
        for (var i = grid[0].Length - 1; i >= 0; i--)
        {
            cur += grid.Sum(row => (long)row[i]);

            if (cur * 2 == total)
                return true;
        }

        return false;
    }
    
    [ProblemSolution("3567")]
    public int[][] MinAbsDiff(int[][] grid, int k)
    {
        var result = new int[grid.Length - k + 1][];

        for (var i = 0; i < result.Length; i++)
            result[i] = new int[grid[0].Length - k + 1];

        for (var i = 0; i + k <= grid.Length; i++)
        {
            for (var j = 0; j + k <= grid[0].Length; j++)
            {
                var list = new List<int>(capacity: k * k);

                for (var innerI = i; innerI < i + k; innerI++)
                {
                    for (var  innerJ = j; innerJ < j + k; innerJ++)
                        list.Add(grid[innerI][innerJ]);
                }

                list.Sort();

                var value = int.MaxValue;

                for (var ind = 1; ind < list.Count; ind++)
                {
                    var diff = list[ind] - list[ind - 1];

                    if (diff != 0)
                        value = Math.Min(value, Math.Abs(diff));
                }

                if (value == int.MaxValue)
                    value = 0;
                
                result[i][j] = value;
            }
        }

        return result;
    }
}