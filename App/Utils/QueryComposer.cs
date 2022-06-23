using App.Models.Options;

namespace App.Utils;

public static class QueryComposer
{
    public static string GenerateSelectQuery(QueryOptions options)
    {
        var query = $"SELECT {string.Join(",", options.Fields)} FROM {options.Table}";

        if (!string.IsNullOrEmpty(options.WhereClause))
            query = $"{query} WHERE {options.WhereClause}";

        if (!string.IsNullOrEmpty(options.OrderBy))
            query = $"{query} ORDER BY {options.OrderBy}";

        if (options.Limit.HasValue)
            query = $"{query} LIMIT {options.Limit.Value}";

        return query;
    }

    public static string GenerateCountQuery(QueryOptions options)
    {
        return $"SELECT COUNT() FROM {options.Table}";
    }
}