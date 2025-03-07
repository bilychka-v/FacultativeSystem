namespace FacultativeSystem.Api.Models;

public class Student
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;
    
    public ICollection<Teacher> Teachers { get; } = new List<Teacher>();
    public ICollection<StudentCourseGrade>  StudentCourseGrades { get; } = new List<StudentCourseGrade>();
}