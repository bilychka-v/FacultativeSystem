namespace FacultativeSystem.Application.Models;

public class Teacher
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    public Guid? CourseId { get; set; }
}