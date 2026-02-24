using Contracts;
using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

public class MakeRepository : IMakeRepository
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;
    private readonly IQueryBuilder _queryBuilder;

    public MakeRepository(IConfiguration config , IQueryBuilder queryBuilder)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("DefaultConnection")!;
        _queryBuilder = queryBuilder;
    }

    public async Task<IEnumerable<MakeEntity>> GetMakesAsync()
    {
        using var connection = new MySqlConnection(_connectionString);
        string query = _queryBuilder.MakeQuery();

        var result = await connection.QueryAsync<MakeEntity>(query);

        return result;
    }

}
