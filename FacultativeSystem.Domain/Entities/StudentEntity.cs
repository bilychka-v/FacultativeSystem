namespace FacultativeSystem.Domain.Entities;

public class StudentEntity
{
    public Guid Id { get; set;  }
    public string UserName { get; set; } = String.Empty;
    
    //list of grades + feedbacks
    public ICollection<FeedbackGradeEntity>? FeedbackGradeEntities { get; set; } = new List<FeedbackGradeEntity>();
}