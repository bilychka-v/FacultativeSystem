namespace FacultativeSystem.Api.Models;

public class Teacher
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;

    public Teacher(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static (Teacher Teacher, string Error) Create(Guid id, string name)
    {
        var error = string.Empty;
        if(string.IsNullOrWhiteSpace(name)) error = "Name is required";
        
        var teacher = new Teacher(id, name);
        
        return (teacher, error);
    }
}