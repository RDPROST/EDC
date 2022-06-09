using System.ComponentModel.DataAnnotations;

namespace EDCC.Models;

public class File
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}