using Riok.Mapperly.Abstractions;
using Contracts;

[Mapper]
public partial class StockMapper
{
    // [ObjectFactory]
    // private static Stock CreateStock() => new();
    public partial Stock Map(StockEntity entity);
}
