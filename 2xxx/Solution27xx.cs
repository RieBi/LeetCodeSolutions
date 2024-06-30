using System.Reflection.Metadata;

namespace LeetCode.Set2XXX;
internal class Solution27XX
{
    [ProblemSolution("2706")]
    public int BuyChoco(int[] prices, int money)
    {
        var min1 = prices[0];
        var min2 = prices[1];
        if (min1 > min2)
            (min1, min2) = (min2, min1);

        for (int i = 2; i < prices.Length; i++)
        {
            if (prices[i] < min2)
                min2 = prices[i];

            if (min1 > min2)
                (min1, min2) = (min2, min1);
        }

        var leftover = money - min1 - min2;
        return leftover >= 0 ? leftover : money;
    }

    [ProblemSolution("2709")]
    public bool CanTraverseAllPairs(int[] nums)
    {
        if (nums.Length == 1)
            return true;

        if (nums.Contains(1))
            return false;

        var max = nums.Max();
        var primes = new bool[max + 1];
        for (int i = 0; i < primes.Length; i++)
            primes[i] = true;
        primes[1] = false;
        var primeList = new List<int>();
        for (int i = 2; i < primes.Length; i++)
            if (primes[i])
                primeList.Add(i);

        for (int n = 2; n * n <= max; n++)
        {
            if (!primes[n])
                continue;
            for (int j = n * n; j <= max; j += n)
            {
                primes[j] = false;
            }
        }

        var primed = new Dictionary<int, int>();

        var dick = new Dictionary<int, List<int>>();
        foreach (var num in nums)
        {
            var factors = PrimeFactor(num);

            if (factors.Count == 1 && !dick.ContainsKey(factors[0]))
                dick[factors[0]] = [];

            for (int i = 1; i < factors.Count; i++)
            {
                if (dick.TryGetValue(factors[i - 1], out var val1))
                    val1.Add(factors[i]);
                else
                    dick[factors[i - 1]] = [factors[i]];

                if (dick.TryGetValue(factors[i], out var val2))
                    val2.Add(factors[i - 1]);
                else
                    dick[factors[i]] = [factors[i - 1]];
            }
        }

        var queue = new Queue<int>();
        queue.Enqueue(dick.Keys.First());
        HashSet<int> visited = [queue.Peek()];
        while (queue.Count > 0)
        {
            var c = queue.Count;
            for (int i = 0; i < c; i++)
            {
                var top = queue.Dequeue();
                foreach (var other in dick[top])
                {
                    if (visited.Contains(other))
                        continue;

                    visited.Add(other);
                    queue.Enqueue(other);
                }
            }
        }

        return dick.Count == visited.Count;

        List<int> PrimeFactor(int num)
        {
            if (primes[num])
                return [num];
            if (primed.ContainsKey(num))
                return [];

            var list = new List<int>();
            foreach (var p in primeList)
            {
                if (num == 1)
                    break;

                while (num % p == 0)
                {
                    if (list.Count == 0 || list[^1] != p)
                        list.Add(p);

                    primed[num] = p;

                    num /= p;

                    if (primed.TryGetValue(num, out var fac))
                    {
                        while (num > 1)
                        {
                            fac = primed[num];
                            if (list.Count == 0 || list[^1] != fac)
                                list.Add(fac);

                            num /= fac;
                        }

                        return list;
                    }
                }
            }

            return list;
        }
    }
}
