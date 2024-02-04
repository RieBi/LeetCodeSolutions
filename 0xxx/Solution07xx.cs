using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode._0xxx;
internal class Solution07xx
{
    [ProblemSolution("700")]
    public TreeNode? SearchBST(TreeNode root, int val)
    {
        return search(root);
        TreeNode? search(TreeNode node)
        {
            if (node.val == val)
                return node;
            else if (node.val > val && node.left is not null)
                return search(node.left);
            else if (node.val < val && node.right is not null)
                return search(node.right);
            else
                return null;
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
}
