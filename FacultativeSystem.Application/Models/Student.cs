namespace FacultativeSystem.Application.Models;

public class Student
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = String.Empty;
   

    // public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public List<FeedbackGrade>?  StudentCourseGrades { get; set; } = [];
    public List<Course> Courses { get; set; } = [];
}