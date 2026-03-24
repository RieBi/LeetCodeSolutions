namespace LeetCode.Set2XXX;
class Solution29XX
{
    [ProblemSolution("2900")]
    public IList<string> GetWordsInLongestSubsequence(int _, string[] words, int[] groups)
    {
        var value = -1;
        var result = new List<string>();
        for (int i = 0; i < groups.Length; i++)
        {
            if (groups[i] != value)
            {
                value = groups[i];
                result.Add(words[i]);
            }
        }

        return result;
    }
    
    [ProblemSolution("2906")]
    public int[][] ConstructProductMatrix(int[][] grid)
    {
        var pref = new int[grid.Length * grid[0].Length];
        var suff = new int[pref.Length];

        const int modulo = 12345;
        var num = 1L;

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[0].Length; j++)
            {
                num *= grid[i][j];
                num %= modulo;
                pref[i * grid[0].Length + j] = (int)num;
            }
        }

        num = 1;

        for (var i = grid.Length - 1; i >= 0; i--)
        {
            for (var j = grid[0].Length - 1; j >= 0; j--)
            {
                num *= grid[i][j];
                num %= modulo;
                suff[i * grid[0].Length + j] = (int)num;
            }
        }

        var result = new int[grid.Length][];
        for (var i = 0; i < grid.Length; i++)
            result[i] = new int[grid[0].Length];

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[0].Length; j++)
            {
                var ind = i * grid[0].Length + j;

                var cur = 1;
                
                if (ind > 0)
                    cur *= pref[ind - 1];
                if (ind < suff.Length - 1)
                    cur *= suff[ind + 1];

                result[i][j] = cur % modulo;
            }
        }

        return result;
    }

    [ProblemSolution("2937")]
    public int FindMinimumOperations(string s1, string s2, string s3)
    {
        var min = Math.Min(s1.Length, s2.Length);
        min = Math.Min(min, s3.Length);
        var count = 0;

        for (int i = 0; i < min; i++)
        {
            if (s1[i] == s2[i] && s1[i] == s3[i])
            {
                count++;
            }
            else
            {
                break;
            }
        }

        var changes = s1.Length - count + s2.Length - count + s3.Length - count;
        return count == 0 ? -1 : changes;
    }

    [ProblemSolution("2938")]
    public long MinimumSteps(string s)
    {
        long ops = 0;
        var left = -1;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '0')
                left = i;
            else
                break;
        }

        left++;

        for (int i = left; i < s.Length; i++)
        {
            if (s[i] == '0')
            {
                ops += (i - left);
                left++;
            }
        }
        return ops;
    }

    [ProblemSolution("2939")]
    public int MaximumXorProduct(long a, long b, int n)
    {
        long x = 0;
        long mask = 1L << (n - 1);
        Console.WriteLine(mask);
        for (int i = 0; i < n; i++)
        {
            if ((a & mask) == (b & mask))
            {
                if ((a & mask) == 0)
                    x = x | mask;
            }
            else
            {
                var valA = a ^ x;
                var valB = b ^ x;

                if (valA < valB)
                {
                    if ((valA & (mask)) == 0)
                        x = x | mask;
                }
                else
                {
                    if ((valB & (mask)) == 0)
                        x = x | mask;
                }
            }

            mask = mask >> 1;
        }
        var modulo = 1000000007;
        Console.WriteLine(x);
        var result = (long)((a ^ x) % modulo) * (long)((b ^ x) % modulo);
        return (int)(result % modulo);
    }

    [ProblemSolution("2966")]
    public int[][] DivideArray(int[] nums, int k)
    {
        Array.Sort(nums);
        var result = new int[nums.Length / 3][];
        for (int i = 0; i < nums.Length; i += 3)
        {
            if (nums[i + 2] - nums[i] <= k)
                result[i / 3] = [nums[i], nums[i + 1], nums[i + 2]];
            else
                return [];
        }

        return result;
    }

    [ProblemSolution("2970")]
    public int IncremovableSubarrayCount(int[] nums)
    {
        var count = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i; j < nums.Length; j++)
            {
                if (isIncremental(i, j))
                    count++;
            }
        }

        return count;

        bool isIncremental(int left, int right)
        {
            var skipped = skipPortion(left, right).GetEnumerator();
            if (!skipped.MoveNext())
                return true;
            var first = skipped.Current;
            while (skipped.MoveNext())
            {
                if (skipped.Current <= first)
                    return false;
                first = skipped.Current;
            }

            return true;
        }

        IEnumerable<int> skipPortion(int left, int right)
        {
            for (int i = 0; i < left; i++)
                yield return nums[i];
            for (int i = right + 1; i < nums.Length; i++)
                yield return nums[i];
        }
    }

    [ProblemSolution("2971")]
    public long LargestPerimeter(int[] nums)
    {
        var sorted = nums.OrderBy(f => f).ToList();
        var total = sorted.Aggregate(0L, (a, b) => a + b);
        for (int i = sorted.Count - 1; i >= 2; i--)
        {
            total -= sorted[i];
            if (total > sorted[i])
                return total + sorted[i];
        }

        return -1;
    }

    [ProblemSolution("2972")]
    public long IncremovableSubarrayCount2(int[] nums)
    {
        var pref = largestPref();
        var suff = smallestSuff();
        if (pref == nums.Length - 1)
            return nums.Length * (nums.Length + 1) / 2;

        var total = (long)((pref + 1) + (nums.Length - suff));
        var left = 0;
        var right = suff;
        while (left <= pref)
        {
            while (right < nums.Length && nums[left] >= nums[right])
                right++;

            if (right == nums.Length)
                break;

            total += (nums.Length - right);
            left++;
        }

        return total + 1;

        int largestPref()
        {
            var ind = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > nums[i - 1])
                    ind++;
                else
                    break;
            }

            return ind;
        }

        int smallestSuff()
        {
            var ind = nums.Length - 1;
            for (var i = ind - 1; i >= 0; i--)
            {
                if (nums[i + 1] > nums[i])
                    ind--;
                else
                    break;
            }

            return ind;
        }
    }

    [ProblemSolution("2973")]
    public long[] PlacedCoins(int[][] edges, int[] cost)
    {
        var nodes = new Dictionary<int, (HashSet<int> relatives, List<long> values, int parent, long coins, int size)>();
        for (int i = 0; i <= edges.Length; i++)
        {
            nodes[i] = (new HashSet<int>(), new List<long>(), -1, -1, 1);
        }

        for (int i = 0; i < edges.Length; i++)
        {
            var node1 = nodes[edges[i][0]];
            var node2 = nodes[edges[i][1]];
            node1.relatives.Add(edges[i][1]);
            node2.relatives.Add(edges[i][0]);
        }

        var root = 0;
        placeCoins(root, -1);

        var result = new long[edges.Length + 1];
        for (int i = 0; i <= edges.Length; i++)
        {
            result[i] = nodes[i].coins;
        }

        return result;

        void placeCoins(int nodeInd, int parent)
        {
            var node = nodes[nodeInd];
            var vals = new List<long>();
            vals.Add(cost[nodeInd]);

            foreach (var other in node.relatives)
            {
                if (other == parent)
                    continue;
                placeCoins(other, nodeInd);
                node.size += nodes[other].size;
                vals = MergeValues(vals, nodes[other].values);
            }

            node.values = vals;
            node.coins = CalculateCoins(vals, node.size);
            nodes[nodeInd] = node;
        }

        List<long> MergeValues(List<long> vals1, List<long> vals2)
        {
            var merged = new List<long>();
            foreach (var val in vals1)
                merged.Add(val);
            foreach (var val in vals2)
                merged.Add(val);

            var positives = merged.Where(f => f > 0).OrderByDescending(f => f).Take(3);
            var negatives = merged.Where(f => f < 0).OrderBy(f => f).Take(2);
            return positives.Concat(negatives).ToList();
        }

        long CalculateCoins(List<long> vals, int size)
        {
            if (size < 3)
                return 1;

            var positives = vals.Where(f => f > 0).OrderByDescending(f => f).Take(3).ToList();
            var negatives = vals.Where(f => f < 0).OrderBy(f => f).Take(2).ToList();
            var val1 = positives.Count == 3 ? (positives.Aggregate(1L, (a, b) => a * b)) : 0;
            var val2 = negatives.Count == 2 && positives.Count >= 1 ? (negatives.Aggregate(1L, (a, b) => a * b) * positives[0]) : 0;
            return Math.Max(val1, val2);
        }
    }
    
    [ProblemSolution("2975")]
    public int MaximizeSquareArea(int m, int n, int[] hFences, int[] vFences)
    {
        Array.Sort(hFences);
        Array.Sort(vFences);

        var hValues = new HashSet<int>();
        var vValues = new HashSet<int>();

        hValues.Add(m - 1);
        vValues.Add(n - 1);

        for (var i = 0; i < hFences.Length; i++)
        {
            hValues.Add(hFences[i] - 1);
            hValues.Add(m - hFences[i]);
            
            for (var j = i + 1; j < hFences.Length; j++)
                hValues.Add(hFences[j] - hFences[i]);
        }

        for (var i = 0; i < vFences.Length; i++)
        {
            vValues.Add(vFences[i] - 1);
            vValues.Add(n - vFences[i]);
            
            for (var j =  i + 1; j < vFences.Length; j++)
                vValues.Add(vFences[j] - vFences[i]);
        }

        hValues.IntersectWith(vValues);

        if (hValues.Count == 0)
            return -1;
        
        var max = hValues.Max();
        return (int)((long)max * max % 1_000_000_007);
    }

    [ProblemSolution("2976")]
    public long MinimumCost(string source, string target, char[] original, char[] changed, int[] cost)
    {
        var dist = new int[26][];
        for (int i = 0; i < 26; i++)
            dist[i] = new int[26];

        for (int i = 0; i < 26; i++)
            for (int j = 0; j < 26; j++)
                dist[i][j] = int.MaxValue / 2;

        for (int i = 0; i < original.Length; i++)
        {
            var s = original[i] - 'a';
            var d = changed[i] - 'a';

            if (cost[i] < dist[s][d])
                dist[s][d] = cost[i];
        }

        for (int i = 0; i < 26; i++)
            dist[i][i] = 0;

        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                for (int k = 0; k < 26; k++)
                {
                    if (dist[j][k] > dist[j][i] + dist[i][k])
                        dist[j][k] = dist[j][i] + dist[i][k];
                }
            }
        }

        var result = 0L;
        for (int i = 0; i < source.Length; i++)
        {
            var s = source[i] - 'a';
            var d = target[i] - 'a';

            if (dist[s][d] == int.MaxValue / 2)
                return -1;
            else
                result += dist[s][d];
        }

        return result;
    }
}
