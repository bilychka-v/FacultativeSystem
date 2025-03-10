using FacultativeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure;

public class DataAccess : DbContext
{
    public DataAccess(DbContextOptions<DataAccess> options) : base(options) { }
    
    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<FeedbackGradeEntity> FeedbackGradeEntities { get; set; }
}