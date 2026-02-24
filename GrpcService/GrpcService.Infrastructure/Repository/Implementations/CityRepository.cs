using Contracts;
using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

public class CityRepository : ICityRepository
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;
    private readonly IQueryBuilder _queryBuilder;

    public CityRepository(IConfiguration config , IQueryBuilder queryBuilder)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("DefaultConnection")!;
        _queryBuilder = queryBuilder;
    }

    public async Task<IEnumerable<CityEntity>> GetCitiesAsync()
    {
        using var connection = new MySqlConnection(_connectionString);
        string query = _queryBuilder.CityQuery();

        var result = await connection.QueryAsync<CityEntity>(query);

        return result;
    }

}
