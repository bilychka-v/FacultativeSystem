namespace FacultativeSystem.Api.Contracts;

public record CourseResponse(
    Guid CourseId,
    string CourseName,
    DateTime StartDate,
    DateTime EndDate
    );