
namespace FacultativeSystem.Application.Models;

public class FeedbackGrade
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }
    // public Guid TeacherId { get; set; }
    public Guid CourseId { get; set; }

    public string? StudentName { get; set; }
    // public string? TeacherName { get; set; }
    public string? CourseName { get; set; }

    public DateTime GradedAt { get; set; }
}