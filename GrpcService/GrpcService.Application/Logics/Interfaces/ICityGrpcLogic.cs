using Contracts;

public interface ICityGrpcLogic
{
    Task<CityResponse> GetCitiesAsync();
}