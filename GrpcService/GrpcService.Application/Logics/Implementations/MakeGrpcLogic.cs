using Grpc.Core;
using Contracts;

public class MakeGrpcLogic : IMakeGrpcLogic
{
    private readonly IMakeRepository _makeRepository;
    private readonly MakeMapper makeMapper;

    public MakeGrpcLogic(IMakeRepository makeRepository, MakeMapper _makeMapper)
    {
        _makeRepository = makeRepository;
        makeMapper = _makeMapper;
    }

    public async Task<MakeResponse> GetMakesAsync()
    {
        var makes = await _makeRepository.GetMakesAsync();
        var result = new MakeResponse();
        result.Makes.AddRange( makes.Select(e => makeMapper.Map(e)));
        return result;
    }
}
