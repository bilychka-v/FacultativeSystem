using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FacultativeSystem.Domain.Entities;

public class CourseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime? StartDate { get; set; } 
    public DateTime? EndDate { get; set; }
    public bool HasUnmarkedStudents { get; set; }
    
    public Guid? TeacherId { get; set; } = Guid.Empty;
    
    //TeacherEntities 1--> CourseEntities 
    [ForeignKey(("TeacherId"))]
    public TeacherEntity? Teacher { get; set; }
    
}