namespace FacultativeSystem.Api.Contracts;

public record RegisterRequest(
    string Email,
    string Password
    );