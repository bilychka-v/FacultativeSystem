
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Api.Models;

public class StudentCourseGrade
{
    public Guid StudentId { get; set; }
    
    // public Course? Course { get; set; }

    public int Grade { get; set; }
    public string Feedback { get; set; } = string.Empty;

    public DateTime GradedAt { get; set; }
}