namespace FacultativeSystem.Application.Models;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime? StartDate { get; set; } 
    public DateTime? EndDate { get; set; }
    public bool HasUnmarkedStudents { get; set; }
    
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } 
    public List<Student>? Students { get; set; } = [];

}