using FluentValidation;

namespace PetFamily.Accounts.Application.Commands.RefreshToken;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(r => r.AccessToken).NotEmpty();
        RuleFor(r => r.RefreshToken).NotEmpty();
    }
}
