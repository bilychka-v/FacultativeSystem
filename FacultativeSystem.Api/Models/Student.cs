namespace FacultativeSystem.Api.Models;

public class Student
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;
    public string Feedback { get; } = String.Empty;
    public int Grade { get; }

    public Student(Guid id, string name, string feedback,  int grade)
    {
        Id = id;
        Name = name;
        Feedback = feedback;
        Grade = grade;
    }

    public (Student Student, string Error) Create(Guid id, string name, string feedback, int grade)
    {
        var error = string.Empty;
        if (string.IsNullOrWhiteSpace(name))
        {
            error="Enter all fields";
        }
        
        var student = new Student(id, Name, Feedback, Grade);
        return (student, error);
    }
}