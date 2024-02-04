using System.Text;

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

    [ProblemSolution("5")]
    public string LongestPalindrome(string s)
    {
        var maxLen = 1;
        string maxStr = s[0].ToString();
        for (int i = 0; i < s.Length; i++) // 'babad'
        {
            var c = s[i];
            int start = i;
            int end = i;
            while (end + 1 < s.Length && s[end + 1] == c)
                end++;
            int endSame = end;
            while (start > 0 && end + 1 < s.Length)
            {
                if (s[start - 1] == s[end + 1])
                {
                    start--;
                    end++;
                }
                else
                    break;
            }

            var len = end - start + 1;
            if (len > maxLen)
            {
                maxLen = len;
                maxStr = s.Substring(start, len);
            }
            i = endSame;
        }

        return maxStr;
    }

    [ProblemSolution("6")]
    public string Convert(string s, int numRows)
    {
        if (numRows == 1)
            return s;

        var lists = new List<char>[numRows];
        for (int i = 0; i < numRows; i++)
            lists[i] = [];

        var row = 0;
        var adder = 1;
        foreach (var ch in s)
        {
            lists[row].Add(ch);
            if (row == 0)
                adder = 1;
            else if (row == numRows - 1)
                adder = -1;

            row += adder;
        }

        var str = new StringBuilder();
        foreach (var list in lists)
            foreach (var ch in list)
                str.Append(ch);

        return str.ToString();
    }

    [ProblemSolution("7")]
    public int Reverse(int x)
    {
        var ar = x.ToString().ToCharArray();
        Array.Reverse(ar);
        var v = new string(ar);
        try
        {
            if (v[v.Length - 1] == '-')
                return -int.Parse(v.Substring(0, v.Length - 1));
            else
                return int.Parse(v);
        }
        catch (System.OverflowException)
        {
            return 0;
        }
    }

    [ProblemSolution("8")]
    public int MyAtoi(string s)
    {
        s = s.TrimStart();
        var num = 0;
        if (s.Length == 0)
            return 0;

        var multiplier = 1;
        if (s[0] == '-')
            multiplier = -1;

        var max = multiplier == 1 ? int.MaxValue : int.MinValue;
        var preMax = Math.Abs(max / 10);
        var last = Math.Abs(max % 10);

        var pos = s[0] is '+' or '-' ? 1 : 0;
        for (; pos < s.Length && char.IsDigit(s[pos]); pos++)
        {
            var toAdd = int.Parse(s[pos].ToString());
            if (Math.Abs(num) > preMax || Math.Abs(num) == preMax && toAdd >= last)
                return max;
            num *= 10;
            num += toAdd * multiplier;
        }

        return num;
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

    [ProblemSolution("15")]
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var frequencies = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (frequencies.TryGetValue(num, out int value))
                frequencies[num] = value + 1;
            else
                frequencies[num] = 1;
        }

        var sorted = nums.Distinct().ToList();
        sorted.Sort();

        var positiveInd = sorted.BinarySearch(0);
        if (positiveInd < 0)
            positiveInd = ~positiveInd;

        var result = new List<IList<int>>();
        for (int i = 0; i < positiveInd; i++)
        {
            for (int j = positiveInd; j < sorted.Count; j++)
            {
                var needed = -(sorted[i] + sorted[j]);
                if (!frequencies.TryGetValue(needed, out int value))
                    continue;

                if ((needed < 0 && needed < sorted[i]) || (needed >= 0 && needed < sorted[j]))
                    continue;

                if ((needed == sorted[i] || needed == sorted[j]) && value == 1)
                    continue;

                List<int> list = [sorted[i], sorted[j], needed];
                result.Add(list);
            }
        }

        if (frequencies.TryGetValue(0, out int zeroes) && zeroes >= 3)
            result.Add([0, 0, 0]);

        return result;
    }

    [ProblemSolution("20")]
    public bool IsValid(string s)
    {
        var opening = new HashSet<char> { '(', '{', '[' };
        var closeToOpen = new Dictionary<char, char> { { ')', '(' }, { '}', '{' }, { ']', '[' } };
        var stack = new Stack<char>();
        foreach (var ch in s)
        {
            if (opening.Contains(ch))
                stack.Push(ch);
            else if (stack.Count == 0 || closeToOpen[ch] != stack.Pop())
                return false;
        }

        return stack.Count == 0;
    }

    [ProblemSolution("23")]
    public ListNode? MergeKLists(ListNode?[] lists)
    {
        var queue = new PriorityQueue<int, int>();
        ListNode? myHead = null;
        ListNode? myStartHead = null;

        for (int i = 0; i < lists.Length; i++)
        {
            if (lists[i] != null)
                queue.Enqueue(i, lists[i]!.val);
        }

        while (queue.Count > 0)
        {
            var listInd = queue.Dequeue();
            if (myHead is null)
                myHead = lists[listInd];
            else
            {
                myHead.next = lists[listInd];
                myHead = myHead.next;
            }

            if (myStartHead is null)
                myStartHead = myHead;

            lists[listInd] = lists[listInd]!.next;
            if (lists[listInd] is not null)
                queue.Enqueue(listInd, lists[listInd]!.val);
        }

        return myStartHead;
    }

    [ProblemSolution("24")]
    public ListNode? SwapPairs(ListNode? head)
    {
        return swap(head);
        ListNode? swap(ListNode? node)
        {
            if (node is null || node.next is null)
                return node;

            var after = swap(node.next.next);
            var newh = node.next;
            node.next.next = node;
            node.next = after;

            return newh;
        }
    }

    [ProblemSolution("36")]
    public bool IsValidSudoku(char[][] board)
    {
        var rowsValid = board.All(f =>
        {
            var filled = f.Where(k => k != '.');
            return filled.Count() == filled.Distinct().Count();
        });

        var colsValid = Enumerable.Range(0, 9).All(f =>
        {
            var set = new HashSet<char>();
            for (int i = 0; i < 9; i++)
            {
                if (set.Contains(board[i][f]))
                    return false;
                if (board[i][f] != '.')
                    set.Add(board[i][f]);
            }

            return true;
        });

        var regionsValid = () => {
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    var set = new HashSet<char>();
                    for (int di = i; di < i + 3; di++)
                    {
                        for (int dj = j; dj < j + 3; dj++)
                        {
                            if (set.Contains(board[di][dj]))
                                return false;
                            if (board[di][dj] != '.')
                                set.Add(board[di][dj]);
                        }
                    }
                }
            }

            return true;
        };

        return rowsValid && colsValid && regionsValid();
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

    [ProblemSolution("70")]
    public int ClimbStairs(int n)
    {
        var fc = new List<int>();
        fc.Add(1);
        fc.Add(2);
        for (int i = 2; i < n; i++)
            fc.Add(fc[i - 1] + fc[i - 2]);

        return fc[n - 1];
    }

    [ProblemSolution("76")]
    public string MinWindow(string s, string t)
    {
        var minSize = int.MaxValue;
        var minL = -1;
        var minR = 0;

        var dickT = new Dictionary<char, int>();
        var dickS = new Dictionary<char, int>();

        foreach (var ch in t)
        {
            dickS[ch] = 0;
            if (dickT.TryGetValue(ch, out int value))
                dickT[ch] = value + 1;
            else
                dickT[ch] = 1;
        }

        var left = 0;
        var right = -1;
        var len = 0;
        var requiredLen = dickT.Values.Sum();
        
        while (right < s.Length)
        {
            if (len == requiredLen && (!dickS.ContainsKey(s[left]) || dickS[s[left]] > dickT[s[left]]))
            {
                if (dickS.TryGetValue(s[left], out int value) && value > dickT[s[left]])
                    dickS[s[left]] = value - 1;
                left++;
            }
            else
            {
                right++;
                if (right == s.Length)
                    break;
                if (dickS.TryGetValue(s[right], out int value))
                {
                    dickS[s[right]] = value + 1;
                    if (value < dickT[s[right]])
                        len++;
                }
            }

            if (len == requiredLen && (right - left < minSize))
            {
                minSize = right - left;
                minL = left;
                minR = right;
            }
        }

        return minL == -1 ? "" : s.Substring(minL, minR - minL + 1);
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
        var stack = new Stack<TreeNode>();
        var result = new List<int>();

        TreeNode? node = root;

        while (stack.Count > 0 || node is not null)
        {
            while (node is not null)
            {
                stack.Push(node);
                node = node.left;
            }

            var top = stack.Pop();
            result.Add(top.val);
            node = top.right;
        }

        return result;
    }
}