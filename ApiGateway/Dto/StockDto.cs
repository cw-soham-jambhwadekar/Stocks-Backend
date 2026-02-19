public class StockDto
{
    public string ProfileId { get; set; } = "";
    public string CarName { get; set; } = "";
    public string ModelName { get; set; } = "";
    public int MakeYear { get; set; }
    public int MakeId { get; set; }
    public int CityId { get; set; }
    public decimal Price { get; set; }
    public string FormattedPrice {get; set;} = "";
    public string Fuel { get; set; } = "";
    public int Km { get; set; }

    public string MakeName { get; set; } = "";
    public string CityName { get; set; } = "";

    public Boolean IsValueForMoney { get; set; }

    public List<string> StockImages { get; set; } = new();
}