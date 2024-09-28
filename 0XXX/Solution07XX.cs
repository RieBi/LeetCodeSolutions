using System.Text;

namespace LeetCode.Set0XXX;
internal class Solution07XX
{
    [ProblemSolution("700")]
    public TreeNode? SearchBST(TreeNode? root, int val)
    {
        while (root is not null)
        {
            if (root.val == val)
                return root;
            else if (root.val < val)
                root = root.right;
            else
                root = root.left;
        }

        return root;
    }

    [ProblemSolution("701")]
    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root is null)
            return new TreeNode(val);

        var node = root;
        while (node.val != val)
        {
            if (val > node.val)
            {
                if (node.right is null)
                    node.right = new TreeNode(val);
                else
                    node = node.right;
            }
            else
            {
                if (node.left is null)
                    node.left = new TreeNode(val);
                else
                    node = node.left;
            }
        }

        return root;
    }

    [ProblemSolution("703")]
    public class KthLargest
    {
        private readonly int k;
        private readonly PriorityQueue<int, int> list = new();

        public KthLargest(int k, int[] nums)
        {
            this.k = k;
            foreach (var v in nums)
                Add(v);
        }

        public int Add(int val)
        {
            if (list.Count < k)
            {
                list.Enqueue(val, val);
            }
            else if (val > list.Peek())
            {
                list.DequeueEnqueue(val, val);
            }

            if (list.Count >= k)
                return list.Peek();
            else
                return -1;
        }
    }

    [ProblemSolution("704")]
    public int Search(int[] nums, int target)
    {
        var low = 0;
        var high = nums.Length - 1;
        while (low <= high)
        {
            var mid = (high + low) / 2;
            var num = nums[mid];
            if (num == target)
                return mid;
            else if (num > target)
                high = mid - 1;
            else
                low = mid + 1;
        }

        return -1;
    }

    [ProblemSolution("705")]
    public class MyHashSet
    {
        private int capacity = 7;
        private int count = 0;
        private List<int>?[] arr;

        public MyHashSet()
        {
            arr = new List<int>[capacity];
        }

        public void Add(int key)
        {
            if (count == capacity)
                Resize(capacity * 2);

            var bucket = GetBucket(key);
            if (arr[bucket] == null)
                arr[bucket] = new List<int>();

            if (!arr[bucket]!.Contains(key))
                arr[bucket]!.Add(key);

            count++;
        }

        public void Remove(int key)
        {
            var bucket = GetBucket(key);
            if (arr[bucket] == null)
                return;

            arr[bucket]!.Remove(key);
            if (arr[bucket]!.Count == 0)
                arr[bucket] = null;

            count--;

            if (count * 4 <= capacity)
                Resize(capacity / 2);
        }

        public bool Contains(int key)
        {
            var bucket = GetBucket(key);
            if (arr[bucket] == null)
                return false;
            return arr[bucket]!.Contains(key);
        }

        private int GetBucket(int key) => (key * 17) % capacity;

        private void Resize(int newCapacity)
        {
            capacity = newCapacity;
            var oldArr = arr;
            arr = new List<int>[capacity];
            
            foreach (var bucket in oldArr)
            {
                if (bucket == null)
                    continue;
                foreach (var val in bucket)
                {
                    Add(val);
                }
            }
        }
    }

    [ProblemSolution("706")]
    public class MyHashMap
    {
        private int capacity = 7;
        private int count = 0;
        private List<(int key, int value)>?[] arr;

        public MyHashMap()
        {
            arr = new List<(int, int)>[capacity];
        }

        public void Put(int key, int value)
        {
            if (count == capacity)
                Resize(capacity * 2);

            var bucket = GetBucket(key);
            if (arr[bucket] == null)
                arr[bucket] = new List<(int, int)>();

            var (list, ind) = GetData(key);

            if (ind == -1)
                list.Add((key, value));
            else
                list[ind] = (key, value);

            count++;
        }

        public int Get(int key)
        {
            if (arr[GetBucket(key)] == null)
                return -1;

            var (list, ind) = GetData(key);

            if (ind == -1)
                return -1;
            else
                return list[ind].value;
        }

        public void Remove(int key)
        {
            if (arr[GetBucket(key)] == null)
                return;

            var (list, ind) = GetData(key);

            if (ind != -1)
                list.RemoveAt(ind);

            if (count * 4 <= capacity)
                Resize(capacity / 2);
        }

        private (List<(int key, int value)>, int) GetData(int key)
        {
            var bucket = GetBucket(key);
            var list = arr[bucket]!;
            var ind = list.FindIndex(f => f.key == key);

            return (list, ind);
        }

        private int GetBucket(int key) => (key * 17) % capacity;

        private void Resize(int newCapacity)
        {
            capacity = newCapacity;
            var oldArr = arr;
            arr = new List<(int, int)>[capacity];

            foreach (var bucket in oldArr)
            {
                if (bucket == null)
                    continue;
                foreach (var val in bucket)
                {
                    Put(val.key, val.value);
                }
            }
        }
    }

    [ProblemSolution("709")]
    public string ToLowerCase(string s) => s.ToLowerInvariant();

    [ProblemSolution("719")]
    public int SmallestDistancePair(int[] nums, int k)
    {
        Array.Sort(nums);

        var low = 0;
        var high = nums[^1] - nums[0];

        while (low < high)
        {
            var mid = (low + high) / 2;

            var count = 0;
            var l = 0;

            for (int r = 0; r < nums.Length; r++)
            {
                while (nums[r] - nums[l] > mid)
                    l++;

                count += r - l;
            }

            if (count < k)
                low = mid + 1;
            else
                high = mid;
        }

        return low;
    }

    [ProblemSolution("725")]
    public ListNode?[] SplitListToParts(ListNode? head, int k)
    {
        var len = 0;
        var cur = head;
        while (cur is not null)
        {
            cur = cur.next;
            len++;
        }

        var size = len / k + 1;
        var bigCount = len % k;
        var smallCount = k - bigCount;

        var result = new ListNode?[k];
        var ind = 0;

        for (int i = 0; i < bigCount; i++)
            updateResult(size);

        size--;
        for (int i = 0; i < smallCount; i++)
            updateResult(size);

        return result;

        void updateResult(int count)
        {
            if (count == 0)
                return;

            result[ind++] = head;
            for (int i = 1; i < count; i++)
                head = head!.next;

            if (head is not null)
                (head, head.next) = (head.next, null);
        }
    }

    [ProblemSolution("726")]
    public string CountOfAtoms(string formula)
    {
        var d = parse(0, out _);
        var builder = new StringBuilder();

        foreach (var v in d.OrderBy(f => f.Key))
            builder.Append($"{v.Key}{(v.Value > 1 ? v.Value : "")}");

        return builder.ToString();

        Dictionary<string, int> parse(int ind, out int end)
        {
            var dick = new Dictionary<string, int>();

            while (ind < formula.Length)
            {
                if (char.IsUpper(formula[ind]))
                {
                    var start = ind;
                    ind++;
                    while (ind < formula.Length && char.IsLower(formula[ind]))
                        ind++;

                    var element = formula[start..ind];

                    ind = parseInt(ind, out var count);

                    if (dick.TryGetValue(element, out var value))
                        dick[element] = value + count;
                    else
                        dick[element] = count;
                }
                else if (formula[ind] == '(')
                {
                    var result = parse(ind + 1, out ind);
                    ind = parseInt(ind, out var multiplier);

                    merge(dick, result, multiplier);
                }
                else if (formula[ind] == ')')
                    break;
                else
                    throw new InvalidOperationException();
            }

            end = ind + 1;
            return dick;
        }

        int parseInt(int ind, out int parsed)
        {
            var start = ind;
            while (ind < formula.Length && char.IsNumber(formula[ind]))
                ind++;

            if (start == ind)
                parsed = 1;
            else
                parsed = int.Parse(formula[start..ind]);

            return ind;
        }

        void merge(Dictionary<string, int> merged, Dictionary<string, int> mergee, int multiplier)
        {
            foreach (var v in mergee)
            {
                if (merged.TryGetValue(v.Key, out var value))
                    merged[v.Key] = value + v.Value * multiplier;
                else
                    merged[v.Key] = v.Value * multiplier;
            }
        }
    }

    [ProblemSolution("729")]
    public class MyCalendar
    {
        private readonly SortedSet<(int x, int y)> _set = new(Comparer<(int , int)>.Create(Compare));

        public bool Book(int start, int end)
        {
            if (_set.Contains((start, end)))
                return false;

            _set.Add((start, end));
            return true;
        }

        private static int Compare((int start, int end) x, (int start, int end) y)
        {
            if (x.end <= y.start)
                return -1;
            else if (x.start >= y.end)
                return 1;
            else
                return 0;
        }
    }

    [ProblemSolution("731")]
    public class MyCalendarTwo
    {
        private readonly Comparison<(int, int)> _comparison = Compare;
        private readonly List<SortedSet<(int x, int y)>> sets;
        private readonly (int x, int y) maxValue = (1_000_000_001, 1_000_000_002);

        public MyCalendarTwo()
        {
            var comparer = Comparer<(int, int)>.Create(_comparison);
            sets = [new(comparer), new(comparer)];
        }

        public bool Book(int start, int end)
        {
            var toBeAdded = new List<(int ind, (int, int) interval)>();

            while (start < end)
            {
                var interval = (start, end);
                for (int i = 0; i < sets.Count; i++)
                {
                    var set = sets[i];

                    var view = set.GetViewBetween((interval.start, interval.start), maxValue);
                    var value = view.Min;

                    if (Compare(interval, value) == 0)
                    {
                        if (value.x > interval.start)
                        {
                            toBeAdded.Add((i, (interval.start, value.x)));
                            start = value.x;
                            break;
                        }
                    }
                    else
                    {
                        toBeAdded.Add((i, interval));
                        start = end;
                        break;
                    }
                }

                if (start == interval.start)
                    return false;
            }

            for (int i = 0; i < toBeAdded.Count; i++)
                sets[toBeAdded[i].ind].Add(toBeAdded[i].interval);

            return true;
        }

        private static int Compare((int start, int end) x, (int start, int end) y)
        {
            if (x.end <= y.start)
                return -1;
            else if (x.start >= y.end)
                return 1;
            else
                return 0;
        }
    }

    [ProblemSolution("733")]
    public int[][] FloodFill(int[][] image, int sr, int sc, int color)
    {
        var startColor = image[sr][sc];
        if (startColor == color)
            return image;

        fillPixel(sr, sc);
        void fillPixel(int r, int c)
        {
            if (r < 0 || c < 0 || r >= image.Length || c >= image[0].Length || image[r][c] != startColor)
                return;

            image[r][c] = color;
            fillPixel(r + 1, c);
            fillPixel(r - 1, c);
            fillPixel(r, c + 1);
            fillPixel(r, c - 1);
        }

        return image;
    }

    [ProblemSolution("739")]
    public int[] DailyTemperatures(int[] temperatures)
    {
        var stack = new Stack<(int val, int ind)>();
        var result = new int[temperatures.Length];
        for (int i = temperatures.Length - 1; i >= 0; i--)
        {
            while (stack.Count > 0 && stack.Peek().val <= temperatures[i])
                stack.Pop();

            var dist = stack.Count == 0 ? 0 : stack.Peek().ind - i;
            result[i] = dist;
            stack.Push((temperatures[i], i));
        }

        return result;
    }

    [ProblemSolution("752")]
    public int OpenLock(string[] deadends, string target)
    {
        var targelem = getElemFromStr(target);
        var startElem = (0, 0, 0, 0);
        var queue = new Queue<((int, int, int, int), int)>();
        queue.Enqueue((startElem, 0));
        var ends = deadends.Select(f =>
        {
            return getElemFromStr(f);
        }).ToHashSet();

        if (ends.Contains(startElem))
            return -1;

        var visited = new HashSet<(int, int, int, int)>();
        visited.Add(startElem);

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();
            var curelem = cur.Item1;
            if (curelem == targelem)
                return cur.Item2;

            for (int i = 0; i < 4; i++)
            {
                for (int k = -1; k < 2; k += 2)
                {
                    var elem = i switch
                    {
                        0 => curelem.Item1,
                        1 => curelem.Item2,
                        2 => curelem.Item3,
                        _ => curelem.Item4
                    };

                    elem += k;
                    if (elem < 0)
                        elem = 9;
                    else if (elem > 9)
                        elem = 0;

                    var other = (i == 0 ? elem : curelem.Item1,
                        i == 1 ? elem : curelem.Item2,
                        i == 2 ? elem : curelem.Item3,
                        i == 3 ? elem : curelem.Item4);

                    if (!ends.Contains(other) && !visited.Contains(other))
                    {
                        visited.Add(other);
                        queue.Enqueue((other, cur.Item2 + 1));
                    }
                }
            }
        }

        return -1;

        (int, int, int, int) getElemFromStr(string str) => (int.Parse(str[0].ToString()), int.Parse(str[1].ToString()), int.Parse(str[2].ToString()), int.Parse(str[3].ToString()));
    }

    [ProblemSolution("771")]
    public int NumJewelsInStones(string jewels, string stones)
    {
        var hash = jewels.ToHashSet();
        return stones.Count(f => hash.Contains(f));
    }

    [ProblemSolution("779")]
    public int KthGrammar(int n, int k)
    {
        if (n == 1)
            return 0;

        var prev = KthGrammar(n - 1, k % 2 == 0 ? k / 2 : k / 2 + 1);
        if (k % 2 == 1)
            return prev;
        else if (prev == 0)
            return 1;
        else
            return 0;
    }

    [ProblemSolution("787")]
    public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
    {
        var prices = new List<(int dst, int price)>[n];
        for (int i = 0; i < n; i++)
            prices[i] = [];

        foreach (var f in flights)
            prices[f[0]].Add((f[1], f[2]));

        var queue = new SortedSet<(int price, int dst, int k)>
        {
            (0, src, 0)
        };

        var been = new int[n][];
        for (int i = 0; i < n; i++)
        {
            been[i] = new int[k + 2];
            for (int j = 0; j < been[i].Length; j++)
                been[i][j] = int.MaxValue;
        }

        been[src][0] = 0;
        while (queue.Count > 0)
        {
            var top = queue.Min;
            queue.Remove(top);

            if (top.k > k)
                continue;

            foreach (var other in prices[top.dst])
            {
                var price = top.price + other.price;
                if (price < been[other.dst][top.k + 1])
                {
                    queue.Remove((been[other.dst][top.k + 1], other.dst, top.k + 1));
                    been[other.dst][top.k + 1] = price;
                    queue.Add((price, other.dst, top.k + 1));
                }
            }
        }

        var least = been[dst].Min();
        return least == int.MaxValue ? -1 : least;
    }
}
