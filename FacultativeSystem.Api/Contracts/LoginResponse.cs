namespace FacultativeSystem.Api.Contracts;

public record LoginResponse
(
    string Email,
    string Token,
    List<string> Roles,
    string Id
);