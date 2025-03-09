namespace FacultativeSystem.Domain.Entities;

public class TeacherEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public CourseEntity? CourseEntities { get; set; }
}