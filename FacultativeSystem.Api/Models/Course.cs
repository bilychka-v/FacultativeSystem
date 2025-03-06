namespace FacultativeSystem.Api.Models;

public class Course
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;
    public DateTime StartDate { get; } 
    public DateTime EndDate { get; }

    public Course(Guid id, string name, DateTime startDate, DateTime endDate)
    {
        Id = id;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static (Course Course, string Error) Create(Guid id, string name, DateTime startDate, DateTime endDate)
    {
        var error = string.Empty;
        if(string.IsNullOrWhiteSpace(name)) error = "Name is required";
        
        var course = new Course(id, name, startDate, endDate);
        return (course, error);
    }
}