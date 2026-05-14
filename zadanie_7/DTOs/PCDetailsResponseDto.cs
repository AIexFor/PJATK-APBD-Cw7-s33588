namespace zadanie_7.DTOs;

public class PCDetailsResponseDto : PCResponseDto
{
    public List<PCComponentDetailDto> Components { get; set; } = new();
}