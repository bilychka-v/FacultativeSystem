namespace FacultativeSystem.Api.Models;

public class Student
{
    public string Name { get; private set; } = String.Empty;
    
    public ICollection<Teacher> Teachers { get; private set; } = new List<Teacher>();
    public ICollection<StudentCourseGrade>  StudentCourseGrades { get; private set; } = new List<StudentCourseGrade>();
}