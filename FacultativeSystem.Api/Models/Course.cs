using System.ComponentModel.DataAnnotations.Schema;

namespace FacultativeSystem.Api.Models;

public class Course
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;
    public DateTime StartDate { get; } 
    public DateTime EndDate { get; }
    
    public ICollection<Student> Students { get; } = new List<Student>();
    
    [ForeignKey(("Teacher"))]
    public Guid TeacherId { get; }
    public Teacher Teacher { get; }

    public Course(Guid id, string name, DateTime startDate, DateTime endDate, 
                    ICollection<Student> students, Guid teacherId, Teacher teacher)
    {
        Id = id;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Students = students;
        TeacherId = teacherId;
        Teacher = teacher;
    }
    
}