using Contracts;

public interface IStockRepository
{
    Task<IEnumerable<StockEntity>> GetStocksAsync(FiltersEntity filters);
}
