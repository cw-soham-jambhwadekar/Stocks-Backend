public interface IStockAggregator
{
    IEnumerable<StockEntity> Aggregate(
        IEnumerable<(StockEntity stock, string? imageUrl)> rows);
}