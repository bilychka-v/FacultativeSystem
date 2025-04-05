using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Domain.Entities;

public class FeedbackGradeEntity
{
    public Guid Id { get; set; }
    public int Grade { get; set; }
    public string Feedback { get; set; } = string.Empty;

    public DateTime GradedAt { get; set; }
    
    // FeedbackGradeEntity 1--> StudentEntities
    [ForeignKey("StudentId")]
    public Guid StudentId { get; set; }
    public StudentEntity? Student { get; set; }
    
    // FeedbackGradeEntity 1--> CourseEntities
    [ForeignKey("CourseId")]
    public Guid CourseId { get; set; }
    public CourseEntity? Course { get; set; }
}