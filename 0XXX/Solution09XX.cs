namespace LeetCode.Set0XXX;
internal class Solution09XX
{
    [ProblemSolution("907")]
    public int SumSubarrayMins(int[] arr)
    {
        var left = new int[arr.Length];
        var right = new int[arr.Length];

        var stack = new Stack<int>();
        for (int i = 0; i < arr.Length; i++)
        {
            left[i] = -1;
            right[i] = arr.Length;
            while (stack.Count > 0 && arr[stack.Peek()] > arr[i])
            {
                var popped = stack.Pop();
                right[popped] = i;
            }

            if (stack.Count > 0)
                left[i] = stack.Peek();
            stack.Push(i);
        }

        var total = 0L;
        var modulo = 1_000_000_007;

        for (int i = 0; i < arr.Length; i++)
            total += (long)arr[i] * (i - left[i]) * (right[i] - i);

        return (int)(total % modulo);
    }

    [ProblemSolution("912")]
    public int[] SortArray(int[] nums)
    {
        for (int i = nums.Length / 2; i >= 0; i--)
            heapify(i, nums.Length - 1);

        for (int last = nums.Length - 1; last > 0; last--)
        {
            (nums[0], nums[last]) = (nums[last], nums[0]);
            heapify(0, last - 1);
        }

        return nums;

        void heapify(int start, int end)
        {
            var max = start;
            do
            {
                start = max;

                var left = max * 2 + 1;
                var right = max * 2 + 2;

                if (left <= end && nums[left] > nums[max])
                    max = left;
                if (right <= end && nums[right] > nums[max])
                    max = right;

                if (start != max)
                    (nums[start], nums[max]) = (nums[max], nums[start]);
            }
            while (start != max);
        }
    }

    [ProblemSolution("921")]
    public int MinAddToMakeValid(string s)
    {
        var result = 0;
        var cur = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
                cur++;
            else if (cur > 0)
                cur--;
            else
                result++;
        }

