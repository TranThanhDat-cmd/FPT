using System.Collections;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Infrastructure.Utilities;

public class SearchUtility<T> where T : class
{
    public static IQueryable<T> ApplySearch(ref IQueryable<T> entities, string keyword, string searchQueryString)
    {
        if (!entities.Any() || string.IsNullOrWhiteSpace(keyword) || string.IsNullOrWhiteSpace(searchQueryString))
        {
            return entities;
        }
        string[] searchParams = searchQueryString.Trim().Split(",").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        StringBuilder searchQueryBuilder = new();
        foreach (string searchParam in searchParams)
        {
            PropertyInfo? propertyInfo = GetPropertyRecursive(typeof(T), searchParam);
            if (propertyInfo is null || propertyInfo.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
            {
                continue;
            }

            searchQueryBuilder.Append($"{searchParam}.ToLower().Contains(@0) or ");
        }
        string searchQuery = searchQueryBuilder.ToString().TrimEnd(' ', 'o', 'r', ' ');
        if (string.IsNullOrEmpty(searchQuery))
        {
            return entities;
        }
        return entities = entities.Where(searchQuery, keyword.ToLower());
    }

    private static PropertyInfo? GetPropertyRecursive(Type baseType, string propertyName)
    {
        string[] parts = propertyName.Split('.');

        PropertyInfo? propertyInfo = baseType.GetProperty(parts[0], BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        if (propertyInfo is null)
        {
            return null;
        }

        return (parts.Length > 1)
            ? GetPropertyRecursive(propertyInfo.PropertyType, parts.Skip(1).Aggregate((a, i) => a + "." + i))
            : baseType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
    }
}
