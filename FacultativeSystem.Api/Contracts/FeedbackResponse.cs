using FacultativeSystem.Application.Models;

namespace FacultativeSystem.Api.Contracts;

public record FeedbackResponse
(
    string Course,
    int? Grade,
    string? Feedback
);