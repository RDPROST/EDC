using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EDCC.Models;

public class Student
{
   [Key]
   public int Id { get; set; }
   
   [Required]
   public int Age { get; set; }

   public enum Sex
   {
      man,
      wooman
   }
   
   [Required]
   public DateTime BirthDay { get; set; }
   
   [Required]
   public DateTime StartTraining { get; set; }
   
   [Required]
   [ForeignKey("AspNetUsers")]
   public string UserId { get; set; }
   
   public virtual ApplicationUser User { get; set; }
   
   [Required]
   [ForeignKey("Groups")]
   public int GroupId { get; set; }
   
   public virtual Group Group { get; set; }

}