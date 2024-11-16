namespace LeetCode.Set3XXX;
internal class Solution32XX
{
    [ProblemSolution("3217")]
    public ListNode ModifiedList(int[] nums, ListNode head)
    {
        var set = nums.ToHashSet();

        while (set.Contains(head.val))
            head = head.next!;

        var cur = head;
        while (cur is not null)
        {
            while (cur.next is not null && set.Contains(cur.next.val))
                cur.next = cur.next.next;

            cur = cur.next;
        }

        return head;
    }

    [ProblemSolution("3254")]
    public int[] ResultsArray(int[] nums, int k)
    {
        if (k == 1)
            return nums;

        var result = new int[nums.Length - k + 1];

        var dequeue = new (int ind, int val)[k];
        var start = 0;
        var count = 0;

        var streak = 1;
        pushEnd(0, nums[0]);

        for (int i = 1; i + 1 < k; i++)
        {
            pushEnd(i, nums[i]);

            if (nums[i] == nums[i - 1] + 1)
                streak++;
            else
                streak = 1;
        }

        for (int i = k - 1; i < nums.Length; i++)
        {
            if (peekStart().ind == i - k)
                popStart();

            while (count > 0 && peekEnd().val <= nums[i])
                popEnd();

            pushEnd(i, nums[i]);

            if (nums[i] == nums[i - 1] + 1)
                streak++;
            else
                streak = 1;

            if (streak >= k)
                result[i - k + 1] = peekStart().val;
            else
                result[i - k + 1] = -1;
        }

        return result;

        void pushEnd(int ind, int val)
        {
            dequeue[(start + count) % dequeue.Length] = (ind, val);
            count++;
        }

        (int ind, int val) peekEnd() => dequeue[(start + count - 1) % dequeue.Length];

        void popEnd() => count--;

        (int ind, int val) peekStart() => dequeue[start];

        void popStart() => (start, count) = ((start + 1) % dequeue.Length, count - 1);
    }
}