using Grpc.Core;
using Contracts;

public class MakeGrpcService : MakeService.MakeServiceBase
{
    private readonly IMakeGrpcLogic _makeLogic;

    public MakeGrpcService(IMakeGrpcLogic makeLogic)
    {
        _makeLogic = makeLogic;
    }

    public override async Task<MakeResponse> GetMakes(
    MakeRequest request,
    ServerCallContext context)
    {
        var result = await _makeLogic.GetMakesAsync();
        return result;
    }
}
