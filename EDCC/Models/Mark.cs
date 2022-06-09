using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EDCC.Models;

public class Mark
{
   [Key]
   public int Id { get; set; }
   
   public int MarkStudent { get; set; }
   
   [ForeignKey("Lessons")]
   public int LessonId { get; set; }
   
   public virtual Lesson Lesson { get; set; }
   
   [ForeignKey("AspNetUsers")]
   public string UserId { get; set; }
   
   public virtual ApplicationUser User { get; set; }
}