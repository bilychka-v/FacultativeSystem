using Sieve.Attributes;

namespace FacultativeSystem.Api.Contracts;

public record CourseByTeacherResponse(
    Guid Id,
    // [property: Sieve(CanFilter = true, CanSort = true)]
    string Name,
    DateTime? StartDate,
    DateTime? EndDate,
    bool HasUnmarkedStudents
    );