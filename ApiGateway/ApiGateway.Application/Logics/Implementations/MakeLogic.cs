using Grpc.Net.Client;
using Contracts;
using System.Linq;

public class MakeLogic : IMakeLogic
{
    private readonly MakeDataAccess _makeDataLayer;
    private readonly MakeMapper makeMapper;

    public MakeLogic(MakeDataAccess makeDataLayer , MakeMapper _makeMapper)
    {
        _makeDataLayer = makeDataLayer;
        makeMapper = _makeMapper;
    }

    public async Task<MakesResponseDto> GetMakesAsync()
    {
        var grpcResult = await _makeDataLayer.GetMakesAsync();
        var response = new MakesResponseDto();
        response.Makes = grpcResult.Makes.Select(make => makeMapper.Map(make)).ToList();
        return response;
    }
}
