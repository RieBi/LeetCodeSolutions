using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode.Set0XXX;
internal partial class Solution05XX
{
    [ProblemSolution("502")]
    public int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
    {
        List<(int profit, int capital)> sorted = profits
            .Zip(capital)
            .OrderByDescending(f => f.Second)
            .ToList();

        var profitQueue = new PriorityQueue<int, int>();

        for (int i = 0; i < k; i++)
        {
            while (sorted.Count > 0 && sorted[^1].capital <= w)
            {
                var item = sorted[^1];
                sorted.RemoveAt(sorted.Count - 1);
                profitQueue.Enqueue(item.profit, -item.profit);
            }

            if (profitQueue.TryDequeue(out var profit, out _))
                w += profit;
            else
                break;
        }

        return w;
    }

    [ProblemSolution("506")]
    public string[] FindRelativeRanks(int[] score)
    {
        var ranks = score
            .Zip(Enumerable.Range(0, score.Length))
            .OrderByDescending(f => f.First)
            .ToList();

        var result = new string[score.Length];
        for (int i = 0; i < score.Length; i++)
        {
            var ind = ranks[i].Second;
            result[ind] = i == 0 ? "Gold Medal" :
                i == 1 ? "Silver Medal" :
                i == 2 ? "Bronze Medal" :
                (i + 1).ToString();
        }

        return result;
    }

    [ProblemSolution("509")]
    public int Fib(int n)
    {
        var cache = new Dictionary<int, int>();
        return calculate(n);
        
        int calculate(int num)
        {
            if (num < 2)
                return num;
            else if (cache.TryGetValue(num, out int value))
                return value;
            else
                return calculate(num - 1) + calculate(num - 2);
        }
    }

    [ProblemSolution("523")]
    public bool CheckSubarraySum(int[] nums, int k)
    {
        var hash = new Dictionary<int, int>();
        var sum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            sum = (sum + nums[i]) % k;
            if (sum == 0 && i > 0)
                return true;

            if (hash.TryGetValue(sum, out var value))
            {
                if (i - value > 1)
                    return true;
            }
            else
            {
                hash[sum] = i;
            }
        }

