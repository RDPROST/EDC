using System.ComponentModel.DataAnnotations;

namespace EDCC.Models;

public class ProjectRole
{
    public int Id { get; set; }
    
    [Required]
    public string RoleName { get; set; }
}