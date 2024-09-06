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
}