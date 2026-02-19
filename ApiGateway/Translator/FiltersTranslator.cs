public class FiltersTranslator : IFiltersTranslator
{
    private static readonly Dictionary<string, string> FuelMap = new()
    {
        { "1", "Petrol" },
        { "2", "Diesel" },
        { "3", "Cng" },
        { "4", "Lpg" },
        { "5", "Electric" },
        { "6", "Hybrid" }
    };
    public FiltersEntity Translate(FiltersDto dto)
    {
        var filters = new FiltersEntity();

        if (!string.IsNullOrWhiteSpace(dto.Fuel))
        {
            var ids = dto.Fuel.Split(" ");

            var invalidIds = ids.Where(id => !FuelMap.ContainsKey(id)).ToList();

            if (invalidIds.Any())
            {
                throw new ArgumentException(
                    $"Invalid fuel ids: {string.Join(", ", invalidIds)}");
            }

            filters.FuelNames = ids.Select(id => FuelMap[id]).ToList();
        }

        if (!string.IsNullOrWhiteSpace(dto.Budget))
        {
            var parts = dto.Budget.Split("-");
            if (parts.Length == 2 &&
                decimal.TryParse(parts[0], out var min) &&
                decimal.TryParse(parts[1], out var max))
            {
                filters.MinBudget = min * 100000;
                filters.MaxBudget = max * 100000;
            }
        }

        if (!string.IsNullOrWhiteSpace(dto.Cars))
        {
            filters.MakeIds = dto.Cars
                .Split(" ")
                .Select(int.Parse)
                .ToList();
        }

        filters.CityId = dto.City;

        filters.So = dto.So;
        filters.Sc = dto.Sc;

        filters.PageNo = dto.Pn;

        return filters;
    }
}
