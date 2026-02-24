public class FiltersEntity
{
    public List<FuelType>? FuelNames { get; set; }
    public decimal? MinBudget { get; set; }
    public decimal? MaxBudget { get; set; }
    public List<int>? MakeIds { get; set; }
    public int? CityId { get; set; }
    public int? So { get; set; }
    public int? Sc { get; set; }
    public int PageNo { get; set; }
}
