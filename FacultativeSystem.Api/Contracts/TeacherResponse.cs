namespace FacultativeSystem.Api.Contracts;

public record TeacherResponse(
    Guid TeacherId,
    string TeacherName,
    List<string>? CourseName
    );