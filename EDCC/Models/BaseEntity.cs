using System.ComponentModel.DataAnnotations;

namespace EDCC.Models;

public abstract class BaseEntity
{
    [DataType(DataType.Date)]
    public DateTime? CreatedAt { get; set; }
   
    [DataType(DataType.Date)]
    public DateTime? UpdatedAt { get; set; }
}