
namespace FacultativeSystem.Api.Contracts;

public record CourseListItemDto(
    Guid Id,
    string Name,
    bool IsActive,
    string? TeacherName
);