
namespace FacultativeSystem.Application.Models;

public class FeedbackGrade
{
    public Guid Id { get; set; }
    public int Grade { get; set; }
    public string Feedback { get; set; } = string.Empty;

    public DateTime GradedAt { get; set; }
    
    // FeedbackGradeEntity 1--> StudentEntities
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public string StudentName { get; set; } = string.Empty;
    
    // FeedbackGradeEntity 1--> CourseEntities
    public Guid CourseId { get; set; }
    
    public Course? Course { get; set; }
}