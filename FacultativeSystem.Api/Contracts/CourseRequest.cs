using System.ComponentModel.DataAnnotations;

public record CourseRequest(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    Guid TeacherId
);