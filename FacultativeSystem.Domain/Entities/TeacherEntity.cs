namespace FacultativeSystem.Domain.Entities;

public class TeacherEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    
    // list of courses
    public List<CourseEntity>? Courses { get; set; } = new();
}