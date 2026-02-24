using Contracts;
using Dapper;

public interface IQueryBuilder
{
    (string Sql, DynamicParameters Parameters) StockQuery(FiltersEntity filters);
    string MakeQuery();
    string CityQuery();
}