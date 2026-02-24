public static class FuelLookup
{
    public static readonly IReadOnlyDictionary<string, string> IdToName =
        new Dictionary<string, string>
        {
            { "1", "Petrol" },
            { "2", "Diesel" },
            { "3", "Cng" },
            { "4", "Lpg" },
            { "5", "Electric" },
            { "6", "Hybrid" }
        };

    public static readonly IReadOnlyDictionary<string, string> NameToId =
        IdToName.ToDictionary(x => x.Value, x => x.Key);
}