
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Api.Models;

public class StudentCourseGrade
{
    public Guid Id { get; }
    
    [ForeignKey("Student")]
    public Guid StudentId { get; }
    public Student Student { get; }

    [ForeignKey("Course")]
    public Guid CourseId { get;  }
    public Course Course { get; }

    public int Grade { get; }
    public string Feedback { get;  } = string.Empty;

    public DateTime GradedAt { get; }
}