namespace LeetCode.Set2XXX;
internal class Solution21XX
{
    [ProblemSolution("2108")]
    public string FirstPalindrome(string[] words)
    {
        foreach (var word in words)
        {
            var left = 0;
            var right = word.Length - 1;
            for (; left < right; left++, right--)
            {
                if (word[left] != word[right])
                    break;
            }

            if (word[left] == word[right])
                return word;
        }

        return "";
    }

    [ProblemSolution("2125")]
    public int NumberOfBeams(string[] bank)
    {
        var precCount = bank[0].Count(f => f == '1');
        var sum = 0;
        for (int i = 1; i < bank.Length; i++)
        {
            var count = bank[i].Count(f => f == '1');
            if (count != 0)
            {
                sum += precCount * count;
                precCount = count;
            }
        }

        return sum;
    }

    [ProblemSolution("2134")]
    public int MinSwaps(int[] nums)
    {
        var ones = nums.Count(f => f == 1);
        if (ones == 0 || ones == nums.Length)
            return 0;

        var result = int.MaxValue;
        var sum = 0;

        for (int i = 0; i < ones - 1; i++)
            sum += nums[i];

        for (int i = 0; i < nums.Length; i++)
        {
            var end = i + ones - 1;
            sum += nums[end % nums.Length];
            if (i > 0)
                sum -= nums[i - 1];

            result = Math.Min(result, ones - sum);
        }

        return result;
    }

    [ProblemSolution("2147")]
    public int NumberOfWays(string corridor)
    {
        var result = 1L;
        var curStreak = 0;
        var plantStreak = 0;
        var modulo = 1_000_000_007L;
        for (int i = 0; i < corridor.Length; i++)
        {
            var curObject = corridor[i];
            if (curStreak != 2)
            {
                if (curObject == 'S')
                    curStreak++;
            }
            else
            {
                if (curObject == 'S')
                {
                    result *= (long)(plantStreak + 1);
                    result %= modulo;
                    curStreak = 1;
                    plantStreak = 0;
                }
                else if (curObject == 'P')
                {
                    plantStreak++;
                }
            }
        }

        if (curStreak != 2)
            result = 0;

        return (int)result;
    }

    [ProblemSolution("2149")]
    public int[] RearrangeArray(int[] nums)
    {
        var stash = new Queue<int>();
        var stash2 = new Queue<int>();
        var l = 0;
        var r = 0;

        while (l < nums.Length)
        {
            var s = l % 2 == 0 ? stash : stash2;
            if (s.Count == 0)
            {
                if (Math.Sign(nums[r]) == 1)
                    stash.Enqueue(nums[r++]);
                else
                    stash2.Enqueue(nums[r++]);
            }
            else
                nums[l++] = s.Dequeue();
        }

        return nums;
    }

    [ProblemSolution("2181")]
    public ListNode MergeNodes(ListNode head)
    {
        var zero = head;
        var node = zero;
        var sum = 0;
        while (node.next is not null)
        {
            node = node.next;
            sum += node.val;

            if (node.val == 0)
            {
                node.val = sum;
                zero.next = node;
                sum = 0;
                zero = node;
            }
        }

        return head.next;
    }

    [ProblemSolution("2191")]
    public int[] SortJumbled(int[] mapping, int[] nums)
    {
        var keys = nums
            .Select(f => toShuffled(f))
            .Zip(Enumerable.Range(0, nums.Length))
            .ToArray();

        Array.Sort(keys, nums);
        return nums;

        int toShuffled(int num)
        {
            var power = 1;
            var result = 0;

            if (num == 0)
            {
                var digit = num % 10;
                digit = mapping[digit];
                num /= 10;

                result += digit * power;
                power *= 10;
                return result;
            }

            while (num > 0)
            {
                var digit = num % 10;
                digit = mapping[digit];
                num /= 10;

                result += digit * power;
                power *= 10;
            }

            return result;
        }
    }

    [ProblemSolution("2192")]
    public IList<IList<int>> GetAncestors(int n, int[][] edges)
    {
        var ancestors = new Dictionary<int, List<int>>();
        for (int i = 0; i < edges.Length; i++)
        {
            var a = edges[i][0];
            var b = edges[i][1];

            if (ancestors.TryGetValue(b, out var value))
                value.Add(a);
            else
                ancestors[b] = [a];
        }

        var result = new List<IList<int>>();
        for (int i = 0; i < n; i++)
        {
            var list = new List<int>();
            var set = new HashSet<int>();
            if (ancestors.TryGetValue(i, out var ancList))
            {
                foreach (var el in ancList)
                {
                    list.Add(el);
                    set.Add(el);
                }

                var ind = 0;
                while (ind < list.Count)
                {
                    if (ancestors.TryGetValue(list[ind], out ancList))
                    {
                        foreach (var el in ancList)
                        {
                            if (!set.Contains(el))
                            {
                                list.Add(el);
                                set.Add(el);
                            }
                        }
                    }

                    ind++;
                }
            }

            list.Sort();
            result.Add(list);
        }

        return result;
    }

    [ProblemSolution("2196")]
    public TreeNode CreateBinaryTree(int[][] descriptions)
    {
        var nodes = new Dictionary<int, TreeNode>();

        for (int i = 0; i < descriptions.Length; i++)
        {
            var parentVal = descriptions[i][0];

            if (!nodes.TryGetValue(parentVal, out TreeNode? parent))
            {
                parent = new(parentVal);
                nodes[parentVal] = parent;
            }

            var childVal = descriptions[i][1];

            if (!nodes.TryGetValue(childVal, out TreeNode? child))
            {
                child = new(childVal);
                nodes[childVal] = child;
            }

            if (descriptions[i][2] == 1)
                parent.left = child;
            else
                parent.right = child;
        }

        for (int i = 0; i < descriptions.Length; i++)
            nodes.Remove(descriptions[i][1]);

        return nodes.Single().Value;
    }
}
