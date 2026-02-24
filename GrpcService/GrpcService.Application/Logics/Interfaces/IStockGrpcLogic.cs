using Contracts;

public interface IStockGrpcLogic
{
    Task<StockResponse> GetStocksAsync(FiltersEntity filters);
}