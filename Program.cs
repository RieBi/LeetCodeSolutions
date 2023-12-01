using System.Reflection;

namespace LeetCode;

internal class Program
{
    public static void Main()
    {
        var count = 0;
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

internal class ProblemSolutionAttribute
{
    public string? ProblemName { get; set; }

    public ProblemSolutionAttribute(string? problemName = null)
    {
        ProblemName = problemName;
    }
}