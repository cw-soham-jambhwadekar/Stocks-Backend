using Riok.Mapperly.Abstractions;
using Contracts;

[Mapper]
public partial class StockMapper
{
    [MapProperty(nameof(Stock), nameof(StockDto.CarName), Use = nameof(MapCarName))]
    [MapProperty(nameof(Stock), nameof(StockDto.FormattedPrice), Use = nameof(MapFormattedPrice))]
    [MapProperty(nameof(Stock), nameof(StockDto.IsValueForMoney), Use = nameof(MapIsValueForMoney))]
    public partial StockDto ToDto(Stock stock);

    private static string MapCarName(Stock stock)
        => $"{stock.MakeYear} {stock.MakeName} {stock.ModelName}";

    private static string MapFormattedPrice(Stock stock)
        => stock.Price < 100000 ? stock.Price.ToString("N0") : (stock.Price / 100000) + " Lakhs";
    
    private static Boolean MapIsValueForMoney(Stock stock)
        => stock.Km < 10000 && stock.Price < 200000;
}
