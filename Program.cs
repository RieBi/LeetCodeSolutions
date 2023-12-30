using LeetCode.Set0xxx;
using LeetCode.Set1xxx;
using LeetCode.Set2xxx;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LeetCode;

internal class Program
{
    public static void Main()
    {
        var s = new Solution13xx();
        s.MinDifficulty([143, 446, 351, 170, 117, 963, 785, 76, 139, 772, 452, 743, 23, 767, 564, 872, 922, 532, 957, 945, 203, 615, 843, 909, 458, 320, 290, 235, 174, 814, 414, 669, 422, 769, 781, 721, 523, 94, 100, 464, 484, 562, 941], 5);
        UpdateReadmeFile();
    }

    public static void UpdateReadmeFile()
    {
        var count = 0;
        ForEachTypeContainingAttribute<ProblemSolutionAttribute>(f =>
        {
            Console.WriteLine($"Problem: {f.ProblemName}");
            count++;
        });
        Console.WriteLine($"Total count: {count}");

        var readmePath = @"..\..\..\README.md";
        var filetext = File.ReadAllText(readmePath);
        filetext = Regex.Replace(filetext, @"\*\*\*\d* problems\*\*\*", $"***{count} problems***");
        File.WriteAllText(readmePath, filetext);

        Console.WriteLine("Overwritten the readme file");
    }

    public static void ForEachTypeContainingAttribute<T>(Action<T> action) where T : System.Attribute
    {
        var assembly = Assembly.GetCallingAssembly();
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<T>();
                if (attribute is not null)
                    action(attribute);
            }

            var typeAttribute = type.GetCustomAttribute<T>();
            if (typeAttribute is not null)
                action(typeAttribute);
        }
    }
}

[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
internal class ProblemSolutionAttribute : Attribute
{
    public string? ProblemName { get; set; }

    public ProblemSolutionAttribute(string? problemName = null)
    {
        ProblemName = problemName;
    }
}