        return result + cur;
    }

    [ProblemSolution("931")]
    public int MinFallingPathSum(int[][] matrix)
    {
        List<(int i, int j)> dirs = [(-1, -1), (-1, 0), (-1, 1)];

        var vals = new List<int>();
        for (int i = 1; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix.Length; j++)
            {
                foreach (var dir in dirs)
                {
                    var newi = dir.i + i;
                    var newj = dir.j + j;
                    if (newi < 0 || newj < 0 || newi >= matrix.Length || newj >= matrix.Length)
                        continue;
                    vals.Add(matrix[i][j] + matrix[newi][newj]);
                }

                matrix[i][j] = vals.Min();
                vals.Clear();
            }
        }

        return matrix[^1].Min();
    }

    [ProblemSolution("938")]
    public int RangeSumBST(TreeNode root, int low, int high)
    {
        var sum = 0;
        propagate(root);

        void propagate(TreeNode? node)
        {
            if (node == null)
                return;

            if (node.val >= low && node.val <= high)
                sum += node.val;

            propagate(node.left);
            propagate(node.right);
        }

        return sum;
    }

    [ProblemSolution("945")]
    public int MinIncrementForUnique(int[] nums)
    {
        Array.Sort(nums);
        var min = nums[0];
        var steps = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] < min)
            {
                steps += min - nums[i];
                nums[i] = min;
            }

            min = nums[i] + 1;
        }

        return steps;
    }

    [ProblemSolution("947")]
    public int RemoveStones(int[][] stones)
    {
        var remove = 0;

        var rows = new Dictionary<int, HashSet<int>>();
        var cols = new Dictionary<int, HashSet<int>>();
        
        for (int i = 0; i < stones.Length; i++)
        {
            if (rows.TryGetValue(stones[i][1], out var val))
                val.Add(stones[i][0]);
            else
                rows[stones[i][1]] = [stones[i][0]];

            if (cols.TryGetValue(stones[i][0], out val))
                val.Add(stones[i][1]);
            else
                cols[stones[i][0]] = [stones[i][1]];
        }

        while (rows.Count > 0)
        {
            var first = rows.First();
            (int x, int y) firstStone = (first.Value.First(), first.Key);

            var total = 0;

            var queue = new Queue<(int x, int y)>();
            queue.Enqueue(firstStone);

            var col = cols[firstStone.x];
            col.Remove(firstStone.y);
            if (col.Count == 0)
                cols.Remove(firstStone.x);

            var row = rows[firstStone.y];
            row.Remove(firstStone.x);
            if (row.Count == 0)
                rows.Remove(firstStone.y);

            while (queue.Count > 0)
            {
                var last = queue.Dequeue();

                if (rows.TryGetValue(last.y, out var sameRow))
                {
                    total += sameRow.Count;
                    foreach (var otherX in sameRow)
                    {
                        var other = (otherX, last.y);
                        cols[other.otherX].Remove(other.y);

                        queue.Enqueue(other);
                    }

                    rows.Remove(last.y);
                }

                if (cols.TryGetValue(last.x, out var sameCol))
                {
                    total += sameCol.Count;
                    foreach (var otherY in sameCol)
                    {
                        var other = (last.x, otherY);
                        rows[other.otherY].Remove(other.x);

                        queue.Enqueue(other);
                    }

                    cols.Remove(last.x);
                }
            }

            remove += total;
        }

        return remove;
    }

    [ProblemSolution("950")]
    public int[] DeckRevealedIncreasing(int[] deck)
    {
        Array.Sort(deck);
        var result = new int[deck.Length];

        for (int i = 0; i < deck.Length; i += 2)
            result[i] = deck[i / 2];

        for (int i = 1; i < deck.Length; i += 2)
        {
            var k = i;
            while (k % 2 == 1)
                k += (deck.Length - (k / 2 + 1));

            result[i] = deck[k / 2];
        }

        return result;
    }

    [ProblemSolution("959")]
    public int RegionsBySlashes(string[] grid)
    {
        var state = new byte[grid.Length][];
        for (int i = 0; i < state.Length; i++)
            state[i] = new byte[grid[0].Length];

        var total = 0;

        for (int i = 0; i < state.Length; i++)
        {
            for (int j = 0; j < state[0].Length; j++)
            {
                if ((state[i][j] & 1) == 0)
                {
                    total++;
                    propagate(i, j, 3);
                }

                if ((state[i][j] & 2) == 0)
                {
                    total++;
                    propagate(i, j, 1);
                }
            }
        }

        return total;

        void propagate(int i, int j, byte dir)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length)
                return;

            var ch = grid[i][j];
            var num = state[i][j];
            bool on;

            if (ch == '/')
            {
                if (dir is 0 or 3)
                    on = (num & 1) > 0;
                else
                    on = (num & 2) > 0;
            }
            else if (ch == '\\')
            {
                if (dir is 2 or 3)
                    on = (num & 1) > 0;
                else
                    on = (num & 2) > 0;
            }
            else
                on = num > 0;

            if (on)
                return;

            if (ch == '/')
            {
                if (dir is 0 or 3)
                {
                    state[i][j] |= 1;
                    propagate(i - 1, j, 2);
                    propagate(i, j - 1, 1);
                }
                else
                {
                    state[i][j] |= 2;
                    propagate(i + 1, j, 0);
                    propagate(i, j + 1, 3);
                }
            }
            else if (ch == '\\')
            {
                if (dir is 2 or 3)
                {
                    state[i][j] |= 1;
                    propagate(i + 1, j, 0);
                    propagate(i, j - 1, 1);
                }
                else
                {
                    state[i][j] |= 2;
                    propagate(i - 1, j, 2);
                    propagate(i, j + 1, 3);
                }
            }
            else
            {
                state[i][j] = 3;
                propagate(i + 1, j, 0);
                propagate(i, j - 1, 1);
                propagate(i - 1, j, 2);
                propagate(i, j + 1, 3);
            }
        }
    }

    [ProblemSolution("974")]
    public int SubarraysDivByK(int[] nums, int k)
    {
        var hash = new Dictionary<int, int>();
        hash[0] = 1;
        var mod = 0;
        var total = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            mod = ((mod + nums[i] % k) + k) % k;
            if (hash.TryGetValue(mod, out var value))
            {
                total += value;
                hash[mod] = value + 1;
            }
            else
                hash[mod] = 1;
        }

        return total;
    }

    [ProblemSolution("995")]
    public int MinKBitFlips(int[] nums, int k)
    {
        var switches = 0;
        var queue = new Queue<int>();
        for (int i = 0; i + k - 1 < nums.Length; i++)
        {
            while (queue.Count > 0 && queue.Peek() <= i - k)
                queue.Dequeue();

            var num = (nums[i] + queue.Count) % 2;
            if (num == 1)
                continue;

            switches++;
            queue.Enqueue(i);
        }

        for (int i = nums.Length - k + 1; i < nums.Length; i++)
        {
            while (queue.Count > 0 && queue.Peek() <= i - k)
                queue.Dequeue();

            if ((nums[i] + queue.Count) % 2 == 0)
                return -1;
        }

        return switches;
    }

    [ProblemSolution("997")]
    public int FindJudge(int n, int[][] trust)
    {
        var trustee = new int[n + 1];
        var trusted = new int[n + 1];

        foreach (var relation in trust)
        {
            trustee[relation[0]]++;
            trusted[relation[1]]++;
        }

        for (int i = 1; i < trustee.Length; i++)
            if (trustee[i] == 0 && trusted[i] == n - 1)
                return i;

        return -1;
    }
}
