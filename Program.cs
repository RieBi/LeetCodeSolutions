using LeetCode._0xxx;
using LeetCode.Set0xxx;
using LeetCode.Set1xxx;
using LeetCode.Set2xxx;
using LeetCode.Set3xxx;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LeetCode;

internal class Program
{
    public static void Main()
    {
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
