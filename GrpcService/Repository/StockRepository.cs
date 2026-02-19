using Contracts;
using Dapper;
using MySql.Data.MySqlClient;

public class StockRepository : IStockRepository
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;

    private class ImageRow
    {
        public string ImageUrl { get; set; } = "";
    }

    public StockRepository(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<StockEntity>> GetStocksAsync(Filters filters)
    {
        using var connection = new MySqlConnection(_connectionString);

        var (whereClause, parameters) = BuildFilters(filters);
        var orderClause = BuildSort(filters);
        var query = BuildQuery(whereClause , orderClause , filters.PageNo);

        var stockDict = new Dictionary<string, StockEntity>();

        await connection.QueryAsync<StockEntity, ImageRow, StockEntity>
        (
            query,
            (stock, image) => MapStock(stockDict, stock, image),
            parameters,
            splitOn: "imageUrl"
        );

        return stockDict.Values;
    }


    private (string whereClause, DynamicParameters parameters) BuildFilters(Filters filters)
    {
        var where = new List<string>();
        var parameters = new DynamicParameters();

        if (filters.FuelNames.Count != 0)
        {
            where.Add("s.fuel IN @Fuels");
            parameters.Add("Fuels", filters.FuelNames);
        }

        if (filters.MinBudget >= 0)
        {
            where.Add("s.price >= @MinBudget");
            parameters.Add("MinBudget", filters.MinBudget);
        }

        if (filters.MaxBudget > 0)
        {
            where.Add("s.price <= @MaxBudget");
            parameters.Add("MaxBudget", filters.MaxBudget);
        }

        if (filters.MakeIds.Count != 0)
        {
            where.Add("s.makeId IN @MakeIds");
            parameters.Add("MakeIds", filters.MakeIds);
        }

        if (filters.CityId > 0)
        {
            where.Add("s.cityId = @CityId");
            parameters.Add("CityId", filters.CityId);
        }

        var whereClause = where.Count != 0
            ? "WHERE " + string.Join(" AND ", where)
            : "";

        return (whereClause, parameters);
    }

    private string BuildSort(Filters filters)
    {
        if(filters.So == 0 && filters.Sc == 2)
        {
            return "order by s.Price";
        }
        else if(filters.So == 1 && filters.Sc == 2)
        {
            return "order by s.Price desc";
        }

        return "order by s.ProfileId";
    }

    private string BuildQuery(string whereClause , string orderClause , int pageNo)
    {
        return $@"
            SELECT
            s.profileId, s.modelName, s.makeYear, s.makeId, 
            s.cityId, s.price, s.fuel, s.km,
            m.makeName, c.cityName, i.imageUrl
            FROM (
                SELECT *
                FROM stock s
                {whereClause}
                {orderClause} 
                LIMIT 30
                offset {pageNo * 30}
            ) s
            JOIN make m ON s.makeId = m.makeId
            JOIN city c ON s.cityId = c.cityId
            LEFT JOIN stock_images i ON s.profileId = i.profileId
            
        ";
    }

    private StockEntity MapStock(
    Dictionary<string, StockEntity> stockDict,
    StockEntity stock,
    ImageRow image)
    {
        if (!stockDict.TryGetValue(stock.ProfileId, out var existing))
        {
            existing = stock;
            existing.StockImages = new List<string>();
            stockDict.Add(existing.ProfileId, existing);
        }

        if (image != null && !string.IsNullOrEmpty(image.ImageUrl))
        {
            existing.StockImages.Add(image.ImageUrl);
        }

        return existing;
    }

}
