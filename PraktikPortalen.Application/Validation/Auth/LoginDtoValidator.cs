using FluentValidation;
using PraktikPortalen.Application.DTOs.Auth;

namespace PraktikPortalen.Application.Validation.Auth
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
