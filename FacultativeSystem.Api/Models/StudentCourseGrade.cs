
using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Api.Models;

public class StudentCourseGrade
{
    [ForeignKey("Student")]
    public Student? Student { get; private set; }

    [ForeignKey("Course")]
    public Course? Course { get; private set; }

    public int Grade { get; private set; }
    public string Feedback { get; private set; } = string.Empty;

    public DateTime GradedAt { get; private set; }
}