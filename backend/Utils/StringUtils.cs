namespace Backend.Utils;

public static class StringUtils
{
    public static string FullName(string firstName, string? middleName, string lastName)
    {
        List<string?> names = [firstName, middleName, lastName];

        return string.Join(" ", names.Where(n => !string.IsNullOrWhiteSpace(n)));
    }
}