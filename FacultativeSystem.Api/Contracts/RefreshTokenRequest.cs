namespace FacultativeSystem.Api.Contracts;

public record RefreshTokenRequest(
    string AccessToken,
    string RefreshToken
    );