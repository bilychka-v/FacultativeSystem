namespace FacultativeSystem.Domain.Entities;

public class TeacherEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    public CourseEntity? CourseEntity { get; set; }
}