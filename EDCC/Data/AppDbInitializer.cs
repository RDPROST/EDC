using EDCC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EDCC.Data;

public class AppDbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        context?.Database.EnsureCreated();
        
        //Create Groups
        if (context != null)
        {
            context.Groups?.AddRange(new List<Group>()
            {
                new Group()
                {
                    Name = "222"
                },
                new Group()
                {
                    Name = "111"
                },
                new Group()
                {
                    Name = "212"
                },
                new Group()
                {
                    Name = "231"
                }
            });
            context.SaveChanges();
        }

        //Create Subjects
        if (context != null)
        {
             context.Subjects?.AddRange(new List<Subject>()
             {
                 new Subject()
                 {
                     Name = "ОА и Пр"
                 },
                 new Subject()
                 {
                     Name = "Ин Яз"
                 },
                 new Subject()
                 {
                     Name = "Физ. культура"
                 }
             });  
             context.SaveChanges();
        }

        //Create Roles

        List<IdentityRole> identityRoles = new List<IdentityRole>();
        if (context != null)
        {
            var roles = new string[] { "Admin", "Manager", "Teacher", "Student" };
            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    var res = context.Roles.Add(new IdentityRole()
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    });
                    identityRoles.Add(res.Entity);
                }
            }
            context.SaveChanges();
        }

        //Create User
        EntityEntry<IdentityUser> respUser = null;
        if (context != null)
        {
            
            var user = new ApplicationUser
            {
                FirstName = "123123123",
                LastName = "123123123",
                Email = "test@test.ru",
                NormalizedEmail = "test@test.ru".ToUpper(),
                UserName = "Tester",
                NormalizedUserName = "Tester".ToUpper(),
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"Secret1");
                user.PasswordHash = hashed;
                respUser = context.Users.Add(user);
                context.SaveChanges();
            }
        }

        if ( context != null && !context.UserRoles.Any(u => u.UserId == respUser.Entity.Id))
        {
            context.UserRoles?.AddRange(new List<IdentityUserRole<string?>>()
            {
                new IdentityUserRole<string?>()
                {
                    RoleId = identityRoles.Find(x => x.Name.Contains("Admin"))?.Id,
                    UserId = respUser?.Entity.Id
                }
            });
            context.SaveChanges();
        }

    }
}