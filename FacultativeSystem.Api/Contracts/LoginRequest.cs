namespace FacultativeSystem.Api.Contracts;

public record LoginRequest(
    string Email,
    string Password);