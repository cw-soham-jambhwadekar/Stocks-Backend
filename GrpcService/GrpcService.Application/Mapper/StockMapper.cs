using Riok.Mapperly.Abstractions;
using Contracts;

[Mapper]
public partial class StockMapper
{
    public partial Stock Map(StockEntity entity);
}
