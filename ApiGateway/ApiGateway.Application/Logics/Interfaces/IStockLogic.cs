using Contracts;

public interface IStockLogic
{
   Task<StocksResponseDto> GetStocksAsync(FiltersDto filtersDto);
}