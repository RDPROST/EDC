using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EDCC.Models;

public class Lesson : BaseEntity
{
   [Key]
   public int Id { get; set; }
   
   [Required]
   public int TimeSlot { get; set; }
   
   [Required]
   [DataType(DataType.Date)]
   public DateTime Date { get; set; }

   [Required]
   [ForeignKey("Groups")]
   public int GroupId { get; set; }
   
   public virtual Group Group { get; set; }
   
   [Required]
   [ForeignKey("AspNetUsers")]
   public string UserId { get; set; }
   
   public virtual ApplicationUser User { get; set; }
   
   [Required]
   [ForeignKey("Subjects")]
   public int SubjectId { get; set; }
   
   public virtual Subject Subject { get; set; }
   
   [DataType(DataType.Upload)]
   [ForeignKey("Files")]
   public int? FileId { get; set; }
   
   public virtual File File { get; set; }

}