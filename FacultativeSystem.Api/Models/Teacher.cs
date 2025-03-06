namespace FacultativeSystem.Api.Models;

public class Teacher
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;
    public Guid CourseId { get; }
    public Course Course { get; }

    public Teacher(Guid id, string name, Guid courseId, Course course)
    {
        Id = id;
        Name = name;
        CourseId = courseId;
        Course = course;
    }
}