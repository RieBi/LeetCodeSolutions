namespace LeetCode.Set0xxx;
internal class Solution00xx
{
    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    [ProblemSolution("1")]
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> dick = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            int value;
            if (dick.TryGetValue(nums[i], out value) && value != i)
                return new int[] { i, value };
            dick[target - nums[i]] = i;

        }

        return new int[0];
    }

    [ProblemSolution("2")]
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        int remainder = 0;

        var lnew = new ListNode();
        var lstart = lnew;
        while (l1 != null || l2 != null)
        {
            lnew.next = new ListNode();
            lnew = lnew.next;

            int num = (l1 is not null ? l1.val : 0) + (l2 is not null ? l2.val : 0);
            num += remainder;
            int sum = num % 10;
            lnew.val = sum;
            remainder = num / 10;

            if (l1 is not null)
                l1 = l1.next;
            if (l2 is not null)
                l2 = l2.next;

        }
        if (remainder != 0)
        {
            lnew.next = new ListNode();
            lnew.next.val = remainder;
        }

        return lstart.next;
    }

    [ProblemSolution("9")]
    public bool IsPalindrome(int x)
    {
        if (x < 0)
            return false;
        if (x < 10)
            return true;
        var log = (int)(Math.Log10(x) + 1);
        var ar = new int[log];

        for (int i = 0; i < ar.Length; i++)
        {
            ar[i] = x % 10;
            x /= 10;
        }

        for (int i = 0; i < ar.Length / 2; i++)
        {
            if (ar[i] != ar[ar.Length - 1 - i])
                return false;
        }

        return true;
    }
}