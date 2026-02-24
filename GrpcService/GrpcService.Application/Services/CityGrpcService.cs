using Grpc.Core;
using Contracts;

public class CityGrpcService : CityService.CityServiceBase
{
    private readonly ICityGrpcLogic _cityLogic;

    public CityGrpcService(ICityGrpcLogic cityLogic)
    {
        _cityLogic = cityLogic;
    }

    public override async Task<CityResponse> GetCities(
    CityRequest request,
    ServerCallContext context)
    {
        var result = await _cityLogic.GetCitiesAsync();
        return result;
    }
}
