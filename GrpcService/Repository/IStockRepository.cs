using Contracts;

public interface IStockRepository
{
    Task<IEnumerable<StockEntity>> GetStocksAsync(Filters filters);
}
