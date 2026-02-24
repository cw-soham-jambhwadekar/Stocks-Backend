using Contracts;
using Dapper;

public class QueryBuilder : IQueryBuilder
{
    public (string Sql, DynamicParameters Parameters) StockQuery(FiltersEntity filters)
    {
        var (where, parameters) = BuildFilters(filters);
        var sort = BuildSort(filters);
        var sql = BuildQuery(where, sort, filters.PageNo);

        return (sql, parameters);
    }

    public string MakeQuery()
    {
        return @"
            SELECT makeId, makeName
            FROM make
            ORDER BY makeName;
        ";
    }

    public string CityQuery()
    {
        return @"
            SELECT cityId, cityName
            FROM city
            ORDER BY cityName;
        ";
    }

    private (string whereClause, DynamicParameters parameters) BuildFilters(FiltersEntity filters)
    {
        var where = new List<string>();
        var parameters = new DynamicParameters();

        if (filters.FuelNames?.Count != 0)
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

        if (filters.MakeIds?.Count != 0)
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

    private string BuildSort(FiltersEntity filters)
    {
        if (filters.So == 0 && filters.Sc == 2)
        {
            return "order by s.Price";
        }
        else if (filters.So == 1 && filters.Sc == 2)
        {
            return "order by s.Price desc";
        }

        return "order by s.ProfileId";
    }

    private string BuildQuery(string whereClause, string orderClause, int pageNo)
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

}