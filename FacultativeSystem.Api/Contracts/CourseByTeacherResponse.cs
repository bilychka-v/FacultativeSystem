namespace FacultativeSystem.Api.Contracts;

public record CourseByTeacherResponse(
    Guid Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate
    );