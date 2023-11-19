namespace LeetCode.Set2xxx;
class Solution29xx
{
    #region Solution for 2937
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
    #endregion

    #region Solution for 2938
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
    #endregion

    #region Solution for 2939
    public int MaximumXorProduct(long a, long b, int n)
    {
        long x = 0;
        long mask = 1l << (n - 1);
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
    #endregion

    //public int[] LeftmostBuildingQueries(int[] heights, int[][] queries)
    //{
    //    for (int i = 0; i < queries.Length; i++)
    //    {
    //        if (queries[i][0] > queries[i][0])
    //        {
    //            var temp = queries[i][0];
    //            queries[i][0] = queries[i][1];
    //            queries[i][1] = temp;
    //        }
    //    }

    //    var qsorted = queries.Zip(Enumerable.Range(0, queries.Length)).OrderBy(f => f.First[1]).ToList();
    //    // (int[] query, int indexOfQeury)
    //    var qstack = new Stack<(int[] query, int ind)>(qsorted.Count);
    //    for (int i = 0; i < qsorted.Count; i++)
    //    {
    //        qstack.Push(qsorted[i]);
    //    }
    //    // (int num, int indexOfNum)
    //    var blist = new List<(int num, int ind)>();


    //    var res = new int[queries.Length];
    //    for (int i = heights.Length - 1; i >= 0 && qstack.Count > 0;)
    //    {
    //        var b = qstack.Peek().query[1];

    //        int left = 0;
    //        int right = 0;
    //        while (b < i)
    //        {
    //            left = 0;
    //            right = blist.Count - 1;
    //            while (left < right)
    //            {
    //                var diff = right - left;
    //                var mid = left + (right - left) / 2;
    //                //if (right % 2 != 0)
    //                //    mid++;

    //                if (blist[mid].num < b)
    //                {
    //                    left = mid + 1;
    //                }
    //                else
    //                {
    //                    right = mid;
    //                }

    //            }

    //            var ind = left;
    //            blist.Insert(ind, (heights[i], i));
    //        }

    //        left = 0;
    //        right = blist.Count - 1;
    //        while (left < right)
    //        {
    //            var diff = right - left;
    //            var mid = left + (right - left) / 2;
    //            //if (right % 2 != 0)
    //            //    mid++;

    //            if (blist[mid].num < b)
    //            {
    //                left = mid + 1;
    //            }
    //            else
    //            {
    //                right = mid;
    //            }

    //        }

    //        var hind = left;
    //    }

    //}
}