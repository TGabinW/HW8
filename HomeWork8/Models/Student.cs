using Models;

public class Student
{
    private string[] _favoriteSubjects = Array.Empty<string>();

    public string Name { get; init; } = string.Empty;
    public string SurName { get; init; } = string.Empty;
    public byte Age { get; init; }
    public Gender Gender { get; init; }
    public IEnumerable<string> FavoriteSubjects
    {
        get => _favoriteSubjects;
        set
        {
            _favoriteSubjects = value.Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }
    }

    public override string ToString()
    {
        return $"{$"{Name} {SurName}".Trim()}: {Age}; {Gender}; ♥ предметы: [{string.Join(", ", _favoriteSubjects)}]";
    }
}
