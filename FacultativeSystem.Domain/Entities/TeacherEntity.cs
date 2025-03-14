namespace FacultativeSystem.Domain.Entities;

public class TeacherEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    
    // list of courses
    // public ICollection<CourseEntity>? CourseEntity { get; set; } = new List<CourseEntity>();
    public List<CourseEntity>? CourseNames { get; set; } = new();
}