namespace zadanie_7.DTOs;

public class ComponentInfoDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ManufacturerDto Manufacturer { get; set; }
    public TypeDto Type { get; set; }
}