namespace FacultativeSystem.Api.Models;

public class Teacher
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;
    public Guid CourseId { get; }
    public Course Course { get; }

   
}