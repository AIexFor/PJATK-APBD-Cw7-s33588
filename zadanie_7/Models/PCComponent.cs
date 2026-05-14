using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanie_7.Models;

public class PCComponent
{
    public int PCId { get; set; }
    [ForeignKey(nameof(PCId))]
    public PC PC { get; set; }
    
    [Column(TypeName = "char(10)")]
    [MaxLength(10)]
    public string ComponentCode { get; set; }
    [ForeignKey(nameof(ComponentCode))]
    public Component Component { get; set; }
    
    public int Amount { get; set; }
}