using System.Runtime.InteropServices;

namespace LeetCode.Set2xxx;
internal class Solution20xx
{
    [ProblemSolution("2000")]
    public string ReversePrefix(string word, char ch)
    {
        var ind = word.IndexOf(ch);
        if (ind == -1)
            return word;

        return new string(word[..(ind + 1)].Reverse().ToArray()) + word[(ind + 1)..];
    }

    [ProblemSolution("2037")]
    public int MinMovesToSeat(int[] seats, int[] students)
    {
        Array.Sort(seats);
        Array.Sort(students);
        return Enumerable.Range(0, seats.Length)
            .Sum(f => Math.Abs(seats[f] - students[f]));
    }

    [ProblemSolution("2092")]
    public IList<int> FindAllPeople(int n, int[][] meetings, int firstPerson)
    {
        var secrets = new bool[n];
        secrets[0] = true;
        secrets[firstPerson] = true;

        var meets = meetings.GroupBy(f => f[2]).OrderBy(f => f.Key);
        foreach (var meet in meets)
        {
            var dick = new Dictionary<int, List<int>>();
            foreach (var rendezvous in meet)
            {
                if (dick.TryGetValue(rendezvous[0], out var val1))
                    val1.Add(rendezvous[1]);
                else
                    dick[rendezvous[0]] = [rendezvous[1]];

                if (dick.TryGetValue(rendezvous[1], out var val2))
                    val2.Add(rendezvous[0]);
                else
                    dick[rendezvous[1]] = [rendezvous[0]];
            }

            var seen = new HashSet<int>();
            foreach (var v in dick)
            {
                if (seen.Contains(v.Key) || !secrets[v.Key])
                    continue;

                seen.Add(v.Key);
                var queue = new Queue<int>();
                queue.Enqueue(v.Key);
                while (queue.Count > 0)
                {
                    var c = queue.Count;
                    for (int i = 0; i < c; i++)
                    {
                        var top = queue.Dequeue();
                        foreach (var other in dick[top])
                        {
                            if (seen.Contains(other))
                                continue;

                            seen.Add(other);
                            secrets[other] = true;
                            queue.Enqueue(other);
                        }
                    }
                }
            }
        }

        var result = new List<int>();
        for (int i = 0; i < n; i++)
            if (secrets[i])
                result.Add(i);

        return result;
    }
}
