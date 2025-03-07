using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Api.Models;

public class Course
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;
    public DateTime StartDate { get; } 
    public DateTime EndDate { get; }
    
    public ICollection<StudentCourseGrade> StudentCourseGrades { get; } = new List<StudentCourseGrade>();
    
    [ForeignKey(("Teacher"))]
    public Guid TeacherId { get; }
    public Teacher Teacher { get; }
    
}