public class FiltersTranslator : IFiltersTranslator
{
    public FiltersEntity Translate(FiltersDto dto)
    {
        var filters = new FiltersEntity();

        if (!string.IsNullOrWhiteSpace(dto.Fuel))
        {
            var ids = dto.Fuel.Split(" ");

            var fuelList = new List<FuelType>();

            foreach (var id in ids)
            {
                if (!int.TryParse(id, out var intId) ||
                    !Enum.IsDefined(typeof(FuelType), intId))
                {
                    throw new ArgumentException($"Invalid fuel id: {id}");
                }

                fuelList.Add((FuelType)intId);
            }

            filters.FuelNames = fuelList;
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
