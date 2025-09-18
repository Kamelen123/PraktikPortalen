using FluentValidation;
using PraktikPortalen.Application.DTOs.Auth;
using PraktikPortalen.Domain.Enums;

namespace PraktikPortalen.Application.Validation.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(128);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain a lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain a digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain a non-alphanumeric character.");

            RuleFor(x => x.Role)
                .IsInEnum()
                .Must(r => r == UserRole.Member || r == UserRole.Admin || r == UserRole.Unassigned)
                .WithMessage("Invalid role.");
        }
    }
}
