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

    [ProblemSolution("49")]
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        var groups = strs
            .GroupBy(f => new string(f.Order().ToArray()))
            .Select(f => (IList<string>)f.ToList())
            .ToList();
        return groups;
    }

    [ProblemSolution("91")]
    public int NumDecodings(string s)
    {
        var dp = new int[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            var sum = 0;
            var oneInd = i - 1;
            var oneNum = oneInd == -1 ? 1 : dp[oneInd];
            if (int.TryParse(s.Substring(i, 1), out int n1) && n1 >= 1 && n1 <= 26)
                sum += oneNum;

            var twoInd = i - 2;
            var twoNum = twoInd < 0 ? 1 : dp[twoInd];
            if (i > 0 && int.TryParse(s.Substring(i - 1, 2), out int n2) && s[i - 1] != '0' && n2 >= 1 && n2 <= 26)
                sum += twoNum;

            dp[i] = sum;
        }

        return dp.Last();
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