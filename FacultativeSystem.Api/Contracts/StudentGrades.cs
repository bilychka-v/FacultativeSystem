namespace FacultativeSystem.Api.Contracts;

public record StudentGrades
    (
        string StudentName,
        int Grade,
        string Feedback
    );