using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Domain.Entities;

public class FeedbackGradeEntity
{
    public Guid Id { get; set; }
    
    [ForeignKey("StudentId")]
    public Guid StudentId { get; set; }
    public StudentEntity StudentEntity { get; set; }

    [ForeignKey("CourseId")]
    public Guid CourseId { get; set; }
    public CourseEntity CourseEntity { get; set; }
    
    [ForeignKey("TeacherId")]
    public Guid TeacherId { get; set; }
    public TeacherEntity TeacherEntity { get; set; }

    public int Grade { get; set; }
    public string Feedback { get; set; } = string.Empty;

    public DateTime GradedAt { get; set; }
}