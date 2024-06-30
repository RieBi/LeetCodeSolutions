using LeetCode._0xxx;
using LeetCode.Set0xxx;
using LeetCode.Set1xxx;
using LeetCode.Set2xxx;
using LeetCode.Set3xxx;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Sources;

namespace LeetCode;

public static partial class Program
{
    public static void Main()
    {
        UpdateReadmeFile();
    }

    public static void UpdateReadmeFile()
    {
        var set = new HashSet<int>();
        ForEachTypeContainingAttribute<ProblemSolutionAttribute>(f =>
        {
            Console.WriteLine($"Problem: {f.ProblemName}");
            set.Add(int.Parse(f.ProblemName!));
        });

        var count = set.Count;
        Console.WriteLine($"Total count: {count}");

        var readmePath = @"..\..\..\README.md";
        var filetext = File.ReadAllText(readmePath);
        filetext = ReadmeFileRegex().Replace(filetext, $"***{count} problems***");
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

    [GeneratedRegex(@"\*\*\*\d* problems\*\*\*")]
    private static partial Regex ReadmeFileRegex();
}

[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
internal class ProblemSolutionAttribute(string? problemName = null) : Attribute
{
    public string? ProblemName { get; set; } = problemName;
}
