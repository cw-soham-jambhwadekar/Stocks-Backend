using Contracts;
using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

public class StockRepository : IStockRepository
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;
    private readonly IQueryBuilder _queryBuilder;

    public StockRepository(IConfiguration config , IQueryBuilder queryBuilder)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("DefaultConnection")!;
        _queryBuilder = queryBuilder;
    }

    public async Task<IEnumerable<StockEntity>> GetStocksAsync(FiltersEntity filters)
    {
        using var connection = new MySqlConnection(_connectionString);

        var (query, parameters) = _queryBuilder.StockQuery(filters);
        var aggregator = new StockAggregator();

        await connection.QueryAsync<StockEntity, ImageRow, StockEntity>
        (
            query,
            aggregator.Map,
            parameters,
            splitOn: "imageUrl"
        );

        return aggregator.Result;
    }

}
