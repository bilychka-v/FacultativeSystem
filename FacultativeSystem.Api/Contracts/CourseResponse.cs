namespace FacultativeSystem.Api.Contracts;

public record CourseResponse(
    Guid Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    Guid TeacherId
    );