namespace FacultativeSystem.Api.Models;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    
    // public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    // public ICollection<StudentCourseGrade>  StudentCourseGrades { get; set; } = new List<StudentCourseGrade>();
}