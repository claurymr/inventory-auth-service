using FluentValidation;
using InventoryAuthService.Api.Contracts;

namespace InventoryAuthService.Api.Validation;

/// <summary>
/// Validator for the <see cref="LoginRequest"/> class.
/// </summary>
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(4).WithMessage("Password must be at least 4 characters");
    }
}