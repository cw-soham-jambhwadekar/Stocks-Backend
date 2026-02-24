public static class UrlBuilder
{
    public static string BuildNextPageUrl(FiltersEntity filters, string basePath)
    {
        var query = new List<string>();

        if (filters.FuelNames?.Any() == true)
        {
            var fuelIds = filters.FuelNames
            .Select(f => ((int)f).ToString());

            query.Add($"fuel={string.Join("+", fuelIds)}");
        }

        if (filters.MinBudget > 0 || filters.MaxBudget > 0)
        {
            var min = filters.MinBudget / 100000;
            var max = filters.MaxBudget / 100000;

            query.Add($"budget={min}-{max}");
        }

        if (filters.MakeIds?.Any() == true)
        {
            query.Add($"cars={string.Join(" ", filters.MakeIds)}");
        }

        if (filters.CityId > 0)
        {
            query.Add($"city={filters.CityId}");
        }

        if (filters.So > 0)
            query.Add($"so={filters.So}");

        if (filters.Sc > 0)
            query.Add($"sc={filters.Sc}");

        query.Add($"pn={filters.PageNo + 1}");

        return $"{basePath}?{string.Join("&", query)}";
    }
}