using FluentValidation;
using PraktikPortalen.Application.DTOs.Users;

namespace PraktikPortalen.Application.Validation.Users
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(128);

            RuleFor(x => x.Role)
                .GreaterThanOrEqualTo(0).WithMessage("Invalid role.");
        }
    }
}
