namespace LeetCode.Set0xxx;
internal class Solution00xx
{
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
    public ListNode AddTwoNumbers(ListNode? l1, ListNode? l2)
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

            l2 = l1?.next!;
            l2 = l2?.next!;

        }
        if (remainder != 0)
        {
            lnew.next = new ListNode();
            lnew.next.val = remainder;
        }

        return lstart.next!;
    }

    [ProblemSolution("3")]
    public int LengthOfLongestSubstring(string s)
    {
        var maxSubstring = 0;
        var curStart = 0;
        var charsIndices = new Dictionary<char, int>();
        for (int i = 0; i < s.Length; i++)
        {
            var valueExists = charsIndices.TryGetValue(s[i], out int index);
            if (valueExists && index >= curStart)
                curStart = index + 1;
            
            charsIndices[s[i]] = i;
            maxSubstring = Math.Max(maxSubstring, i - curStart + 1);
        }

        return maxSubstring;
    }

    [ProblemSolution("4")]
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        if (nums1.Length > nums2.Length)
            (nums1, nums2) = (nums2, nums1);

        var m = nums1.Length;
        var n = nums2.Length;
        var left = 0;
        var right = m;

        while (left <= right)
        {
            var partitionA = (left + right) / 2;
            var partitionB = (m + n + 1) / 2 - partitionA;

            var maxLeftA = (partitionA == 0) ? int.MinValue : nums1[partitionA - 1];
            var minRightA = (partitionA == m) ? int.MaxValue : nums1[partitionA];
            var maxLeftB = (partitionB == 0) ? int.MinValue : nums2[partitionB - 1];
            var minRightB = (partitionB == n) ? int.MaxValue : nums2[partitionB];

            if (maxLeftA <= minRightB && maxLeftB <= minRightA)
            {
                if ((m + n) % 2 == 0)
                {
                    return ((double)Math.Max(maxLeftA, maxLeftB) + (double)Math.Min(minRightA, minRightB)) / 2;
                }
                else
                {
                    return Math.Max(maxLeftA, maxLeftB);
                }
            }
            else if (maxLeftA > minRightB)
            {
                right = partitionA - 1;
            }
            else
            {
                left = partitionA + 1;
            }
        }

        return 0;
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

    [ProblemSolution("11")]
    public int MaxArea(int[] height)
    {
        var max = 0;
        var left = 0;
        var right = height.Length - 1;

        while (left < right)
        {
            var lower = Math.Min(height[left], height[right]);
            var area = (right - left) * lower;
            max = Math.Max(max, area);

            if (height[left] <= height[right])
                left++;
            else
                right--;
        }

        return max;
    }

    [ProblemSolution("94")]
    public IList<int> InorderTraversal(TreeNode root)
    {
        var result = new List<int>();
        if (root == null)
            return result;

        var stack = new Stack<TreeNode>();
        var inStack = new HashSet<TreeNode>();
        stack.Push(root);
        inStack.Add(root);


        while (stack.Count > 0)
        {
            var top = stack.Peek();
            if (top.left is not null && !inStack.Contains(top.left))
            {
                stack.Push(top.left);
                inStack.Add(top.left);
            }
            else
            {
                result.Add(top.val);
                stack.Pop();
                if (top.right is not null)
                {
                    stack.Push(top.right);
                    inStack.Add(top.right);
                }
            }
        }

        return result;
    }
}