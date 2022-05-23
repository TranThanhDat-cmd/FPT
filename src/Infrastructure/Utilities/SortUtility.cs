using System.Collections;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Infrastructure.Utilities;

public class SortUtility<T> where T : class
{
    public static IQueryable<T> ApplySort(IQueryable<T> entities, string? orderByQueryString)
    {
        if (!entities.Any() || string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return entities;
        }

        string[] orderParams = orderByQueryString.Trim().ToLower().Split(',').Where(orderParam => !string.IsNullOrWhiteSpace(orderParam)).ToArray();

        StringBuilder orderQueryBuilder = new();
        foreach (string orderParam in orderParams)
        {
            string propertyFromQueryName = orderParam.Trim().Split(" ")[0];
            PropertyInfo? propertyInfo = GetPropertyRecursive(typeof(T), propertyFromQueryName);

            if(propertyInfo is null || propertyInfo.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
            {
                continue;
            }
            string sortingOrder = orderParam.EndsWith(" desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{propertyFromQueryName} {sortingOrder}, ");
        }

        string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        if (string.IsNullOrEmpty(orderQuery))
        {
            return entities;
        }
        return entities.OrderBy(orderQuery);
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
