using FacultativeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FacultativeSystem.Infrastructure;

public class DataAccess : DbContext
{

    public DataAccess(DbContextOptions<DataAccess> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "FacultativeSystem.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();
            
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("WebDatabase"));
    }

    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<FeedbackGradeEntity> FeedbackGradeEntities { get; set; }
}
