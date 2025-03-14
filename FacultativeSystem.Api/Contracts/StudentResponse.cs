namespace FacultativeSystem.Api.Contracts;

public record StudentResponse (
    Guid StudentId,
    string Username
);