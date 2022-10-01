using Models;

namespace Host;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Получено {args.Length} аргументов командной строки. Начинаю сортировку");

        var students = ParseArgumentsAsStudents(args);
        Console.WriteLine($"\n\tИсходный массив студентов содержит {students.Count} значений:");
        Print(students);

        PrintFavoriteSubjectsIntersect(students);

        Console.WriteLine("Нажмите любую клавишу для выхода");
        Console.ReadKey();
    }

    private static List<Student> ParseArgumentsAsStudents(IReadOnlyCollection<string> args)
    {
        var students = new List<Student>(args.Count);
        foreach (var arg in args)
        {
            var parts = arg.Split(';');
            var fullName = parts[0].Split(' ');
            var subjects = parts[3].Split(',');

            students.Add(new Student
            {
                Name = fullName[0],
                SurName = fullName[1],
                Age = byte.Parse(parts[1]),
                Gender = Enum.Parse<Gender>(parts[2]),
                FavoriteSubjects = subjects
            });
        }

        return students;
    }

    private static void Print(IEnumerable<Student> students)
    {
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }

    private static void PrintBobs(IReadOnlyCollection<Student> students)
    {
        const string name = "Bob";
        var enumerable = students.Where(s => s.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        Console.WriteLine($"\tСтуденты с именем {name}:");
        Print(enumerable);
    }

    private static void PrintFemale(IReadOnlyCollection<Student> students)
    {
        var enumerable = students.Where(s => s.Gender == Gender.F);
        Console.WriteLine("\tСтуденты-девушки:");
        Print(enumerable);
    }

    private static void PrintOldest(IReadOnlyCollection<Student> students)
    {
        var oldestAge = students.Max(x => x.Age);
        var enumerable = students.Where(s => s.Age == oldestAge);
        Console.WriteLine("\tСамый старый студент(ы):");
        Print(enumerable);
    }

    private static void PrintFavoriteSubjectsIntersect(IReadOnlyCollection<Student> students)
    {
        for (var i = 0; i < students.Count - 1; i++)
        {
            for (var j = i + 1; j < students.Count; j++)
            {
                var a = students.ElementAt(i);
                var b = students.ElementAt(j);
                var common = a.FavoriteSubjects
                    .Intersect(b.FavoriteSubjects, StringComparer.CurrentCultureIgnoreCase)
                    .Distinct(StringComparer.CurrentCultureIgnoreCase).ToArray();

                if (common.Any())
                {
                    Console.WriteLine(
                        $"{common.Length} общих ♥предметов есть у студентов {a} и {b}: [{string.Join(", ", common)}]");
                }
            }
        }
    }

    private static void PrintSortVersion1(IReadOnlyCollection<Student> students)
    {
        var enumerable = students.OrderBy(x => x.Name).ThenBy(x => x.SurName).ThenBy(x => x.Age);
        Console.WriteLine("\tСортировка по Name, SurName, Age:");
        Print(enumerable);
    }

    private static void PrintSortVersion2(IReadOnlyCollection<Student> students)
    {
        var enumerable = students.OrderBy(x => x.Age).ThenBy(x => x.Gender).ThenBy(x => x.Name).ThenBy(x => x.SurName);
        Console.WriteLine("\tСортировка по Age, Gender, Name, SurName:");
        Print(enumerable);
    }
}
