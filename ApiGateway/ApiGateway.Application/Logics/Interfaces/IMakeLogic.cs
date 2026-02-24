using Contracts;

public interface IMakeLogic
{
   Task<MakesResponseDto> GetMakesAsync();
}