using zadanie_7.DTOs;

namespace zadanie_7.Services;

public interface IPCService
{
    Task<List<PCResponseDto>> GetAllPCsAsync();
    Task<PCDetailsResponseDto?> GetPCDetailsAsync(int id);
    Task<PCResponseDto> CreatePCAsync(PCRequestDto dto);
    Task<bool> UpdatePCAsync(int id, PCRequestDto dto);
    Task<bool> DeletePCAsync(int id);
}