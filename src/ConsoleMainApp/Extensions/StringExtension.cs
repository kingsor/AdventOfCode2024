namespace ConsoleMainApp.Extensions;

public static class StringExtension
{
    public static string IndexOfAny(this string s, List<string> anyOf, StringComparison stringComparisonType = StringComparison.CurrentCultureIgnoreCase)
    {
        var dicFounds = new Dictionary<int, string>();

        anyOf.ForEach(sub =>
        {
            var pos = s.IndexOf(sub, stringComparisonType);
            if (pos >= 0)
            {
                dicFounds.Add(pos, sub);
            }
        });

        var keys = dicFounds.Keys.ToList();
        var found = keys.Any() ? keys.Min() : -1;

        return found >= 0 ? dicFounds[found] : string.Empty;
    }

    public static string LastIndexOfAny(this string s, List<string> anyOf, StringComparison stringComparisonType = StringComparison.CurrentCultureIgnoreCase)
    {
        var dicFounds = new Dictionary<int, string>();

        anyOf.ForEach(sub =>
        {
            var pos = s.LastIndexOf(sub, stringComparisonType);
            if (pos >= 0)
            {
                dicFounds.Add(pos, sub);
            }
        });

        var keys = dicFounds.Keys.ToList();
        var found = keys.Any() ? keys.Max() : -1;

        return found >= 0 ? dicFounds[found] : string.Empty;
    }
    
}
