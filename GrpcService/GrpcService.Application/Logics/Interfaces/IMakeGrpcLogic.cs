using Contracts;

public interface IMakeGrpcLogic
{
    Task<MakeResponse> GetMakesAsync();
}