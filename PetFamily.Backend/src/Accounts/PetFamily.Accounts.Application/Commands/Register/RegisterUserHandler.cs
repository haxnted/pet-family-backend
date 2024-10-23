using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Framework;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Application.Commands.Register;

public class RegisterUserHandler(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IValidator<RegisterUserCommand> validator,
    ILogger<RegisterUserHandler> logger) : ICommandHandler<RegisterUserCommand>
{
    public async Task<UnitResult<ErrorList>> Execute(
        RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToList();

        var existsUserWithUserName = await userManager.FindByNameAsync(command.UserName);
        if (existsUserWithUserName != null)
            return Errors.General.AlreadyExist("username").ToErrorList();

        var role = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Participant, cancellationToken);
        if (role is null)
            return Errors.General.NotFound().ToErrorList();

        var user = new User
        {
            PhotoPath = "",
            SocialLinks = [],
            UserName = command.UserName,
            Email = command.Email,
            Role = role
        };

        var result = await userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
        {
            logger.LogInformation("User {UserName} has created a new account", command.UserName);
            return UnitResult.Success<ErrorList>();
        }

        var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
        return new ErrorList(errors);
    }
}
