namespace FacultativeSystem.Domain.Entities;

public class StudentEntity
{
    public Guid Id { get; set;  }
    public string UserName { get; set; } = String.Empty;
    
    public ICollection<FeedbackGradeEntity> FeedbackGradeEntities { get; set; } = new List<FeedbackGradeEntity>();
    public ICollection<CourseEntity> CourseEntities { get; set; } = new List<CourseEntity>();
}