        return false;
    }

    [ProblemSolution("542")]
    public int[][] UpdateMatrix(int[][] mat)
    {
        var transforms = new List<(int, int)>
        {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1)
        };

        var queue = new Queue<int>();
        var lookup = new bool[mat.Length][];
        for (int i = 0; i < lookup.Length; i++)
            lookup[i] = new bool[mat[i].Length];

        for (int i = 0; i < mat.Length; i++)
        {
            for (int j = 0; j < mat[0].Length; j++)
            {
                if (mat[i][j] == 0)
                {
                    queue.Enqueue(i * mat[0].Length + j);
                    lookup[i][j] = true;
                }
            }
        }

        while (queue.Count > 0)
        {
            var elem = queue.Dequeue();
            var i = elem / mat[0].Length;
            var j = elem % mat[0].Length;

            foreach (var trans in transforms)
            {
                var newi = i + trans.Item1;
                var newj = j + trans.Item2;
                if (newi < 0 || newj < 0 || newi >= mat.Length || newj >= mat[0].Length || lookup[newi][newj])
                    continue;

                lookup[newi][newj] = true;
                mat[newi][newj] = mat[i][j] + 1;
                queue.Enqueue(newi * mat[0].Length + newj);
            }
        }

        return mat;
    }

    [ProblemSolution("559")]
    public int MaxDepth(Node root)
    {
        if (root is null)
            return 0;

        var max = 0;
        foreach (var child in root.children)
            max = Math.Max(max, MaxDepth(child));

        return max + 1;
    }

    [ProblemSolution("564")]
    public string NearestPalindromic(string n)
    {
        if (n.Length > 1 && n[0] == '1' && n[^1] == '1'
            && n.Skip(1).Take(n.Length - 2).All(f => f == '0'))
            return new string('9', n.Length - 1);

        if (n.Length > 1 && n.All(f => f == '9'))
            return $"1{new string('0', n.Length - 1)}1";

        var str = new StringBuilder(n);
        for (int i = str.Length / 2; i < str.Length; i++)
            str[i] = str[^(i + 1)];

        var result = str.ToString();
        var midInd = str.Length / 2;
        var numberN = long.Parse(n);

        string? resultHigh = null;
        string? resultLow = null;

        if (result[midInd] != '0')
        {
            var build = new StringBuilder(result);
            var newChar = (char)(str[midInd] - 1);
            (build[midInd], build[^(midInd + 1)]) = (newChar, newChar);

            resultLow = build.ToString();        }

        if (result[midInd] != '9')
        {
            var build = new StringBuilder(result);
            var newChar = (char)(str[midInd] + 1);
            (build[midInd], build[^(midInd + 1)]) = (newChar, newChar);

            resultHigh = build.ToString();
        }

        if (resultHigh is not null)
        {
            var numRes = long.Parse(result);
            var numHigh = long.Parse(resultHigh);

            if (Math.Abs(numberN - numHigh) < Math.Abs(numberN - numRes))
                result = resultHigh;
        }

        if (resultLow is not null)
        {
            var numRes = long.Parse(result);
            var numLow = long.Parse(resultLow);

            if (Math.Abs(numberN - numLow) < Math.Abs(numberN - numRes))
                result = resultLow;
        }

        if (result == n)
        {
            var mid = str[midInd];

            var adder = mid == '0' ? 1 : -1;
            var newChar = (char)(str[midInd] + adder);

            (str[midInd], str[^(midInd + 1)]) = (newChar, newChar);
            result = str.ToString();

            if (adder == 1)
            {
                for (int i = midInd; i < str.Length - 1; i++)
                    (str[midInd], str[^(midInd + 1)]) = ('9', '9');

                newChar = (char)(str[0] - 1);
                (str[0], str[^1]) = (newChar, newChar);

                var resNum = long.Parse(result.ToString());
                var num = long.Parse(str.ToString());

                if (Math.Abs(numberN - num) <= Math.Abs(numberN - resNum))
                    result = str.ToString();
            }
        }

        var numberResult = long.Parse(result);

        if (numberResult > numberN)
        {
            var diff = numberResult - numberN;
            var potentialLower = (numberN - diff).ToString();

            var isPalindrom = true;
            for (int i = 0; i < potentialLower.Length / 2; i++)
            {
                if (potentialLower[i] != potentialLower[^(i + 1)])
                {
                    isPalindrom = false;
                    break;
                }
            }

            if (isPalindrom)
                result = potentialLower;
        }



        return result;
    }

    [ProblemSolution("567")]
    public bool CheckInclusion(string s1, string s2)
    {
        var s1Chars = new Dictionary<char, int>();
        for (int i = 0; i < s1.Length; i++)
        {
            if (s1Chars.TryGetValue(s1[i], out var value))
                s1Chars[s1[i]] = value + 1;
            else
                s1Chars[s1[i]] = 1;
        }

        var s2Chars = new Dictionary<char, int>();
        var left = 0;
        for (int i = 0; i < s2.Length; i++)
        {
            if (s2Chars.TryGetValue(s2[i], out var value))
                s2Chars[s2[i]] = value + 1;
            else
                s2Chars[s2[i]] = 1;

            value++;

            s1Chars.TryGetValue(s2[i], out var actual);

            while (value > actual)
            {
                if (s2[left] == s2[i])
                    value--;

                s2Chars[s2[left]] = s2Chars[s2[left]] - 1;
                left++;
            }

            if ((left <= i) && (i - left + 1 == s1.Length))
                return true;
        }

        return false;
    }

    [ProblemSolution("576")]
    public int FindPaths(int m, int n, int maxMove, int startRow, int startColumn)
    {
        List<(int i, int j)> transitions = [(0, -1), (0, 1), (1, 0), (-1, 0)];

        var prev = createMatrix(m, n);
        prev[startRow][startColumn] = 1;
        var result = 0;
        var modulo = 1_000_000_007;
        for (int count = 0; count < maxMove; count++)
        {
            var cur = createMatrix(m, n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    foreach (var transition in transitions)
                    {
                        var newI = i + transition.i;
                        var newJ = j + transition.j;

                        if (newI < 0 || newJ < 0 || newI >= m || newJ >= n)
                            result = (result + prev[i][j]) % modulo;
                        else
                            cur[newI][newJ] = (prev[i][j] + cur[newI][newJ]) % modulo;
                    }
                }
            }

            prev = cur;
        }

        return result;

        static int[][] createMatrix(int m, int n)
        {
            var ar = new int[m][];
            for (int i = 0; i < ar.Length; i++)
                ar[i] = new int[n];
            return ar;
        }
    }

    [ProblemSolution("589")]
    public IList<int> Preorder(Node root)
    {
        var stack = new Stack<(Node node, int cur)>();
        if (root is not null)
            stack.Push((root, 0));
        var list = new List<int>();

        while (stack.Count > 0)
        {
            var top = stack.Pop();
            if (top.cur == 0)
                list.Add(top.node.val);
            if (top.cur >= top.node.children.Count)
                continue;

            stack.Push((top.node, top.cur + 1));
            stack.Push((top.node.children[top.cur], 0));
        }

        return list;
    }

    [ProblemSolution("590")]
    public IList<int> Postorder(Node root)
    {
        var stack = new Stack<(Node node, int cur)>();
        if (root is not null)
            stack.Push((root, 0));
        var list = new List<int>();

        while (stack.Count > 0)
        {
            var top = stack.Pop();
            if (top.cur >= top.node.children.Count)
            {
                list.Add(top.node.val);
                continue;
            }

            stack.Push((top.node, top.cur + 1));
            stack.Push((top.node.children[top.cur], 0));
        }

        return list;
    }

    [ProblemSolution("592")]
    public string FractionAddition(string expression)
    {
        var regex = new Regex(@"(?<fr>[+-]?\d+\/\d+)(?<fr>[+-]\d+\/\d+)*");
        var match = regex.Match(expression);
        var fractions = match.Groups["fr"].Captures.Select(f => f.Value).ToList();

        var (numerator, denominator) = getParts(fractions[0]);
        for (int i = 1; i < fractions.Count; i++)
        {
            var (num2, den2) = getParts(fractions[i]);

            var newDen = lcm(denominator, den2);
            numerator *= newDen / denominator;
            num2 *= newDen / den2;
            denominator = newDen;
            numerator += num2;
        }

        var div = gcd(Math.Abs(numerator), denominator);

        numerator /= div;
        denominator /= div;

        return $"{numerator}/{denominator}";

        (int numerator, int denominator) getParts(string fraction)
        {
            var slash = fraction.IndexOf('/');
            return (int.Parse(fraction[..slash]), int.Parse(fraction[(slash + 1)..]));
        }

        static int gcd(int a, int b)
        {
            if (a < b)
                (a, b) = (b, a);

            while (b != 0)
                (a, b) = (b, a % b);

            return a;
        }

        static int lcm(int a, int b) => Math.Abs(a * b) / gcd(a, b);
    }

    [ProblemSolution("599")]
    public string[] FindRestaurant(string[] list1, string[] list2)
    {
        var commons = list1.Zip(Enumerable.Range(0, list1.Length))
            .Concat(list2.Zip(Enumerable.Range(0, list2.Length)))
            .GroupBy(f => f.First)
            .Where(f => f.Count() == 2);

        var min = commons.Select(f => f.Sum(el => el.Second)).Min();
        return commons.Where(f => f.Sum(el => el.Second) == min)
            .Select(f => f.Key)
            .ToArray();
    }
}
