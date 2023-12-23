using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode._0xxx;
internal class Solution07xx
{
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
}
