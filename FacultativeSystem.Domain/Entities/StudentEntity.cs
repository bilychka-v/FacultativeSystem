namespace FacultativeSystem.Domain.Entities;

public class StudentEntity
{
    public Guid Id { get; set;  }
    public string? UserName { get; set; } = String.Empty;
    public int Age { get; set; }
    
    //list of grades + feedbacks
    public ICollection<FeedbackGradeEntity>? FeedbackGradeEntities { get; set; } = new List<FeedbackGradeEntity>();
}