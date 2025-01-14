public static class Validator
{
    public static string ValidateNonEmptyString(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{propertyName} cannot be null, empty, or whitespace.");
        }
        return value;
    }
}
