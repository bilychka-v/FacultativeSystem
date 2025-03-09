namespace FacultativeSystem.Api.Models;

public class Course
{
    public string Name { get; set; } = String.Empty;
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    
    public ICollection<StudentCourseGrade> StudentCourseGrades { get; set; } = new List<StudentCourseGrade>();
    
    public Guid TeacherId { get; set; }
    
}