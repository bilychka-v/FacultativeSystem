namespace FacultativeSystem.Api.Models;

public class Student
{
    public Guid Id { get; }
    public string Name { get; } = String.Empty;
    
    public ICollection<Course> Courses { get; } = new List<Course>();
    public ICollection<StudentCourseGrade>  StudentCourseGrades { get; } = new List<StudentCourseGrade>();

    public Student(Guid id, string name, ICollection<Course> courses, ICollection<StudentCourseGrade> studentCourseGrades)
    {
        Id = id;
        Name = name;
        Courses = courses;
        StudentCourseGrades = studentCourseGrades;
    }
    
}