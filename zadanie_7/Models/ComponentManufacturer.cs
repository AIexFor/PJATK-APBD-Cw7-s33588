using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanie_7.Models;

public class ComponentManufacturer
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(30)]
    public string Abbreviation { get; set; }
    
    [Required, MaxLength(300)]
    public string FullName { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime FoundationDate { get; set; }

    public ICollection<Component> Components { get; set; } = new List<Component>();
}