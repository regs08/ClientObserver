public static class NullPropertyChecker
{
    public static string CheckForNullProperties<T>(T objectToCheck)
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(string) && string.IsNullOrEmpty(property.GetValue(objectToCheck) as string))
            {
                return property.Name;
            }
        }
        return null;
    }
}
