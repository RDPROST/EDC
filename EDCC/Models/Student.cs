using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fingers10.ExcelExport.Attributes;
using Microsoft.AspNetCore.Identity;

namespace EDCC.Models;

public class Student
{
   [Key]
   [IncludeInReport(Order = 1)]
   public int Id { get; set; }
   
   [Required]
   [IncludeInReport(Order = 2)]
   public int Age { get; set; }

   public enum Sex
   {
      man,
      wooman
   }
   
   [Required]
   [IncludeInReport(Order = 3)]
   public DateTime BirthDay { get; set; }
   
   [Required]
   [IncludeInReport(Order = 4)]
   public DateTime StartTraining { get; set; }
   
   [Required]
   [ForeignKey("AspNetUsers")]
   public string UserId { get; set; }
   
   public virtual ApplicationUser User { get; set; }
   
   [Required]
   [IncludeInReport(Order = 5)]
   [ForeignKey("Groups")]
   public int GroupId { get; set; }
   
   public virtual Group Group { get; set; }

}