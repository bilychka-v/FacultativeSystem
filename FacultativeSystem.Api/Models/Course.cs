using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Api.Models;

public class Course
{
    public string Name { get; private set; } = String.Empty;
    public DateTime StartDate { get; private set; } 
    public DateTime EndDate { get; private set; }
    
    public ICollection<StudentCourseGrade> StudentCourseGrades { get; private set; } = new List<StudentCourseGrade>();
    
    [ForeignKey(("Teacher"))]
    public Teacher? Teacher { get; private set; }
    
}