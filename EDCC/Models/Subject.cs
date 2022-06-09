using System.ComponentModel.DataAnnotations;

namespace EDCC.Models;

public class Subject
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please write Name group")]
    public string Name { get; set; }
}