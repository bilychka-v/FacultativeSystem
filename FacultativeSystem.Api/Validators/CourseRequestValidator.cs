using FacultativeSystem.Api.Contracts;
using FluentValidation;

namespace FacultativeSystem.Api.Validators;

public class CourseRequestValidator : AbstractValidator<CourseRequest>
{
    public CourseRequestValidator()
    {
        RuleFor(c => c.StartDate)
            .LessThan(c => c.EndDate).WithMessage("Start date must be earlier than end date");
        
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Course name is required");
        
        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("Course start date is required");

        RuleFor(c => c.EndDate)
            .NotEmpty().WithMessage("Course end date is required");

        RuleFor(c => c.TeacherId)
            .NotEmpty().WithMessage("TeacherId is required");
        
    }
}
