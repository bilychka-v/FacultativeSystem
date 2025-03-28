namespace FacultativeSystem.Application.Models;

public class Student
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    
    public List<FeedbackGrade>?  StudentCourseGrades { get; set; } = [];
    public List<string>? Courses { get; set; } = [];
}