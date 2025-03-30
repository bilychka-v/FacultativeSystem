namespace FacultativeSystem.Api.Contracts;

public record RegisterRequest(
    string UserName,
    string Email,
    string Password
    );