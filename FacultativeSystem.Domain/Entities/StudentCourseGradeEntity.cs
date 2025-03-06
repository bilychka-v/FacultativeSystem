using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Domain.Entities;

public class StudentCourseGradeEntity
{
    public Guid Id { get; set; }
    
    [ForeignKey("StudentId")]
    public Guid StudentId { get; set; }
    public StudentEntity Student { get; set; }

    [ForeignKey("CourseId")]
    public Guid CourseId { get; set; }
    public CourseEntity Course { get; set; }

    public int Grade { get; set; }
    public string Feedback { get; set; } = string.Empty;

    public DateTime GradedAt { get; set; }
}