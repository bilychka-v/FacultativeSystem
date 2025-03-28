using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure;

public class AuthDbContext : IdentityDbContext<IdentityUser>

{
    private DbContextOptions<AuthDbContext> _options;
    public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
    {
        _options = options;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var studentRoleId = "55b1a53a-8646-42b6-b05e-510be7b0f99c";
        var teacherRoleId = "8fb8c0f1-4167-4918-bdd5-35290888ff99";
        var adminRoleId = "12345678-1234-1234-1234-123456789abc";
        var roles = new List<IdentityRole>()
        {
            new IdentityRole()
            {
                Id = studentRoleId,
                Name = "student",
                NormalizedName = "student".ToUpper(),
                ConcurrencyStamp = studentRoleId,
            },
            new IdentityRole()
            {
                Id = teacherRoleId,
                Name = "teacher",
                NormalizedName = "teacher".ToUpper(),
                ConcurrencyStamp = teacherRoleId,
            },
            new IdentityRole()
            {
                Id = adminRoleId,
                Name = "admin",
                NormalizedName = "admin".ToUpper(),
                ConcurrencyStamp = adminRoleId,
            }
        };
        
        modelBuilder.Entity<IdentityRole>().HasData(roles);
        
        
        var adminUserId = "29c447a4-bc6d-46ac-9cc0-d08db3544a4f";
        var admin = new IdentityUser()
        {
            Id = adminUserId,
            UserName = "admin1@gmail.com",
            Email = "admin1@gmail.com",
            NormalizedEmail = "admin1@gmail.com".ToUpper(),
            NormalizedUserName = "admin1@gmail.com".ToUpper(),
            EmailConfirmed = true
        };
        
        admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
        modelBuilder.Entity<IdentityUser>().HasData(admin);

        var adminRole = new IdentityUserRole<string>()
        {
            RoleId = adminRoleId,
            UserId = adminUserId,
        };
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminRole);
    }
}