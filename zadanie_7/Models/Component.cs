using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanie_7.Models;

public class Component
{
    [Key]
    [Column(TypeName = "char(10)")]
    [MaxLength(10)]
    public string Code { get; set; }
    
    [Required, MaxLength(300)]
    public string Name { get; set; }
    
    [Column(TypeName = "nvarchar(max)")]
    public string Description { get; set; }
    
    public int ComponentManufacturersId { get; set; }
    [ForeignKey(nameof(ComponentManufacturersId))]
    public ComponentManufacturer ComponentManufacturer { get; set; }
    
    public int ComponentTypesId { get; set; }
    [ForeignKey(nameof(ComponentTypesId))]
    public ComponentType ComponentType { get; set; }

    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}