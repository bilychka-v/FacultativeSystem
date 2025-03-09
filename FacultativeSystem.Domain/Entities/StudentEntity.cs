namespace FacultativeSystem.Domain.Entities;

public class StudentEntity
{
    public Guid Id { get; set;  }
    public string Name { get; set; } = String.Empty;
    
    public ICollection<StudentCourseGradeEntity> StudentCourseCourseGradeEntities { get; set; } = new List<StudentCourseGradeEntity>();
    public ICollection<TeacherEntity> TeacherEntities { get; set; } = new List<TeacherEntity>();
}