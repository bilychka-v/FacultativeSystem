namespace FacultativeSystem.Api.Contracts;

public record CourseRequest(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    Guid Id
    );