using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EDCC.Models;
using Microsoft.AspNetCore.Identity;

namespace EDCC.Data;

public class ApplicationDbContext : IdentityDbContext <IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<EDCC.Models.Group>? Groups { get; set; }

    public DbSet<EDCC.Models.Student>? Students { get; set; }
    public DbSet<EDCC.Models.Subject>? Subjects { get; set; }

    public DbSet<EDCC.Models.Lesson>? Lessons { get; set; }
    
    public DbSet<EDCC.Models.Mark>? Marks { get; set; }
    
    public DbSet<IdentityUser>? ApplicationUsers { get; set; }
}