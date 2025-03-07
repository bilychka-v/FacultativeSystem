using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Domain.Entities;

public class CourseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    
    public ICollection<StudentCourseGradeEntity> StudentCourseGradeEntities { get; set; } = new List<StudentCourseGradeEntity>();
    
    [ForeignKey(("TeacherEntities"))]
    public Guid TeacherId { get; set; }

    public TeacherEntity TeacherEntities { get; set; } = null!;
}