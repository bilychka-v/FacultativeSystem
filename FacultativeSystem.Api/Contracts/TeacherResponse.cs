namespace FacultativeSystem.Api.Contracts;

public record TeacherResponse(
    Guid Id,
    string UserName,
    List<string>? CourseName
    );