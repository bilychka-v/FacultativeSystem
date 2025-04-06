namespace FacultativeSystem.Api.Contracts;

public record StudentResponse (
    Guid Id,
    string? UserName,
    List<string> Courses
);